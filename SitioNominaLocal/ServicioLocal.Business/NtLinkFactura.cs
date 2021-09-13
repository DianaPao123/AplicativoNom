using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using ServicioLocal.Business.ReportExecution;
using ServicioLocalContract;

namespace ServicioLocal.Business
{
    public class NtLinkFactura : NtLinkBusiness
    {
        public string Uuid { get; set; }
        private facturas _factura;
        public Comprobante Cfdi { get; set; }


        public List<facturasdetalle> Detalles { get; set; }

        public facturas Factura
        {
            get { return _factura; }
            set { _factura = value; }
        }

        public clientes Receptor { get; set; }

        public empresa Emisor { get; set; }



        public static byte[] GetXmlData(string uuid)
        {
            return GetData(uuid, ".xml");
        }

        public static byte[] GetPdfData(string uuid)
        {
            return GetData(uuid, ".pdf");
        }



        public static byte[] GetData(string uuid, string tipo)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var venta = db.facturas.Where(p => p.Uid == uuid).FirstOrDefault();
                    
                    if (venta == null)
                    {
                        Logger.Error("No se encontró la factura: " + uuid);
                        return null;
                    }
                    var empresa = db.empresa.Where(p => p.IdEmpresa == venta.IdEmpresa).FirstOrDefault();
                    if (empresa == null)
                    {
                        Logger.Error("No se encontró la factura: " + uuid);
                        return null;
                    }

                    string ruta = BlobUtils.CreaRutas(empresa.RFC);
                    string xmlFile = ruta + "/Facturas_" + uuid + tipo;
                    byte[] res = BlobUtils.DownloadFile(ruta, xmlFile);
                    if (res != null)
                    {
                        return res;
                    }
                    else
                    {
                        Logger.Error("No se encontró la factura: " + uuid);
                        return null;
                    }

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }


