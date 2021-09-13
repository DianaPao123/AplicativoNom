using System;
using System.Configuration;
using System.IO;
using log4net;

namespace ServicioLocalContract
{
    public class BlobUtils
    {
        public static bool ExisteKeyCer(string rfc, string formatoLlave)
        {
            try
            {
                // Verify local exist of certs
                return true;

            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }
        
        public static bool BlobExists(string rfc, string blobName)
        {

            try
            {
                string basicPath = ConfigurationManager.AppSettings["RutaBasicaSistema"];
                string path = Path.Combine(basicPath, rfc, blobName);
                return File.Exists(path);
                // Lest validate that the blob/ folder exist
                // return true; // temporal
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }

        public static byte[] DownloadFile(string path, string file)
        {
            try
            {
                // Get the files from local file lets dissapear the second parameter
                return File.ReadAllBytes(Path.Combine(path, file));
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return null;
            }
        }

        public static byte[] GetKey(string rfc, string formatoLlave)
        {
            try
            {
                string basicPath = ConfigurationManager.AppSettings["RutaBasicaSistema"];
                string path = Path.Combine(basicPath, rfc);
                return File.ReadAllBytes(Path.Combine(path, "Certs_csd.key.key"));

                //  Get the bytes of the key from local, lets use:
                // Appsetting[rutas] + rfc + certs + key
                // return null; // temporal
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return null;
            }
        }

        public static byte[] GetCer(string rfc)
        {
            try
            {
                string basicPath = ConfigurationManager.AppSettings["RutaBasicaSistema"];
                string path = Path.Combine(basicPath, rfc);
                return File.ReadAllBytes(Path.Combine(path, "Certs_csd.cer"));
                // Get the bytes of the key from local, lets use:
                // Appsetting[rutas] + rfc + certs + key
                //return null; // temporal
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return null;
            }
        }

        protected static ILog Logger = LogManager.GetLogger(typeof(BlobUtils));
        public static bool UploadBlob(string rfc, string blobName, byte[] contents)
        {

            try
            {
                string basicPath = ConfigurationManager.AppSettings["RutaBasicaSistema"];
                string path = Path.Combine(basicPath, rfc);
                File.WriteAllBytes(Path.Combine(path, blobName), contents);
                // lets save the file we will use
                // AppSetting[Rutas] + RFC + blobname
                // return true;
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }


        public static string CreaRutas(string rfc)
        {
            // We must create the folder if doesnt exist, and return the filename
            string basicPath = ConfigurationManager.AppSettings["RutaBasicaSistema"];
            string path = Path.Combine(basicPath, rfc);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
