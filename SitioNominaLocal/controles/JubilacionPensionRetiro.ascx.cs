using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaLocal.controles
{
    public partial class JubilacionPensionRetiro : System.Web.UI.UserControl
    {
        public string IngresoAcumulable { get; set; }
        public string TotalUnaExhibicion { get; set; }
        public string MontoDiario { get; set; }
        public string IngresoNoAcumulable { get; set; }
        public string TotalParcialidad { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void limpiar()
        {
            txtIngresoAcumulable.Text = "";
            txtTotalUnaExhibicion.Text = "";
            txtMontoDiario.Text = "";
            txtIngresoNoAcumulable.Text = "";
            txtTotalParcialidad.Text = "";

        
        }

        public void ObtenerDatos()
        {
           IngresoAcumulable =txtIngresoAcumulable.Text;
           TotalUnaExhibicion =txtTotalUnaExhibicion.Text;
           MontoDiario=txtMontoDiario.Text;
           IngresoNoAcumulable=txtIngresoNoAcumulable.Text;
           TotalParcialidad = txtTotalParcialidad.Text;
        
        }
    }
}