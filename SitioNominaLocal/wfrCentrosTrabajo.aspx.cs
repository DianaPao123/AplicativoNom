using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrCentrosTrabajo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idEmpresaStr = this.Request.QueryString["idEmpresa"];
                int idEmpresa;
                if (!string.IsNullOrEmpty(idEmpresaStr) && int.TryParse(idEmpresaStr, out idEmpresa))
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        this.hdIdempresa.Value = idEmpresaStr;
                        var centros = cliente.ListaCentros(idEmpresa);
                        this.gvCentros.DataSource = centros;
                        this.gvCentros.DataBind();
                    }
                }
                
            }
        }

        protected void gvSucursales_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarCentro")
            {
                DataKey key = this.gvCentros.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCentro= Convert.ToInt32(key.Value);
                    Response.Redirect("~/wfrEditaCentro.aspx?idCentro=" + idCentro + "&idEmpresa=" + this.Request.QueryString["idEmpresa"]);
                }
            }
        }

        protected void gvSucursales_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
        }

        protected void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfrEditaCentro.aspx?idEmpresa=" + this.Request.QueryString["idEmpresa"]);
        }
    }
}