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
    public partial class wfrClientesContratos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idEmpresaString = this.Request.QueryString["idSistema"];
                int idSistema;
                hidIdCliente.Value = idEmpresaString;
                if (!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idSistema))
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var c = cliente.ObtenerSistemaById(idSistema);
                        this.lblTitulo.Text = "Contratos de " + c.RazonSocial;
                        var contratos = cliente.ListaContratos(idSistema);
                        this.GvContratos.DataSource = contratos;
                        this.GvContratos.DataBind();
                        ddlDistribuidor.Items.Clear();
                        var dist = cliente.ListaDistribuidores();
                        Distribuidores d = new Distribuidores();
                        d.Nombre = "Ninguno";
                        dist.Insert(0,d);
                        ddlDistribuidor.DataTextField = "Nombre";
                        ddlDistribuidor.DataValueField = "IdDistribuidor";
                        ddlDistribuidor.DataSource = dist;
                        ddlDistribuidor.DataBind();
                    }
                }
            }
        }

        protected void btnNuevoContrato_Click(object sender, EventArgs e)
        {
            mpeNuevoContrato.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mpeNuevoContrato.Hide();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string idEmpresaString = this.Request.QueryString["idSistema"];
            int idSistema = 0;
            hidIdCliente.Value = idEmpresaString;
            var cliente = NtLinkClientFactory.Cliente();
            var c = cliente.ObtenerSistemaById(idSistema);
            using (cliente as IDisposable)
            {
                if (Convert.ToInt32(ddlDistribuidor.SelectedValue) == 0)
                {
                    Contratos con = new Contratos();
                    con.FechaContrato = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    con.Timbres = int.Parse(txtTimbres.Text);
                    con.Observaciones = txtObservaciones.Text;
                    con.IdSistema = int.Parse(hidIdCliente.Value);
                    con.TipoContrato = ddlTipoContrato.SelectedValue;
                    cliente.GuardarContrato(con);
                    var contratos = cliente.ListaContratos(con.IdSistema);
                    this.GvContratos.DataSource = contratos;
                }
                else
                {
                    #region Contrato de Distribuidores;
                    DistContratos dis = new DistContratos();
                    dis.FechaContrato = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    dis.Timbres = int.Parse(txtTimbres.Text);
                    dis.Observaciones = txtObservaciones.Text;
                    dis.IdSistema = int.Parse(hidIdCliente.Value);
                    dis.TipoContrato = ddlTipoContrato.SelectedValue;
                    dis.IdDistribuidor = Convert.ToInt32(ddlDistribuidor.SelectedValue);
                    dis.Costo = Convert.ToDecimal(ddlPrecios.SelectedValue);
                    dis.Pocentaje = Convert.ToInt32(ddlPorcentaje.SelectedValue);
                    dis.Comision = (dis.Costo * dis.Pocentaje) / 100;
                    dis.Status = "Pendiente";
                    dis.Productos = ddlProductos.SelectedValue;
                    cliente.GuardarDisContrato(dis);
                    #endregion
                    #region Contratos;
                    Contratos con = new Contratos();
                    con.FechaContrato = dis.FechaContrato;
                    con.Timbres = dis.Timbres;
                    con.Observaciones = dis.Observaciones;
                    con.IdSistema = dis.IdSistema;
                    con.TipoContrato = dis.TipoContrato;
                    cliente.GuardarContrato(con);
                    #endregion
                    var contratos = cliente.ListaContratos(con.IdSistema);
                    this.GvContratos.DataSource = contratos;
                }
                this.GvContratos.DataBind();
            }
            mpeNuevoContrato.Hide();
            
        }
    }
}