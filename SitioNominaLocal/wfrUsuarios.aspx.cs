using System;
using System.Web.UI;
using ServicioLocalContract;
using System.ServiceModel;

namespace GafLookPaid
{
    public partial class wfrUsuarios : Page
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
                    this.ddlPerfiles.DataSource = clienteServicio.ObtenerPerfiles();
                    this.ddlPerfiles.DataBind();

                    string userId = this.Request.QueryString["IdUsuario"];
                    if (!string.IsNullOrEmpty(userId))
                    {
                        UsuarioLocal usr = clienteServicio.ObtenerUsuarioById(userId);
                        this.txtEmail.Text = usr.Email;
                        this.ddlPerfiles.SelectedValue = usr.Perfil;
                    }

                    string idEmpresa = this.Request.QueryString["idEmpresa"];
                    this.ddlEmpresas.SelectedValue = idEmpresa;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var clienteServicio = NtLinkClientFactory.Cliente();
                using (clienteServicio as IDisposable)
                {
                    empresa emp = clienteServicio.ObtenerEmpresaById((int) Session["idEmpresa"]);
                    clienteServicio.GuardarUsuario(txtNombreCompleto.Text, txtEmail.Text,txtPassword.Text,
                                                   int.Parse(this.ddlEmpresas.SelectedValue),
                                                   this.ddlPerfiles.SelectedValue,txtEmail.Text, 
                                                   txtIniciales.Text);
                }
                this.Response.Redirect("wfrUsuariosConsulta.aspx");
            }
            catch (FaultException fe)
            {
                this.lblErrorMessage.Text = fe.Reason.ToString();
            }
            catch (Exception ee)
            {
                //throw;
            }
            
        }
    }
}