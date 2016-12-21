using NServiceBus;
using NServiceBus.Logging;
using Payveris.ConsolidatedServices;
using Payveris.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Payveris.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ServiceStack.OrmLite;

namespace Payveris.ScheduledPaymentsListener
{
    public class CheckPaymentsHandler : IHandleMessages<CheckScheduledPayments>
    {
        static ILog m_logger = LogManager.GetLogger<CheckPaymentsHandler>();
        
        //public IBus context { get; set; }

        public async Task Handle(CheckScheduledPayments message, IMessageHandlerContext context)
        {
            List<string> MPLIds = new List<string>(ConfigurationManager.AppSettings["MPLIdList"].Split(new char[] { ';' }));
            DateTime StartTime = DateTime.UtcNow.Date; //set default to start from start of day
            DateTime EndTime = DateTime.UtcNow;

            //persist the last poll time so if anything happens to the service, when it is restarted we will get everything that heppened in between
            var dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["NSBPersistence"].ConnectionString, SqlServerDialect.Provider);
            using (var db = dbFactory.Open())
            {
                StartTime = db.SingleById<LastPollSingleton>(1).LastPollTime;
                await db.UpdateAsync(new LastPollSingleton { Id=1, LastPollTime = EndTime }).ConfigureAwait(false);
            }

            foreach (string MPLId in MPLIds)
            {
                ScheduledPaymentType[] atResponse = CheckPaymentsSubmitted(MPLId, StartTime, EndTime).Result;

                if (atResponse.Any())
                {
                    //m_logger.Info("Payveris scheduled payment found and published:\n" + atResponse.Events.ToString());
                    foreach (ScheduledPaymentType Payment in atResponse)
                    {
                        await context.Publish(new PaymentScheduled() { Amount = Payment.Amount, LoanId = Payment.CreatedUserCode,  MPLId = MPLId, PaymentId = Payment.ScheduledPaymentId}).ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task<ScheduledPaymentType[]> CheckPaymentsSubmitted(string ClientCode, DateTime StartTime, DateTime EndTime)
        {
            string PV_BASEADDRESS = (ConfigurationManager.AppSettings["IsDev"] == "true") ? "https://payverisapidev.crbnj.net" : "https://payverisapi.crbnj.net";
            const string PV_TRANSACTION_API = "/api/Payments/GetAllScheduledPayments";
            string PV_API_PASS = (ConfigurationManager.AppSettings["IsDev"] == "true") ? "^klcTdlq9!" : "WZo%EpU4J9";
            string PV_CLIENT_ID = (ConfigurationManager.AppSettings["IsDev"] == "true") ? "payverisdevuser" : "apayverisapi";


            string strBearerToken = CRBAuthClient.GetBearerToken(PV_CLIENT_ID, PV_API_PASS);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PV_BASEADDRESS);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strBearerToken);
                client.Timeout = TimeSpan.FromMinutes(1);

                //GET api/Payments/GetAllScheduledPayments?ClientCode={ClientCode}&StartTime={StartTime}&EndTime={EndTime}
                string strFullURL = PV_TRANSACTION_API + "?ClientCode="+ ClientCode + "&StartTime="+ StartTime + "&EndTime=" + EndTime;

                HttpResponseMessage response = await client.GetAsync(strFullURL).ConfigureAwait(continueOnCapturedContext: false);

                if (!response.IsSuccessStatusCode)
                {
                    HttpError eResponse = await response.Content.ReadAsAsync<HttpError>().ConfigureAwait(continueOnCapturedContext: false);
                    throw (new Exception(response.StatusCode + " " + eResponse.First().ToString()));
                }
                else
                {
                    ScheduledPaymentType[] atResponse = await response.Content.ReadAsAsync<ScheduledPaymentType[]>().ConfigureAwait(continueOnCapturedContext: false);
                    return atResponse;
                }
            }
        }
    }

    public class CheckScheduledPayments : ICommand
    { }

    public class LastPollSingleton
    {
        public int Id { get; set; }
        public DateTime LastPollTime { get; set; }
    }

}
