using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrEmpresasConsulta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.FillView();
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
                    Response.Redirect("wfrEmpresa.aspx?idEmpresa=" + idCliente);
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
            if (e.CommandName.Equals("CentrosTrabajo"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrCentrosTrabajo.aspx?idEmpresa=" + idCliente);
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
            var sistema = Session["idSistema"] as long?;
            var idEmpresa = Session["idEmpresa"] as int?;
            using (cliente as IDisposable)
            {
                this.gvEmpresas.DataSource = cliente.ListaEmpresas(Session["perfil"] as string, idEmpresa.Value, sistema.Value, null);
                ViewState["empresas"] = this.gvEmpresas.DataSource;
                this.gvEmpresas.DataBind();
            }
        }

        #endregion
    }
}