using System;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Web;

namespace ServicioLocal.Business
{
    class GeneradorCadenasTimbre : NtLinkBusiness
    {
        private XmlTextReader xsltReader;
        private string xsl;
        private StringReader xsltInput;
        private XslCompiledTransform xsltTransform = new XslCompiledTransform();


        public GeneradorCadenasTimbre(bool timbre)
        {
            try
            {
                ServicioLocal.Business.GeneradorCadenas.LocalFileResolver resolver = new ServicioLocal.Business.GeneradorCadenas.LocalFileResolver();
                //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Xsl");
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Xsl");
                var xsl = File.ReadAllText(path + "\\cadenaoriginal_TFD_1_0.xslt");
                xsltInput = new StringReader(xsl);
                xsltReader = new XmlTextReader(xsltInput);
                xsltTransform.Load(xsltReader, new XsltSettings(false, true), resolver);
            }
            catch (Exception exception)
            {
                Logger.Error("Error(GeneradorCadenas):" + exception);
            }
        }


        /// <summary>
        /// Generador de cadenas originales de timbre
        /// </summary>
        public GeneradorCadenasTimbre()
        {
            try
            {
                ServicioLocal.Business.GeneradorCadenas.LocalFileResolver resolver = new ServicioLocal.Business.GeneradorCadenas.LocalFileResolver();
                //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Xsl");
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Xsl");
                var xsl = File.ReadAllText(path + "\\cadenaoriginal_TFD_1_0.xslt");
                xsltInput = new StringReader(xsl);
                xsltReader = new XmlTextReader(xsltInput);
                xsltTransform.Load(xsltReader, new XsltSettings(false, true), resolver);
            }
            catch (Exception exception)
            {
                Logger.Error("Error(GeneradorCadenas):" + exception);
            }
        }

        public string CadenaOriginal(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("Error", "xml");
            }
            StringReader xmlInput = new StringReader(xml);
            XmlTextReader xmlReader = new XmlTextReader(xmlInput);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter transformedXml = new XmlTextWriter(stringWriter);
            try
            {
                xsltTransform.Transform(xmlReader, transformedXml);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return stringWriter.ToString();
        }
    }
}
