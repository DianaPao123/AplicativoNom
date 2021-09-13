using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;
using ServicioLocalContract;
using System.Text;
using System.Linq;
using Label = System.Web.UI.WebControls.Label;

namespace GafLookPaid
{
    public partial class wfrFacturasConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                var perfil = Session["perfil"] as string;
                var sistema = Session["idSistema"] as long?;
                var idEmp = Session["idEmpresa"] as int?;
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    string guidString = ((Guid)Session["userId"]).ToString();
                    empresa empresa = cliente.ObtenerEmpresaByUserId(guidString);
                    
                    int empresaId = perfil != null && perfil.Equals("Administrador") ? 0 : empresa.IdEmpresa;
                    this.ddlClientes.Items.Clear();
                    this.ddlClientes.DataSource = cliente.ListaClientes(perfil, empresaId, string.Empty, true);
                    this.ddlClientes.DataBind();
                    this.ddlEmpresas.DataSource = cliente.ListaEmpresas(perfil, idEmp.Value, sistema.Value, null);
                    this.ddlEmpresas.Enabled = perfil.Equals("Administrador");
                    this.ddlEmpresas.DataBind();
                    this.txtFechaInicial.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("d");
                    this.txtFechaFinal.Text = DateTime.Today.ToString("d");
                    
