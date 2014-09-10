namespace Slb.ConsumerWindowsFormsApplication
{
    using System;
    using System.Windows.Forms;

    using NServiceBus;

    using Slb.Messages;

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void AddWorkButtonClick(object sender, EventArgs e)
        {
            var addWork = new AddWork { Id = Guid.NewGuid(), Seconds = 5 };

            Program.Bus.Send(addWork);
        }
    }
}