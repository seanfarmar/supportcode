namespace NServiceBusApp
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SendMessageButtonClick(object sender, EventArgs e)
        {
            var msg = new NServiceBusMessages.Message();

            Program.Bus.Send(msg);
        }
    }
}