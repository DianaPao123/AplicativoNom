using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaLocal.controles
{
    public partial class AccionesOTitulos : System.Web.UI.UserControl
    {
       public string ValorMercado { get; set; }
       public string PrecioAlOtorgarse { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void obtenerDatos()
        {
          ValorMercado=txtValorMercado.Text;
          PrecioAlOtorgarse=txtPrecioAlOtorgarse.Text;
        }

        public void Limpiar()
        {
            txtValorMercado.Text="";
            txtPrecioAlOtorgarse.Text="";
        }
    }
}