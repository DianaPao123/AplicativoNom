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

namespace GafLookPaid
{
    public partial class WfrNomina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    this.txtFechaPago.Text = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString("yyyy-MM-dd");
                    var perfil = Session["perfil"] as string;
                    var sistema = Session["idSistema"] as long?;
                    var idEmp = Session["idEmpresa"] as int?;

                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var empresas = cliente.ListaEmpresas(perfil, idEmp.Value, sistema.Value, null);
                        var listaEmpresas = new List<empresa>(empresas);

                        this.ddlEmpresa.DataSource = listaEmpresas;
                        this.ddlEmpresa.DataBind();

                        int idEmpresa = listaEmpresas.First().IdEmpresa;
                        var centros = cliente.ListaCentros(idEmpresa);
                        if (centros.Count > 0)
                        {
                            this.DdlCentroTrabajo.DataSource = centros;
                            this.DdlCentroTrabajo.DataBind();
                            this.DdlCentroTrabajo_OnSelectedIndexChanged(null, null);
                        }
                        else
                        {
                            lblError.Text = "Debes dar de alta por lo menos un centro de trabajo";
                            return;
                        }

                        this.ddlClientes.DataSource = cliente.ListaEmpleados(perfil, idEmpresa, "", false);
                        this.ddlClientes.DataBind();
                        ddlClientes_SelectedIndexChanged(null, null);
                        if (!cliente.TieneConfiguradoCertificado(idEmpresa))
                        {
                            this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                            this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled = this.ddlMoneda.Enabled = false;
                            return;
                        }

                        if (listaEmpresas.Count > 0)
                        {
                            this.txtFolio.Text = cliente.SiguienteFolioFactura(idEmpresa);
                            ddlClientes_SelectedIndexChanged(null, null);
                        }

                        //this.ddlSucursales.DataSource = cliente.ListaSucursales(idEmpresa);
                        //ddlSucursales.DataValueField = "LugarExpedicion";
                        //ddlSucursales.DataTextField = "Nombre";

