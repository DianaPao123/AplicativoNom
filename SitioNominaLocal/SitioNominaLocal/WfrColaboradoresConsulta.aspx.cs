using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class WfrColaboradoresConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var perfil = Session["perfil"] as string;
                var sistema = Session["idSistema"] as long?;
                var empresa = perfil.Equals("Administrador") ? new empresa { IdEmpresa = 0 } : Session["empresa"] as empresa;
                var idEmpresa = Session["idEmpresa"] as int?;
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    this.ddlEmpresa.DataSource = cliente.ListaEmpresas(perfil, idEmpresa.Value, sistema.Value, null);
                }

                this.ddlEmpresa.Enabled = perfil.Equals("Administrador");
                this.ddlEmpresa.DataBind();
                this.GetClientes();
            }
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EditarCliente"))
            {
                DataKey key = this.gvClientes.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("~/wfrColaboradores.aspx?idCliente=" + idCliente);
                }
            }
            else if (e.CommandName.Equals("Eliminar"))
            {

                try
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        int idCliente = Convert.ToInt32(e.CommandArgument);
                        var cl = cliente.ObtenerClienteById(idCliente);
                        cliente.EliminarCliente(cl);

                    }
                    this.GetClientes();
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch (Exception ee)
                {
                    lblError.Text = "Ocurrió un error al eliminar el cliente";

                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.GetClientes();
        }

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/wfrColaboradores.aspx?idEmpresa=" + this.ddlEmpresa.SelectedValue);
        }

        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvClientes.DataSource = ViewState["clientes"];
            this.gvClientes.PageIndex = e.NewPageIndex;
            this.gvClientes.DataBind();
        }

        #region Helper Methods

        private void GetClientes()
        {
            int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);

            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                var clientes = cliente.ListaEmpleados(Session["perfil"] as string, idEmpresa, this.txtBusqueda.Text, false);
                ViewState["clientes"] = clientes;
                this.gvClientes.DataSource = clientes;
                this.gvClientes.DataBind();
            }
        }

        #endregion

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}