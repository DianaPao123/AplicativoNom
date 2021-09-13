using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace SitioNominaAdmin
{
    public partial class wfrUsuariosLista : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LlenarUsuarios(txtBuscar.Text.Trim());
            }
        }

        private void LlenarUsuarios(string patron)
        {
            var cliente = NtLinkClientFactory.Cliente();
            if (!string.IsNullOrEmpty(patron))
            using (cliente as IDisposable)
            {
                var usuarios = cliente.UsuariosObtenerLista(patron);
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Desbloquear", StringComparison.CurrentCultureIgnoreCase))
            {
                //var nombreUsuario = (string) gvUsuarios.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                var nombreUsuario = (string) gvUsuarios.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["UserName"];
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    cliente.DesbloquearUsuario(nombreUsuario);
                }
                LlenarUsuarios(txtBuscar.Text.Trim());
            }
            else if (e.CommandName.Equals("CambiarPass", StringComparison.CurrentCultureIgnoreCase))
            {
                var idUsuarioString = (Guid)gvUsuarios.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["UserId"];
                this.lblUserIdCambiarPassword.Text = idUsuarioString.ToString();
                this.mpeCambiarPassword.Show();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LlenarUsuarios(txtBuscar.Text.Trim());
        }

        protected void btnAceptarPassword_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                cliente.CambiarPassword(this.lblUserIdCambiarPassword.Text, this.txtPassword.Text);
            }
            this.lblMensaje.Text = "Password cambiado con exito";
            this.lblMensaje.ForeColor = Color.Green;
            this.lblUserIdCambiarPassword.Text = string.Empty;
            this.mpeCambiarPassword.Hide();
        }
    }
}