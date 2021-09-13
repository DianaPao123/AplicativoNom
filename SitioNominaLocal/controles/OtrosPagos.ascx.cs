using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaLocal.controles
{
    public partial class OtrosPagos : System.Web.UI.UserControl
    {
       public string SaldoAFavor {get;set;}
       public string RemanenteSalFav {get;set;}
       public string Ano { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Limpiar()
        {

            txtSaldoAFavor.Text = "";
            txtRemanenteSalFav.Text = "";
            txtAno.Text = "";

        }

        public void ObtenerDatos()
        {

           SaldoAFavor = txtSaldoAFavor.Text;
           RemanenteSalFav =txtRemanenteSalFav.Text;
           Ano =txtAno.Text;
        }
    }
}