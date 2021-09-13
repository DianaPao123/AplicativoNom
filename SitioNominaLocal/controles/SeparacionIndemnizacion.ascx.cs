using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaLocal.controles
{
    public partial class SeparacionIndemnizacion : System.Web.UI.UserControl
    {
        public string IngresoAcumulable{get; set;}
        public string TotalPagado{get; set;}
        public string UltimoSueldoMensOrd{get; set;}
        public string IngresoNoAcumulable{get; set;}
        public string NumAñosServicio { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Limpiar()
        {
            txtIngresoAcumulable.Text = "";
            txtTotalPagado.Text = "";
            txtUltimoSueldoMensOrd.Text = "";
            txtIngresoNoAcumulable.Text = "";
            txtNumAñosServicio.Text = "";

        }

        public void ObtenerDatos()
        {

            IngresoAcumulable=txtIngresoAcumulable.Text;
            TotalPagado=txtTotalPagado.Text;
            UltimoSueldoMensOrd=txtUltimoSueldoMensOrd.Text;
            IngresoNoAcumulable=txtIngresoNoAcumulable.Text;
            NumAñosServicio = txtNumAñosServicio.Text;
        }
    }
}