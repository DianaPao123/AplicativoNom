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
    public partial class wfrDistribuidorCliente : System.Web.UI.Page
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
            gvDistribuidor.DataSource = distribuidores;
            gvDistribuidor.DataBind();
        }

        protected void gvDistribuidor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPorcentaje = (Label)e.Row.FindControl("lblPorcentaje");
                int a = Convert.ToInt32(e.Row.Cells[5].Text);
                int b = Convert.ToInt32(e.Row.Cells[6].Text);
                int c = Convert.ToInt32(e.Row.Cells[7].Text);
                int consumindos = ((b + c) * a) / 100;
                lblPorcentaje.Text = (consumindos).ToString();
                if (consumindos >= 80)
                {
                    e.Row.ForeColor = Color.Black;
                    e.Row.BackColor = Color.Red;
                }



            }
        }
    }
}