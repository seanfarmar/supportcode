namespace PortalWebAccess
{
    using System;
    using System.Web.UI;
    using Security;

    public partial class Login : Page
    {
        private readonly AuthenticationService _authenticationService;

        public Login()
        {
            _authenticationService = new AuthenticationService(Global.Bus);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
            _authenticationService.Authenticate(
                UserName.Text,
                Password.Text
                );

            //LoginStatus.Text = result.ToString();
        }
    }
}