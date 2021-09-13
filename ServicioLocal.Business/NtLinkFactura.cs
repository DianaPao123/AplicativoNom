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
            comprobante.Emisor.Nombre = emp.RazonSocial;
            comprobante.Emisor.RegimenFiscal = factura.Factura.Regimen;
           
            comprobante.Emisor.Rfc = emp.RFC;
            if (comprobante.Emisor.Rfc.Length>12)
            comprobante.CURPEmisor = emp.CURP;
         
            comprobante.Titulo = factura.Factura.Titulo;
            comprobante.TipoDeComprobante = "N";
            comprobante.Receptor = new ComprobanteReceptor();
            comprobante.Receptor.Nombre = cliente.RazonSocial + " " + cliente.ApellidoPaterno + " " + cliente.ApellidoMaterno;
            comprobante.Receptor.Rfc = cliente.RFC;
            comprobante.Receptor.UsoCFDI = "G03";
           
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            comprobante.Fecha =factura.Factura.Fecha.ToString("s");
            comprobante.Total = Decimal.Round(factura.Factura.Total.Value, 6);
            if (factura.Factura.Folio != null)
            {
                factura.Factura.Folio = GetNextFolio(factura.Factura.IdEmpresa.Value);
            }
            comprobante.Leyenda = factura.Factura.Leyenda;
            comprobante.LeyendaInferior = emp.LeyendaInferior;
            comprobante.LeyendaSuperior = emp.LeyendaSuperior;
            comprobante.Folio = factura.Factura.Folio;
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            
            if (!string.IsNullOrEmpty(factura._factura.MetodoID))
            {

                comprobante.MetodoPagoSpecified = true;
                comprobante.MetodoPago = factura._factura.MetodoID;
            }
            else

                comprobante.MetodoPagoSpecified = false;
        //    comprobante.NumCtaPago = factura.Factura.Cuenta;
            var moneda = "MXN";
            if (factura.Factura.Moneda == 1)
                moneda = "MXN";
            if (factura.Factura.Moneda == 2)
                moneda = "USD";
            if (factura.Factura.Moneda == 3)
                moneda = "EUR";

            comprobante.Moneda = moneda;
            comprobante.Regimen = comprobante.Emisor.RegimenFiscal;
            comprobante.SubTotal = Decimal.Round(factura.Factura.SubTotal.Value, 6);// factura.Factura.Total.Value - factura.Factura.IVA.Value + factura.Factura.RetencionIva;
            comprobante.Serie = factura.Factura.Serie;
            if (!string.IsNullOrEmpty(factura._factura.FormaPago))
            {
                comprobante.FormaPagoSpecified = true;
                comprobante.FormaPago = factura.Factura.FormaPago;
            }
            else
                comprobante.FormaPagoSpecified = false;
           // comprobante.FormaDePago = factura.Factura.FormaPago;
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
           // comprobante.condicionesDePago = factura.Factura.FormaPago;
            comprobante.FechaPago = factura.Factura.FechaPago;
            comprobante.Proyecto = factura.Factura.Proyecto;//campo nuevo
            
            comprobante.TituloOtros = factura.Factura.TituloOtros;

            if (!string.IsNullOrEmpty(factura._factura.Descuento))///valida si existe descuento suma de deducciones
            {
                if (Convert.ToDecimal(factura._factura.Descuento) > 0)
                {
                    comprobante.Descuento = Convert.ToDecimal(factura._factura.Descuento);
                    comprobante.DescuentoSpecified = true;
                }
            }
            if (!string.IsNullOrEmpty(factura.Factura.TipoRelacion))
            {
                comprobante.CfdiRelacionados = new ComprobanteCfdiRelacionados();
                comprobante.CfdiRelacionados.CfdiRelacionado = new List<ComprobanteCfdiRelacionadosCfdiRelacionado>();
                comprobante.CfdiRelacionados.TipoRelacion = factura.Factura.TipoRelacion;
                List<ComprobanteCfdiRelacionadosCfdiRelacionado> UUDI = new List<ComprobanteCfdiRelacionadosCfdiRelacionado>();
                foreach (string uudi in factura.Factura.UUID)
                {
                    UUDI.Add(new ComprobanteCfdiRelacionadosCfdiRelacionado
                    {
                        UUID = uudi
                    });
                }
                comprobante.CfdiRelacionados.CfdiRelacionado = UUDI;
            }
	
            DateTime MinDateTime = DateTime.ParseExact("1800/01/01", "yyyy/MM/dd", new CultureInfo("en-US"));
            //-------------------------------------------NOMINA--------------------------------------------------------
            if (factura.Factura.TipoDocumento == TipoDocumento.Nomina)
            {
                var dto = factura.Factura.Nomina;
                if (dto != null)
                {
                    Nomina nomina = new Nomina();
                    
                    nomina.Version = "1.2";
                    if (dto.TipoNomina == "E")
                        nomina.TipoNomina = c_TipoNomina.E;
                    else
                        nomina.TipoNomina = c_TipoNomina.O;
                    nomina.FechaPago = dto.FechaPago;
                    nomina.FechaInicialPago = dto.FechaInicialPago;
                    nomina.FechaFinalPago = dto.FechaFinalPago;
                    nomina.NumDiasPagados = dto.NumDiasPagados.ToString("F3");
                    if (dto.TotalPercepciones!=0)
                    {
                        nomina.TotalPercepcionesSpecified = true;
                        nomina.TotalPercepciones = dto.TotalPercepciones;
                    }
                    else
                        nomina.TotalPercepcionesSpecified = false;
                    if (dto.TotalDeducciones!=0)
                    {
                        nomina.TotalDeduccionesSpecified = true;
                        nomina.TotalDeducciones = dto.TotalDeducciones;
                    }
                    else
                        nomina.TotalDeduccionesSpecified = false;

                    if (dto.TotalOtrosPagos!=0)
                    {
                        nomina.TotalOtrosPagosSpecified = true;
                        nomina.TotalOtrosPagos = dto.TotalOtrosPagos;
                    }
                    else
                        nomina.TotalOtrosPagosSpecified = false;
                    //------------------------------------EMISOR
                    if (dto.emisor != null)
                    {
                        if (dto.emisor.Curp == null && dto.emisor.MontoRecursoPropio == 0 && dto.emisor.OrigenRecurso == null && dto.emisor.RegistroPatronal == null && dto.emisor.RfcPatronOrigen == null)
                        { var no = true; }
                        else
                        {
                            nomina.Emisor = new NominaEmisor();
                            if (!string.IsNullOrEmpty(dto.emisor.Curp))
                                nomina.Emisor.Curp = dto.emisor.Curp;
                            if (!string.IsNullOrEmpty(dto.emisor.RegistroPatronal))
                                nomina.Emisor.RegistroPatronal = dto.emisor.RegistroPatronal;
                            if (!string.IsNullOrEmpty(dto.emisor.RfcPatronOrigen))
                                nomina.Emisor.RfcPatronOrigen = dto.emisor.RfcPatronOrigen;

                            if (!string.IsNullOrEmpty(dto.emisor.OrigenRecurso))
                            {
                                nomina.Emisor.EntidadSNCF = new NominaEmisorEntidadSNCF();
                                if (dto.emisor.MontoRecursoPropio != 0)
                                {
                                    nomina.Emisor.EntidadSNCF.MontoRecursoPropio = dto.emisor.MontoRecursoPropio;
                                    nomina.Emisor.EntidadSNCF.MontoRecursoPropioSpecified = true;
                                }
                                else
                                    nomina.Emisor.EntidadSNCF.MontoRecursoPropioSpecified = false;
                                if (dto.emisor.OrigenRecurso == "IF")
                                    nomina.Emisor.EntidadSNCF.OrigenRecurso = c_OrigenRecurso.IF;
                                if (dto.emisor.OrigenRecurso == "IM")
                                    nomina.Emisor.EntidadSNCF.OrigenRecurso = c_OrigenRecurso.IM;
                                if (dto.emisor.OrigenRecurso == "IP")
                                    nomina.Emisor.EntidadSNCF.OrigenRecurso = c_OrigenRecurso.IP;

                            }
                            //-----------------------
                        }
                    }
                    //--------------------------------------EMISOR
                    //----------------------RECEPTOR---------------
                    nomina.Receptor = new NominaReceptor();
                    if (dto.receptor.subContratacion!=null)
                    {
                        List<NominaReceptorSubContratacion> SUB = new List<NominaReceptorSubContratacion>();
                        foreach (var s in dto.receptor.subContratacion)
                        {
                            NominaReceptorSubContratacion sub = new NominaReceptorSubContratacion();
                            sub.PorcentajeTiempo = s.PorcentajeTiempo.ToString("F3");
                            sub.RfcLabora =s.RfcLabora;
                            SUB.Add(sub);

                        }
                        nomina.Receptor.SubContratacion = SUB;
                    }
                    nomina.Receptor.Curp = dto.receptor.Curp;
                    if (!string.IsNullOrEmpty(dto.receptor.NumSeguridadSocial))
                        nomina.Receptor.NumSeguridadSocial = dto.receptor.NumSeguridadSocial;

                    if (dto.receptor.FechaInicioRelLaboral!=null)
                    {
                        nomina.Receptor.FechaInicioRelLaboralSpecified = true;
                        nomina.Receptor.FechaInicioRelLaboral = dto.receptor.FechaInicioRelLaboral;
                        nomina.Receptor.Antigüedad = dto.receptor.Antigüedad.ToString();
                    }
                    else
                        nomina.Receptor.FechaInicioRelLaboralSpecified = false;

                   

                    ///..........convert enum-----------------
                    c_TipoContrato myTipoContrato;
                    Enum.TryParse("Item" + dto.receptor.TipoContrato, out myTipoContrato);
                    nomina.Receptor.TipoContrato = myTipoContrato;
                    /*
                    if (dto.receptor.TipoContrato == "01")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item01;
                    if (dto.receptor.TipoContrato == "Item02")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item02;
                    if (dto.receptor.TipoContrato == "Item03")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item03;
                    if (dto.receptor.TipoContrato == "Item04")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item04;
                    if (dto.receptor.TipoContrato == "Item05")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item05;
                    if (dto.receptor.TipoContrato == "Item06")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item06;
                    if (dto.receptor.TipoContrato == "Item07")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item07;
                    if (dto.receptor.TipoContrato == "Item08")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item08;
                    if (dto.receptor.TipoContrato == "Item09")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item09;
                    if (dto.receptor.TipoContrato == "Item10")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item10;
                    if (dto.receptor.TipoContrato == "Item99")
                        nomina.Receptor.TipoContrato = c_TipoContrato.Item99;
                    */


                    if (!string.IsNullOrEmpty(dto.receptor.Sindicalizado))
                    {
                        nomina.Receptor.SindicalizadoSpecified = true;
                        if (dto.receptor.Sindicalizado == "Si")
                            nomina.Receptor.Sindicalizado = NominaReceptorSindicalizado.Sí;
                        else
                            nomina.Receptor.Sindicalizado = NominaReceptorSindicalizado.No;
                    }
                    else
                    {

                        nomina.Receptor.SindicalizadoSpecified = false;
                    }

                    if (!string.IsNullOrEmpty(dto.receptor.TipoJornada))
                    {
                        nomina.Receptor.TipoJornadaSpecified = true;
                        ///..........convert enum-----------------
                        c_TipoJornada myStatus;
                        //dto.receptor.TipoJornada = "08";
                        Enum.TryParse("Item"+ dto.receptor.TipoJornada, out myStatus);
                        //---------------------------------------------
                        nomina.Receptor.TipoJornada = myStatus;

                    }
                    else
                        nomina.Receptor.TipoJornadaSpecified = false;

                   ServicioLocal.Business.c_TipoRegimen myRegimen;
                   Enum.TryParse("Item" + dto.receptor.TipoRegimen, out myRegimen);
                    //--------------------------------------------
                    
                    nomina.Receptor.TipoRegimen = myRegimen;

                    nomina.Receptor.NumEmpleado = dto.receptor.NumEmpleado;
                    if (!string.IsNullOrEmpty(dto.receptor.Departamento))
                        nomina.Receptor.Departamento = dto.receptor.Departamento;
                    if (!string.IsNullOrEmpty(dto.receptor.Puesto))
                        nomina.Receptor.Puesto = dto.receptor.Puesto;

                    if (!string.IsNullOrEmpty(dto.receptor.RiesgoPuesto.ToString()))
                    {
                        c_RiesgoPuesto myRiesgoPuesto;
                        Enum.TryParse("Item" + dto.receptor.RiesgoPuesto.ToString(), out myRiesgoPuesto);

                        nomina.Receptor.RiesgoPuesto = myRiesgoPuesto;
                        nomina.Receptor.RiesgoPuestoSpecified = true;
                    }
                    else
                        nomina.Receptor.RiesgoPuestoSpecified = false;

                    c_PeriodicidadPago myPeriodicidadPago;
                    Enum.TryParse("Item" + dto.receptor.PeriodicidadPago.ToString(), out myPeriodicidadPago);

                    nomina.Receptor.PeriodicidadPago = myPeriodicidadPago;

                    if (!string.IsNullOrEmpty(dto.receptor.Banco))
                    {
                        c_Banco myBanco;
                        Enum.TryParse("Item" + dto.receptor.Banco.ToString(), out myBanco);
                        nomina.Receptor.BancoSpecified = true;
                        nomina.Receptor.Banco = myBanco;
                    }
                    else
                        nomina.Receptor.BancoSpecified = false;

                    if (!string.IsNullOrEmpty(dto.receptor.CuentaBancaria))
                        nomina.Receptor.CuentaBancaria = dto.receptor.CuentaBancaria;

                    if (dto.receptor.SalarioBaseCotApor!=0)
                    {
                        nomina.Receptor.SalarioBaseCotApor = dto.receptor.SalarioBaseCotApor == -1 ? 0 : dto.receptor.SalarioBaseCotApor;
                        nomina.Receptor.SalarioBaseCotAporSpecified = true;
                    }
                    else
                        nomina.Receptor.SalarioBaseCotAporSpecified = false;

                    if (dto.receptor.SalarioDiarioIntegrado!=0)
                    {
                        nomina.Receptor.SalarioDiarioIntegrado = dto.receptor.SalarioDiarioIntegrado == -1 ? 0.ToString("F") : dto.receptor.SalarioDiarioIntegrado.ToString("F");
                        nomina.Receptor.SalarioDiarioIntegradoSpecified = true;
                    }
                    else
                        nomina.Receptor.SalarioDiarioIntegradoSpecified = false;

                    c_Estado myEstado;
                    Enum.TryParse(dto.receptor.ClaveEntFed, out myEstado);
                    nomina.Receptor.ClaveEntFed = myEstado;

                    //---------------------FINRECEPTOR-------------
                    //---------------------percepciones---------------------
                    if (dto.Percepciones != null)
                    {
                        nomina.Percepciones = new NominaPercepciones();
                        //---
                        if (dto.Percepciones.percepcion != null)
                        {
                            nomina.Percepciones.Percepcion = new  List<NominaPercepcionesPercepcion>();
                            //int i = 0;
                            foreach (var p in dto.Percepciones.percepcion)
                            {
                                NominaPercepcionesPercepcion per = new NominaPercepcionesPercepcion();
                                if (p.ValorMercado != null)
                                {
                                    per.AccionesOTitulos = new NominaPercepcionesPercepcionAccionesOTitulos();
                                    per.AccionesOTitulos.ValorMercado = p.ValorMercado??0;
                                    per.AccionesOTitulos.PrecioAlOtorgarse = p.PrecioAlOtorgarse??0;

                                }
                                if (p.horasExtra != null)
                                {
                                    if (p.horasExtra.Count > 0)
                                    {
                                        per.HorasExtra = new List<NominaPercepcionesPercepcionHorasExtra>();
                                        List<NominaPercepcionesPercepcionHorasExtra> H = new List<NominaPercepcionesPercepcionHorasExtra>();

                                        foreach (var ho in p.horasExtra)
                                        {
                                            NominaPercepcionesPercepcionHorasExtra h = new NominaPercepcionesPercepcionHorasExtra();
                                            h.Dias = ho.Dias;
                                            h.HorasExtra = ho.HoraExtra;
                                            h.ImportePagado = ho.ImportePagado;
                                            c_TipoHoras myTipoHoras;
                                            Enum.TryParse("Item" + ho.TipoHoras, out myTipoHoras);
                                            h.TipoHoras = myTipoHoras;
                                            H.Add(h);
                                        }
                                        per.HorasExtra = H;
                                    }
                                }
                                c_TipoPercepcion myTipoPercepcion;
                                Enum.TryParse("Item" + p.TipoPercepcion, out myTipoPercepcion);
                                per.TipoPercepcion = myTipoPercepcion;

                                per.Clave = p.Clave;
                                per.Concepto = p.Concepto;
                                per.ImporteExento = p.ImporteExento.ToString("F");
                                per.ImporteGravado = p.ImporteGravado.ToString("F");

                                nomina.Percepciones.Percepcion.Add(per);

                            }
                        }
                        //----
                        
                        if (dto.Percepciones.IngresoNoAcumulable!= 0)
                        {
                            nomina.Percepciones.JubilacionPensionRetiro = new NominaPercepcionesJubilacionPensionRetiro();
                            nomina.Percepciones.JubilacionPensionRetiro.IngresoAcumulable = dto.Percepciones.IngresoAcumulable;
                            nomina.Percepciones.JubilacionPensionRetiro.IngresoNoAcumulable = dto.Percepciones.IngresoNoAcumulable;

                            if (dto.Percepciones.TotalUnaExhibicion!=0)
                            {
                                nomina.Percepciones.JubilacionPensionRetiro.TotalUnaExhibicion = dto.Percepciones.TotalUnaExhibicion;
                                nomina.Percepciones.JubilacionPensionRetiro.TotalUnaExhibicionSpecified = true;
                            }
                            else
                                nomina.Percepciones.JubilacionPensionRetiro.TotalUnaExhibicionSpecified = false;
                            if (dto.Percepciones.TotalParcialidad!=0)
                            {

                                nomina.Percepciones.JubilacionPensionRetiro.TotalParcialidad = dto.Percepciones.TotalParcialidad;
                                nomina.Percepciones.JubilacionPensionRetiro.TotalParcialidadSpecified = true;
                            }
                            else
                                nomina.Percepciones.JubilacionPensionRetiro.TotalParcialidadSpecified = false;
                            if (dto.Percepciones.MontoDiario!=0)
                            {

                                nomina.Percepciones.JubilacionPensionRetiro.MontoDiario = dto.Percepciones.MontoDiario;
                                nomina.Percepciones.JubilacionPensionRetiro.MontoDiarioSpecified = true;
                            }
                            else
                                nomina.Percepciones.JubilacionPensionRetiro.MontoDiarioSpecified = false;

                        }
                        if (dto.Percepciones.IngresoAcumulable!=0|| dto.Percepciones.IngresoNoAcumulable!=0||dto.Percepciones.TotalPagado!=0)
                        {
                            nomina.Percepciones.SeparacionIndemnizacion = new NominaPercepcionesSeparacionIndemnizacion();
                            nomina.Percepciones.SeparacionIndemnizacion.IngresoAcumulable = dto.Percepciones.SeparacionIndemnizacionIngresoAcumulable;
                            nomina.Percepciones.SeparacionIndemnizacion.IngresoNoAcumulable = dto.Percepciones.SeparacionIndemnizacionIngresoNoAcumulable;
                            nomina.Percepciones.SeparacionIndemnizacion.NumAñosServicio = dto.Percepciones.NumAñosServicio;
                            nomina.Percepciones.SeparacionIndemnizacion.TotalPagado = dto.Percepciones.TotalPagado;
                            nomina.Percepciones.SeparacionIndemnizacion.UltimoSueldoMensOrd = dto.Percepciones.UltimoSueldoMensOrd;
                        }

                        if (dto.Percepciones.TotalSueldos!=0)
                        {
                            nomina.Percepciones.TotalSueldos = dto.Percepciones.TotalSueldos;
                            nomina.Percepciones.TotalSueldosSpecified = true;
                        }
                        else
                            nomina.Percepciones.TotalSueldosSpecified = false;
                        if (dto.Percepciones.TotalSeparacionIndemnizacion!=0)
                        {

                            nomina.Percepciones.TotalSeparacionIndemnizacion = dto.Percepciones.TotalSeparacionIndemnizacion;
                            nomina.Percepciones.TotalSeparacionIndemnizacionSpecified = true;
                        }
                        else
                            nomina.Percepciones.TotalSeparacionIndemnizacionSpecified = false;
                        if (dto.Percepciones.TotalJubilacionPensionRetiro!=0)
                        {
                            nomina.Percepciones.TotalJubilacionPensionRetiro = dto.Percepciones.TotalJubilacionPensionRetiro;
                            nomina.Percepciones.TotalJubilacionPensionRetiroSpecified = true;

                        }
                        else
                            nomina.Percepciones.TotalJubilacionPensionRetiroSpecified = false;

                        nomina.Percepciones.TotalGravado =dto.Percepciones.TotalGravado;
                        nomina.Percepciones.TotalExento = dto.Percepciones.TotalExento;


                    }
                    //-----------------------------fin percepciones------------
                    //---------------------deducciones
                    if (dto.Deducciones != null)
                    {
                        if (dto.Deducciones.Deduccion.Count > 0)
                        {
                            nomina.Deducciones = new NominaDeducciones();
                            nomina.Deducciones.Deduccion = new List<NominaDeduccionesDeduccion>();
                            List<NominaDeduccionesDeduccion> DED = new List<NominaDeduccionesDeduccion>();
                            foreach (var d in dto.Deducciones.Deduccion)
                            {
                                NominaDeduccionesDeduccion ded = new NominaDeduccionesDeduccion();
                                c_TipoDeduccion myTipoDeduccion;
                                Enum.TryParse("Item" + d.TipoDeduccion.ToString(), out myTipoDeduccion);
                                ded.TipoDeduccion = myTipoDeduccion;

                                ded.Clave = d.Clave;
                                ded.Concepto = d.Concepto;
                                ded.Importe = d.Importe.ToString("F");
                                DED.Add(ded);
                            }
                            nomina.Deducciones.Deduccion = DED;
                            if (dto.Deducciones.TotalOtrasDeducciones != 0)
                            {
                                nomina.Deducciones.TotalOtrasDeducciones = dto.Deducciones.TotalOtrasDeducciones;
                                nomina.Deducciones.TotalOtrasDeduccionesSpecified = true;
                            }
                            else
                                nomina.Deducciones.TotalOtrasDeduccionesSpecified = false;
                            if (dto.Deducciones.TotalImpuestosRetenidos != 0)
                            {

                                nomina.Deducciones.TotalImpuestosRetenidos = dto.Deducciones.TotalImpuestosRetenidos;
                                nomina.Deducciones.TotalImpuestosRetenidosSpecified = true;
                            }
                            else
                                nomina.Deducciones.TotalImpuestosRetenidosSpecified = false;
                        }
                    }
                    //-------------------fin dedducciones
                    //--------------------otrosPagos----------------------------------
                    if (dto.otroPago != null)
                    {
                        if (dto.otroPago.Count > 0)
                        {
                            nomina.OtrosPagos = new List<NominaOtroPago>();
                            List<NominaOtroPago> OTROS = new List<NominaOtroPago>();
                            foreach (var o in dto.otroPago)
                            {
                                NominaOtroPago otros = new NominaOtroPago();
                                if (o.SubsidioCausado != -1)
                                {
                                    otros.SubsidioAlEmpleo = new NominaOtroPagoSubsidioAlEmpleo();
                                    otros.SubsidioAlEmpleo.SubsidioCausado = o.SubsidioCausado;
                                }
                                if (o.SaldoAFavor != 0)
                                {
                                    otros.CompensacionSaldosAFavor = new NominaOtroPagoCompensacionSaldosAFavor();
                                    otros.CompensacionSaldosAFavor.SaldoAFavor = o.SaldoAFavor;
                                    otros.CompensacionSaldosAFavor.Año = o.Año;
                                    otros.CompensacionSaldosAFavor.RemanenteSalFav = o.RemanenteSalFav;

                                }
                                otros.Clave = o.Clave;
                                c_TipoOtroPago myTipoOtroPago;
                                Enum.TryParse("Item" + o.TipoOtroPago, out myTipoOtroPago);
                                otros.TipoOtroPago = myTipoOtroPago;
                                otros.Concepto = o.Concepto;
                                otros.Importe = o.Importe.ToString("F");
                                OTROS.Add(otros);
                            }
                            nomina.OtrosPagos = OTROS;
                        }
                    }
                    //--------------------fin otrosPagos--------------------------------------
                    //--------------------------incapacidades-----------------------------
                    if (dto.incapacidades!=null)
                    {
                        if (dto.incapacidades.Count > 0)
                        {
                            nomina.Incapacidades = new List<NominaIncapacidad>();
                            List<NominaIncapacidad> INC = new List<NominaIncapacidad>();
                            foreach (var i in dto.incapacidades)
                            {
                                NominaIncapacidad inc = new NominaIncapacidad();
                                inc.DiasIncapacidad = i.DiasIncapacidad;
                                c_TipoIncapacidad myTipoIncapacidad;
                                Enum.TryParse("Item" + i.TipoIncapacidad, out myTipoIncapacidad);
                                inc.TipoIncapacidad = myTipoIncapacidad;

                                if (i.ImporteMonetario != 0)
                                {
                                    inc.ImporteMonetario = i.ImporteMonetario.ToString("F");
                                    inc.ImporteMonetarioSpecified = true;
                                }
                                else
                                    inc.ImporteMonetarioSpecified = false;
                                INC.Add(inc);
                            }
                            nomina.Incapacidades = INC;
                        }

                    }
                    //-------------------------fin incapacidades---------------------

                    comprobante.Nomina = nomina;

                }
            }
            //----------------------------------FIN NOMINA------------------------------------------------

            if (factura.Factura.TipoDocumento == TipoDocumento.Amc71)
            {
                comprobante.AddendaAmece = factura.Factura.AddendaAmece;
            }
            if (factura.Factura.TipoDocumento == TipoDocumento.HomeDepot)
            {
                comprobante.AddendaHomeDepot = factura.Factura.AddendaHomeDepot;
            }



            List<ComprobanteConcepto> conceptos = new List<ComprobanteConcepto>();
            /*
              foreach (facturasdetalle detalle in factura.Detalles)
            {
                ComprobanteConcepto con = new ComprobanteConcepto();
                con.Descripcion = detalle.Descripcion;
                if (!string.IsNullOrEmpty(detalle.ConceptoNoIdentificacion))
                    con.NoIdentificacion = detalle.ConceptoNoIdentificacion;
                    con.Cantidad = detalle.Cantidad;
                    con.ValorUnitario = numerodecimales(detalle.Precio, (int)mone.Decimales);
                    con.Importe = numerodecimales(detalle.Total, (int)mone.Decimales);
                    con.Unidad = detalle.Unidad;
                    con.ClaveProdServ = detalle.Codigo;
                    con.ClaveUnidad = detalle.ConceptoClaveUnidad;
                */
            foreach (facturasdetalle detalle in factura.Detalles)
            {
                ComprobanteConcepto con = new ComprobanteConcepto();
                con.Descripcion = detalle.Descripcion;
                if (!string.IsNullOrEmpty(detalle.Codigo))
                    con.NoIdentificacion = detalle.Codigo;
                con.Detalles = detalle.Descripcion2;
                con.Cantidad = detalle.Cantidad;
                con.ValorUnitario = detalle.Precio.ToString();
                con.Importe = Decimal.Round(detalle.TotalPartida, 6).ToString();
               // con.Unidad = detalle.Unidad;
                con.OrdenCompra = detalle.OrdenCompra;
                con.ClaveProdServ = "84111505";
                con.ClaveUnidad = "ACT";

                if (!string.IsNullOrEmpty(factura._factura.Descuento))///valida si existe descuento suma de deducciones
                {
                    if (Convert.ToDecimal(factura._factura.Descuento) > 0)
                    {

                        con.Descuento = Convert.ToDecimal(factura._factura.Descuento);
                        con.DescuentoSpecified = true;
                    }
                }  
                conceptos.Add(con);
            }
            comprobante.Conceptos = conceptos.ToArray();
           
            //ComprobanteImpuestos impuestos = new ComprobanteImpuestos();
            //impuestos.TotalImpuestosRetenidos = "0";
            //impuestos.TotalImpuestosTrasladados = "0";
           


            comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.Total.ToString(), comprobante.Moneda);
           

            //comprobante.Impuestos = impuestos;
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
