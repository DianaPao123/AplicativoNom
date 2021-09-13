using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace SitioNominaAdmin
{
    public partial class wfrEmpresasConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    this.ddlClientes.Items.Clear();
                    var clientes = cliente.ListaSistemas("");
                    var todas = clientes;
                    Sistemas a = new Sistemas();
                    a.RazonSocial = "Todas";
                    todas.Insert(0,a);
                    this.ddlClientes.DataTextField = "RazonSocial";
                    this.ddlClientes.DataValueField = "IdSistema";
                    this.ddlClientes.DataSource = todas;
                    this.ddlClientes.DataBind();
                    this.FillView();
                }
                
            }
        }

        protected void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrEmpresa.aspx");
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EditarEmpresa"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrEmpresas.aspx?idEmpresa=" + idCliente);
                }
            }
            if (e.CommandName.Equals("EditarSucursal"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa=" + idCliente);
                }
            }
            if (e.CommandName.Equals("EditarConceptos"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrConceptos.aspx?idEmpresa=" + idCliente);
                }
            }
        }
        protected void gvEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpresas.DataSource = ViewState["empresas"];
            this.gvEmpresas.PageIndex = e.NewPageIndex;
            this.gvEmpresas.DataBind();
        }

        #region HelperMethods

        private void FillView()
        {

            var cliente = NtLinkClientFactory.Cliente();
            var sistema =int.Parse(ddlClientes.SelectedValue);
            var idCliente = ddlClientes.SelectedIndex;
            
            using (cliente as IDisposable)
            {
                this.gvEmpresas.DataSource = cliente.ListaEmpresas("Administrador", idCliente, sistema, null);
                ViewState["empresas"] = this.gvEmpresas.DataSource;
                this.gvEmpresas.DataBind();
            }
        }

        #endregion

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillView();
        }
    }
}