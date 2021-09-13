using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,tz);

            Console.WriteLine(fecha.ToString("dd-MM-yyyy HH:mm:ss"));

            Console.ReadKey();
            
            //string StorageConnString = "DefaultEndpointsProtocol=https;AccountName=pruebasntlink;AccountKey=yUYfFTVXfHt/5/6gs2EVNL/0E9yu8fbprgPYSKW4NEGv3YHaWoarlHAyj5OXdYOr4rs9KATT0NMRShm8B8EH/A==";
            //CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(StorageConnString);
            //var blobStorageClient = StorageAccount.CreateCloudBlobClient();
            //var container = blobStorageClient.GetContainerReference("cfdi-files");
            //var blobs = container.ListBlobs("cadenaoriginal_TFD_1_0.xslt").ToList();
            //var xsl = blobs.FirstOrDefault().StorageUri.ToString();

            //LocalFileResolver resolver = new LocalFileResolver();
            //StringReader xsltInput = new StringReader(xsl);
            //XmlTextReader xsltReader = new XmlTextReader(xsltInput);
            //XslCompiledTransform xsltTransform = new XslCompiledTransform();

            //var dddd = new Uri(System.IO.Path.GetDirectoryName(xsl).ToString() + "/");
            //    var ffff =System.IO.Path.GetFileName(xsl);

            //xsltTransform.Load(xsltReader, new XsltSettings(false, true), resolver);
            

        }
    }

    public class LocalFileResolver : XmlUrlResolver
    {
        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Xsl");
            //return base.ResolveUri(new Uri(path + "\\"), relativeUri);

            string StorageConnString = "DefaultEndpointsProtocol=https;AccountName=pruebasntlink;AccountKey=yUYfFTVXfHt/5/6gs2EVNL/0E9yu8fbprgPYSKW4NEGv3YHaWoarlHAyj5OXdYOr4rs9KATT0NMRShm8B8EH/A==";
            CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(StorageConnString);
            var blobStorageClient = StorageAccount.CreateCloudBlobClient();
            var container = blobStorageClient.GetContainerReference("cfdi-files");
            var blobs = container.ListBlobs("cadenaoriginal_TFD_1_0.xslt").ToList();
            var xsl = blobs.FirstOrDefault().StorageUri.ToString();

            //return base.ResolveUri(new Uri(path + "\\"), relativeUri);
            return base.ResolveUri(new Uri(System.IO.Path.GetDirectoryName(xsl).ToString() + "/"),System.IO.Path.GetFileName(xsl));

        }
    }

}
