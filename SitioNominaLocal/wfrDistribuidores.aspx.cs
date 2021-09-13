using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;
namespace GafLookPaid
{
    public partial class wfrDistribuidores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            LlenarGrid();

           
        }

        private void LlenarGrid()
        {
            var id = Convert.ToInt32(Session["IdDistribuidor"].ToString());
            var cliente = NtLinkClientFactory.Cliente();
            var distribuidores = cliente.ListaDisContratos(id);
            //gridview  comision distribuidore
            gvComisionDis.DataSource = cliente.listaComDis();
            gvComisionDis.DataBind();
            //comisiontimbredistribuidores
            GvCtdistri.DataSource = cliente.lisDistribuidores();
            GvCtdistri.DataBind();

            gvDistribuidor.DataSource = distribuidores;
            gvDistribuidor.DataBind();

        }

        protected void gvDistribuidor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label estatus = (Label)e.Row.FindControl("Label2");
                if (estatus.Text == "Pagado")
                {
                    e.Row.BackColor = Color.LightGreen;
                }
            }
        }

      
        
    }
}