using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Org.BouncyCastle.X509;
using ServicioLocalContract;

namespace ServicioLocal.Business
{
    class ValidadorEstructura : NtLinkBusiness, ICloneable, IDisposable
    {

        public ValidadorEstructura()
        {

        }

        public object Clone()
        {
            var c = this.MemberwiseClone() as ValidadorEstructura;
            c.settings = settings.Clone();
            return c;
        }

        public void Dispose()
        {
            settings = null;
        }

        public ValidadorInput Validate2(XmlReader reader, string content)
        {
            var result = new ValidadorInput() { };
            result.XmlString = content;
            try
            {
                result.Errores = new List<string>();
                settings.ValidationEventHandler += (s, a) =>
                {
                    XmlReader r = (XmlReader)s;
                    result.Errores.Add(r.Name + " - " + a.Message);
                };
                double totalImpuestos = 0;
                double totalConceptos = 0;
                double totalRetenciones = 0;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "Concepto")
                        {
                            totalConceptos = totalConceptos + double.Parse(reader.GetAttribute("Importe"));
                        }
                        if (reader.LocalName == "Traslado")
                        {
                            totalImpuestos = totalImpuestos + double.Parse(reader.GetAttribute("Importe"));
                        }
                        if (reader.LocalName == "Retencion")
                        {
                            totalRetenciones = totalRetenciones + double.Parse(reader.GetAttribute("Importe"));
                        }
                        if (reader.LocalName == "Emisor")
                            result.RfcEmisor = reader.GetAttribute("rfc");
                        if (reader.LocalName == "TimbreFiscalDigital")
                        {
                            result.CadenaTimbre = "||" + 
                                reader.GetAttribute("Version") + "|" + 
                                reader.GetAttribute("UUID") + "|" +
                                reader.GetAttribute("FechaTimbrado") + "|" +
                                reader.GetAttribute("SelloCFD") + "|" + 
                                reader.GetAttribute("NoCertificadoSAT") + "||";
                            result.SelloSat = reader.GetAttribute("SelloSAT");
                            try
                            {
                                result.FechaTimbrado = Convert.ToDateTime(reader.GetAttribute("FechaTimbrado"));

                            }
                            catch (Exception ee)
                            {
                                result.Errores.Add("Formato incorrecto de fecha");
                                Logger.Error("FechaTimbrado->" + reader.GetAttribute("FechaTimbrado"), ee);
                                result.Valido = false;
                                break;
                            }
                            result.NoCertificadoSat = reader.GetAttribute("noCertificadoSAT");

                        }
                        if (reader.LocalName == "Comprobante")
                        {
                            var fecha = reader.GetAttribute("Fecha");
                            try
                            {
                                result.Fecha = Convert.ToDateTime(fecha);

                            }
                            catch (Exception ee)
                            {
                                result.Errores.Add("Formato incorrecto de fecha");
                                Logger.Error("Fecha->" + fecha, ee);
                                result.Valido = false;
                                break;
                            }
                            result.Total = double.Parse(reader.GetAttribute("Total"));
                            result.SubTotal = double.Parse(reader.GetAttribute("SubTotal"));
                            result.Sello = reader.GetAttribute("Sello");
                            result.NoCertificado = reader.GetAttribute("NoCertificado");
                            result.Certificado = reader.GetAttribute("Certificado");
                            X509CertificateParser parser = new X509CertificateParser();
                            result.Certificate =parser.ReadCertificate(Convert.FromBase64String(result.Certificado));
                            result.Version = reader.GetAttribute("Version");
                            if (result.Version == "2.2")
                            {
                                result.Folio = reader.GetAttribute("Folio");
                                result.Serie = reader.GetAttribute("Serie");
                                result.NoAprobacion = reader.GetAttribute("NoAprobacion");
                                result.AnoAprobacion = reader.GetAttribute("AnoAprobacion");
                            }
                        }

                    }
                }
                if (result.Errores.Count == 0)
                {
                    result.SumaImpuestos = totalImpuestos;
                    result.SumaRetenciones = totalRetenciones;
                    result.SumaConceptos = totalConceptos;
                    result.Valido = true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
            }

            return result;

        }

        private XmlReaderSettings settings;
        public ValidadorEstructura(string version)
        {
            settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.DtdProcessing =DtdProcessing.Ignore;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            IEnumerable<string> files;
            if (version == "3.3")
            {
                files = Directory.EnumerateFiles(Path.Combine(ConfigurationManager.AppSettings["RutaArchivosXsd"],"3.3"));
            }
            else files = Directory.EnumerateFiles(Path.Combine(ConfigurationManager.AppSettings["RutaArchivosXsd"], "2.2"));
            foreach (string schemaFile in files)
            {
                settings.Schemas.Add(null, schemaFile);
            }
        }

    }
}
