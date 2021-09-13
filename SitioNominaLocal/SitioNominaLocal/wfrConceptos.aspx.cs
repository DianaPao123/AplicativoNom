using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrConceptos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["idEmpresa"] != null)
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        txtIdEmpresa.Value = Request.QueryString["idEmpresa"];
                        this.gvDetalles.DataSource = cliente.BuscarProducto("", int.Parse(Request.QueryString["idEmpresa"]));
                        this.gvDetalles.DataBind();
                        //cliente.BuscarProducto("",)
                    }
                }
                
            }
        }

        protected void gvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = int.Parse(e.CommandArgument.ToString());

                int idProducto = Convert.ToInt32(this.gvDetalles.DataKeys[index].Value);
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var prod = cliente.ObtenerProductoById(idProducto);
                    this.txtCodigo.Text = prod.Codigo;
                    this.txtDescripcion.Text = prod.Descripcion;
                    this.txtIdProducto.Value = prod.IdProducto.ToString();
                    this.txtObservaciones.Text = prod.Observaciones;
                    this.txtPrecioUnitario.Text = prod.PrecioP.ToString();
                    this.txtUnidad.Text = prod.Unidad;
                    lblConcepto.Text = "Editar " + prod.Descripcion;
                }
                
                this.mpeBuscarConcepto.Show();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (txtIdProducto.Value != null)
            {
                producto p = new producto
                                 {
                                     IdProducto = txtIdProducto.Value == "" ? 0 : int.Parse(txtIdProducto.Value),
                                     Codigo = txtCodigo.Text == "" ? null : txtCodigo.Text,
                                     PrecioP = decimal.Parse(txtPrecioUnitario.Text),
                                     Observaciones = txtObservaciones.Text == "" ? null : txtObservaciones.Text,
                                     Unidad = txtUnidad.Text,
                                     Descripcion = txtDescripcion.Text,
                                     IdEmpresa = int.Parse(txtIdEmpresa.Value)
                                 };
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    if (cliente.GuardarConcepto(p))
                    {
                        this.gvDetalles.DataSource = cliente.BuscarProducto("", int.Parse(txtIdEmpresa.Value));
                        this.gvDetalles.DataBind();
                    }
                }
            }
        }

        protected void btnNuevoConcepto_Click(object sender, EventArgs e)
        {
            lblConcepto.Text = "Nuevo Concepto";
            this.txtCodigo.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdProducto.Value = string.Empty;
            this.txtObservaciones.Text = string.Empty;
            this.txtPrecioUnitario.Text = string.Empty;
            this.txtUnidad.Text = string.Empty;
            mpeBuscarConcepto.Show();
        }
    }
}