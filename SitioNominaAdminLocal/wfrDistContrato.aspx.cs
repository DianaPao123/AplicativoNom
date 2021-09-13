using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace SitioNominaAdmin
{
    public partial class wfrDistContrato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var dist = cliente.ListaDistribuidores();
                    this.ddlDistribuidor.DataTextField = "Nombre";
                    this.ddlDistribuidor.DataValueField = "IdDistribuidor";
                    this.ddlDistribuidor.DataSource = dist.ToList();
                    this.ddlDistribuidor.DataBind();
                    Carga();
                }
            }
        }
        private void Carga()
        {
            
                var cliente = NtLinkClientFactory.Cliente();
                var i = Session["IdC"];
                var datos = cliente.Contratos(Convert.ToInt32(i));
                datos.ToString();
                this.txtFecha.Text = datos.FechaContrato.ToString().Substring(0, 10);
                this.ddlTipoContrato.SelectedValue = datos.TipoContrato;
                this.txtTimbres.Text = datos.Timbres.ToString();
                this.txtObservaciones.Text = datos.Observaciones;
                this.ddlDistribuidor.SelectedIndex = datos.IdDistribuidor;
                this.txtPorcentaje.Text = datos.Pocentaje.ToString();
                this.txtCosto.Text = datos.Costo.ToString();    
            
            
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrDistribuidores.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            var i = Session["IdC"];
            var dis = cliente.Contratos(Convert.ToInt32(i));
            using (cliente as IDisposable)
            {
                dis.FechaContrato = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", new CultureInfo("es-MX"));
                dis.Timbres = int.Parse(txtTimbres.Text);
                dis.Observaciones = txtObservaciones.Text;
                dis.TipoContrato = ddlTipoContrato.SelectedValue;
                dis.IdDistribuidor = Convert.ToInt32(ddlDistribuidor.SelectedValue);
                dis.Costo = Convert.ToDecimal(txtCosto.Text);
                dis.Pocentaje = Convert.ToInt32(txtPorcentaje.Text);
                dis.Comision = (dis.Costo * dis.Pocentaje) / 100;
                cliente.GuardarDisContrato(dis);
                Carga();
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrDistribuidores.aspx");
        }
    }
}