using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ServicioLocalContract;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using ServicioLocalContract;
using System.Web;

namespace ServicioLocal.Business
{
    public class NtLinkEmpresa : NtLinkBusiness
    {

        

        public List<empresa> GetList(string perfil, int idEmpresa, long idSistema)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (perfil.Equals("Administrador"))
                    {
                        var res =
                            db.empresa.Where(p => p.idSistema == idSistema).Select(
                                p =>
                                new
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return
                            res.Select(
                                p =>
                                new empresa()
                                {
                                    RFC = p.RFC,
                                    idSistema = p.idSistema,
                                    IdEmpresa = p.IdEmpresa,
                                    RazonSocial = p.RazonSocial,
                                    TimbresConsumidos = p.TimbresConsumidos,
                                    Linea = p.Linea,
                                    Baja = p.Baja,
                                    Bloqueado = p.Bloqueado
                                }).ToList();
                    }
                    
                    else
                    {
                        var res =
                            db.empresa.Where(p => p.IdEmpresa == idEmpresa).Select(
                                p =>
                                new
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return res.Select(
                                p =>
                                new empresa()
                                {
                                    RFC = p.RFC,
                                    idSistema = p.idSistema,
                                    IdEmpresa = p.IdEmpresa,
                                    RazonSocial = p.RazonSocial,
                                    TimbresConsumidos = p.TimbresConsumidos,
                                    Linea = p.Linea,
                                    Baja = p.Baja,
                                    Bloqueado = p.Bloqueado
                                }).ToList();
                    }
                        
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public List<empresa> GetListForLine(string Linea)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (Linea == null)
                    {
                        Linea = "A";
                    }
                    if (Linea != null)
                    {
                        var res =
                            db.empresa.Where(p => p.Linea == Linea).Select(
                                p =>
                                new
                                    {
                                        p.RFC,
                                        p.idSistema, 
                                        p.IdEmpresa,
                                        p.RazonSocial,
                                        p.TimbresConsumidos,
                                        Linea,
                                        p.Baja,
                                        p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return
                            res.Select(
                                p =>
                                new empresa()
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).ToList();
                    }
                    else
                    {
                        return db.empresa.Select(p => new empresa() { RFC = p.RFC, idSistema = p.idSistema, IdEmpresa = p.IdEmpresa, RazonSocial = p.RazonSocial, TimbresConsumidos = p.TimbresConsumidos, Linea = p.Linea, Baja = p.Baja, Bloqueado = p.Bloqueado }).OrderBy(p => p.RFC).ToList();
                    }
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public empresa GetById(int idEmpresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var empr = db.empresa.Where(p => p.IdEmpresa == idEmpresa).FirstOrDefault();
                    if (empr != null)
                    {
                        //string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], empr.RFC);
                        //string cer = Path.Combine(path, "Certs", "csd.cer");

                        //if (File.Exists(cer))
                        //{
                        //    X509Certificate2 cert = new X509Certificate2(cer);
                        //    empr.VencimientoCert = cert.GetExpirationDateString();
                        //}

                        byte[] certBytes = BlobUtils.GetCer(empr.RFC);

                        if (certBytes != null)
                        {
                            X509Certificate2 cert = new X509Certificate2(certBytes);
                            empr.VencimientoCert = cert.GetExpirationDateString();
                        }

                    }
                    return empr;

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }


        public empresa GetByRfc(string rfc)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var empr = db.empresa.FirstOrDefault(p => p.RFC == rfc);
                    if (empr != null)
                    {
                        //string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], empr.RFC);
                        //string cer = Path.Combine(path, "Certs", "csd.cer");
                        //if (File.Exists(cer))
                        //{
                        //    X509Certificate2 cert = new X509Certificate2(cer);
                        //    empr.VencimientoCert = cert.GetExpirationDateString();
                        //}

                        byte[] certBytes = BlobUtils.GetCer(empr.RFC);
                        
                        if (certBytes != null)
                        {
                            X509Certificate2 cert = new X509Certificate2(certBytes);
                            empr.VencimientoCert = cert.GetExpirationDateString();
                        }

                    }
                    return empr;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException !=null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }

        private bool Validar(empresa e)
        {
            //TODO: Validar los campos requeridos y generar excepcion
            {
                if (string.IsNullOrEmpty(e.RazonSocial))
                {

                    throw new FaultException("La Razón Social no puede ir vacía");
                }
                if (string.IsNullOrEmpty(e.RFC))
                {
                    throw new FaultException("El RFC no puede ir vacío");
                }
                if (string.IsNullOrEmpty(e.Email))
                {
                    throw new FaultException("El campo Email es Obligatorio");
                }
                
                if (string.IsNullOrEmpty(e.Ciudad))
                {
                    throw new FaultException("El campo Municipio es Obligatorio");
                }
               
                if (string.IsNullOrEmpty(e.RegimenFiscal))
                {
                    throw new FaultException("El campo Regimen Fiscal es Obligatorio");
                }
                Regex regex =
                    new Regex(
                        @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*");
                if (!regex.IsMatch(e.Email))
                {
                    throw new FaultException("El campo Email esta mal formado");
                }
                Regex reg = new Regex("^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]{2}[0-9,A]$");
                if (!reg.IsMatch(e.RFC))
                {
                    throw new FaultException("El RFC es inválido");
                }

                
            }
            return true;
        }

        public bool Save(empresa e, byte[] cert, byte[] llave, string passwordLlave, byte[] logo, string formatoLlave)
        {

            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (Validar(e))
                    {
                        string path = BlobUtils.CreaRutas(e.RFC);

                        //string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                        if (logo != null)
                        {
                            //File.WriteAllBytes(Path.Combine(path, "Logo.png"), logo);
                            BlobUtils.UploadBlob(e.RFC ,path + "/Resources_logo.png", logo);
                        }
                        if (!ValidaRfcEmisor(e.RFC, cert))
                        {
                            throw new FaultException("El rfc del emisor no corresponde con el certificado");
                        }
                        string pathCer = path + "/Certs_csd.cer";
                        string pathKey = path + "/Certs_csd.key" + formatoLlave;
                        BlobUtils.UploadBlob(e.RFC, pathCer, cert);
                        BlobUtils.UploadBlob(e.RFC, pathKey, llave);
                        var key = OpensslKey.DecodePrivateKey(llave, e.PassKey, formatoLlave);
                        if(key == null)
                        {
                            throw new FaultException("El password de la llave es incorrecto");
                        }
                        if (e.IdEmpresa == 0)
                        {
                            if (db.empresa.Any(l => l.RFC.Equals(e.RFC) && l.idSistema == e.idSistema))
                            {
                                throw new FaultException("El RFC ya ha sido dato de alta");
                            }
                            db.empresa.AddObject(e);
                        }
                        else
                        {
                            db.empresa.Where(p => p.IdEmpresa == e.IdEmpresa).FirstOrDefault();
                            db.empresa.ApplyCurrentValues(e);
                        }
                        db.SaveChanges();
                        return true;
                    }
                    Logger.Error("Fallo de validación");
                    return false;
                }
            }
            catch (ApplicationException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return false;
            }
        }


        public bool ValidaRfcEmisor(string rfc, byte[] certificado)
        {
            try
            {
                X509Certificate2 cer = new X509Certificate2(certificado);
                if (certificado == null)
                    return false;
                var name = cer.SubjectName.Name;
                name = name.Replace("\"", "");
                string strLRfc =
                    name.Substring(name.LastIndexOf("2.5.4.45=") + 9, 13).Trim();
                return strLRfc == rfc;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }


       


        public bool Save(empresa e, byte[] logo)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (Validar(e))
                    {
                        if (e.IdEmpresa == 0)
                        {
                            if (db.empresa.Any(l => l.RFC.Equals(e.RFC) && l.idSistema == e.idSistema))
                            {
                                throw new FaultException("El RFC ya ha sido dato de alta");
                            }
                            db.empresa.AddObject(e);
                        }
                        else
                        {
                            db.empresa.FirstOrDefault(p => p.IdEmpresa == e.IdEmpresa);
                            db.empresa.ApplyCurrentValues(e);
                        }
                        db.SaveChanges();
                        string path = BlobUtils.CreaRutas(e.RFC);

                        //string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                        if (logo != null)
                        {
                            //File.WriteAllBytes(Path.Combine(path, "Logo.png"), logo);
                            BlobUtils.UploadBlob(e.RFC, path + "/Resources_logo.png", logo);
                        }
                        return true;
                    }
                    return false;
                }
            }
            catch (ApplicationException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException.Message);
                return false;
            }
        }

