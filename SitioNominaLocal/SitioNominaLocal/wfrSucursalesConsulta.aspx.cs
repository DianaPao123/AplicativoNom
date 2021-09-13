using System;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrSucursalesConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var perfil = Session["perfil"] as string;
            var sistema = Session["idSistema"] as long?;
            if(!this.IsPostBack)
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var idEmpresa = int.Parse(Request.QueryString["idEmpresa"]);
                    hdIdempresa.Value = idEmpresa.ToString();
                    var emp = cliente.ObtenerEmpresaById(idEmpresa);
                    this.lblEmpresa.Text = emp.RazonSocial;
                    this.gvSucursales.DataSource = cliente.ListaSucursales(idEmpresa);
                    ViewState["sucursales"] = this.gvSucursales.DataSource;
                    this.gvSucursales.DataBind();
                }
            }
        }

        protected void gvSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var idSucursal = (long)this.gvSucursales.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
            if(e.CommandName.Equals("EditarSucursal"))
            {
                this.Response.Redirect("wfrSucursales.aspx?idSucursal=" + idSucursal + "&idEmpresa="+ hdIdempresa.Value);
            }
        }

        protected void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrSucursales.aspx?idEmpresa=" + hdIdempresa.Value);
        }

        protected void gvSucursales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSucursales.DataSource = ViewState["sucursales"];
            this.gvSucursales.PageIndex = e.NewPageIndex;
            this.gvSucursales.DataBind();
        }
    }
}