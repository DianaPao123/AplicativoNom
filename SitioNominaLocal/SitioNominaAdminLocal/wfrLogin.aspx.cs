using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace SitioNominaAdmin
{
    public partial class wfrLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    this.Response.Redirect("Default.aspx");
                }
            }
        }

        protected void logMain_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                
                usuarios res = cliente.AdminLogin(this.logMain.UserName, this.logMain.Password);
                //usuarios res = cliente.AdminLogin("Admin","AABBCc22++");
                if (res != null)
                {
                    
                    Session["userId"] = res.idusuario;
                    Session["usuario"] = res;
                    e.Authenticated = true;
                }
                else
                {
                    e.Authenticated = false;
                }
            }
        }
    }
}