                    ddlEmpresas_SelectedIndexChanged(null,null);
                }
                this.FillView();
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if(e.CommandName.Equals("DescargarXml"))
            {
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    string xml = Encoding.UTF8.GetString(cliente.FacturaXml(uuid));
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + uuid + ".xml");
                    this.Response.ContentType = "text/xml";
                    this.Response.Charset = "UTF-8";
                    this.Response.Write(xml);
                    this.Response.End();
                }
            }
            else if (e.CommandName.Equals("DescargarPdf"))
            {
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + uuid + ".pdf");
                    this.Response.ContentType = "application/pdf";
                    var pdf = cliente.FacturaPdf(uuid);
                    if (pdf == null)
                    {
                        this.lblError.Text = "Archivo no encontrado";
                        return;
                    }
                    this.Response.BinaryWrite(pdf);
                    this.Response.End();
                }
            }
            else if (e.CommandName.Equals("EnviarEmail"))
            {

                var idCliente = (int) this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdCliente"];
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    clientes c = cliente.ObtenerClienteById(idCliente);
                    this.lblEmailCliente.Text = c.Email;
                }
                this.lblGuid.Text = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                this.mpeEmail.Show();
            }
            else if(e.CommandName.Equals("Pagar"))
            {
                var id = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var venta = cliente.GetFactura(id);
                    this.txtFechaPago.Text = venta.FechaPago.HasValue ? venta.FechaPago.Value.ToString("dd/MM/yyyy") : string.Empty;
                    this.lblFolioPago.Text = venta.folio;
                    this.txtReferenciaPago.Text = venta.ReferenciaPago;
                }
                this.lblIdventa.Text = id.ToString();
                this.mpePagar.Show();
            }
            else if (e.CommandName.Equals("Cancelar"))
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var venta = cliente.GetFactura(id);
                        var cancelacion = cliente.CancelarFactura(venta.RfcEmisor, venta.Guid);
                        lblError.Text = cancelacion;
                        this.FillView();
                    }
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch (Exception fe)
                {
                    ;
                }

            }

            else if (e.CommandName.Equals("Acuse"))
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var fact = cliente.GetFactura(id);
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fact.idventa + ".pdf");
                        this.Response.ContentType = "application/pdf";
                        var pdf = cliente.AcuseCancelacion(id);
                        if (pdf == null || pdf.Length == 0)
                        {
                            this.lblError.Text = "Archivo no encontrado";
                            return;
                        }
                        this.Response.BinaryWrite(pdf);
                        this.Response.End();
                    }
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch (Exception fe)
                {
                    ;
                }

            }

        }
        
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var ex = new Export();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=Reporte.xlsx");
            this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            this.Response.BinaryWrite(ex.GridToExcel(this.gvFacturaCustumer, "Facturas"));
            this.Response.End();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filtro = rbStatus.SelectedValue;
            var cliente = NtLinkClientFactory.Cliente();
            var perfil = Session["perfil"] as string;
            var iniciales = Session["iniciales"] as string;
            //if (string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
            //    return;
            using (cliente as IDisposable)
            {
                var empresaId = int.Parse(this.ddlEmpresas.SelectedValue);
                var sistema = Session["idSistema"] as long?;
                this.ddlClientes.Items.Clear();
                this.ddlClientes.DataSource = cliente.ListaClientes(perfil, empresaId, string.Empty, true);
                this.ddlClientes.DataBind();
                DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);

                List<vventas> ventas = cliente.ListaFacturas(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue),
                    int.Parse(this.ddlClientes.SelectedValue), filtro, "A", iniciales).OrderByDescending(p => p.folio).ToList();

                var lista = new List<vventas>(ventas);
                ViewState["facturas"] = lista;
                this.gvFacturas.DataSource = lista;
                this.gvFacturas.DataBind();
                CalculaTotales(lista);
                this.gvFacturaCustumer.DataSource = lista;
                this.gvFacturaCustumer.DataBind();
            }
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFacturas.DataSource = ViewState["facturas"];
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.gvFacturas.DataBind();
            this.CalculaTotales(ViewState["facturas"] as List<vventas>);
        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                string uuid = this.lblGuid.Text;
                
                byte[] xml = cliente.FacturaXml(uuid);
                byte[] pdf = cliente.FacturaPdf(uuid);
                var atts = new List<EmailAttachment>();
                atts.Add(new EmailAttachment { Attachment = xml,Name = uuid + ".xml"});
                atts.Add(new EmailAttachment {Attachment = pdf, Name = uuid + ".pdf"});
                var idEmp = Session["idEmpresa"] as int?;
                var empresa = cliente.ObtenerEmpresaById(idEmp.Value);
                var emails = new List<string>();

                if (!string.IsNullOrEmpty(this.lblEmailCliente.Text))
                {
                    emails.Add(this.lblEmailCliente.Text);
                }
                emails.AddRange(this.txtEmails.Text.Split(','));
                try
                {
                    cliente.SendMailByteArray(emails, atts, "Se envía el recibo de nómina con folio " + uuid + " y su representación visual.",
                          "Envio de Recibo de Nómina", empresa.Email, empresa.RazonSocial);
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                
                this.mpeEmail.Hide();
            }
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                try
                {
                    cliente.Pagarfactura(int.Parse(this.lblIdventa.Text), DateTime.Parse(this.txtFechaPago.Text), this.txtReferenciaPago.Text);
                }
                catch(FaultException fe)
                {
                    this.lblErrorPago.Text = fe.Message;
                }
            }
            FillView();
            this.mpePagar.Hide();
        }

        #region Helper Methods

        private void FillView()
        {
            var perfil = Session["perfil"] as string;
            var iniciales = Session["iniciales"] as string;
            var cliente = NtLinkClientFactory.Cliente();
            var filtro = rbStatus.SelectedValue;
            if (!string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
            using (cliente as IDisposable)
            {
                DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);
                var ventas = perfil == "Administrador" ? (cliente.ListaFacturas(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue),
                    int.Parse(this.ddlClientes.SelectedValue), filtro, "A")
                        ) : (cliente.ListaFacturas(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue),
                    int.Parse(this.ddlClientes.SelectedValue), filtro, "A", iniciales)
                        ); 
                

                List<vventas> lista;
                if(!string.IsNullOrEmpty(this.txtTexto.Text))
                {
                    lista = ventas.Where(l => (l.Cliente != null && l.Cliente.Contains(this.txtTexto.Text))
                        || (l.observaciones != null && l.observaciones.Contains(this.txtTexto.Text))).ToList();
                }
                else
                {
                    lista = ventas.ToList();
                }
                var gridFatura = new GridView();
                foreach (DataControlField colum in gvFacturas.Columns)
                {
                    gridFatura.Columns.Add(colum);
                }

                ViewState["facturas"] = lista;

                this.gvFacturas.DataSource = lista;
                this.gvFacturas.DataBind();
                                
                CalculaTotales(lista);
                this.gvFacturaCustumer.DataSource = lista;
                this.gvFacturaCustumer.DataBind();
            }
        }

        private void CalculaTotales(List<vventas> lista)
        {
            var subt = lista.Where(c=>c.Cancelado == 0).Sum(p => p.SubTotal);
            var total = lista.Where(c=>c.Cancelado == 0).Sum(p => p.Total);
            var iva = lista.Where(c => c.Cancelado == 0).Sum(p => p.Iva);
            var retiva = lista.Where(c => c.Cancelado == 0 && c.RetIva.HasValue).Sum(p => p.RetIva);
            var retisr = lista.Where(c => c.Cancelado == 0 && c.RetIsr.HasValue).Sum(p => p.RetIsr);
            var ieps = lista.Where(c => c.Cancelado == 0 && c.Ieps.HasValue).Sum(p => p.Ieps);

            if (this.gvFacturas.FooterRow != null)
            {
                this.gvFacturas.FooterRow.Cells[0].Text = "TOTAL";
                this.gvFacturas.FooterRow.Cells[6].Text = subt.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[7].Text = iva.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[8].Text = retiva.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[9].Text = retisr.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[10].Text = ieps.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[11].Text = total.Value.ToString("C");
            }
        }

        #endregion

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[13].Text == "Cancelado")
                    e.Row.BackColor = Color.FromName("#FEDDB8");
                if (e.Row.Cells[13].Text == "Pendiente")
                    e.Row.BackColor = Color.FromName("#e4e5e7");
                if (e.Row.Cells[13].Text == "Pagado")
                    e.Row.BackColor = Color.FromName("#b3d243");
            }
        }

        protected void btnCerrarPagar_Click(object sender, EventArgs e)
        {
            FillView();
            mpePagar.Show();
        }
    }
}