        public static Comprobante GeneraCfd(NtLinkFactura factura, bool enviar)
        {
            try
            {
                
                Logger.Debug(factura.Emisor.RFC);
                empresa emp = factura.Emisor;
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    Logger.Error("El rfc es erróneo " + cliente.RFC);
                    throw new ApplicationException("El rfc es erróneo");
                }
                // string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC, "Certs");
                byte[] certBytes = BlobUtils.GetCer(emp.RFC);
                X509Certificate2 cert = new X509Certificate2(certBytes);
                byte[] llave = BlobUtils.GetKey(emp.RFC, emp.RutaKey);
                var comprobante = GetComprobante(factura, cliente, emp);
                GeneradorCfdi gen = new GeneradorCfdi();
                comprobante.articulos = factura.Detalles;
                gen.GenerarCfd(comprobante, cert, llave, emp.PassKey, emp.RutaKey);

                //string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], emp.RFC);
                //if (!Directory.Exists(ruta))
                //    Directory.CreateDirectory(ruta);
                var ruta = BlobUtils.CreaRutas(emp.RFC);
                //string xmlFile = Path.Combine(ruta, comprobante.Complemento.timbreFiscalDigital.UUID + ".xml");
                string xmlFile = ruta + "/Facturas_" + comprobante.Complemento.timbreFiscalDigital.UUID + ".xml";
                
                Logger.Debug(comprobante.XmlString);
                BlobUtils.UploadBlob(emp.RFC,xmlFile,Encoding.UTF8.GetBytes(comprobante.XmlString));
                byte[] pdf = new byte[0];

                try
                {
                    pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento,factura._factura.Metodo);
                    string pdfFile = ruta + "/Facturas_" + comprobante.Complemento.timbreFiscalDigital.UUID + ".pdf";
                    BlobUtils.UploadBlob(emp.RFC,pdfFile,pdf);
                }
                catch (Exception ee)
                {
                    Logger.Error(ee);
                    if (ee.InnerException != null)
                        Logger.Error(ee.InnerException);
                }
                if (enviar)
                {
                    try
                    {
                        Logger.Debug("Enviar Correo");
                        byte[] xmlBytes = Encoding.UTF8.GetBytes(comprobante.XmlString);
                        var atts = new List<EmailAttachment>();
                        atts.Add(new EmailAttachment
                                     {
                                         Attachment = xmlBytes,
                                         Name = comprobante.Complemento.timbreFiscalDigital.UUID + ".xml"
                                     });
                        atts.Add(new EmailAttachment
                                     {
                                         Attachment = pdf,
                                         Name = comprobante.Complemento.timbreFiscalDigital.UUID + ".pdf"
                                     });
                        Mailer m = new Mailer();
                        if (factura.Receptor.Bcc != null)
                            m.Bcc = factura.Receptor.Bcc;
                        List<string> emails = new List<string>();
                        emails.Add(cliente.Email);

                        m.Send(emails, atts,
                               "Se envía el recibo de nómina con folio " + comprobante.Complemento.timbreFiscalDigital.UUID +
                               " y su representación visual.",
                               "Envío de Recibo de Nómina", emp.Email, emp.RazonSocial);
                    }
                    catch (Exception ee)
                    {
                        Logger.Error(ee.Message);
                        if (ee.InnerException != null)
                            Logger.Error(ee.InnerException);
                    }
                }
                return comprobante;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }
        }



        public void EnviarFactura(string rfc, string folioFiscal, List<string> rec, List<string> bcc)
        {
            string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], rfc);
            string pdfFile = Path.Combine(ruta, folioFiscal + ".pdf");
            string xmlFile = Path.Combine(ruta, folioFiscal + ".xml");

            if (File.Exists(pdfFile) && File.Exists(xmlFile))
            {
                try
                {
                    using (var db = new NtLinkLocalServiceEntities())
                    {
                        var venta = db.facturas.FirstOrDefault(p => p.Uid == folioFiscal);
                        if (venta == null)
                        {
                            throw new FaultException("No se encontró la factura");
                        }
                        var emp = db.empresa.FirstOrDefault(e => e.IdEmpresa == venta.IdEmpresa);
                        Logger.Debug("Enviar Correo");
                        byte[] xmlBytes = File.ReadAllBytes(xmlFile);
                        var atts = new List<EmailAttachment>();
                        atts.Add(new EmailAttachment
                        {
                            Attachment = xmlBytes,
                            Name = Path.GetFileName(xmlFile)
                        });
                        atts.Add(new EmailAttachment
                        {
                            Attachment = File.ReadAllBytes(pdfFile),
                            Name = Path.GetFileName(pdfFile)
                        });
                        Mailer m = new Mailer();
                        if (bcc != null && bcc.Count > 0)
                        {
                            m.Bcc = bcc[0];
                        }

                        m.Send(rec, atts,
                               "Se envía el recibo de nómina con folio " + folioFiscal +
                               " y su representación visual.",
                               "Envío de Recibo de Nómina", emp.Email, emp.RazonSocial);
                    }

                }
                catch (FaultException ee)
                {
                    Logger.Error(ee + folioFiscal + " " + rfc);
                    throw;

                }
                catch (Exception ee)
                {
                    Logger.Error(ee.Message);
                    if (ee.InnerException != null)
                        Logger.Error(ee.InnerException);
                }
            }
            else
            {
                throw new FaultException("No se encontró la factura");
            }

        }


        private static Comprobante GetComprobante(NtLinkFactura factura, clientes cliente, empresa emp)
        {
            Logger.Debug(factura.Factura.Folio);
            Comprobante comprobante = new Comprobante();
            comprobante.Emisor = new ComprobanteEmisor();
            comprobante.Emisor.nombre = emp.RazonSocial;
            comprobante.Emisor.RegimenFiscal = new[]
                                                   {
                                                       new ComprobanteEmisorRegimenFiscal
                                                           {Regimen = factura.Factura.Regimen}
                                                   };
            comprobante.Emisor.rfc = emp.RFC;
            comprobante.Emisor.DomicilioFiscal = new t_UbicacionFiscal();
            comprobante.Emisor.DomicilioFiscal.calle = emp.Calle;
            comprobante.Emisor.DomicilioFiscal.noExterior = emp.NoExt;
            comprobante.Emisor.DomicilioFiscal.noInterior= emp.NoInt;
            comprobante.Emisor.DomicilioFiscal.colonia = emp.Colonia;
            comprobante.Emisor.DomicilioFiscal.codigoPostal = emp.CP;
            comprobante.Emisor.DomicilioFiscal.municipio = emp.Ciudad;
            comprobante.Emisor.DomicilioFiscal.pais = "México";
            comprobante.Emisor.DomicilioFiscal.estado = emp.Estado;
            comprobante.Titulo = factura.Factura.Titulo;
            comprobante.tipoDeComprobante = ComprobanteTipoDeComprobante.egreso;
            comprobante.Receptor = new ComprobanteReceptor();
            comprobante.Receptor.nombre = cliente.RazonSocial + " " + cliente.ApellidoPaterno + " " + cliente.ApellidoMaterno;
            comprobante.Receptor.rfc = cliente.RFC;
            comprobante.Receptor.Domicilio = new t_Ubicacion();
            comprobante.Receptor.Domicilio.pais = cliente.Pais;
            comprobante.Receptor.Domicilio.calle = cliente.Calle;
            comprobante.Receptor.Domicilio.municipio = cliente.Ciudad;
            comprobante.Receptor.Domicilio.estado = cliente.Estado;
            comprobante.Receptor.Domicilio.colonia = cliente.Colonia;
            comprobante.Receptor.Domicilio.codigoPostal = cliente.CP;
            comprobante.Receptor.Domicilio.noExterior = cliente.NoExt;
            comprobante.Receptor.Domicilio.noInterior = cliente.NoInt;
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            comprobante.fecha = Convert.ToDateTime(factura.Factura.Fecha.ToString("s"));
            comprobante.total = Decimal.Round(factura.Factura.Total.Value, 6);
            if (factura.Factura.Folio != null)
            {
                factura.Factura.Folio = GetNextFolio(factura.Factura.IdEmpresa.Value);
            }
            comprobante.Leyenda = factura.Factura.Leyenda;
            comprobante.LeyendaInferior = emp.LeyendaInferior;
            comprobante.LeyendaSuperior = emp.LeyendaSuperior;
            comprobante.folio = factura.Factura.Folio;
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            comprobante.metodoDePago = factura.Factura.MetodoID;//nuevo
            comprobante.NumCtaPago = factura.Factura.Cuenta;
            var moneda = "MXN";
            if (factura.Factura.Moneda == 1)
                moneda = "MXN";
            if (factura.Factura.Moneda == 2)
                moneda = "USD";
            if (factura.Factura.Moneda == 3)
                moneda = "EUR";

            comprobante.Moneda = moneda;
            comprobante.Regimen = comprobante.Emisor.RegimenFiscal[0].Regimen;
            comprobante.subTotal = Decimal.Round(factura.Factura.SubTotal.Value, 6);// factura.Factura.Total.Value - factura.Factura.IVA.Value + factura.Factura.RetencionIva;
            comprobante.serie = factura.Factura.Serie;
            comprobante.formaDePago = factura.Factura.FormaPago;
            comprobante.VoBoNombre = factura.Factura.VoBoNombre;
            comprobante.VoBoPuesto = factura.Factura.VoBoPuesto;
            comprobante.VoBoArea = factura.Factura.VoBoArea;
            comprobante.AutorizoNombre = factura.Factura.AutorizoNombre;
            comprobante.AutorizoPuesto = factura.Factura.AutorizoPuesto;
            comprobante.AutorizoArea = factura.Factura.AutorizoArea;
            comprobante.RecibiNombre = factura.Factura.RecibiNombre;
            comprobante.RecibiPuesto = factura.Factura.RecibiPuesto;
            comprobante.RecibiArea = factura.Factura.RecibiArea;
            comprobante.VoBoTitulo = factura.Factura.VoBoTitulo;
            comprobante.RecibiTitulo = factura.Factura.RecibiTitulo;
            comprobante.AutorizoTitulo = factura.Factura.AutorizoTitulo;
            comprobante.AgregadoArea = factura.Factura.AgregadoArea;
            comprobante.AgregadoNombre = factura.Factura.AgregadoNombre;
            comprobante.AgregadoPuesto = factura.Factura.AgregadoPuesto;
            comprobante.AgregadoTitulo = factura.Factura.AgregadoTitulo;
            comprobante.condicionesDePago = factura.Factura.FormaPago;
            comprobante.FechaPago = factura.Factura.FechaPago;
            comprobante.Proyecto = factura.Factura.Proyecto;//campo nuevo
            comprobante.CURPEmisor = emp.CURP;
            comprobante.TituloOtros = factura.Factura.TituloOtros;

            DateTime MinDateTime = DateTime.ParseExact("1800/01/01", "yyyy/MM/dd", new CultureInfo("en-US"));
            if (factura.Factura.TipoDocumento == TipoDocumento.Nomina)
            {
                var dto = factura.Factura.Nomina;
                if (dto != null)
                {
                    Nomina nomina = new Nomina()
                    {
                        Banco = dto.Banco,
                        CLABE = dto.CLABE,
                        Departamento = dto.Departamento,
                        FechaInicialPago = dto.FechaInicialPago,

                        NumEmpleado = dto.NumEmpleado,
                        NumSeguridadSocial = dto.NumSeguridadSocial,
                        Puesto = dto.Puesto,
                        PeriodicidadPago = dto.PeriodicidadPago,
                        TipoRegimen = dto.TipoRegimen,
                        RiesgoPuesto = dto.RiesgoPuesto,

                        //SalarioBaseCotApor = dto.SalarioBaseCotApor,
                        SalarioBaseCotAporSpecified = dto.SalarioBaseCotApor == -1 ? false : true,
                        SalarioBaseCotApor = dto.SalarioBaseCotApor == -1 ? 0 : dto.SalarioBaseCotApor,
                        
                        //SalarioDiarioIntegrado = dto.SalarioDiarioIntegrado,
                        SalarioDiarioIntegradoSpecified = dto.SalarioDiarioIntegrado == -1 ? false : true,
                        SalarioDiarioIntegrado = dto.SalarioDiarioIntegrado == -1 ? 0 : dto.SalarioDiarioIntegrado,

                        TipoContrato = dto.TipoContrato,
                        TipoJornada = dto.TipoJornada,
                        
                        //Antiguedad = dto.Antiguedad,
                        Antiguedad = dto.Antiguedad,
                        //FechaInicioRelLaboralSpecified = dto.FechaInicioRelLaboral == MinDateTime ? false : true,
                        //AntiguedadSpecified = dto.FechaInicioRelLaboral == MinDateTime ? false : true,

    

                        CURP = dto.CURP,
                        FechaPago = dto.FechaPago,
                        FechaFinalPago = dto.FechaFinalPago,

                        NumDiasPagados = dto.NumDiasPagados,

                        RegistroPatronal = string.IsNullOrEmpty(dto.RegistroPatronal) ? null : dto.RegistroPatronal
                        //RegistroPatronal = "Hola"
                    };
                    nomina.BancoSpecified = nomina.Banco != "0";
                    if (dto.Percepciones != null && dto.Percepciones.Percepcion != null &&
                        dto.Percepciones.Percepcion.Count > 0)
                    {
                        var percepciones = dto.Percepciones.Percepcion.Select(p => new NominaPercepcionesPercepcion
                        {
                            Clave = p.Clave,
                            Concepto = p.Concepto,
                            ImporteExento =
                                p.ImporteExento,
                            ImporteGravado =
                                p.ImporteGravado,
                            TipoPercepcion =
                                p.TipoPercepcion.ToString().PadLeft(3,'0')
                        }).ToList();


                        NominaPercepciones per = new NominaPercepciones()
                        {
                            Percepcion = percepciones.ToArray(),
                            TotalExento = dto.Percepciones.TotalExento,
                            TotalGravado = dto.Percepciones.TotalGravado
                        };
                        nomina.Percepciones = per;
                    }
                    if (dto.Deducciones != null && dto.Deducciones.Deduccion != null &&
                        dto.Deducciones.Deduccion.Count > 0)
                    {
                        var deducciones = dto.Deducciones.Deduccion.Select(p => new NominaDeduccionesDeduccion()
                        {
                            Clave = p.Clave,
                            Concepto = p.Concepto,
                            ImporteExento = p.ImporteExento,
                            ImporteGravado =
                                p.ImporteGravado,
                            TipoDeduccion = p.TipoDeduccion.ToString().PadLeft(3,'0')
                        }).ToList();

                        NominaDeducciones ded = new NominaDeducciones()
                        {
                            Deduccion = deducciones.ToArray(),
                            TotalExento = dto.Deducciones.TotalExento,
                            TotalGravado = dto.Deducciones.TotalGravado
                        };


                        nomina.Deducciones = ded;
                    }
                    comprobante.Nomina = nomina;
                   // comprobante.Titulo = "Recibo de Nomina";

                }

            }


            if (factura.Factura.TipoDocumento == TipoDocumento.Amc71)
            {
                comprobante.AddendaAmece = factura.Factura.AddendaAmece;
            }
            if (factura.Factura.TipoDocumento == TipoDocumento.HomeDepot)
            {
                comprobante.AddendaHomeDepot = factura.Factura.AddendaHomeDepot;
            }



            List<ComprobanteConcepto> conceptos = new List<ComprobanteConcepto>();
            foreach (facturasdetalle detalle in factura.Detalles)
            {
                ComprobanteConcepto con = new ComprobanteConcepto();
                con.descripcion = detalle.Descripcion;
                if (!string.IsNullOrEmpty(detalle.Codigo))
                    con.noIdentificacion = detalle.Codigo;
                con.Detalles = detalle.Descripcion2;
                con.cantidad = detalle.Cantidad;
                con.valorUnitario = detalle.Precio;
                con.importe = Decimal.Round(detalle.TotalPartida, 6);
                con.unidad = detalle.Unidad;
                con.OrdenCompra = detalle.OrdenCompra;

                if (!string.IsNullOrEmpty(detalle.CuentaPredial))
                {
                    con.CuentaPredial = detalle.CuentaPredial;
                    var predial = new ComprobanteConceptoCuentaPredial
                        {
                            numero = detalle.CuentaPredial
                        };
                    con.Items = new object[] { predial };
                }
                conceptos.Add(con);
            }
            comprobante.Conceptos = conceptos.ToArray();
           

            ComprobanteImpuestos impuestos = new ComprobanteImpuestos();

            impuestos.totalImpuestosTrasladados = 0;//Decimal.Round(factura.Factura.IVA.Value, 6);
            impuestos.totalImpuestosTrasladadosSpecified = true;
            //13/06/2014 Jorge
            if (comprobante.Nomina != null && comprobante.Nomina.Deducciones != null &&
                comprobante.Nomina.Deducciones.Deduccion != null)
            {
                var descuento =
                    comprobante.Nomina.Deducciones.Deduccion.Where(p => p.TipoDeduccion != "002")
                        .Sum(j => (j.ImporteExento + j.ImporteGravado));
                var retencionIsr = comprobante.Nomina.Deducciones.Deduccion.Where(p => p.TipoDeduccion == "002")
                        .Sum(j => (j.ImporteExento + j.ImporteGravado));
                if (descuento > 0)
                {
                    comprobante.descuento = descuento;
                    comprobante.descuentoSpecified = true;
                    comprobante.motivoDescuento = "Deducciones nomina";
                }
                if (retencionIsr > 0)
                {
                    ComprobanteImpuestosRetencion ret = new ComprobanteImpuestosRetencion();
                    ret.impuesto = ComprobanteImpuestosRetencionImpuesto.ISR;
                    ret.importe = retencionIsr;
                    impuestos.Retenciones = new[] {ret};
                    impuestos.totalImpuestosRetenidos = retencionIsr;
                    impuestos.totalImpuestosRetenidosSpecified = true;

                }
                
            }



            comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.total.ToString(), comprobante.Moneda);
           

            
            if (factura.Factura.TipoDocumento == TipoDocumento.Nomina)
            {
                comprobante.DatosAduana = factura.Factura.DatosAduanera;
                comprobante.ConceptosAduana = factura.Factura.ConceptosAduanera
                    .Select(p => new ComprobanteConcepto
                    {
                        unidad = p.Unidad.ToString().PadLeft(3, '0'),
                        descripcion = p.Descripcion,
                        valorUnitario = p.Precio,
                        importe = p.Total,
                        Detalles = p.Descripcion2
                    }).ToList();

            }
            comprobante.Impuestos = impuestos;
            return comprobante;
        }

        
        public static byte[] GeneraPreviewRs(NtLinkFactura factura)
        {
            try
            {
                empresa emp = factura.Emisor;
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    throw new ApplicationException("El rfc es erróneo");
                }
                //string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC, "Certs");
                //X509Certificate2 cert = new X509Certificate2(Path.Combine(path, "csd.cer"));
                //string rutaLlave = Path.Combine(path, "csd.key");

                var comprobante = GetComprobante(factura, cliente, emp);
                GeneradorCfdi gen = new GeneradorCfdi();
                gen.GenerarCfdPreview(comprobante);
                //comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.total.ToString(), comprobante.Moneda);
                byte[] pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento,factura._factura.Metodo);
                return pdf;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }

        }



        public static byte[] GeneraPreview(NtLinkFactura factura)
        {
            try
            {
                empresa emp = factura.Emisor;
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    throw new ApplicationException("El rfc es erróneo");
                }
               
                var comprobante = GetComprobante(factura, cliente, emp);
                GeneradorCfdi gen = new GeneradorCfdi();
                gen.GenerarCfdPreview(comprobante);
               

                //comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.total.ToString(), comprobante.Moneda);
                byte[] pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento,factura._factura.Metodo);
                return pdf;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }

        }



        public static string GetNextFolio(int idEmpresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    string folio = db.facturas.Where(p => p.IdEmpresa == idEmpresa).Max(p => p.Folio);
                    int i = 0;
                    if (folio != null)
                    {
                        i = int.Parse(folio);
                    }
                    i++;
                    return i.ToString().PadLeft(4, '0');
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }


        private bool ValidaFolio(string folio, int idEmpresa)
        {
            if (string.IsNullOrEmpty(folio))
                throw new ApplicationException("El folio de la factura no puede ir vacío");
            return true;

        }



        public static byte[] GetAcuseCancelacion(string report, int idVenta)
        {
            try
            {
                Logger.Debug(report + "->" + idVenta);
                ReportExecutionService rs = new ReportExecutionService();
                string userName = ConfigurationManager.AppSettings["RSUser"];
                string password = ConfigurationManager.AppSettings["RSPass"];
                string url = ConfigurationManager.AppSettings["RSUrlExec"];

                //string userName = RoleEnvironment.GetConfigurationSettingValue("RSUserName");
                //string password = RoleEnvironment.GetConfigurationSettingValue("RSPass");
                //string url = RoleEnvironment.GetConfigurationSettingValue("RSUrlExec");
                rs.Credentials = new NetworkCredential(userName, password);
                rs.Url = url;
                string reportPath = report;//"/ReportesNtLink/Pdf";
                string format = "Pdf";
                string devInfo = @"<DeviceInfo> <OutputFormat>PDF</OutputFormat> </DeviceInfo>";
                ParameterValue[] parameters = new ParameterValue[1];

                parameters[0] = new ParameterValue();
                parameters[0].Name = "idVenta";
                parameters[0].Value = idVenta.ToString();

                ExecutionHeader execHeader = new ExecutionHeader();
                rs.Timeout = 300000;
                rs.ExecutionHeaderValue = execHeader;
                rs.LoadReport(reportPath, null);
                rs.SetExecutionParameters(parameters, "en-US");


                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streamIDs;
                var res = rs.Render(format, devInfo, out fileNameExtension, out mimeType, out encoding, out warnings, out streamIDs);
                Logger.Debug(res.Length);
                return res;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }




        public void CancelarFactura(string uuid, string acuse)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        AcuseCancelacion ac = AcuseCancelacion.Parse(acuse);
                        fact.Observaciones = acuse;
                        fact.Cancelado = 1;
                        fact.EstatusCancelacion = ac.Status;
                        fact.FechaCancelacion = ac.FechaCancelacion;
                        fact.SelloCancelacion = ac.SelloSat;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }

        public NtLinkFactura(int idFactura)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idFactura == 0)
                    {
                        this.Factura = new facturas();
                        this.Detalles = new List<facturasdetalle>();
                    }
                    else
                    {
                        this.Factura = db.facturas.Where(p => p.idVenta == idFactura).FirstOrDefault();
                        if (Factura == null)
                        {
                            throw new ApplicationException("La factura " + idFactura.ToString() + " No se encontró");
                        }
                        this.Detalles = db.facturasdetalle.Where(p => p.idVenta == idFactura).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }

        }


        public bool Save()
        {
            try
            {
                _factura.RetIsr = _factura.RetencionIsr;
                _factura.RetIva = _factura.RetencionIva;
                if (_factura.DatosAduanera != null)
                    _factura.Ieps = _factura.DatosAduanera.IEPS;

                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (_factura.idVenta == 0 && ValidaFolio(_factura.Folio, _factura.IdEmpresa.Value))
                    {
                        db.facturas.AddObject(_factura);
                        var ee = db.Sistemas.FirstOrDefault(p => p.IdSistema == Emisor.idSistema);
                        if (ee.TimbresConsumidos == null)
                            ee.TimbresConsumidos = 0;
                        ee.TimbresConsumidos = ee.TimbresConsumidos + 1;
                                                
                        db.SaveChanges();
                    }
                    else
                    {
                        db.facturas.ApplyCurrentValues(_factura);
                    }
                    foreach (facturasdetalle detalle in Detalles)
                    {
                        if (detalle.idproducto == 0)
                        {
                            producto prod = new producto();
                            prod.Unidad = detalle.Unidad;
                            prod.Codigo = detalle.Codigo;
                            prod.Descripcion = detalle.Descripcion;
                            prod.Observaciones = detalle.Descripcion2;
                            prod.PrecioP = detalle.Precio;
                            prod.UltimaVenta = detalle.Precio;
                            prod.IdEmpresa = Factura.IdEmpresa;
                            var ntprod = new NtLinkProducto();
                            ntprod.SaveProducto(prod);
                            detalle.idproducto = prod.IdProducto;
                        }
                        else
                        {
                            producto prod = db.producto.Where(p => p.IdProducto == detalle.idproducto).FirstOrDefault();
                            prod.UltimaVenta = detalle.Precio;
                            //prod.Modificacion = DateTime.Now;
                            prod.Modificacion = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);


                            db.producto.ApplyCurrentValues(prod);
                        }

                        detalle.idVenta = _factura.idVenta;
                        if (detalle.IdFacturaDetalle == 0)
                            db.facturasdetalle.AddObject(detalle);
                        else
                            db.facturasdetalle.ApplyCurrentValues(detalle);


                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }
        }

        public static void Pagar(int idVenta, DateTime fechaPago, string referenciaPago)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    facturas fac = db.facturas.Single(l => l.idVenta == idVenta);
                    fac.FechaPago = fechaPago;
                    fac.ReferenciaPago = referenciaPago;
                    db.facturas.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
    }
}
