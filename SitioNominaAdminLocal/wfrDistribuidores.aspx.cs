using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;
using System.Drawing;

namespace SitioNominaAdmin
{
    public partial class wfrDistribuidores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            var cliente = NtLinkClientFactory.Cliente();
            Distribuidores d= new Distribuidores();
            if(!this.IsPostBack)
            using (cliente as IDisposable)
            {
                var source = cliente.ListaDisContratos(0);
                gvDistribuidor.DataSource = source;
                gvDistribuidor.DataBind();

                // comision distribuidores sitio  administracion
                gvComisonAD.DataSource = cliente.listaComDis();
                gvComisonAD.DataBind();
                //comisiontimbredistribuidores
                GvCtdistri.DataSource = cliente.lisDistribuidores();
                GvCtdistri.DataBind();


                ddlDistribuidores.Items.Clear();
                var dis = cliente.ListaDistribuidores();
                var orden = dis;
                d.Nombre = "Todos";
                orden.Insert(0,d);
                ddlDistribuidores.DataTextField = "Nombre";
                ddlDistribuidores.DataValueField = "IdDistribuidor";
                ddlDistribuidores.DataSource = orden;
                ddlDistribuidores.DataBind();
                
            }
        }

        protected void gvDistribuidor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label estatus = (Label) e.Row.FindControl("Label2");
                if (estatus.Text == "Pagado")
                {
                    e.Row.BackColor = Color.LightGreen;
                }
            }
        }

        protected void gvDistribuidor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            Label id = (Label) gvDistribuidor.Rows[e.NewSelectedIndex].FindControl("Label3");
            cliente.UpdateDistribuidor(Convert.ToInt32(id.Text));
            var source = cliente.ListaDisContratos(Convert.ToInt32(ddlDistribuidores.SelectedValue));
            gvDistribuidor.DataSource = source;
            gvDistribuidor.DataBind();
        }

        protected void gvDistribuidor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Label id = (Label)gvDistribuidor.Rows[e.NewEditIndex].FindControl("Label3");
            Session["IdC"] = id.Text;
            Response.Redirect("wfrDistContrato.aspx");
        }

        protected void ddlDistribuidores_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            var source = cliente.ListaDisContratos(Convert.ToInt32(ddlDistribuidores.SelectedValue));
            gvDistribuidor.DataSource = source;
            gvDistribuidor.DataBind();
        }

        
    }
}