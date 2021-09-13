using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;
using System.Drawing;

namespace GafLookPaid
{
    public partial class WfrNomina : System.Web.UI.Page
    {
        private string AñoR;
        private string MesR;
        private string DiaR;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                try
                {
                    DivCfdiRelacionados.Attributes.Add("style", "display:none;");

                    this.UCOtrosPagos.Visible = false;
                    this.AgregarDeduccion.Enabled = false;
                    this.txtTotalOtrasDeducciones.Enabled = false;
                    this.txtTotalImpuestosRetenidos.Enabled = false;
                    this.ChJubilacionPensionRetiro.Visible = false;
                    this.ChSeparacionIndemnizacio.Visible = false;
                    this.PercepcionesTotales.Visible = false;
                    this.btnAgregarPercepcion.Enabled = false;
                    this.btnAgregarHorasExtra.Enabled = false;
                    this.AccionesOTitulos.Visible = false;
                    this.JubilacionPensionRetiro.Visible = false;
                    this.SeparacionIndemnizacion.Visible = false;
                    this.txtFechaPago.Text = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString("yyyy-MM-dd");
                    string perfil = this.Session["perfil"] as string;
                    long? sistema = this.Session["idSistema"] as long?;
                    int? idEmp = this.Session["idEmpresa"] as int?;
                    IServicioLocal cliente = NtLinkClientFactory.Cliente();
                    int IdEmpresaE;
                    using (cliente as IDisposable)
                    {
                        List<empresa> empresas = cliente.ListaEmpresas(perfil, idEmp.Value, sistema.Value, null);
                        List<empresa> listaEmpresas = new List<empresa>(empresas);
                        this.ddlEmpresa.DataSource = listaEmpresas;
                        this.ddlEmpresa.DataBind();
                        int idEmpresa;
                        IdEmpresaE = (idEmpresa = listaEmpresas.First<empresa>().IdEmpresa);
                        empresa emp = cliente.ObtenerEmpresaById(idEmpresa);
                        this.ViewState["RegimenFiscalVS"] = emp.RegimenFiscal;
                        this.ViewState["RFCVS"] = emp.RFC;
                        List<CentrosTrabajo> centros = cliente.ListaCentros(idEmpresa);
                        if (centros.Count <= 0)
                        {
                            this.lblError.Text = "Debes dar de alta por lo menos un centro de trabajo";
                            return;
                        }
                        this.DdlCentroTrabajo.DataSource = centros;
                        this.DdlCentroTrabajo.DataBind();
                        this.DdlCentroTrabajo_OnSelectedIndexChanged(null, null);
                        this.ddlClientes.DataSource = cliente.ListaEmpleados(perfil, idEmpresa, "", false);
                        this.ddlClientes.DataBind();
                        this.ddlClientes_SelectedIndexChanged(null, null);
                        if (!cliente.TieneConfiguradoCertificado(idEmpresa))
                        {
                            this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                            this.btnGenerarFactura.Enabled = (this.BtnVistaPrevia.Enabled = false);
                            return;
                        }
                        if (listaEmpresas.Count > 0)
                        {
                            this.txtFolio.Text = cliente.SiguienteFolioFactura(idEmpresa);
                            this.ddlClientes_SelectedIndexChanged(null, null);
                        }
                    }
                    this.ViewState["detalles"] = new List<facturasdetalle>();
                    this.ViewState["iva"] = 0m;
                    this.ViewState["total"] = 0m;
                    this.ViewState["subtotal"] = 0m;
                    this.ViewState["CfdiRelacionado"] = new List<string>();
                    if (this.ddlOrigenRecurso.SelectedValue == "IM")
                    {
                        this.txtMontoRecursoPropio.Enabled = true;
                        this.txtMontoRecursoPropio.Visible = true;
                        this.lblMontoRecursoPropio.Visible = true;
                        this.txtMontoRecursoPropio.Text = "";
                        this.RequiredFieldValidator18.Enabled = true;
                    }
                    else
                    {
                        this.txtMontoRecursoPropio.Enabled = false;
                        this.txtMontoRecursoPropio.Visible = false;
                        this.lblMontoRecursoPropio.Visible = false;
                        this.RequiredFieldValidator18.Enabled = false;
                    }
                    this.Fecha_Sello(IdEmpresaE);
                }
                catch (Exception ex)
                {
                    base.Trace.Write(ex.Message);
                    base.Trace.Write(ex.StackTrace);
                }
            }

        }
        

        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            mpexFac.Show();
        }


        protected void lnkDeleteFac_Click(object sender, EventArgs e)
        {
            btnGenerarFactura.Enabled = false;
            mpexFac.Hide();
            up1.Update();
            this.GuardarFactura();
            btnGenerarFactura.Enabled = true;


        }
        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string perfil = this.Session["perfil"] as string;
            long? sistema = this.Session["idSistema"] as long?;
            int? idEmp = this.Session["idEmpresa"] as int?;
            IServicioLocal cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                if (!cliente.TieneConfiguradoCertificado(idEmpresa))
                {
                    this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                    this.btnGenerarFactura.Enabled = (this.BtnVistaPrevia.Enabled = false);
                }
                else
                {
                    empresa emp = cliente.ObtenerEmpresaById(idEmpresa);
                    this.ViewState["RegimenFiscalVS"] = emp.RegimenFiscal;
                    this.ViewState["RFCVS"] = emp.RFC;
                    this.lblError.Text = "";
                    this.btnGenerarFactura.Enabled = (this.BtnVistaPrevia.Enabled = true);
                    this.txtFolio.Text = cliente.SiguienteFolioFactura(idEmpresa);
                    this.ddlClientes.DataSource = cliente.ListaEmpleados(perfil, emp.IdEmpresa, "", false);
                    this.ddlClientes.DataBind();
                    this.ddlClientes_SelectedIndexChanged(null, null);
                    this.ViewState["detalles"] = new List<facturasdetalle>();
                    this.ViewState["iva"] = 0m;
                    this.ViewState["total"] = 0m;
                    this.ViewState["subtotal"] = 0m;
                    this.BindDetallesToGridView();
                    List<CentrosTrabajo> centros = cliente.ListaCentros(idEmpresa);
                    if (centros.Count > 0)
                    {
                        this.DdlCentroTrabajo.DataSource = null;
                        this.DdlCentroTrabajo.DataBind();
                        this.DdlCentroTrabajo.DataSource = centros;
                        this.DdlCentroTrabajo.DataBind();
                        this.DdlCentroTrabajo_OnSelectedIndexChanged(null, null);
                        this.Fecha_Sello(idEmpresa);
                    }
                    else
                    {
                        this.lblError.Text = "Debes dar de alta por lo menos un centro de trabajo";
                    }
                }
            }

        }

        protected void btnGenerarPreview_Click(object sender, EventArgs e)
        {
            if (!ValidarFactura())
                return;
          
            var pdf = Preview();
            if (pdf == null)
            {
                this.lblError.Text = "Error al generar vista previa";
                return;
            }
            Response.AddHeader("Content-Disposition", "attachment; filename=preview.pdf");
            this.Response.ContentType = "application/pdf";
            this.lblError.Text = string.Empty;
            this.Response.BinaryWrite(pdf);
            this.Response.End();
        }


        protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
       
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.ClearAll();
        }

        #region Helper Methods
        protected void Fecha_Sello(int idEmp)
        {
            try
            {
                string idEmpresaString = idEmp.ToString();
                int idEmpresa;
                if (!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idEmpresa))
                {
                    IServicioLocal clienteServicio = NtLinkClientFactory.Cliente();
                    empresa empresa = clienteServicio.ObtenerEmpresaById(idEmpresa);
                    Sistemas sistema = clienteServicio.ObtenerSistemaById((int)empresa.idSistema.Value);
                    string FechaVenceString = empresa.VencimientoCert;
                    this.lblVencimiento.ForeColor = Color.Blue;
                    this.lblVencimiento.Text = "Su CSD caduca el dia: " + FechaVenceString;
                    int[] FechaVenceInt = new int[]
					{
						Convert.ToInt32(FechaVenceString.Substring(6, 4)),
						Convert.ToInt32(FechaVenceString.Substring(3, 2)),
						Convert.ToInt32(FechaVenceString.Substring(0, 2)),
						Convert.ToInt32(FechaVenceString.Substring(11, 2)),
						Convert.ToInt32(FechaVenceString.Substring(17, 2)),
						Convert.ToInt32(FechaVenceString.Substring(17, 2))
					};
                    if (FechaVenceString.Substring(20, 1) == "p")
                    {
                        if (FechaVenceInt[3] != 12)
                        {
                            FechaVenceInt[3] += 12;
                        }
                    }
                    else if (FechaVenceString.Substring(20, 1) == "a" && FechaVenceInt[3] == 12)
                    {
                        FechaVenceInt[3] = 0;
                    }
                    DateTime FechaVence = new DateTime(FechaVenceInt[0], FechaVenceInt[1], FechaVenceInt[2], FechaVenceInt[3], FechaVenceInt[4], FechaVenceInt[5]);
                    TimeSpan c = FechaVence - DateTime.Now;
                    if (c <= TimeSpan.Parse("15.00:00:00.0"))
                    {
                        this.lblVencimiento.ForeColor = Color.Red;
                        bool bloq = true;
                        this.LblDiasSello.Text = "*Su CSD ha Caducado*. Favor de tramitar un sello nuevo en el SAT para poder continuar con la factura.";
                        if (c > TimeSpan.Parse("00.00:00:00.0"))
                        {
                            bloq = false;
                            this.LblDiasSello.Text = "*Su CSD caduca en:";
                            string dias = c.ToString("dd");
                            if (dias != "00")
                            {
                                Label expr_23F = this.LblDiasSello;
                                expr_23F.Text = expr_23F.Text + " " + dias + " Dias";
                            }
                            string horas = c.ToString("hh");
                            if (horas != "00")
                            {
                                if (dias != "00")
                                {
                                    Label expr_29B = this.LblDiasSello;
                                    expr_29B.Text += ",";
                                }
                                Label expr_2B7 = this.LblDiasSello;
                                expr_2B7.Text = expr_2B7.Text + " " + horas + " Horas";
                            }
                            string min = c.ToString("mm");
                            if (min != "00")
                            {
                                if (dias != "00" || horas != "00")
                                {
                                    Label expr_326 = this.LblDiasSello;
                                    expr_326.Text += ",";
                                }
                                Label expr_342 = this.LblDiasSello;
                                expr_342.Text = expr_342.Text + " " + min + " Minutos";
                            }
                            Label expr_366 = this.LblDiasSello;
                            expr_366.Text += ".";
                        }
                        if (this.ddlEmpresa.Items.Count > 1 && bloq)
                        {
                            this.ddlEmpresaE.Visible = true;
                            this.lblpop.Visible = true;
                        }
                        else
                        {
                            this.ddlEmpresaE.Visible = false;
                            this.lblpop.Visible = false;
                        }
                        this.mpeSellos.Show();
                    }
                }
            }
            catch (Exception ex_3E9)
            {
            }
        }

      

        private bool ValidarFactura()
        {
            string regimenFiscal = this.ViewState["RegimenFiscalVS"].ToString();
            string RFCEmisor = this.ViewState["RFCVS"].ToString();
            bool result;
            if (RFCEmisor.Length == 12)
            {
                if (regimenFiscal == "Sueldos y Salarios e Ingresos Asimilados a Salarios" || regimenFiscal == "Arrendamiento" || regimenFiscal == "Demás ingresos" || regimenFiscal == "Ingresos por Dividendos (socios y accionistas)" || regimenFiscal == "Personas Físicas con Actividades Empresariales y Profesionales" || regimenFiscal == "Ingresos por intereses" || regimenFiscal == "Sin obligaciones fiscales" || regimenFiscal == "Incorporación Fiscal" || regimenFiscal == "Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras" || regimenFiscal == "De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales" || regimenFiscal == "Enajenación de acciones en bolsa de valores" || regimenFiscal == "Régimen de los ingresos por obtención de premios" || regimenFiscal == "Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" || regimenFiscal == "Régimen Intermedio de las Personas Físicas con Actividades Empresariales" || regimenFiscal == "Régimen de Arrendamiento" || regimenFiscal == "Otro")
                {
                    this.lblError.Text = "Debe contener la clave de RegimenFiscal de acuerdo al tipo de Persona moral.";
                    result = false;
                    return result;
                }
            }
            else if (RFCEmisor.Length == 13)
            {
                if (regimenFiscal == "General de Ley Personas Morales" || regimenFiscal == "Personas Morales con Fines no Lucrativos" || regimenFiscal == "Consolidación" || regimenFiscal == "Sociedades Cooperativas de Producción que optan por diferir sus ingresos" || regimenFiscal == "Opcional para Grupos de Sociedades" || regimenFiscal == "Coordinados")
                {
                }
            }
            if (this.lblTotalPercepciones.Text == "$0.00" && this.lblTotalOtrosPagos.Text == "$0.00")
            {
                this.lblError.Text = "La factura no tiene percepciones u Otros Pagos";
                result = false;
            }
            else if (Convert.ToDateTime(this.txtFechaInicio.Text) > Convert.ToDateTime(this.txtFechaFin.Text))
            {
                this.lblError.Text = "La fecha inicio debe de ser menor o igual a la fecha final";
                result = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.txtRegistroPatronal.Text))
                {
                    string FechaInicioLaboral = this.ViewState["FechaInicioLaboral"].ToString();
                    string NoSeguridadSocial = this.ViewState["NoSeguridadSocial"].ToString();
                    string SalarioDiario = this.ViewState["SalarioDiario"].ToString();
                    if (string.IsNullOrEmpty(FechaInicioLaboral))
                    {
                        this.lblError.Text = "La fecha inicio Laboral debe de existir.";
                        result = false;
                        return result;
                    }
                    if (string.IsNullOrEmpty(NoSeguridadSocial))
                    {
                        this.lblError.Text = "Le numero de seguro social debe de existir.";
                        result = false;
                        return result;
                    }
                    if (string.IsNullOrEmpty(SalarioDiario))
                    {
                        this.lblError.Text = "Le salario diario integrado debe de existir.";
                        result = false;
                        return result;
                    }
                    if (Convert.ToDateTime(FechaInicioLaboral) > Convert.ToDateTime(this.txtFechaFin.Text))
                    {
                        this.lblError.Text = "La fecha Inicio Laboral debe de ser menor o igual a la fecha final";
                        result = false;
                        return result;
                    }
                }
                this.JubilacionPensionRetiro.ObtenerDatos();
                if (!string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalUnaExhibicion))
                {
                    if (!string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalParcialidad))
                    {
                        this.lblError.Text = "Le Total Parcialidad no debe de existir.";
                        result = false;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(this.JubilacionPensionRetiro.MontoDiario))
                    {
                        this.lblError.Text = "Le Monto Diario no debe de existir.";
                        result = false;
                        return result;
                    }
                }
                if (!string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalParcialidad))
                {
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.MontoDiario))
                    {
                        this.lblError.Text = "Le Monto Diario debe de existir.";
                        result = false;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalUnaExhibicion))
                    {
                        this.lblError.Text = "Le Total una Exhibición no debe de existir.";
                        result = false;
                        return result;
                    }
                }
                if (this.txtFechaPago.Visible && !string.IsNullOrEmpty(this.txtFechaPago.Text))
                {
                    DateTime fecha = DateTime.ParseExact(this.txtFechaPago.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fecha2 = DateTime.ParseExact(AzureUtils.ConvertDateFromMxToUTC(fecha).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    if (fecha2 > AzureUtils.GetDateUTC())
                    {
                        this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                        result = false;
                        return result;
                    }
                    if (fecha2.Year != AzureUtils.GetDateUTC().Year)
                    {
                        this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                        result = false;
                        return result;
                    }
                }
                List<HorasExtra> horasExtras = this.ViewState["HorasExtra"] as List<HorasExtra>;
                if (horasExtras != null)
                {
                    if (horasExtras.Count > 0)
                    {
                        List<Percepcion> percepciones = this.ViewState["percepciones"] as List<Percepcion>;
                        if (percepciones == null)
                        {
                            this.lblError.Text = "Si TipoPercepcion no es Horas extra, no debe existir el elemento HorasExtra";
                            result = false;
                            return result;
                        }
                        if (percepciones.Count == 0)
                        {
                            this.lblError.Text = "Si TipoPercepcion no es Horas extra, no debe existir el elemento HorasExtra";
                            result = false;
                            return result;
                        }
                        List<Percepcion> per = percepciones.FindAll((Percepcion x) => x.TipoPercepcion.Contains("019"));
                        if (per == null)
                        {
                            this.lblError.Text = "Si TipoPercepcion no es Horas extra, no debe existir el elemento HorasExtra";
                            result = false;
                            return result;
                        }
                        if (per.Count == 0)
                        {
                            this.lblError.Text = "Si TipoPercepcion no es Horas extra, no debe existir el elemento HorasExtra";
                            result = false;
                            return result;
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        private void GuardarFactura()
        {
            bool error = false;
            if (this.ValidarFactura())
            {
                List<facturasdetalle> detalles = new List<facturasdetalle>
				{
					new facturasdetalle
					{
						Cantidad = 1m,
						Unidad = "ACT",
						ClaveUnidad = "ACT",
						ClaveProdServ = "84111505",
						Descripcion = "Pago de nómina",
						Precio = 0.0m
					}
				};
                string iniciales = this.Session["iniciales"] as string;
                try
                {
                    IServicioLocal clienteServicio = NtLinkClientFactory.Cliente();
                    int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                    using (clienteServicio as IDisposable)
                    {
                        DatosNomina empleado = clienteServicio.ObtenerDatosNomina(idCliente);
                        clientes clien = clienteServicio.ObtenerClienteById(idCliente);
                        int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                        empresa emp = clienteServicio.ObtenerEmpresaById(idEmpresa);
                        this.ViewState["SubContratacion"] = clienteServicio.ListaSubContratacion(idCliente);
                        facturas fact = this.GetFactura(iniciales, empleado, clien, emp);
                        fact.idcliente = idCliente;
                        detalles[0].Precio = fact.Nomina.TotalOtrosPagos + fact.Nomina.TotalPercepciones;
                        if (!clienteServicio.GuardarFactura(fact, detalles, true, fact.ConceptosAduanera))
                        {
                            this.lblError.Text = "* Error al generar la factura";
                            return;
                        }
                        this.lblError.Text = string.Empty;
                    }
                    this.ClearAll();
                }
                catch (FaultException ae)
                {
                    error = true;
                    this.lblError.Text = ae.Message;
                }
                catch (ApplicationException ae2)
                {
                    error = true;
                    if (ae2.InnerException != null)
                    {
                    }
                    this.lblError.Text = ae2.Message;
                }
                catch (Exception ae3)
                {
                    error = true;
                    if (ae3.InnerException != null)
                    {
                    }
                    this.lblError.Text = "Error al generar el comprobante:" + ae3.Message;
                }
                if (!error)
                {
                    this.mpeCFDIG.Show();
                    this.lblError.Text = "Comprobante generado correctamente  y enviado por correo electrónico";
                }
            }
        }

        private facturas GetFactura(string iniciales, DatosNomina datosNomina, clientes cliente, empresa emp )
        {
            if (string.IsNullOrEmpty(this.lblTotal.Text))
            {
                this.lblTotal.Text = "0.0";
            }
            if (string.IsNullOrEmpty(this.lblTotalPercepciones.Text))
            {
                this.lblTotalPercepciones.Text = "0.0";
            }
            if (string.IsNullOrEmpty(this.lblTotalDeducciones.Text))
            {
                this.lblTotalDeducciones.Text = "0.0";
            }
            if (string.IsNullOrEmpty(this.lblTotalOtrosPagos.Text))
            {
                this.lblTotalOtrosPagos.Text = "0.0";
            }
            string titulo = "Recibo de Nómina";
            if (datosNomina.Regimen == "05" || datosNomina.Regimen == "06" || datosNomina.Regimen == "07" || datosNomina.Regimen == "08" || datosNomina.Regimen == "09" || datosNomina.Regimen == "10" || datosNomina.Regimen == "11" || datosNomina.Regimen == "99")
            {
                titulo = "Recibo de Salarios Asimilados";
            }
            decimal sub = decimal.Parse(this.lblTotalPercepciones.Text, NumberStyles.Currency) + decimal.Parse(this.lblTotalOtrosPagos.Text, NumberStyles.Currency);
            facturas fact = new facturas
            {
                Titulo = titulo,
                TipoDocumento = TipoDocumento.Nomina,
                IdEmpresa = new int?(int.Parse(this.ddlEmpresa.SelectedValue)),
                Importe = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency),
                IVA = new decimal?(0m),
                SubTotal = new decimal?(sub),
                Total = new decimal?(decimal.Parse(this.lblTotal.Text, NumberStyles.Currency)),
                RetencionIsr = decimal.Parse(this.lblTotalDeducciones.Text, NumberStyles.Currency),
                Moneda = new int?(1),
                idcliente = int.Parse(this.ddlClientes.SelectedValue),
                Fecha = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow),
                Folio = this.txtFolio.Text.PadLeft(4, '0'),
                Serie = (string.IsNullOrEmpty(this.txtSerie.Text) ? null : this.txtSerie.Text),
                nProducto = new int?(1),
                captura = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow),
                Cancelado = new short?(0),
                Usuario = iniciales,
                LugarExpedicion = this.lblLugarExpedicion.Text,
                Proyecto = this.txtProyecto.Text,
                MetodoID = "PUE",
                Metodo = "PUE",
                MonedaS = "MXN",
                Cuenta = null,
                FormaPago = "99",
                Descuento = decimal.Parse(this.lblTotalDeducciones.Text, NumberStyles.Currency).ToString(),
                Tipo = new int?(1)
            };
            string fechaI = this.txtFechaInicio.Text;
            string fechaF = this.txtFechaFin.Text;
            string fechaP = this.txtFechaPagoNomina.Text;
            NominaDto dto = new NominaDto();
            string s = "";
            string s2 = "$";
            dto.TipoNomina = this.ddlTipoNomina.SelectedValue;
            dto.FechaPago = fechaP;
            dto.FechaInicialPago = fechaI;
            dto.FechaFinalPago = fechaF;
            dto.NumDiasPagados = Convert.ToDecimal(this.txtDiasPagados.Text);
            dto.TotalPercepciones = Convert.ToDecimal(this.lblTotalPercepciones.Text.Replace(s2, s));
            dto.TotalDeducciones = Convert.ToDecimal(this.lblTotalDeducciones.Text.Replace(s2, s));
            dto.TotalOtrosPagos = Convert.ToDecimal(this.lblTotalOtrosPagos.Text.Replace(s2, s));
            if (dto.TotalDeducciones > 0m)
            {
                fact.Descuento = dto.TotalDeducciones.ToString();
            }
            if (emp != null)
            {
                dto.emisor = new Emisor();
                if (emp.RFC.Length == 13)
                {
                    if (!string.IsNullOrEmpty(emp.CURP))
                    {
                        dto.emisor.Curp = emp.CURP;
                    }
                }
                if (!string.IsNullOrEmpty(this.txtRegistroPatronal.Text))
                {
                    dto.emisor.RegistroPatronal = this.txtRegistroPatronal.Text;
                }
                if (!string.IsNullOrEmpty(this.txtMontoRecursoPropio.Text))
                {
                    dto.emisor.MontoRecursoPropio = Convert.ToDecimal(this.txtMontoRecursoPropio.Text);
                }
                if (this.ddlOrigenRecurso.SelectedValue != "0")
                {
                    dto.emisor.OrigenRecurso = this.ddlOrigenRecurso.SelectedValue;
                }
            }
            dto.receptor = new Receptor();
            List<SubContratacion> subContrataciones = this.ViewState["SubContratacion"] as List<SubContratacion>;
            if (subContrataciones != null)
            {
                if (subContrataciones.Count > 0)
                {
                    dto.receptor.subContratacion = new List<Subcontratacion>();
                    List<Subcontratacion> subC = new List<Subcontratacion>();
                    foreach (SubContratacion subt in subContrataciones)
                    {
                        subC.Add(new Subcontratacion
                        {
                            PorcentajeTiempo = Convert.ToDecimal(subt.PorcentajeTiempo),
                            RfcLabora = subt.RfcLabora
                        });
                    }
                    dto.receptor.subContratacion = subC;
                }
            }
            dto.receptor.Curp = cliente.CURP;
            if (!string.IsNullOrEmpty(datosNomina.NoSeguridadSocial))
            {
                dto.receptor.NumSeguridadSocial = datosNomina.NoSeguridadSocial;
            }
            bool flag;
            if (datosNomina.TipoContrato != "09" && datosNomina.TipoContrato != "10" && datosNomina.TipoContrato != "99")
            {
                DateTime arg_6F2_0 = datosNomina.FechaInicio;
                flag = (1 == 0);
                dto.receptor.FechaInicioRelLaboral = datosNomina.FechaInicio.ToString("yyyy-MM-dd");
                string s3 = WfrNomina.GetWeekNumber(Convert.ToDateTime(this.txtFechaFin.Text), datosNomina.FechaInicio);
                if (s3 != "Fecha Invalida")
                {
                    dto.receptor.Antigüedad = s3;
                }
            }
            dto.receptor.TipoContrato = datosNomina.TipoContrato;
            if (!string.IsNullOrEmpty(datosNomina.Sindicalizado))
            {
                dto.receptor.Sindicalizado = datosNomina.Sindicalizado;
            }
            if (!string.IsNullOrEmpty(datosNomina.TipoJornada))
            {
                dto.receptor.TipoJornada = datosNomina.TipoJornada;
            }
            dto.receptor.TipoRegimen = datosNomina.Regimen;
            dto.receptor.NumEmpleado = datosNomina.NoEmpleado;
            if (!string.IsNullOrEmpty(datosNomina.Departamento))
            {
                dto.receptor.Departamento = datosNomina.Departamento;
            }
            if (!string.IsNullOrEmpty(datosNomina.Puesto))
            {
                dto.receptor.Puesto = datosNomina.Puesto;
            }
            if (!string.IsNullOrEmpty(this.ddlRiesgoPuesto.SelectedValue))
            {
                dto.receptor.RiesgoPuesto = this.ddlRiesgoPuesto.SelectedValue;
            }
            dto.receptor.PeriodicidadPago = this.ddlPeriodicidad.SelectedValue;
            if (!string.IsNullOrEmpty(datosNomina.Banco))
            {
                dto.receptor.Banco = datosNomina.Banco;
            }
            if (!string.IsNullOrEmpty(datosNomina.Clabe))
            {
                dto.receptor.CuentaBancaria = datosNomina.Clabe;
            }
            decimal arg_8BB_0 = datosNomina.SalarioBase;
            flag = (1 == 0);
            if (datosNomina.SalarioBase != 0m)
            {
                dto.receptor.SalarioBaseCotApor = datosNomina.SalarioBase;
            }
            decimal arg_8F5_0 = datosNomina.SalarioDiario;
            flag = (1 == 0);
            if (datosNomina.SalarioDiario != 0m)
            {
                dto.receptor.SalarioDiarioIntegrado = datosNomina.SalarioDiario;
            }
            dto.receptor.ClaveEntFed = datosNomina.ClaveEntFed;
            dto.receptor.UsoCFDI = "G03";
            if (this.ChPercepciones.Checked)
            {
                dto.Percepciones = new Percepciones();
                dto.Percepciones.percepcion = new List<Percepcion>();
                List<Percepcion> percep = this.ViewState["percepciones"] as List<Percepcion>;
                List<HorasExtra> horasExtras = this.ViewState["HorasExtra"] as List<HorasExtra>;
                using (List<Percepcion>.Enumerator enumerator2 = percep.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        Percepcion per = enumerator2.Current;
                        per.horasExtra = horasExtras.FindAll((HorasExtra x) => x.clave.Contains(per.Clave));
                    }
                }
                for (int i = 0; i < percep.Count; i++)
                {
                    if (percep[i].horasExtra != null)
                    {
                        if (percep[i].horasExtra.Count > 0)
                        {
                            Percepcion pe = percep[i];
                            percep.Remove(pe);
                            percep.Insert(0, pe);
                            break;
                        }
                    }
                }
                dto.Percepciones.percepcion = percep;
                if (this.ChJubilacionPensionRetiro.Checked)
                {
                    this.JubilacionPensionRetiro.ObtenerDatos();
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.IngresoAcumulable))
                    {
                        this.JubilacionPensionRetiro.IngresoAcumulable = "0";
                    }
                    dto.Percepciones.IngresoAcumulable = Convert.ToDecimal(this.JubilacionPensionRetiro.IngresoAcumulable);
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.IngresoNoAcumulable))
                    {
                        this.JubilacionPensionRetiro.IngresoNoAcumulable = "0";
                    }
                    dto.Percepciones.IngresoNoAcumulable = Convert.ToDecimal(this.JubilacionPensionRetiro.IngresoNoAcumulable);
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalUnaExhibicion))
                    {
                        this.JubilacionPensionRetiro.TotalUnaExhibicion = "0";
                    }
                    dto.Percepciones.TotalUnaExhibicion = Convert.ToDecimal(this.JubilacionPensionRetiro.TotalUnaExhibicion);
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.TotalParcialidad))
                    {
                        this.JubilacionPensionRetiro.TotalParcialidad = "0";
                    }
                    dto.Percepciones.TotalParcialidad = Convert.ToDecimal(this.JubilacionPensionRetiro.TotalParcialidad);
                    if (string.IsNullOrEmpty(this.JubilacionPensionRetiro.MontoDiario))
                    {
                        this.JubilacionPensionRetiro.MontoDiario = "0";
                    }
                    dto.Percepciones.MontoDiario = Convert.ToDecimal(this.JubilacionPensionRetiro.MontoDiario);
                }
                if (this.ChSeparacionIndemnizacio.Checked)
                {
                    this.SeparacionIndemnizacion.ObtenerDatos();
                    dto.Percepciones.SeparacionIndemnizacionIngresoAcumulable = Convert.ToDecimal(this.SeparacionIndemnizacion.IngresoAcumulable);
                    dto.Percepciones.SeparacionIndemnizacionIngresoNoAcumulable = Convert.ToDecimal(this.SeparacionIndemnizacion.IngresoNoAcumulable);
                    dto.Percepciones.NumAñosServicio = Convert.ToInt32(this.SeparacionIndemnizacion.NumAñosServicio);
                    dto.Percepciones.TotalPagado = Convert.ToDecimal(this.SeparacionIndemnizacion.TotalPagado);
                    dto.Percepciones.UltimoSueldoMensOrd = Convert.ToDecimal(this.SeparacionIndemnizacion.UltimoSueldoMensOrd);
                }
                this.PercepcionesTotales.ObtenerDatos();
                if (string.IsNullOrEmpty(this.PercepcionesTotales.TotalSueldos))
                {
                    this.PercepcionesTotales.TotalSueldos = "0";
                }
                dto.Percepciones.TotalSueldos = Convert.ToDecimal(this.PercepcionesTotales.TotalSueldos.Replace("$", ""));
                if (string.IsNullOrEmpty(this.PercepcionesTotales.TotalSeparacionIndemnizacion))
                {
                    this.PercepcionesTotales.TotalSeparacionIndemnizacion = "0";
                }
                dto.Percepciones.TotalSeparacionIndemnizacion = Convert.ToDecimal(this.PercepcionesTotales.TotalSeparacionIndemnizacion.Replace("$", ""));
                if (string.IsNullOrEmpty(this.PercepcionesTotales.TotalJubilacionPensionRetiro))
                {
                    this.PercepcionesTotales.TotalJubilacionPensionRetiro = "0";
                }
                dto.Percepciones.TotalJubilacionPensionRetiro = Convert.ToDecimal(this.PercepcionesTotales.TotalJubilacionPensionRetiro.Replace("$", ""));
                dto.Percepciones.TotalGravado = Convert.ToDecimal(this.PercepcionesTotales.TotalGravado.Replace("$", ""));
                dto.Percepciones.TotalExento = Convert.ToDecimal(this.PercepcionesTotales.TotalExento.Replace("$", ""));
            }
            if (this.ChDeducciones.Checked)
            {
                List<Deduccion> deduccion = this.ViewState["deducciones"] as List<Deduccion>;
                dto.Deducciones = new Deducciones();
                dto.Deducciones.Deduccion = new List<Deduccion>();
                dto.Deducciones.Deduccion = deduccion;
                if (string.IsNullOrEmpty(this.txtTotalOtrasDeducciones.Text))
                {
                    dto.Deducciones.TotalOtrasDeducciones = 0m;
                }
                else
                {
                    dto.Deducciones.TotalOtrasDeducciones = Convert.ToDecimal(this.txtTotalOtrasDeducciones.Text.Replace("$", ""));
                }
                if (string.IsNullOrEmpty(this.txtTotalImpuestosRetenidos.Text))
                {
                    dto.Deducciones.TotalImpuestosRetenidos = 0m;
                }
                else
                {
                    dto.Deducciones.TotalImpuestosRetenidos = Convert.ToDecimal(this.txtTotalImpuestosRetenidos.Text.Replace("$", ""));
                }
            }
            List<OtroPago> otroPagos = this.ViewState["otrosPagos"] as List<OtroPago>;
            if (otroPagos != null)
            {
                dto.otroPago = new List<OtroPago>();
                dto.otroPago = otroPagos;
            }
            List<Incapacidad> incapacidades = this.ViewState["Incapacidad"] as List<Incapacidad>;
            if (incapacidades != null)
            {
                dto.incapacidades = new List<Incapacidad>();
                dto.incapacidades = incapacidades;
            }
            fact.Nomina = dto;
            fact.Fecha = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);
            fact.FechaPago = new DateTime?(Convert.ToDateTime(dto.FechaPago));
            fact.Vencimiento = new DateTime?(AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow));
            List<string> CfdiRelacionado = this.ViewState["CfdiRelacionado"] as List<string>;
            if (CfdiRelacionado != null && CfdiRelacionado.Count<string>() > 0)
            {
                fact.UUID = CfdiRelacionado;
                fact.TipoRelacion = this.ddlTipoRelacion.SelectedValue;
            }
            return fact;
        }

        private byte[] Preview()
        {
            
            
            bool error = false;
           // var subTotal = decimal.Parse(this.lblTotalPercepciones.Text, NumberStyles.Currency);
            var detalles = new List<facturasdetalle>()
                                   {
                                       new facturasdetalle(){Cantidad = 1,Unidad = "ACT", Descripcion ="Pago de nómina", Precio = 0.0M}
                                   };
            var iniciales = Session["iniciales"] as string;//
            

            try
            {
                var clienteServicio = NtLinkClientFactory.Cliente();
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                clientes c = clienteServicio.ObtenerClienteById(idCliente);
                var empleado = clienteServicio.ObtenerDatosNomina(idCliente);
                //----------
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                var emp = clienteServicio.ObtenerEmpresaById(idEmpresa);

                      
                var fact = GetFactura(iniciales, empleado,c,emp);
                detalles[0].Precio = fact.Nomina.TotalOtrosPagos + fact.Nomina.TotalPercepciones;
          
                using (clienteServicio as IDisposable) 
                {
                    var pdf = clienteServicio.FacturaPreview(fact, detalles, fact.ConceptosAduanera);
                    if (pdf == null)
                    {
                        this.lblError.Text = "* Error al generar la factura";
                        return null;
                    }
                    else return pdf;
                }
            }
            catch (FaultException ae)
            {
                error = true;
                this.lblError.Text = ae.Message;
            }
            catch (ApplicationException ae)
            {
                error = true;
                //Logger.Error(ae.Message);
                if (ae.InnerException != null)
                {
                    //Logger.Error(ae.InnerException.Message);
                }
                this.lblError.Text = ae.Message;
            }
            catch (Exception ae)
            {
                error = true;
                //Logger.Error(ae.Message);
                if (ae.InnerException != null)
                {
                    //Logger.Error(ae.InnerException.Message);
                }
                this.lblError.Text = "Error al generar el comprobante: " + ae.Message;

            }
            if (!error)
            {
                this.lblError.Text = "Comprobante generado correctamente";
            }
            this.lblError.Text = string.Empty;
            return null;
        }

        private void ClearAll()
        {
            ChPercepciones.Checked = false;
            ChJubilacionPensionRetiro.Visible = false;
            ChSeparacionIndemnizacio.Visible = false;
            PercepcionesTotales.Visible = false;
            btnAgregarPercepcion.Enabled = false;
            btnAgregarHorasExtra.Enabled = false;
            JubilacionPensionRetiro.Visible = false;
            SeparacionIndemnizacion.Visible = false;
            ChJubilacionPensionRetiro.Checked = false;
            ChSeparacionIndemnizacio.Checked = false;
            //------
            ChDeducciones.Checked = false;
            AgregarDeduccion.Enabled = false;

            SeparacionIndemnizacion.Limpiar();
            PercepcionesTotales.limpiar();
            UCOtrosPagos.Limpiar();
            JubilacionPensionRetiro.limpiar();
            AccionesOTitulos.Limpiar();
            ddlClave.Items.Clear();
            txtFechaPagoNomina.Text =txtFechaInicio.Text=txtFechaFin.Text=txtDiasPagados.Text=
            this.txtProyecto.Text =  txtProyecto.Text =
                this.txtSerie.Text = string.Empty;
          //  this.ddlStatusRecibo.SelectedIndex = 0;
            this.txtFechaPago.Text = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString("yyyy-MM-dd");
            this.lblTotalDeducciones.Text = 0M.ToString("C");
            this.lblTotalPercepciones.Text = 0M.ToString("C");
            this.lblTotal.Text = 0M.ToString("C");
          //  this.ddlMetodoPago.SelectedIndex = 0;
           // this.ddlMetodoPago_SelectedIndexChanged(null, null);
            var detalles = new List<facturasdetalle>();
            ViewState["percepciones"] = new List<Percepcion>();
            ViewState["otrosPagos"] =new List<OtroPago>();
            ViewState["Incapacidad"] =new  List<Incapacidad>();
            ViewState["deducciones"] = new List<Deduccion>();
            ViewState["HorasExtra"] =new List<HorasExtra>();
            
            ViewState["detalles"] = detalles;
            this.BindDetallesToGridView();
            BindDeduccionesToGridView();
            BindOtrosPagosToGridView();
            BindHorasExtrasToGridView();
            BindIncapacidadToGridView();

            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                this.txtFolio.Text = cliente.SiguienteFolioFactura(idEmpresa);
            }


        }

        private void BindDetallesToGridView()
        {
            var percepciones = ViewState["percepciones"] as List<Percepcion>;
            if (percepciones == null)
            {
                percepciones = new List<Percepcion>();
            }
            /*var deducciones = ViewState["deducciones"] as List<Deduccion>;
            if (deducciones == null)
            {
                deducciones = new List<Deduccion>();
            }*/
            gvPercepciones.DataSource = percepciones;
            gvPercepciones.DataBind();
           // GvDeducciones.DataSource = deducciones;
            //GvDeducciones.DataBind();
        }
        private void BindDeduccionesToGridView()
        {
            var deducciones = ViewState["deducciones"] as List<Deduccion>;
            if (deducciones == null)
            {
                deducciones = new List<Deduccion>();
            }
             GvDeducciones.DataSource = deducciones;
            GvDeducciones.DataBind();
        }
        private void BindOtrosPagosToGridView()
        {
            var otrosPagos = ViewState["otrosPagos"] as List<OtroPago>;
            if (otrosPagos == null)
            {
                otrosPagos = new List<OtroPago>();
            }
            GvOtrosPagos.DataSource = otrosPagos;
            GvOtrosPagos.DataBind();
        }
        private void BindIncapacidadToGridView()
        {
            var incapacidades = ViewState["Incapacidad"] as List<Incapacidad>;
            if (incapacidades == null)
            {
                incapacidades = new List<Incapacidad>();
            }
            GridIncapacidad.DataSource = incapacidades;
            GridIncapacidad.DataBind();
        }
        private void BindHorasExtrasToGridView()
        {
            var horasExtras = ViewState["HorasExtra"] as List<HorasExtra>;
            if (horasExtras == null)
            {
                horasExtras = new List<HorasExtra>();
            }


            gvHorasExtra.DataSource = horasExtras;
            gvHorasExtra.DataBind();
        }

        private void UpdateTotalesPercepcion()
        {
            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
            string s1 = "";
            string s2 = "$";  
            var percepciones = ViewState["percepciones"] as List<Percepcion>;
            if (percepciones == null)
            {
                percepciones = new List<Percepcion>();
            }

            decimal TotalGravado = 0;
            decimal TotalExento = 0;
            decimal TotalSueldos = 0;
            decimal TotalSeparacionIndemnizacion = 0;
            decimal TotalJubilacionPensionRetiro = 0;
            foreach (Percepcion per in percepciones)
            {
                TotalGravado += per.ImporteGravado;
                TotalExento += per.ImporteExento;

                if (per.TipoPercepcion != "022" && per.TipoPercepcion != "023" && per.TipoPercepcion != "025"
                    && per.TipoPercepcion != "039" && per.TipoPercepcion != "044")
                {
                    TotalSueldos += per.ImporteGravado + per.ImporteExento;
                }
                if (per.TipoPercepcion == "022" || per.TipoPercepcion == "023" || per.TipoPercepcion == "025")
                {
                    TotalSeparacionIndemnizacion += per.ImporteGravado + per.ImporteExento;
                }
                if (per.TipoPercepcion == "039" || per.TipoPercepcion == "044")
                {
                    TotalJubilacionPensionRetiro += per.ImporteGravado + per.ImporteExento;
                }
            }
            //if (TotalExento != 0)
                PercepcionesTotales.TotalExento = TotalExento.ToString("C", cul);
            //if (TotalGravado != 0)
                PercepcionesTotales.TotalGravado = TotalGravado.ToString("C", cul);
            if (TotalSueldos != 0)
                PercepcionesTotales.TotalSueldos = TotalSueldos.ToString("C", cul);
            if (TotalSeparacionIndemnizacion != 0)
                PercepcionesTotales.TotalSeparacionIndemnizacion = TotalSeparacionIndemnizacion.ToString("C", cul);
            if (TotalJubilacionPensionRetiro != 0)
                PercepcionesTotales.TotalJubilacionPensionRetiro = TotalJubilacionPensionRetiro.ToString("C", cul);

            lblTotalPercepciones.Text = (TotalSueldos + TotalSeparacionIndemnizacion + TotalJubilacionPensionRetiro).ToString("C", cul);
            PercepcionesTotales.IngresarDatos();
           
            lblTotal.Text = (Convert.ToDecimal(lblTotalPercepciones.Text.Replace(s2, s1)) -
                              Convert.ToDecimal(lblTotalDeducciones.Text.Replace(s2, s1)) +
                              Convert.ToDecimal(lblTotalOtrosPagos.Text.Replace(s2, s1))).ToString("C", cul);
            
        }
        private void UpdateTotalesDeducciones()
        {
            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
            string s1 = "";
            string s2 = "$";
            var deducciones = ViewState["deducciones"] as List<Deduccion>;
            if (deducciones == null)
            {
                deducciones = new List<Deduccion>();
            }
            var ded = 0M;
            foreach (Deduccion detalle in deducciones)
            {
                ded += detalle.Importe;
            }
            lblTotalDeducciones.Text = ded.ToString("C", cul);
            lblTotal.Text = (Convert.ToDecimal(lblTotalPercepciones.Text.Replace(s2, s1)) -
                              Convert.ToDecimal(lblTotalDeducciones.Text.Replace(s2, s1)) +
                              Convert.ToDecimal(lblTotalOtrosPagos.Text.Replace(s2, s1))).ToString("C", cul);
             
        }
        private void UpdateTotalesOtroPago()
        {
            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
            string s1 = "";
            string s2 = "$";
            var otroPagos = ViewState["otrosPagos"] as List<OtroPago>;
            if (otroPagos == null)
                otroPagos = new List<OtroPago>();

            var otro = 0M;
            foreach (OtroPago otr in otroPagos)
            {
                otro += otr.Importe;
            }
            lblTotalOtrosPagos.Text = otro.ToString("C", cul);
            lblTotal.Text = (Convert.ToDecimal(lblTotalPercepciones.Text.Replace(s2, s1)) -
                              Convert.ToDecimal(lblTotalDeducciones.Text.Replace(s2, s1)) +
                              Convert.ToDecimal(lblTotalOtrosPagos.Text.Replace(s2, s1))).ToString("C", cul);
             
        }
        /*
        private void UpdateTotales()
        {
           // if (!string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
            {
               
                CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
                var cliente = NtLinkClientFactory.Cliente();
                var percepciones = ViewState["percepciones"] as List<Percepcion>;
                if (percepciones == null)
                {
                    percepciones = new List<Percepcion>();
                }
                var deducciones = ViewState["deducciones"] as List<Deduccion>;
                if (deducciones == null)
                {
                    deducciones = new List<Deduccion>();
                }
                
                var ded = 0M;
                var per = 0M;
                var otro = 0M;
                var total = 0M;
                foreach (Percepcion detalle in percepciones)
                {
                    per += detalle.ImporteExento + detalle.ImporteGravado;
                    total += detalle.ImporteExento + detalle.ImporteGravado;

                }
                foreach (Deduccion detalle in deducciones)
                {
                    ded += detalle.Importe;
                }
                if (deducciones != null)
                {
                   
                }
                var otroPagos = ViewState["otrosPagos"] as List<OtroPago>;
                if (otroPagos == null)
                    otroPagos = new List<OtroPago>();

                foreach (OtroPago otr in otroPagos)
                {
                    otro += otr.Importe;
                }

                lblTotal.Text = (total - ded +otro).ToString("C", cul);
                lblTotalDeducciones.Text = ded.ToString("C", cul);
                lblTotalPercepciones.Text = per.ToString("C", cul);
                lblTotalOtrosPagos.Text = otro.ToString("C", cul);
               
            }
            
        }
        */
        #endregion

        //protected void ddlStatusFactura_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlStatusFactura.SelectedValue == "1")
        //    {
        //        this.lblFechaPago.Visible = true;
        //        this.txtFechaPago.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //        this.txtFechaPago.Visible = true;
        //    }
        //    else
        //    {
        //        this.lblFechaPago.Visible = false;
        //        this.txtFechaPago.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //        this.txtFechaPago.Visible = false;
        //    }
        //}

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            if (string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
                return;
            using (cliente as IDisposable)
            {
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                clientes c = cliente.ObtenerClienteById(idCliente);
                var datosNomina = cliente.ObtenerDatosNomina(idCliente);
               //....................para validar tipode contrato con registro patronal
                string tipocontrato = datosNomina.TipoContrato;
                if (!string.IsNullOrEmpty(tipocontrato))
               {
                 if (tipocontrato == "01" || tipocontrato == "02" ||
                     tipocontrato == "03" || tipocontrato == "04" || tipocontrato == "05" ||
                     tipocontrato == "06" || tipocontrato == "07" || tipocontrato == "08")
                 {
                   //  txtRegistroPatronal.Enabled = true;
                     ViewState["FechaInicioLaboral"] = datosNomina.FechaInicio;
                     ViewState["NoSeguridadSocial"] = datosNomina.NoSeguridadSocial;
                     ViewState["SalarioDiario"] = datosNomina.SalarioDiario;
                     var str = DdlCentroTrabajo.SelectedValue;
                     int centro = int.Parse(str);
                     var ct = cliente.ObtenerCentroById(centro);
                      if (ct != null)
                      {
                          txtRegistroPatronal.Text = ct.RegistroPatronal;
                      }

                 }
                 else
                 {
                    
                     txtRegistroPatronal.Enabled = false;
                     txtRegistroPatronal.Text = "";
                     
                 }
                }
                //-------------------------------------------------------
                if (datosNomina == null)
                {
                    this.lblError.Text = "El empleado no tiene capturados los datos de la nomina";
                    btnGenerarFactura.Enabled = false;
                }
                else
                {
                    this.lblError.Text = "";
                    btnGenerarFactura.Enabled = true;
                }
                var sb = new StringBuilder();
                sb.AppendLine(c.RazonSocial + " " + c.ApellidoPaterno + " " + c.ApellidoMaterno);
                sb.AppendLine(c.RFC);
                sb.AppendLine(c.Calle + " " + c.NoExt + " " + c.NoInt + " " + c.Colonia);
                sb.AppendLine(c.Ciudad + " " + c.Estado + " " + c.CP);
                this.txtRazonSocial.Text = sb.ToString();
                //this.txtCuenta.Text = c.CuentaPago;
                this.txtTipoJornada.Text = datosNomina.TipoJornada;


            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            /*var detalles = ViewState["detalles"] as List<facturasdetalle>;
            var edicion = detalles[Convert.ToInt32(this.hidNumero.Value)];

            edicion.idproducto = int.Parse(this.hidDetalle.Value);
            edicion.Cantidad = decimal.Parse(this.txtCantidadEdita.Text);
            edicion.Codigo = this.txtCodigoEdita.Text;
            edicion.Descripcion = this.txtDescripcionEdita.Text;
            edicion.Descripcion2 = this.txtObservacionesEdita.Text;
            edicion.Precio = decimal.Parse(this.txtPrecioUnitarioEdita.Text);
            edicion.CuentaPredial = this.txtCuentaPredialEdita.Text;
            edicion.ImporteIva = ((decimal)edicion.PorcentajeIva / 100) * edicion.TotalPartida;
            ViewState["detalles"] = detalles;
            this.BindDetallesToGridView();
            this.UpdateTotales();*/
        }

        

        //protected void ddlCondicionesPago_SelectedIndexChanged(object sender, EventArgs e)
        //{
           

        //}

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        protected void btnAgregarPercepcion_Click(object sender, EventArgs e)
        {

            if (validarPercepcion())
            {
                CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
                var percepciones = ViewState["percepciones"] as List<Percepcion>;
                if (percepciones == null)
                {
                    percepciones = new List<Percepcion>();
                }
                Percepcion percepcion = new Percepcion();
                /*
                decimal num1 = 0.0M;
                string num2 = "";

                num1 = Convert.ToDecimal(txtImporteExcento.Text, cul);
                num2 = num1.ToString("F");
                percepcion.ImporteExento = Convert.ToDecimal(num2);
                num1 = 0.0M; num2 = "";
                num1 = Convert.ToDecimal(txtImporteGravado.Text, cul);
                num2 = num1.ToString("F");
                percepcion.ImporteGravado = Convert.ToDecimal(num2);
                */
                percepcion.ImporteExento = Convert.ToDecimal(txtImporteExcento.Text);
                percepcion.ImporteGravado = Convert.ToDecimal(txtImporteGravado.Text);
 
                AccionesOTitulos.obtenerDatos();
                if (!string.IsNullOrEmpty(AccionesOTitulos.ValorMercado))
                 //   percepcion.ValorMercado = 0;
                //else
                    percepcion.ValorMercado = Convert.ToDecimal(AccionesOTitulos.ValorMercado);
                if (!string.IsNullOrEmpty(AccionesOTitulos.PrecioAlOtorgarse))
                 //   percepcion.PrecioAlOtorgarse = 0;
                //else
                    percepcion.PrecioAlOtorgarse = Convert.ToDecimal(AccionesOTitulos.PrecioAlOtorgarse);

                percepcion.Clave = txtClave.Text;
                percepcion.TipoPercepcion = ddlPercepcion.SelectedValue.ToString();
                percepcion.Concepto = ddlPercepcion.SelectedItem.ToString();
                percepciones.Add(percepcion);
                ViewState["percepciones"] = percepciones;


                BindDetallesToGridView();
                UpdateTotalesPercepcion();
                if (ddlPercepcion.SelectedValue == "019")
                ddlClave.Items.Add(txtClave.Text);
                txtImporteGravado.Text = "";
                txtImporteExcento.Text = "";
                txtClave.Text = "";
                lblClaveError.Visible = false;
                AccionesOTitulos.Limpiar();
            }
        }
        private bool validarPercepcion()
        {
            PercepcionesTotales.ObtenerDatos();
            this.lblError.Text = "";

            if ((Convert.ToDecimal(txtImporteExcento.Text) + Convert.ToDecimal(txtImporteGravado.Text)) <= 0)
            {
                this.lblError.Text = "La suma de ImporteExcento y ImporteGravado debe ser mayor a 0";
                return false;
            }
            /*
            if (ddlPercepcion.SelectedValue != "022" && ddlPercepcion.SelectedValue != "023" &&
                ddlPercepcion.SelectedValue != "025" && ddlPercepcion.SelectedValue != "039" &&
                ddlPercepcion.SelectedValue != "044")
            {

                if (string.IsNullOrEmpty(PercepcionesTotales.TotalSueldos))
                {
                    this.lblError.Text = "El TotalSueldos debe de existir";
                return false;
                }
            }
            */
            //----raras--------------
            if (ddlPercepcion.SelectedValue == "014")
            {
             var incapacidades = ViewState["Incapacidad"] as List<Incapacidad>;
             if (incapacidades == null)
             {
              
                     this.lblError.Text = "La incapacidad debe existir";
                     return false;
               
             }
             else
             {
                 if (incapacidades.Count == 0)
                 {
                     this.lblError.Text = "La incapacidad debe existir";
                     return false;
                 }

                decimal totalImport=0;
                 foreach (Incapacidad inc in incapacidades)
                 {
                     totalImport = +inc.ImporteMonetario;
                 }

                 if ((Convert.ToDecimal(txtImporteExcento.Text) + Convert.ToDecimal(txtImporteGravado.Text))
                     != totalImport)
                 {
                     this.lblError.Text = "La suma de los campos ImporteMonetario debe ser igual a la suma de los valores ImporteGravado e ImporteExento de la percepción";
                     return false;
                 }
         
             }
            }
            /*
            if (ddlPercepcion.SelectedValue == "019")
            {
                var horasExtras = ViewState["HorasExtra"] as List<HorasExtra>;
                if (horasExtras != null)
                {
                    if (horasExtras.Count == 0)
                    {
                        this.lblError.Text = "Las Horas Extras debe existir";
                        return false;
                    }
                }
            
            }*/
            if (ddlPercepcion.SelectedValue == "022" || ddlPercepcion.SelectedValue == "023" ||
                ddlPercepcion.SelectedValue == "025")
            {
                if (ChSeparacionIndemnizacio.Checked == false)
                {
                    this.lblError.Text = "El atributo TotalSeparacionIndemnizacion y el elemento SeparacionIndemnizacion deben de existir";
                    return false;

                }
               
            }
            if (ddlPercepcion.SelectedValue == "039")
            { JubilacionPensionRetiro.ObtenerDatos();
                if(string.IsNullOrEmpty(JubilacionPensionRetiro.TotalUnaExhibicion) )
                {
                 this.lblError.Text = "El atributo TotalUnaExhibicion debe de existir";
                  return false;
                }

                if (!string.IsNullOrEmpty(JubilacionPensionRetiro.MontoDiario))
                {
                    this.lblError.Text = "El atributo MontoDiario no debe de existir";
                    return false;
                }
                if (!string.IsNullOrEmpty(JubilacionPensionRetiro.TotalParcialidad))
                {
                    this.lblError.Text = "El atributo TotalParcialidad no debe de existir";
                    return false;
                }
            }
            if (ddlPercepcion.SelectedValue == "044")
            {
                JubilacionPensionRetiro.ObtenerDatos();
                if (!string.IsNullOrEmpty(JubilacionPensionRetiro.TotalUnaExhibicion))
                {
                    this.lblError.Text = "El atributo TotalUnaExhibicion no debe de existir";
                    return false;
                }

                if (string.IsNullOrEmpty(JubilacionPensionRetiro.MontoDiario))
                {
                    this.lblError.Text = "El atributo MontoDiario debe de existir";
                    return false;
                }
                if (string.IsNullOrEmpty(JubilacionPensionRetiro.TotalParcialidad))
                {
                    this.lblError.Text = "El atributo TotalParcialidad debe de existir";
                    return false;
                }
            }
            /*
            if (ddlPercepcion.SelectedValue == "022" || ddlPercepcion.SelectedValue == "023" ||
                ddlPercepcion.SelectedValue == "025" )
            {

                if (string.IsNullOrEmpty(PercepcionesTotales.TotalSeparacionIndemnizacion))
                {
                    this.lblError.Text = "El Total Separación Indemnizacion debe de existir";
                    return false;
                }
                
            }
              */  
            return true;
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            /*
            var percepciones = ViewState["percepciones"] as List<Percepcion>;
                if (percepciones == null)
                {
                    percepciones = new List<Percepcion>();
                }
                Percepcion percepcion = new Percepcion();
              

                //percepciones.perc = deducciones.Count + 1;
                percepcion.ImporteExento = Convert.ToDecimal(txtImporteExcento.Text);
                percepcion.ImporteGravado =Convert.ToDecimal(txtImporteGravado.Text);
                percepcion.ValorMercado = Convert.ToDecimal(txtValorMercado.Text);
                percepcion.PrecioAlOtorgarse = Convert.ToDecimal(txtPrecioAlOtorgarse.Text);

                percepcion.Clave = txtClave.Text;
                percepcion.TipoPercepcion = ddlPercepcion.SelectedValue.ToString();
                percepcion.Concepto = ddlPercepcion.SelectedItem.ToString();
                percepciones.Add(percepcion);
                ViewState["percepciones"] = percepciones;
               
            
            //-----------------------------------
            else if (lblAgregar.Text == "Agregar Deducción")
            {
                var deducciones = ViewState["deducciones"] as List<Deduccion>;
                if (deducciones == null)
                {
                    deducciones = new List<Deduccion>();
                }
                Deduccion deduccion = new Deduccion();
                decimal importeGravado = 0;
                decimal importeExcento = 0;
               // int tipoPercepcion = 
                if (!decimal.TryParse(txtImporteExcento.Text, out importeExcento))
                {
                    this.lblErrorPercepcion.Text = "El importe excento está mal escrito";
                    return;
                }
                if (!decimal.TryParse(txtImporteGravado.Text, out importeGravado))
                {
                    this.lblErrorPercepcion.Text = "El importe gravado está mal escrito";
                    return;
                }

           //     deduccion.ImporteExento = importeExcento;
            //    deduccion.ImporteGravado = importeGravado;
            //    deduccion.Clave = ddlDeduccion.SelectedValue.ToString();
            //    deduccion.TipoDeduccion = int.Parse(deduccion.Clave);
                deduccion.Concepto = ddlDeduccion.SelectedItem.ToString();
                deducciones.Add(deduccion);
                ViewState["deducciones"] = deducciones;
                
            }
            //---------------------------------------
            

            BindDetallesToGridView();
            UpdateTotales();

            txtImporteGravado.Text = "";
            txtImporteExcento.Text = "";
            txtValorMercado.Text = "";
            txtPrecioAlOtorgarse.Text = "";*/

          }

        protected void AgregarDeduccion_Click(object sender, EventArgs e)
        {
            //lblAgregar.Text = "Agregar Deducción";
            //trPercepcion.Visible = false;
            //tdDeduccion.Visible = true;
            //txtImporteExcento.Text = "";
            //txtImporteGravado.Text = "";
            //mpeBuscarConcepto.Show();
            if (validarDeduccion())
            {
                CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
              
                var deducciones = ViewState["deducciones"] as List<Deduccion>;
                if (deducciones == null)
                {
                    deducciones = new List<Deduccion>();
                }
                Deduccion deduccion = new Deduccion();

                deduccion.Clave = txtClaveDed.Text;
                deduccion.Concepto = txtConceptpDed.Text;
                deduccion.Importe = Convert.ToDecimal(txtImporteDed.Text);
                deduccion.TipoDeduccion = ddlTipoDed.SelectedValue;
               
                deducciones.Add(deduccion);
                ViewState["deducciones"] = deducciones;
                //---------sumatoria para TotalImpuestosRetenidos
                decimal total = 0;
                decimal total2 = 0;
                if (ddlTipoDed.SelectedValue == "002")
                {
                    foreach (Deduccion detalle in deducciones)
                    {
                        if (detalle.TipoDeduccion == "002")
                            total += detalle.Importe;
                    }


                    if (total != 0)
                    {
                        txtTotalImpuestosRetenidos.Text = total.ToString("C", cul);
                    }
                }
                //------------------
                else
                {
                    foreach (Deduccion detalle in deducciones)
                    {
                        if (detalle.TipoDeduccion != "002")
                            total2 += detalle.Importe;
                    }
                    if (total2 != 0)
                    {
                        txtTotalOtrasDeducciones.Text = total2.ToString("C", cul);
                    }
                }


                BindDeduccionesToGridView();

                UpdateTotalesDeducciones();
                txtClaveDed.Text = "";
                txtConceptpDed.Text = "";
                txtImporteDed.Text = "";
            }
        }

        private bool validarDeduccion()
        {
            this.lblError.Text = "";

            if (Convert.ToDecimal(txtImporteDed.Text) <= 0)
            {
                this.lblError.Text = "El Importe debe ser mayor a 0";
                return false;
            }
            if (ddlTipoDed.SelectedValue == "006")
            {
                var incapacidades = ViewState["Incapacidad"] as List<Incapacidad>;
                if (incapacidades == null)
                {
                    this.lblError.Text = "La Incapacidad debe de existir";
                    return false;
                }
                else
                {
                    decimal total = 0;
                    foreach (Incapacidad detalle in incapacidades)
                    {
                        total += detalle.ImporteMonetario;
                     
                    }
                    if (Convert.ToDecimal(txtImporteDed.Text) != total)
                    {
                        this.lblError.Text = "Le Inporte debe de ser igual a la suma suma de los nodos Incapacidad:ImporteMonetario";
                        return false;
                    }
                }
            }
            return true;
        }

        protected void DdlCentroTrabajo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var str = DdlCentroTrabajo.SelectedValue;
            int centro = int.Parse(str);
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                var ct = cliente.ObtenerCentroById(centro);
                if (ct != null)
                {
                    this.txtNombreCentro.Text = ct.Nombre;
                    this.txtRegistroPatronal.Text = ct.RegistroPatronal;
                    this.ddlRiesgoPuesto.SelectedValue = ct.RiesgoTrabajo;


                    //this.txtPeriodicidadPago.Text = ct.TipoNomina;
                    this.lblLugarExpedicion.Text = ct.CP;

                }
            }
        }

        protected void gvPercepciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var percepciones = ViewState["percepciones"] as List<Percepcion>;
                if (percepciones != null)
                {
                    string cla = percepciones[id].Clave;//
                    percepciones.RemoveAt(id);
                    ViewState["percepciones"] = percepciones;
                    BindDetallesToGridView();
                    try
                    {
                        ddlClave.Items.RemoveAt(id);
                    }
                    catch (Exception) { }
                    //-------
                   var horasExtras = ViewState["HorasExtra"] as List<HorasExtra>;
                   if (horasExtras != null)
                   {
                     horasExtras.RemoveAll(c => c.clave == cla);
                     ViewState["HorasExtra"] = horasExtras;
                     BindHorasExtrasToGridView();
                   }
                   
                    //-----------------------
                   UpdateTotalesPercepcion();
                }
                
                //ddlClave.Items.Remove("Tokyo");


            }
        }

        protected void GvDeducciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var deducciones = ViewState["deducciones"] as List<Deduccion>;
                if (deducciones != null)
                {
                    deducciones.RemoveAt(id);
                    ViewState["deducciones"] = deducciones;
                    BindDeduccionesToGridView();
                    UpdateTotalesDeducciones();
                   
                }             
            }
        }

        protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if(ddlTipoNomina.SelectedValue.ToString() == "Otro")
            {
                txtTipoNominaOtro.Visible = true;
                txtTipoNominaOtro.Text = "";
                valOtro.Enabled = true;
            }
            else
            {
                txtTipoNominaOtro.Visible = false;
                txtTipoNominaOtro.Text = "";
                valOtro.Enabled = false;
            }
             */
            ddlPeriodicidad.Items.Clear();
            ddlPeriodicidad.DataBind();

            if (ddlTipoNomina.SelectedValue == "O")
            {

                ListItem oItem1 = new ListItem("Diario", "01");
                ddlPeriodicidad.Items.Add(oItem1);
                ListItem oItem2 = new ListItem("Semanal", "02");
                ddlPeriodicidad.Items.Add(oItem2);
                ListItem oItem3 = new ListItem("Catorcenal", "03");
                ddlPeriodicidad.Items.Add(oItem3);
                ListItem oItem4 = new ListItem("Quincenal", "04");
                ddlPeriodicidad.Items.Add(oItem4);
                ListItem oItem5 = new ListItem("Mensual", "05");
                ddlPeriodicidad.Items.Add(oItem5);
                ListItem oItem6 = new ListItem("Bimestral", "06");
                ddlPeriodicidad.Items.Add(oItem6);
                ListItem oItem7 = new ListItem("Unidad obra", "07");
                ddlPeriodicidad.Items.Add(oItem7);
                ListItem oItem8 = new ListItem("Comisión", "08");
                ddlPeriodicidad.Items.Add(oItem8);
                ListItem oItem9 = new ListItem("Precio alzado", "09");
                ddlPeriodicidad.Items.Add(oItem9);

            }
            else
            {
                ListItem oItem1 = new ListItem("Otra Periodicidad", "99");
                ddlPeriodicidad.Items.Add(oItem1);


            }
            ddlPeriodicidad.DataBind();

        }

        /*protected void ddlStatusRecibo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatusRecibo.SelectedValue == "1")
            {
                //this.lblFechaPago.Visible = true; 
                this.txtFechaPago.Text = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString("yyyy-MM-dd");
            }
            else
            {
                //this.lblFechaPago.Visible = false;
                this.txtFechaPago.Text = "";
            }
        }
        */
        protected void ChJubilacionPensionRetiro_CheckedChanged(object sender, EventArgs e)
        {
            if (ChJubilacionPensionRetiro.Checked == true)
                JubilacionPensionRetiro.Visible = true;
            else
                JubilacionPensionRetiro.Visible = false;
        }

        protected void ChSeparacionIndemnizacio_CheckedChanged(object sender, EventArgs e)
        {
            if (ChSeparacionIndemnizacio.Checked == true)
                SeparacionIndemnizacion.Visible = true;
            else
                SeparacionIndemnizacion.Visible = false;
          
        }

        protected void btnCerrarConcepto_Click(object sender, EventArgs e)
        {

        }

        protected void ChAccionesOTitulos_CheckedChanged(object sender, EventArgs e)
        {
            if (ChAccionesOTitulos.Checked == true)
            {
                AccionesOTitulos.Visible = true;
                AccionesOTitulos.Limpiar();
            }
            else
            {
                AccionesOTitulos.Limpiar();
                AccionesOTitulos.Visible = false;
            }
           // mpePercepcion.Hide();
        }

        protected void gvHorasExtra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var horasExtras = ViewState["HorasExtra"] as List<HorasExtra>;
                if (horasExtras != null)
                {
                    horasExtras.RemoveAt(id);
                    ViewState["HorasExtra"] = horasExtras;
                    BindHorasExtrasToGridView();
                }
                //                
            }
        }
        protected void GvOtrosPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var otroPagos = ViewState["otrosPagos"] as List<OtroPago>;
                if (otroPagos != null)
                {
                    otroPagos.RemoveAt(id);
                    ViewState["otrosPagos"] = otroPagos;
                    BindOtrosPagosToGridView();
                    UpdateTotalesOtroPago();
                }
                //                
            }
        }


        protected void btnAgregarHorasExtra_Click(object sender, EventArgs e)
        {
            if (ddlClave.Items.Count < 1)
                lblClaveError.Visible = true;
            else
            {
                lblClaveError.Visible = false;
                var horasExtras = ViewState["HorasExtra"] as List<HorasExtra>;
                if (horasExtras == null)
                {
                    horasExtras = new List<HorasExtra>();
                }
                HorasExtra horasExtra = new HorasExtra();
                horasExtra.clave = ddlClave.SelectedValue;
                horasExtra.Dias = Convert.ToInt32(txtDias.Text);
                horasExtra.HoraExtra = Convert.ToInt32(txtHorasExtra.Text);
                horasExtra.ImportePagado = Convert.ToDecimal(txtImportePagado.Text);
                horasExtra.TipoHoras = ddlTipoHoras.SelectedValue.ToString();
                horasExtras.Add(horasExtra);
                ViewState["HorasExtra"] = horasExtras;

                txtDias.Text = "";
                txtHorasExtra.Text = "";
                txtImportePagado.Text = "";

                BindHorasExtrasToGridView();


            }
        }

        protected void ChPercepciones_CheckedChanged(object sender, EventArgs e)
        {
            if (ChPercepciones.Checked == true)
            {
                ChJubilacionPensionRetiro.Visible = true;
                ChSeparacionIndemnizacio.Visible = true;
                PercepcionesTotales.Visible = true;
                PercepcionesTotales.limpiar();
                btnAgregarPercepcion.Enabled = true;
                btnAgregarHorasExtra.Enabled = true;
                var horasExtras = new List<HorasExtra>();
                ViewState["HorasExtra"] = horasExtras;
                BindHorasExtrasToGridView();
                var percepciones = new List<Percepcion>();
                ViewState["percepciones"] = percepciones;
                BindDetallesToGridView();
            }
            else
            {

                ChJubilacionPensionRetiro.Visible = false;
                ChSeparacionIndemnizacio.Visible = false;
                PercepcionesTotales.Visible = false;
                btnAgregarPercepcion.Enabled = false;
                btnAgregarHorasExtra.Enabled = false;
                JubilacionPensionRetiro.Visible = false;
                SeparacionIndemnizacion.Visible = false;
                ChJubilacionPensionRetiro.Checked = false;
                ChSeparacionIndemnizacio.Checked = false;
            }
        }

        protected void ChDeducciones_CheckedChanged(object sender, EventArgs e)
        {
            if (ChDeducciones.Checked == true)
            {
                AgregarDeduccion.Enabled = true;
               // txtTotalOtrasDeducciones.Enabled = true;
               // txtTotalImpuestosRetenidos.Enabled = true;
            }
            else
            {

                AgregarDeduccion.Enabled = false;
              //  txtTotalOtrasDeducciones.Enabled = false;
                //txtTotalImpuestosRetenidos.Enabled = false;
            }
        }

        protected void AgregarOtros_Click(object sender, EventArgs e)
        {
                 UCOtrosPagos.ObtenerDatos();
              
            if(validarOtrosPagos())
            {
                      
                      var otroPagos = ViewState["otrosPagos"] as List<OtroPago>;
                      if (otroPagos == null)
                      {
                          otroPagos = new List<OtroPago>();
                      }
                      OtroPago otroPago = new OtroPago();

                      otroPago.Clave = txtClaveOtros.Text;
                      otroPago.Concepto = txtConceptoOtros.Text;
                      otroPago.Importe = Convert.ToDecimal(txtImporteOtros.Text);
                      otroPago.TipoOtroPago = ddlTipoOtros.SelectedValue;
                      if (string.IsNullOrEmpty(txtSubsidio.Text))
                          otroPago.SubsidioCausado = -1;
                      else
                          otroPago.SubsidioCausado = Convert.ToDecimal(Convert.ToDecimal(txtSubsidio.Text));


                      if (string.IsNullOrEmpty(UCOtrosPagos.Ano))
                          otroPago.Año = 0;
                      else
                          otroPago.Año = Convert.ToInt16(UCOtrosPagos.Ano);
                      if (string.IsNullOrEmpty(UCOtrosPagos.SaldoAFavor))
                          otroPago.SaldoAFavor = 0;
                      else
                          otroPago.SaldoAFavor = Convert.ToDecimal(UCOtrosPagos.SaldoAFavor);
                      if (string.IsNullOrEmpty(UCOtrosPagos.RemanenteSalFav))
                          otroPago.RemanenteSalFav = 0;
                      else
                          otroPago.RemanenteSalFav = Convert.ToDecimal(UCOtrosPagos.RemanenteSalFav);

                      otroPagos.Add(otroPago);
                      ViewState["otrosPagos"] = otroPagos;

                      BindOtrosPagosToGridView();
                      UpdateTotalesOtroPago();

                      txtClaveOtros.Text = "";
                      txtConceptoOtros.Text = "";
                      txtImporteOtros.Text = "";
                      txtSubsidio.Text = "";

                      UCOtrosPagos.Limpiar();
                  }

        }


        private bool validarOtrosPagos()
        {
            this.lblError.Text = "";
        
              DateTime localDate = DateTime.Now;
              int año=localDate.Year;
              int mes = localDate.Month;

            if(!string.IsNullOrEmpty( txtFechaPagoNomina.Text))
            {
                DateTime fechames = Convert.ToDateTime(txtFechaPagoNomina.Text);
                mes = fechames.Month;
            }

              if (!string.IsNullOrEmpty(UCOtrosPagos.Ano))
              {
                  if (mes == 12)
                  {
                      if (año < Convert.ToInt16(UCOtrosPagos.Ano))
                      {
                          this.lblError.Text = "El año otros pagos debe ser menor que el año actual";
                          return false;
                      }
                  }
                  else
                      if (año <= Convert.ToInt16(UCOtrosPagos.Ano))
                      {
                          this.lblError.Text = "El año otros pagos debe ser menor que el año actual";
                          return false;
                      }

              }
            if(!string.IsNullOrEmpty(UCOtrosPagos.SaldoAFavor)&&!string.IsNullOrEmpty(UCOtrosPagos.RemanenteSalFav))
              if (Convert.ToDecimal(UCOtrosPagos.SaldoAFavor) < Convert.ToDecimal(UCOtrosPagos.RemanenteSalFav))
              {
                  this.lblError.Text = "El saldo a favor debe ser mayor o igual a RemanenteSalFav";
                  return false;
              }

            if (!string.IsNullOrEmpty(txtSubsidio.Text))
            {
                if (Convert.ToDecimal(txtSubsidio.Text)<Convert.ToDecimal(txtImporteOtros.Text))
              {
                  this.lblError.Text = "El Subsidio debe ser mayor o igual a importe";
                  return false;
              }
            
            }

           
            if (ddlTipoOtros.SelectedValue == "002")
            {
                if (Convert.ToDecimal(txtImporteOtros.Text) < 0)
                {
                    this.lblError.Text = "El ImporteOtros debe ser mayor a 0";
                    return false;

                }
                if (string.IsNullOrEmpty(txtSubsidio.Text))
                {
                    this.lblError.Text = "El Subsidio debe ser de existir";
                    return false;
                }
            }
            else
            {
                if (Convert.ToDecimal(txtImporteOtros.Text) <= 0)
                {
                    this.lblError.Text = "El ImporteOtros debe ser mayor a 0";
                    return false;

                } 
            }

            if (ddlTipoOtros.SelectedValue == "004")
            {
                if(string.IsNullOrEmpty(UCOtrosPagos.SaldoAFavor))
                {
                    this.lblError.Text = "El SaldoAFavor debe ser de existir";
                    return false;
                }
            }
              return true;
        }
        protected void ChCompensacionSaldosAFavor_CheckedChanged(object sender, EventArgs e)
        {
            if (ChCompensacionSaldosAFavor.Checked == true)
            {
                UCOtrosPagos.Visible = true;
            }
            else
            {
                UCOtrosPagos.Visible = false;
                UCOtrosPagos.Limpiar();
            }
        }

        protected void btnAgregarIncapacidad_Click(object sender, EventArgs e)
        {

            var incapacidades = ViewState["Incapacidad"] as List<Incapacidad>;
            if (incapacidades == null)
            {
                incapacidades = new List<Incapacidad>();
            }
            Incapacidad incapacidad = new Incapacidad();

            incapacidad.DiasIncapacidad = Convert.ToInt16(txtDiasIncapacidad.Text);
            incapacidad.ImporteMonetario =Convert.ToDecimal(txtImporteMonetario.Text);
            incapacidad.TipoIncapacidad = ddlTipoIncapacidad.SelectedValue;


            incapacidades.Add(incapacidad);
            ViewState["Incapacidad"] = incapacidades;

            BindIncapacidadToGridView();

            txtDiasIncapacidad.Text="";
            txtImporteMonetario.Text="";

            


        }

        protected void GridIncapacidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var incapacidades = ViewState["Incapacidad"] as List<Incapacidad>;
                if (incapacidades != null)
                {
                    incapacidades.RemoveAt(id);
                    ViewState["Incapacidad"] = incapacidades;
                    BindIncapacidadToGridView();
                }
                //                
            }
        }
        /*
        protected void ddlOrigenRecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrigenRecurso.SelectedValue == "IM")
            {
                txtMontoRecursoPropio.Enabled = true;
                txtMontoRecursoPropio.Visible = true;
                txtMontoRecursoPropio.Enabled = true;
                lblMontoRecursoPropio.Visible = true;
            }
            else
            {
                txtMontoRecursoPropio.Enabled = false;
                txtMontoRecursoPropio.Visible = false;
                txtMontoRecursoPropio.Enabled = false;
                lblMontoRecursoPropio.Visible = false;
            }
        }
        */
        public static string GetWeekNumber(DateTime newdt, DateTime olddt)
        {
            TimeSpan ts = newdt - olddt;
            int dias = ts.Days;
            dias = (dias + 1) / 7;

            return "P"+dias + "W";
        }
        private string DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            Int32 anios;
            Int32 meses;
            Int32 dias;
            String str = "P";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "Fecha Invalida";
            }
            if (anios > 0)
                str = str + anios.ToString() + "Y";
            if (meses > 0 || anios>0)
                str = str + meses.ToString() + "M";
            if (dias > 0)
                str = str + dias.ToString() + "D";

            return str;
        }

        protected void ddlPercepcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPercepcion.SelectedValue == "045")
            {
                AccionesOTitulos.Visible = true;
                AccionesOTitulos.Limpiar();
                ChAccionesOTitulos.Checked = true;
            }
            else
            {
                AccionesOTitulos.Limpiar();
                AccionesOTitulos.Visible = false;
                ChAccionesOTitulos.Checked = false;
            }
            if (ddlPercepcion.SelectedValue == "039" || ddlPercepcion.SelectedValue == "044")
            { 
              JubilacionPensionRetiro.Visible = true;
              ChJubilacionPensionRetiro.Checked = true;
            }
            else
            {
                JubilacionPensionRetiro.Visible = false;
                ChJubilacionPensionRetiro.Checked = false;
            }
        }

        protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOrigenRecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrigenRecurso.SelectedValue == "IM")
            {
                txtMontoRecursoPropio.Enabled = true;
                txtMontoRecursoPropio.Visible = true;
                lblMontoRecursoPropio.Visible = true;
                txtMontoRecursoPropio.Text = "";
                RequiredFieldValidator18.Enabled = true;
            }
            else
            {
                txtMontoRecursoPropio.Enabled = false;
                txtMontoRecursoPropio.Visible = false;
                lblMontoRecursoPropio.Visible = false;
                RequiredFieldValidator18.Enabled = false;
            }


        }


        protected void btnCfdiRelacionado_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtUUDI.Text))
            {
                List<string> CfdiRelacionado = new List<string>();
                CfdiRelacionado = this.ViewState["CfdiRelacionado"] as List<string>;
                if (CfdiRelacionado == null)
                    CfdiRelacionado = new List<string>();
                CfdiRelacionado.Add(this.txtUUDI.Text);
                this.ViewState["CfdiRelacionado"] = CfdiRelacionado;
                this.BindCfdiRelacionadoToGridView();
                this.txtUUDI.Text = "";
            }
        }
        protected void gvCfdiRelacionado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton lb = e.Row.FindControl("gvlnkDelete") as LinkButton;
            if (lb != null)
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
        }
        protected void gvCfdiRelacionado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarCfdiRelacionado"))
            {
                List<string> CfdiRelacionado = this.ViewState["CfdiRelacionado"] as List<string>;
                CfdiRelacionado.RemoveAt(Convert.ToInt32(e.CommandArgument));
                this.ViewState["CfdiRelacionado"] = CfdiRelacionado;
                this.BindCfdiRelacionadoToGridView();
            }
        }

        private void BindCfdiRelacionadoToGridView()
        {
            List<string> CfdiRelacionado = this.ViewState["CfdiRelacionado"] as List<string>;
            if (CfdiRelacionado != null && CfdiRelacionado.Count > 0)
            {
                int noColumns = this.gvCfdiRelacionado.Columns.Count;
                this.gvCfdiRelacionado.Columns[noColumns - 1].Visible = true;
            }
            else
            {
                int noColumns = this.gvCfdiRelacionado.Columns.Count;
                this.gvCfdiRelacionado.Columns[noColumns - 1].Visible = false;
            }
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("UUID");
            int t = 0;
            foreach (string array in CfdiRelacionado)
            {
                DataRow row = table.NewRow();
                row["ID"] = t + 1;
                row["UUID"] = array;
                table.Rows.Add(row);
                t++;
            }
            this.gvCfdiRelacionado.DataSource = table;
            this.gvCfdiRelacionado.DataBind();
        }

        protected void cbCfdiRelacionados_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCfdiRelacionados.Checked == true)
            {
                DivCfdiRelacionados.Attributes.Add("style", "display:block;");
            }
            else
            {
                DivCfdiRelacionados.Attributes.Add("style", "display:none;");

            }
        }

        protected void btclose_Click(object sender, EventArgs e)
        {
            mpeSellos.Hide();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

        }

    }
}