                        //this.ddlSucursales.DataBind();
                    }

                    ViewState["detalles"] = new List<facturasdetalle>();
                    ViewState["iva"] = 0M;
                    ViewState["total"] = 0M;
                    ViewState["subtotal"] = 0M;

                    //this.BindDetallesToGridView();
                    this.UpdateTotales();
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.Message); 
                    Trace.Write(ex.StackTrace);
                }
            }
        }
        

        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            this.GuardarFactura();
        }

       

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var perfil = Session["perfil"] as string;
            var sistema = Session["idSistema"] as long?;
            var idEmp = Session["idEmpresa"] as int?;
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                if (!cliente.TieneConfiguradoCertificado(idEmpresa))
                {
                    this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                    this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled = this.ddlMoneda.Enabled = false;
                    return;
                }
                var emp = cliente.ObtenerEmpresaById(idEmpresa);
                lblError.Text = "";
                //this.ddlSucursales.DataSource = cliente.ListaSucursales(idEmpresa);
                //ddlSucursales.DataValueField = "LugarExpedicion";
                //ddlSucursales.DataTextField = "Nombre";
                //ddlSucursales.DataBind();
                this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled = this.ddlMoneda.Enabled = true;
                this.txtFolio.Text = cliente.SiguienteFolioFactura(idEmpresa);
                this.ddlClientes.DataSource = cliente.ListaEmpleados(perfil, emp.IdEmpresa, "", false);
                this.ddlClientes.DataBind();
                ddlClientes_SelectedIndexChanged(null, null);
                ViewState["detalles"] = new List<facturasdetalle>();
                ViewState["iva"] = 0M;
                ViewState["total"] = 0M;
                ViewState["subtotal"] = 0M;

                this.BindDetallesToGridView();
                this.UpdateTotales();


                var centros = cliente.ListaCentros(idEmpresa);
                if (centros.Count > 0)
                {
                    this.DdlCentroTrabajo.DataSource = null;
                    this.DdlCentroTrabajo.DataBind();
                    this.DdlCentroTrabajo.DataSource = centros;
                    this.DdlCentroTrabajo.DataBind();
                    this.DdlCentroTrabajo_OnSelectedIndexChanged(null, null);
                }
                else
                {
                    lblError.Text = "Debes dar de alta por lo menos un centro de trabajo";
                    return;
                }

            }
        }

        protected void btnGenerarPreview_Click(object sender, EventArgs e)
        {
            if (!ValidarFactura())
                return;
            Response.AddHeader("Content-Disposition", "attachment; filename=preview.pdf");
            this.Response.ContentType = "application/pdf";
            var pdf = Preview();
            if (pdf == null)
            {
                this.lblError.Text = "Error al generar vista previa";
                return;
            }
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

      

        private bool ValidarFactura()
        {
            if ((ViewState["percepciones"] as List<Percepcion>) != null && (ViewState["percepciones"] as List<Percepcion>).Count == 0)
            {
                this.lblError.Text = "La factura no puede estar vacía";
                return false;
            }
            if (string.IsNullOrEmpty(this.txtFolio.Text))
            {
                this.lblError.Text = "Escribe el folio de la factura";
                return false;
            }
            if (txtFechaPago.Visible && !string.IsNullOrEmpty(txtFechaPago.Text))
            {
                //var fecha = DateTime.ParseExact(txtFechaPago.Text, "yyyy-MM-dd", new CultureInfo("es-MX"));
                var fecha = DateTime.ParseExact(txtFechaPago.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var fecha2 = DateTime.ParseExact(AzureUtils.ConvertDateFromMxToUTC(fecha).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                
                if (fecha2 > AzureUtils.GetDateUTC())
                {
                    this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                    return false;
                }
                if (fecha2.Year != AzureUtils.GetDateUTC().Year)
                {
                    this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                    return false;
                }
            }
            /*
            if ((this.ddlMetodoPago.SelectedValue == "02" || this.ddlMetodoPago.SelectedValue == "03" || this.ddlMetodoPago.SelectedValue == "04"
           || this.ddlMetodoPago.SelectedValue == "05") && string.IsNullOrEmpty(this.txtCuenta.Text))
            {
                this.lblError.Text = "Falta agregar el #Cuenta o # Tarjeta. ";
                return false;
            }
            */
            return true;
        }

        private void GuardarFactura()
        {
            bool error = false;
            if (ValidarFactura())
            {
                //var detalles = ViewState["detalles"] as List<facturasdetalle>;
                var subTotal = decimal.Parse(this.lblTotalPercepciones.Text, NumberStyles.Currency);
                var detalles = new List<facturasdetalle>()
                                   {
                                       new facturasdetalle(){Cantidad = 1,Unidad = "Servicio", Descripcion =ddlConcepto.SelectedValue, Precio = subTotal}
                                   };
                var iniciales = Session["iniciales"] as string;
                
                
                try
                {

                    var clienteServicio = NtLinkClientFactory.Cliente();
                    int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                    using (clienteServicio as IDisposable)
                    {

                        var empleado = clienteServicio.ObtenerDatosNomina(idCliente);
                        var clien = clienteServicio.ObtenerClienteById(idCliente);
                        
                        var fact = GetFactura(iniciales, empleado,clien);
                        fact.idcliente = idCliente;
                        
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
                catch (ApplicationException ae)
                {
                    error = true;
                    //Logger.Error(ae.Message);
                    if (ae.InnerException != null)
                    {
                        //Logger.Error(ae.InnerException.Message);
                    }
                    this.lblError.Text = ae.Message;
                }//asd
                catch (Exception ae)
                {
                    error = true;
                    //Logger.Error(ae.Message);
                    if (ae.InnerException != null)
                    {
                        //Logger.Error(ae.InnerException.Message);
                    }
                    this.lblError.Text = "Error al generar el comprobante:" + ae.Message;
                }
                if (!error)
                {
                    this.lblError.Text = "Comprobante generado correctamente  y enviado por correo electrónico";
                }
                // this.lblError.Text = string.Empty;
            }
        }

        private facturas GetFactura(string iniciales, DatosNomina datosNomina, clientes cliente )
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            var fact = new facturas
                           {
                               Titulo = ddlTipoDocumento.SelectedValue,
                               TipoDocumento = TipoDocumento.Nomina,
                               IdEmpresa = int.Parse(this.ddlEmpresa.SelectedValue),
                               Importe = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency),
                               IVA = 0,
                               SubTotal = decimal.Parse(this.lblTotalPercepciones.Text, NumberStyles.Currency),
                               Total = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency),
                               RetencionIsr = decimal.Parse(this.lblTotalDeducciones.Text, NumberStyles.Currency),
                               Moneda = int.Parse(this.ddlMoneda.SelectedValue),
                               idcliente = int.Parse(this.ddlClientes.SelectedValue),
                               //Fecha = DateTime.Now,
                               //Fecha = DateTime.Parse(AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString(new CultureInfo("es-MX")), new CultureInfo("es-MX")),
                               Fecha = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow),
                               Folio = this.txtFolio.Text.PadLeft(4, '0'),
                               Serie = string.IsNullOrEmpty(this.txtSerie.Text) ? null : this.txtSerie.Text,
                               nProducto = 1,
                               captura = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow),
                               Cancelado = 0,
                               Usuario = iniciales,
                               //LugarExpedicion = this.ddlSucursales.SelectedValue,
                               LugarExpedicion = this.lblLugarExpedicion.Text,
                               Proyecto = this.txtProyecto.Text,
                               MetodoID = this.ddlMetodoPago.SelectedValue != "99" ? this.ddlMetodoPago.SelectedValue : "99",
                               Metodo = this.ddlMetodoPago.SelectedValue != "99"
                                       ? this.ddlMetodoPago.SelectedItem.Text : txtMetodoPago.Text,
                              
                               MonedaS = this.ddlMoneda.SelectedItem.Text,
                               Cuenta = string.IsNullOrEmpty(this.txtCuenta.Text) ? null : this.txtCuenta.Text,
                               FormaPago ="En una sola exhibición", //ddlCondicionesPago.SelectedValue,
                               Descuento =
                                   decimal.Parse(this.lblTotalDeducciones.Text, NumberStyles.Currency).ToString(),
                               Tipo = 1,
                           };
            
            DateTime fechaI = DateTime.ParseExact(txtFechaInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fechaF = DateTime.ParseExact(txtFechaFin.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fechaP = DateTime.ParseExact(txtFechaPagoNomina.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            NominaDto dto = new NominaDto();

            dto.Banco = datosNomina.Banco == null ? "0" : datosNomina.Banco;
                dto.CLABE = datosNomina.Clabe;
                dto.Departamento = datosNomina.Departamento;
                dto.FechaInicialPago = fechaI;
                dto.FechaFinalPago = fechaF;
                dto.CURP =  cliente.CURP;
                dto.NumEmpleado = datosNomina.NoEmpleado;
                dto.NumSeguridadSocial = datosNomina.NoSeguridadSocial;
                dto.Puesto = datosNomina.Puesto;

                if (ddlTipoNomina.SelectedValue.ToString() == "Otro")
                    dto.PeriodicidadPago = txtTipoNominaOtro.Text;
                else
                    dto.PeriodicidadPago = ddlTipoNomina.SelectedValue.ToString();
            
                dto.TipoRegimen = int.Parse(datosNomina.Regimen);
                dto.RiesgoPuesto = int.Parse(ddlRiesgoPuesto.SelectedValue);
                dto.SalarioBaseCotApor = datosNomina.SalarioBase; 
                dto.SalarioDiarioIntegrado = datosNomina.SalarioDiario;
                dto.TipoContrato = datosNomina.TipoContrato;
                dto.TipoJornada = txtTipoJornada.Text;
                dto.FechaPago = fechaP;

            dto.RegistroPatronal = txtRegistroPatronal.Text;

            

            var percepciones = ViewState["percepciones"] as List<Percepcion>;
            if (percepciones != null && percepciones.Count > 0)
            {
                Percepciones per = new Percepciones()
                {
                    Percepcion = percepciones,
                    TotalExento = percepciones.Sum(p => p.ImporteExento),
                    TotalGravado = percepciones.Sum(p => p.ImporteGravado)
                };
                dto.Percepciones = per;
            }

            var deducciones = ViewState["deducciones"] as List<Deduccion>;
            if (deducciones != null && deducciones.Count > 0)
            {
                Deducciones ded = new Deducciones()
                {
                    Deduccion = deducciones,
                    TotalExento = deducciones.Sum(p => p.ImporteExento),
                    TotalGravado = deducciones.Sum(p => p.ImporteGravado)
                };
                dto.Deducciones = ded;
            }
            dto.NumDiasPagados = string.IsNullOrEmpty(txtDiasPagados.Text) ? 0 : decimal.Parse(txtDiasPagados.Text);

            fact.Nomina = dto;

            fact.ConceptosAduanera = new List<facturasdetalle>();
            if (deducciones != null && deducciones.Count > 0)
            {
                foreach (Deduccion deduc in deducciones)
                {
                    facturasdetalle det = new facturasdetalle();
                    det.Descripcion = deduc.Concepto;
                    det.Codigo = deduc.Clave;
                    det.Precio = deduc.ImporteExento;
                    det.Total = deduc.ImporteGravado;
                    det.Descripcion2 = "deduccion";
                    det.Unidad = deduc.TipoDeduccion.ToString();
                    fact.ConceptosAduanera.Add(det);
                }
            }
            if (percepciones != null && percepciones.Count > 0)
            {
                foreach (Percepcion perc in percepciones)
                {
                    facturasdetalle det = new facturasdetalle();
                    det.Descripcion = perc.Concepto;
                    det.Codigo = perc.Clave;
                    det.Precio = perc.ImporteExento;
                    det.Total = perc.ImporteGravado;
                    det.Descripcion2 = "percepcion";
                    det.Unidad = perc.TipoPercepcion.ToString();
                    fact.ConceptosAduanera.Add(det);
                }
            }
            
            //fact.Fecha = DateTime.Now;
            fact.Fecha = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);
            if (ddlStatusRecibo.SelectedValue == "1" && !string.IsNullOrEmpty(txtFechaPago.Text))
            {
                fact.FechaPago = dto.FechaPago;
            }
            //fact.Vencimiento = DateTime.UtcNow;
            fact.Vencimiento = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);

            return fact;
        }

        private byte[] Preview()
        {
            bool error = false;
            var subTotal = decimal.Parse(this.lblTotalPercepciones.Text, NumberStyles.Currency);
            var detalles = new List<facturasdetalle>()
                                   {
                                       new facturasdetalle(){Cantidad = 1,Unidad = "Servicio", Descripcion =ddlConcepto.SelectedValue, Precio = subTotal}
                                   };
            var iniciales = Session["iniciales"] as string;//
            

            try
            {
                var clienteServicio = NtLinkClientFactory.Cliente();
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                clientes c = clienteServicio.ObtenerClienteById(idCliente);
                var empleado = clienteServicio.ObtenerDatosNomina(idCliente);
                var fact = GetFactura(iniciales, empleado,c);
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
            this.txtProyecto.Text = this.txtMetodoPago.Text = this.txtCuenta.Text = txtProyecto.Text =
                this.txtSerie.Text = string.Empty;
            this.ddlStatusRecibo.SelectedIndex = 0;
            this.txtFechaPago.Text = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow).ToString("yyyy-MM-dd");
            this.lblTotalDeducciones.Text = 0M.ToString("C");
            this.lblTotalPercepciones.Text = 0M.ToString("C");
            this.lblTotal.Text = 0M.ToString("C");
            this.ddlMetodoPago.SelectedIndex = 0;
            this.ddlMetodoPago_SelectedIndexChanged(null, null);
            var detalles = new List<facturasdetalle>();
            ViewState["percepciones"] = new List<Percepcion>();

            
            ViewState["deducciones"] = new List<Deduccion>();
            
            ViewState["detalles"] = detalles;
            this.BindDetallesToGridView();
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
            var deducciones = ViewState["deducciones"] as List<Deduccion>;
            if (deducciones == null)
            {
                deducciones = new List<Deduccion>();
            }
            gvPercepciones.DataSource = percepciones;
            gvPercepciones.DataBind();
            GvDeducciones.DataSource = deducciones;
            GvDeducciones.DataBind();
        }

        private void UpdateTotales()
        {
            if (!string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
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
                var total = 0M;
                foreach (Percepcion detalle in percepciones)
                {
                    per += detalle.ImporteExento + detalle.ImporteGravado;
                    total += detalle.ImporteExento + detalle.ImporteGravado;

                }
                foreach (Deduccion detalle in deducciones)
                {
                    ded += detalle.ImporteExento + detalle.ImporteGravado;
                }
                lblTotal.Text = (total - ded).ToString("C", cul);
                lblTotalDeducciones.Text = ded.ToString("C", cul);
                lblTotalPercepciones.Text = per.ToString("C", cul);

            }
            
        }

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
                this.txtCuenta.Text = c.CuentaPago;
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

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMetodoPago.SelectedValue == "99")
            {
                txtMetodoPago.Visible = true;
                lblMetodoPago.Text = "Escriba el método de pago";
                lblMetodoPago.Visible = true;

            }
            else
            {
                txtMetodoPago.Visible = false;
                lblMetodoPago.Visible = false;
            }
        }

        //protected void ddlCondicionesPago_SelectedIndexChanged(object sender, EventArgs e)
        //{
           

        //}

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        protected void btnAgregarPercepcion_Click(object sender, EventArgs e)
        {
            lblAgregar.Text = "Agregar Percepción";
            trPercepcion.Visible = true;
            tdDeduccion.Visible = false;
            txtImporteExcento.Text = "";
            txtImporteGravado.Text = "";
            
            mpeBuscarConcepto.Show();

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lblAgregar.Text == "Agregar Percepción")
            {
                var percepciones = ViewState["percepciones"] as List<Percepcion>;
                if (percepciones == null)
                {
                    percepciones = new List<Percepcion>();
                }
                Percepcion percepcion = new Percepcion();
                decimal importeGravado = 0;
                decimal importeExcento = 0;
                int tipoPercepcion = 0;
                if(!decimal.TryParse(txtImporteExcento.Text, out importeExcento))
                {
                    this.lblErrorPercepcion.Text = "El importe excento está mal escrito";
                    return;
                }
                if (!decimal.TryParse(txtImporteGravado.Text, out importeGravado))
                {
                    this.lblErrorPercepcion.Text = "El importe gravado está mal escrito";
                    return;
                }

                //percepciones.perc = deducciones.Count + 1;
                percepcion.ImporteExento = importeExcento;
                percepcion.ImporteGravado = importeGravado;
                percepcion.Clave = ddlPercepcion.SelectedValue.ToString();
                percepcion.TipoPercepcion = int.Parse(percepcion.Clave);
                percepcion.Concepto = ddlPercepcion.SelectedItem.ToString();
                percepciones.Add(percepcion);
                ViewState["percepciones"] = percepciones;
               
            }
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

                //deduccion.DeduccionID = deducciones.Count + 1;
                deduccion.ImporteExento = importeExcento;
                deduccion.ImporteGravado = importeGravado;
                deduccion.Clave = ddlDeduccion.SelectedValue.ToString();
                deduccion.TipoDeduccion = int.Parse(deduccion.Clave);
                deduccion.Concepto = ddlDeduccion.SelectedItem.ToString();
                deducciones.Add(deduccion);
                ViewState["deducciones"] = deducciones;
                
            }
            BindDetallesToGridView();
            UpdateTotales();

            txtImporteGravado.Text = "";
            txtImporteExcento.Text = "";

            mpeBuscarConcepto.Hide();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblAgregar.Text = "Agregar Deducción";
            trPercepcion.Visible = false;
            tdDeduccion.Visible = true;
            txtImporteExcento.Text = "";
            txtImporteGravado.Text = "";
            mpeBuscarConcepto.Show();
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

                    this.lblLugarExpedicion.Text = ct.Municipio + " " + ct.Estado;
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
                    percepciones.RemoveAt(id);
                    ViewState["percepciones"] = percepciones;
                    BindDetallesToGridView();
                    UpdateTotales();
                }
//                
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
                    BindDetallesToGridView();
                    UpdateTotales();
                }             
            }
        }

        protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        protected void ddlStatusRecibo_SelectedIndexChanged(object sender, EventArgs e)
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

    }
}