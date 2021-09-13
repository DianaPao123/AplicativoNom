using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        void Page_Init(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                this.Response.Redirect("wfrLogin.aspx");
            }
            else
            {                
                this.lblEmpresa.Text = Session["razonSocial"] as string;
                this.lblNombreUsuario.Text = Session["nombre"] as string;

                var perfil = Session["perfil"] as string;
                if (perfil != "Administrador")
                {
                    this.NavigationMenu.Items.RemoveAt(5);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
    }
}
