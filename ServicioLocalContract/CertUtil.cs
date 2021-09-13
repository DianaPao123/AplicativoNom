using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ServicioLocalContract
{
    public class CertUtil
    {
        public static bool ValidaCert(string fileName)
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(fileName);
                return true;
            }
            catch (Exception)
            {
                throw new ApplicationException("El archivo no es un certificado válido");
            }
           
        }

        public static bool ValidaKey(string fileName, string pass, string formato)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                RSACryptoServiceProvider rsa = OpensslKey.DecodePrivateKey(bytes, pass, formato);
                if (rsa== null)
                {
                    throw new ApplicationException("No es un archivo de llave privada válido");
                }
                return true;
            }
            catch (Exception)
            {
                //TODO: Log Exception
                throw new ApplicationException("No es un archivo de llave privada válido");
            }
        }


        
    }
}