        public bool TieneConfiguradoCertificado(int idEmpresa)
        {
            using (var db = new NtLinkLocalServiceEntities())
            {
                empresa emp = db.empresa.Single(l => l.IdEmpresa == idEmpresa);

                string path = BlobUtils.CreaRutas(emp.RFC);
                string pathCer = path + "/Certs/csd.cer";
                string pathKey = path + "/Certs/csd.key";

                //if (BlobUtils.ExisteKeyCer(emp.RFC, emp.RutaKey))
                //{
                //    return true;
                //}

                if (BlobUtils.BlobExists(emp.RFC, "certs_csd.key.key"))
                {
                    return true;
                }

                return false;
            }
        }

        public List<empresaPantalla> GetPantallas(int idEmpresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    return db.empresaPantalla.Where(l => l.idEmpresa == idEmpresa).ToList();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }

        public bool SavePantallas(List<empresaPantalla> pantallas)
        {
            try
            {
                if (pantallas != null && pantallas.Count >0 )
                {
                    int idEmpresa = pantallas.First().idEmpresa;
                    using (var db = new NtLinkLocalServiceEntities())
                    {
                        List<empresaPantalla> currPantallas =
                            db.empresaPantalla.Where(l => l.idEmpresa == idEmpresa).ToList();

                        foreach (var pantalla in currPantallas)
                        {
                            db.empresaPantalla.DeleteObject(pantalla);
                        }

                        foreach (var pantalla in pantallas)
                        {
                            db.empresaPantalla.AddObject(pantalla);
                        }

                        db.SaveChanges();
                    }
                }
                
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }
        }

        public string ValidaCSD(empresa e, byte[] cert, byte[] llave, string passwordLlave, string formatoLlave)
        {
            try
            {
                
                var result = OpensslKey.DecodePrivateKey(llave, passwordLlave, formatoLlave);
                if (result !=  null)
                return "El Certificado CSD  es correcto";
                
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return "";
            }
            return "El Password de la llave no es correcta";
        }

       

       
    }
}
