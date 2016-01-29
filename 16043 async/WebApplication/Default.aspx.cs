using System;
using System.Threading.Tasks;
using System.Web.UI;

public partial class Default : Page
{
    protected async void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TextBox1.Text))
        {
            return;
        }

        int number;

        if (!int.TryParse(TextBox1.Text, out number))
        {
            return;
        }

        var erroCode =  await SendEnumMessageAsyncTask(number);

        Label1.Text = Enum.GetName(typeof(ErrorCodes), erroCode);
    }

    private async Task<ErrorCodes> SendEnumMessageAsyncTask(int number)
    {
        Command command = new Command
        {
            Id = number
        };

        Task<ErrorCodes> statusTask = Global.Bus.Send("Samples.AsyncPages.Server", command)
            .Register<ErrorCodes>();

        return await statusTask;
    }
}