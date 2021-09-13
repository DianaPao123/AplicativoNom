using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;
using System.Drawing;

namespace GafLookPaid
{
    public partial class wfrUsuariosConsulta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                var sistema = Session["idSistema"] as long?;
                var idEmp = Session["idEmpresa"] as int?;
                var clienteServicio = NtLinkClientFactory.Cliente();
                using (clienteServicio as IDisposable)
                {
                    this.ddlEmpresas.DataSource = clienteServicio.ListaEmpresas(Session["perfil"] as string, idEmp.Value, sistema.Value, null);
                    this.ddlEmpresas.DataBind();
                }
                this.GetUsuarios();
            }
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUsuarios();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string idUsuarioString = this.gvUsuarios.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            if (e.CommandName.Equals("EditarUsuario"))
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var usuario = cliente.ObtenerUsuarioById(idUsuarioString);
                    this.txtNombre.Text = usuario.NombreCompleto;
                    this.txtIniciales.Text = usuario.Iniciales;
                    this.txtIdUsuario.Value = usuario.UserId.ToString();
                    this.ddlPerfil.SelectedValue = usuario.Perfil;
                }
                this.mpeEditarUsuario.Show();
            }
            else if (e.CommandName.Equals("CambiarPassword"))
            {
                this.lblUserIdCambiarPassword.Text = idUsuarioString;
                this.mpeCambiarPassword.Show();
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrUsuarios.aspx");
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


        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUsuarios.DataSource = ViewState["usuarios"];
            this.gvUsuarios.PageIndex = e.NewPageIndex;
            this.gvUsuarios.DataBind();
        }

        #region Helper Methods

        private void GetUsuarios()
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                empresa emp = cliente.ObtenerEmpresaById(int.Parse(this.ddlEmpresas.SelectedValue));
                this.gvUsuarios.DataSource = cliente.UsuariosLista(emp.IdEmpresa);
                ViewState["usuarios"] = this.gvUsuarios.DataSource;
                this.gvUsuarios.DataBind();
            }
        }

        #endregion

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                var usuario = cliente.ObtenerUsuarioById(this.txtIdUsuario.Value);
                usuario.NombreCompleto = this.txtNombre.Text;
                usuario.Iniciales = this.txtIniciales.Text;
                usuario.Perfil = this.ddlPerfil.SelectedValue;
                cliente.EditarUsuario(usuario);
                GetUsuarios();
            }
        }
    }
}