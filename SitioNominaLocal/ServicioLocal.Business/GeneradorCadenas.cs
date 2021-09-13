using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using log4net;
using log4net.Config;
using System.Web;

namespace ServicioLocal.Business
{
    class GeneradorCadenas 
    {
        private XmlTextReader xsltReader;
        private StringReader xsltInput;
        private XslCompiledTransform xsltTransform = new XslCompiledTransform();
        private static readonly ILog Log = LogManager.GetLogger(typeof(GeneradorCadenas));

        public class LocalFileResolver : XmlUrlResolver
        {
            public override Uri ResolveUri(Uri baseUri, string relativeUri)
            {
                //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Xsl");
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Xsl");
                return base.ResolveUri(new Uri(path + "\\"), relativeUri);
            }
        }



        public GeneradorCadenas()
        {

            try
            {
                LocalFileResolver resolver = new LocalFileResolver();
                //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Xsl");
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Xsl");
                var xsl = File.ReadAllText(path + "\\cadenaoriginal_3_2.xslt");
                xsltInput = new StringReader(xsl);
                xsltReader = new XmlTextReader(xsltInput);
                xsltReader.Read();
                xsltTransform.Load(xsltReader, new XsltSettings(false, true), resolver);
            }
            catch (Exception exception)
            {
                Log.Error("Error(GeneradorCadenas):" + exception);
            }

        }

      
        public string CadenaOriginal(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("Archivo XML Inválido", "xml");
            }
            
            StringReader xmlInput = new StringReader(xml);
            XmlTextReader xmlReader = new XmlTextReader(xmlInput);
            xmlReader.Read();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter transformedXml = new XmlTextWriter(stringWriter);
 
            try
            {
                xsltTransform.Transform(xmlReader, transformedXml);
            }
            catch (Exception ex)
            {
                Log.Error("Error(CadenaOriginal)" + ex);
                throw;
            }
            return HttpUtility.HtmlDecode(stringWriter.ToString());
        }

    }

}
