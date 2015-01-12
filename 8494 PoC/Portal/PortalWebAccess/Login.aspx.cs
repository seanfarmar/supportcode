using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalWebAccess
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly Security.AuthenticationService _authenticationService;

        public Login()
        {
            _authenticationService = new Security.AuthenticationService(Global.Bus);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
            _authenticationService.Authenticate(
                this.UserName.Text,
                this.Password.Text
                );

            //LoginStatus.Text = result.ToString();
        }
    }
}