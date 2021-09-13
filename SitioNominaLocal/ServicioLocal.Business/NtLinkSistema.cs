using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using ServicioLocalContract;
using System.Data.Sql;

namespace ServicioLocal.Business
{
    public class NtLinkSistema : NtLinkBusiness
    {

        public List<Contratos> ListaContratos(int idSistema)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var con = db.Contratos.Where(p => p.IdSistema == idSistema).ToList();
                    return con;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }
        
        public void GuardarContrato(Contratos contrato)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (contrato.IdContrato == 0)
                    {
                        db.Contratos.AddObject(contrato);
                    }
                    else
                    {
                        db.Contratos.FirstOrDefault(p => p.IdContrato == contrato.IdContrato);
                        db.Contratos.ApplyCurrentValues(contrato);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);

            }
        }

        public void GuardarDisContrato(DistContratos contrato)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (contrato.IdContrato == 0)
                    {
                        db.DistContratos.AddObject(contrato);
                    }
                    else
                    {
                        db.DistContratos.FirstOrDefault(p => p.IdContrato == contrato.IdContrato);
                        db.DistContratos.ApplyCurrentValues(contrato);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);

            }
        }

        public List<Paquetes> ListaPaquetes()
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var paquetes = db.Paquetes.ToList();
                    return paquetes;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }

        public bool SavePaquete(Paquetes paquete)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (db.Paquetes.Any(p => p.Descripcion == paquete.Descripcion))
                    {
                        throw new FaultException("Descripción duplicada");
                    }
                    if (paquete.IdPaquete == 0)
                    {
                        db.AddToPaquetes(paquete);
                        db.SaveChanges();
                    }
                    else
                    {
                        var pa = db.Paquetes.Where(p => p.IdPaquete == paquete.IdPaquete).FirstOrDefault();
                        db.Paquetes.ApplyCurrentValues(paquete);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }
        }



        public Sistemas GetSistema(int idSistema)
        {

            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var sistema = db.Sistemas.Where(c => c.IdSistema == idSistema);
                    return sistema.FirstOrDefault();
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

        public Sistemas GetSistema(string rfc)
        {

            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var sistema = db.Sistemas.Where(c => c.Rfc == rfc);
                    return sistema.FirstOrDefault();
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


        private bool Existe(Sistemas e)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    return db.Sistemas.Any(p => p.Rfc == e.Rfc);
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


        private bool Validar(Sistemas e)
        {
            //TODO: Validar los campos requeridos y generar excepcion
            {
                if (string.IsNullOrEmpty(e.RazonSocial))
                {
                    throw new FaultException("La Razón Social no puede ir vacía");
                }
                if (string.IsNullOrEmpty(e.Rfc))
                {
                    throw new FaultException("El campo RFC es Obligatorio");
                }
                if (string.IsNullOrEmpty(e.Email))
                {
                    throw new FaultException("El campo Email es Obligatorio");
                }
                if (string.IsNullOrEmpty(e.Calle))
                {
                    throw new FaultException("El campo Calle es Obligatorio");
                }
                if (string.IsNullOrEmpty(e.Ciudad))
                {
                    throw new FaultException("El campo Municipio es Obligatorio");
                }
                if (string.IsNullOrEmpty(e.RegimenFiscal))
                {
                    throw new FaultException("El campo Régimen es Obligatorio");
                }

                Regex reg = new Regex("^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]{2}[0-9,A]$");
                if (!reg.IsMatch(e.Rfc))
                {
                    throw new FaultException<ApplicationException>(new ApplicationException("El RFC es inválido"), "El RFC es inválido");
                }
                Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*");
                if (!regex.IsMatch(e.Email))
                {
                    throw new FaultException("El campo Email esta mal formado");
                }
                if (e.IdSistema == 0 && Existe(e))
                {
                    throw new FaultException("El RFC ya fue capturado");
                }
                
            }
            return true;
        }


        private void EnviarCorreo(string email,string razonSocial, string usuario, string password)
        {
            try
            {
                //var archivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources", "TextoEmail.html");
                var archivo = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Resources", "TextoEmail.html");

                var content = File.ReadAllText(archivo,Encoding.UTF8);
                content = content.Replace("[RazonSocial]", razonSocial).Replace("[UserName]", usuario).Replace(
                    "[Password]", password);
                content = content.Replace("[LinkReciboNomina]", HttpContext.Current.Request.Url.Host + "");

                Mailer mailer = new Mailer();
                var recipients = new List<string>();
                recipients.Add(email);
                mailer.Send(recipients, new List<string>(), content, "Notificacion: Registro de Solicitud de Usuario - Recibo de Nómina", "nomina@ntlink.com.mx", "Servicio de Recibo de Nómina Nt Link");
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw new FaultException("Ocurrió un error al enviar el correo electronico, revise el archivo de log para ver los detalles");
            }
        }


        public bool SaveSistema(Sistemas sistema, ref string resultado, string nombreCompleto, string iniciales)
        {

            try
            {
                if (Validar(sistema))
                {
                    using (var db = new NtLinkLocalServiceEntities())
                    {
                        if (sistema.IdSistema == 0)
                        {
                            // Crear random password
                            if (db.aspnet_Membership.Any(p => p.LoweredEmail == sistema.Email.ToLower()))
                            {
                                throw new FaultException("El Email ya fue registrado");
                            }
                            db.Sistemas.AddObject(sistema);
                            db.SaveChanges();
                            var password = Membership.GeneratePassword(8, 2);
                            var userName = sistema.Email;
                            empresa em = new empresa()
                            {
                                PrimeraVez = true,
                                RazonSocial = sistema.RazonSocial,
                                RFC = sistema.Rfc,
                                Ciudad = sistema.Ciudad,
                                Colonia = sistema.Colonia,
                                CP = sistema.Cp,
                                Calle = sistema.Calle,
                                NoExt = sistema.NoExt,
                                NoInt = sistema.NoInt,
                                Email = sistema.Email,
                                Estado = sistema.Estado,
                                idSistema = sistema.IdSistema,
                                RegimenFiscal = sistema.RegimenFiscal,
                                Linea = "A"
                            };
                            NtLinkEmpresa emp = new NtLinkEmpresa();
                            emp.Save(em, null);
                            var e2 = emp.GetByRfc(em.RFC);
                            NtLinkUsuarios.CreateUser(userName, password, sistema.Email, e2.IdEmpresa, "Administrador", nombreCompleto, iniciales);
                            Logger.Info( "se creó el usuario: "+ userName + " con el password: " + password);
                            try
                            {
                                EnviarCorreo(sistema.Email, sistema.RazonSocial, userName, password);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                            }
                            resultado = "se creó el usuario: "+ userName + " con el password: " + password;
                            
                        }
                        else
                        {
                            var y = db.Sistemas.Where(p => p.IdSistema == sistema.IdSistema).FirstOrDefault();
                            db.Sistemas.ApplyCurrentValues(sistema);
                            db.SaveChanges();
                            resultado = "Registro actualizado correctamente";
                        }
                       

                        return true;
                    }
                }
                return false;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                resultado = "Error al guardar el registro";
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }
        }



        public List<Sistemas> GetSistemasLista(string filtro)
        {

            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (string.IsNullOrEmpty(filtro))
                        return db.Sistemas.ToList();
                    else
                        return db.Sistemas.Where(p => p.RazonSocial.Contains(filtro) || p.Rfc.Contains(filtro)).ToList();
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



        public void IncrementaTimbres(long idSistema, int idempresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    int res = db.ExecuteStoreCommand(
                        "update sistemas set TimbresConsumidos = TimbresConsumidos + 1 where IdSistema = {0}",
                        idSistema);
                    res = db.ExecuteStoreCommand(
                        "update empresa set TimbresConsumidos = TimbresConsumidos + 1 where Idempresa = {0}",
                        idempresa);
                    
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }

      
    }
}
