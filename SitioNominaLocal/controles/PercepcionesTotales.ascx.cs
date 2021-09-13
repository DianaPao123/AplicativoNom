using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaLocal.controles
{
    public partial class PercepcionesTotales : System.Web.UI.UserControl
    {
        public string TotalSueldos{get; set;}
        public string TotalJubilacionPensionRetiro{get; set;}
        public string TotalExento{get; set;}
        public string TotalSeparacionIndemnizacion { get; set; }
        public string TotalGravado{get; set;} 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void limpiar()
        {
            txtTotalSueldos.Text = "";
            txtTotalJubilacionPensionRetiro.Text = "";
            txtTotalExento.Text = "";
            txtTotalSeparacionIndemnizacion.Text = "";
            txtTotalGravado.Text = "";

        
        }

        public void ObtenerDatos()
        {
        TotalSueldos=txtTotalSueldos.Text;
        TotalJubilacionPensionRetiro=txtTotalJubilacionPensionRetiro.Text;
        TotalExento=txtTotalExento.Text ;
        TotalSeparacionIndemnizacion=txtTotalSeparacionIndemnizacion.Text;
        TotalGravado = txtTotalGravado.Text;
        }

        public void IngresarDatos()
        {
            txtTotalSueldos.Text = TotalSueldos;  
            txtTotalJubilacionPensionRetiro.Text=TotalJubilacionPensionRetiro;
            txtTotalExento.Text=TotalExento;
            txtTotalSeparacionIndemnizacion.Text=TotalSeparacionIndemnizacion;
            txtTotalGravado.Text = TotalGravado;
        
        }
    }
}