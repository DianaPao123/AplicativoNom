using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Web.Security;
using ClienteNtLink;
using ServicioLocal.Business;
using ServicioLocalContract;

namespace ServicioWebNomina
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = int.MaxValue)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicioLocal" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicioLocal.svc or ServicioLocal.svc.cs at the Solution Explorer and start debugging.
    public class ServicioLocal : NtLinkBusiness, IServicioLocal
    {


        public byte[] AcuseCancelacion(int idVenta)
        {
            string ReportURL = ConfigurationManager.AppSettings["ReporteAcuseCancelacion"];

            return NtLinkFactura.GetAcuseCancelacion("/" + ReportURL, idVenta);

        }

        public bool EliminarCliente(clientes cliente)
        {
            
            var nlc = new NtLinkClientes();
            return nlc.EliminarCliente(cliente);
        }

        public bool EliminarSubcontratacion(int idcliente)
        {

            var nlc = new NtLinkClientes();
            return nlc.EliminarSubcontratacion(idcliente);
        }

        public List<Contratos> ListaContratos(int idSistema)
        {
            var nls = new NtLinkSistema();
            return nls.ListaContratos(idSistema);
        }

        public List<ElementoDist> ListaDisContratos(int idDistribuidor)
        {
            var ld = new NtLinkDistribuidor();
            return ld.ListaDisContratos(idDistribuidor);
        }

        public List<Distribuidores> ListaDistribuidores()
        {
            var nld = new NtLinkDistribuidor();
            return nld.ListaDistribuidores();
        }

        public DistContratos Contratos(int idContrato)
        {
            var nld = new NtLinkDistribuidor();
            return nld.Contratos(idContrato);
        }

        public void GuardarContrato(Contratos contrato)
        {
            var nls = new NtLinkSistema();
            nls.GuardarContrato(contrato);
        }

        public void GuardarDisContrato(DistContratos contrato)
        {
            var nls = new NtLinkSistema();
            nls.GuardarDisContrato(contrato);
        }


        public ComprobantePdf GetComprobantePdfx(string uudi)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            return ventas.GetComprobantePdf(uudi);
        }

        public void EnviarFactura(string rfc, string folioFiscal, List<string> rec, List<string> bcc)
        {
            NtLinkFactura nlf = new NtLinkFactura(0);
            nlf.EnviarFactura(rfc, folioFiscal, rec, bcc);
        }

        public string CancelarFactura(string rfc, string folioFiscal)
        {
            try
            {
                var cliente = new ClienteTimbradoNtlink();
                string respuesta = cliente.CancelaCfdi(folioFiscal, rfc);
                if (respuesta.StartsWith("<?xml version=\"1.0\"?>"))
                {
                    NtLinkFactura fact = new NtLinkFactura(0);

                    fact.CancelarFactura(folioFiscal, respuesta);

                    return "Comprobante Cancelado correctamente";
                }
                throw new FaultException("Error al cancelar el comprobante, " + respuesta);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "Error al cancelar el comprobante";
            }
        }

        // ServicioWebNomina.ServicioLocal
        public string CancelarFactura33(string rfcEmisor, string folioFiscal, string expresion, string rfcReceptor)
        {
            string result;
            try
            {
                ClienteTimbradoNtlink cliente = new ClienteTimbradoNtlink();
                string respuesta = cliente.CancelaCfdi(folioFiscal, rfcEmisor, expresion, rfcReceptor);
                if (!respuesta.StartsWith("<?xml version=\"1.0\"?>"))
                {
                    throw new FaultException("Error al cancelar el comprobante, " + respuesta);
                }
                NtLinkFactura fact = new NtLinkFactura(0);
                fact.CancelarFactura(folioFiscal, respuesta);
                result = "Comprobante Cancelado correctamente";
            }
            catch (FaultException fe_53)
            {
                throw;
            }
            catch (Exception ex)
            {
                NtLinkBusiness.Logger.Error(ex);
                result = "Error al cancelar el comprobante";
            }
            return result;
        }

        public vventas GetFactura(int idFactura)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetById(idFactura);
            Logger.Debug("Resultado: " + (lista == null));
            return lista;
        }

        public List<empresaPantalla> ObtenerPantallasPorIdEmpresa(int idEmpresa)
        {
            return new NtLinkEmpresa().GetPantallas(idEmpresa);
        }

        public bool ActualizarPantallasPorEmpresa(List<empresaPantalla> pantallas)
        {
            return new NtLinkEmpresa().SavePantallas(pantallas);
        }

        public bool GuardarConcepto(producto producto)
        {
            Logger.Debug(producto.Descripcion);
            NtLinkProducto prod = new NtLinkProducto();
            var res = prod.SaveProducto(producto);
            Logger.Debug(res);
            return res;
        }

        public bool TieneConfiguradoCertificado(int idEmpresa)
        {
            return new NtLinkEmpresa().TieneConfiguradoCertificado(idEmpresa);
        }

        public UsuarioLocal Login(string userName, string password)
        {
            Logger.Debug("Login: " + userName);
            var x = NtLinkLogin.ValidateUser(userName, password);
            Logger.Debug(x != null);
            if (x != null)
            {
                NtLinkUsuarios us = new NtLinkUsuarios();
                var roles = NtLinkUsuarios.GetRolesForUser(userName);
                var profile = UserProfile.GetUserProfile(userName);

                return new UsuarioLocal
                {
                    Perfil = roles[0],
                    UserId = new Guid(x.ProviderUserKey.ToString()),
                    UserName = userName,
                    Bloqueado = x.IsLockedOut,
                    Email = x.Email,
                    NombreCompleto = profile.NombreCompleto,
                    Iniciales = profile.Iniciales,
                    CambiarPassword = profile.CambiarPassword
                };
            }

            return null;
        }

        public List<vventas> ListaCuentas(DateTime inicio, DateTime end, int idEmpresa, int idCliente, string status, string linea = null)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetListCuentas(inicio, end, idEmpresa, status, idCliente, linea);
            Logger.Debug("Resultado: " + lista.Count + " regs.");
            return lista;
        }

        public List<vventas> ListaFacturas(DateTime inicio, DateTime end, int idEmpresa, int idCliente, string status, string linea = null, string iniciales = null)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetList(inicio, end, idEmpresa, status, idCliente, linea, iniciales);
            Logger.Debug("Resultado: " + lista.Count + " regs.");
            return lista;
        }
        public List<vPagos> ListaFacturasCuentas(int idCliente, string folio)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetListCuentas(idCliente, folio);
            if (lista != null)
                Logger.Debug("Resultado: " + lista.Count + " regs.");
            return lista;
        }

        public List<vPagos> ListaPagosVenta(int idventa)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetListCuentas(idventa);
            if (lista != null)
                Logger.Debug("Resultado: " + lista.Count + " regs.");
            return lista;
        }


        public List<empresa> ListaEmpresas(string perfil, int idempresa, long idSistema, string linea = null)
        {
            NtLinkEmpresa emp = new NtLinkEmpresa();
            if (idempresa == 0)
            {
                var emps = emp.GetListForLine(linea);
                Logger.Debug("Resultado: " + emps.Count + " regs.");
                return emps;
            }
            else
            {
                var emps = emp.GetList(perfil, idempresa, idSistema);
                if (linea != null)
                {
                    emps = emps.Where(p => p.Linea == linea).ToList();
                }
                Logger.Debug("Resultado: " + emps.Count + " regs.");
                return emps;
            }


        }
        public List<SubContratacion> ListaSubContratacion(int idcliente)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var list = clientes.GetListSubContratacion(idcliente);
                return list;

        }
        public empresa ObtenerEmpresaByUserId(string userId)
        {
            var empresa = NtLinkUsuarios.GetEmpresaByUserId(userId);
            if (empresa != null)
            {
                Logger.Debug(empresa.IdEmpresa);
                return empresa;
            }
            return null;
        }


        public bool AplicarPagoAnticipo(Pagos pago, List<FacturasPagos> facturas)
        {
            NtLinkPagos pagos = new NtLinkPagos();
            var res = pagos.AplicarPago(pago.IdPago, facturas, pago.Fecha);
            Logger.Info(res);
            return res;
        }

        public bool RecuperarPassword(string rfc, string email)
        {
            NtLinkUsuarios nlu = new NtLinkUsuarios();
            var res = nlu.RecuperarMail(rfc, email);
            Logger.Debug(res);
            return res;
        }


        public UsuarioLocal ObtenerUsuarioById(string userId)
        {
            var usuario = NtLinkUsuarios.GetUser(userId);
            NtLinkUsuarios us = new NtLinkUsuarios();
            var roles = NtLinkUsuarios.GetRolesForUser(usuario.UserName);
            Logger.Debug(usuario.ProviderUserKey);
            UserProfile p = UserProfile.GetUserProfile(usuario.UserName);
            return new UsuarioLocal
            {
                Perfil = roles[0],
                UserId = new Guid(usuario.ProviderUserKey.ToString()),
                UserName = usuario.UserName,
                NombreCompleto = p.NombreCompleto,
                Iniciales = p.Iniciales,
                Email = usuario.Email,
                CambiarPassword = p.CambiarPassword
            };

        }


        public List<clientes> ListaClientes(string perfil, int idEmpresa, string filtro, bool lista)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var list = clientes.GetList(perfil, idEmpresa, filtro, lista);
            list = list.Where(p => p.Tipo == 0).ToList();
            Logger.Debug("Resultado: " + list.Count + " regs.");
            if (lista)
                list.Add(new clientes { RazonSocial = "Todos", idCliente = 0, Tipo = 0 });

            return list;
        }

        public List<clientes> ListaEmpleados(string perfil, int idEmpresa, string filtro, bool lista)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var list = clientes.GetList(perfil, idEmpresa, filtro, lista);
            Logger.Debug("Resultado: " + list.Count + " regs.");
            list = list.Where(p => p.Tipo == 1).ToList();
            return list;
        }

        public List<clientes> ListaClientesGaf(string linea)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var list = clientes.GetList(linea);
            Logger.Debug("Resultado: " + list.Count + " regs.");
            return list;
        }

        public bool GuardarListaFacturas(List<vventas> lista)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var res = ventas.SaveList(lista);
            Logger.Debug(res);
            return res;
        }

        public byte[] FacturaXml(string uuid)
        {
            return NtLinkFactura.GetXmlData(uuid);
        }

        public byte[] FacturaPdf(string uuid)
        {
            return NtLinkFactura.GetPdfData(uuid);
        }

        //public byte[] FacturaXml(int idVenta)
        // {
        //     throw new NotImplementedException();
        // }

        //public byte[] FacturaPdf(int idVenta)
        //{
        //    throw new NotImplementedException();
        //}

        public int GuardarCliente(clientes cliente)
        {
            Regex inv = new Regex("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (!inv.IsMatch(cliente.Email))
            {
                throw new FaultException("Email incorrecto");
            }
            NtLinkClientes c = new NtLinkClientes();
            var res = c.SaveCliente(cliente);
            Logger.Debug(res);
            return res;
        }

        public bool GuardarEmpresa(empresa empresa, byte[] cert, byte[] llave, string passwordLlave, byte[] logo, string formatoLlave)
        {
            bool result;
            NtLinkEmpresa nle = new NtLinkEmpresa();
            if (cert == null || llave == null)
            {
                result = nle.Save(empresa, logo);
            }
            else
            {
                result = nle.Save(empresa, cert, llave, passwordLlave, logo, formatoLlave);
            }
            Logger.Debug(result);
            return result;
        }

        public string SiguienteFolioFactura(int idEmpresa)
        {
            var result = NtLinkFactura.GetNextFolio(idEmpresa);
            Logger.Debug(result);
            return result;

        }

        public string TipoCambio()
        {
            var res = NtLInkTipoCambio.GetTipoCambioUsd();
            Logger.Debug(res);
            return res;
        }

        public List<producto> BuscarProducto(string query, int idEmpresa)
        {
            try
            {
                NtLinkProducto prod = new NtLinkProducto();
                var result = prod.ProductSearch(query, idEmpresa);
                Logger.Debug("Resultado: " + result.Count + " regs.");
                return result;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                throw new FaultException("Ocurrió un error de comunicación");
            }

        }

        public producto ObtenerProductoById(int id)
        {
            return new NtLinkProducto().GetProduct(id);
        }


        //
        private void ValidaDatosVaciosComprobante(ref NtLinkFactura fact)            
        {
            fact.Receptor.Calle = string.IsNullOrEmpty(fact.Receptor.Calle) ? null : fact.Receptor.Calle;
            fact.Receptor.NoExt = string.IsNullOrEmpty(fact.Receptor.NoExt) ? null : fact.Receptor.NoExt;
            fact.Receptor.NoInt = string.IsNullOrEmpty(fact.Receptor.NoInt) ? null : fact.Receptor.NoInt;
            fact.Receptor.Colonia = string.IsNullOrEmpty(fact.Receptor.Colonia) ? null : fact.Receptor.Colonia;
            fact.Receptor.CP = string.IsNullOrEmpty(fact.Receptor.CP) ? null : fact.Receptor.CP;
            fact.Receptor.Ciudad = string.IsNullOrEmpty(fact.Receptor.Ciudad) ? null : fact.Receptor.Ciudad;
            fact.Receptor.Estado = string.IsNullOrEmpty(fact.Receptor.Estado) ? null : fact.Receptor.Estado;
            fact.Receptor.Pais = string.IsNullOrEmpty(fact.Receptor.Pais) ? null : fact.Receptor.Pais;

            fact.Emisor.Calle = string.IsNullOrEmpty(fact.Emisor.Calle) ? null : fact.Emisor.Calle;
            fact.Emisor.NoExt = string.IsNullOrEmpty(fact.Emisor.NoExt) ? null : fact.Emisor.NoExt;
            fact.Emisor.NoInt = string.IsNullOrEmpty(fact.Emisor.NoInt) ? null : fact.Emisor.NoInt;
            fact.Emisor.Colonia = string.IsNullOrEmpty(fact.Emisor.Colonia) ? null : fact.Emisor.Colonia;
            fact.Emisor.CP = string.IsNullOrEmpty(fact.Emisor.CP) ? null : fact.Emisor.CP;
            fact.Emisor.Ciudad = string.IsNullOrEmpty(fact.Emisor.Ciudad) ? null : fact.Emisor.Ciudad;
            fact.Emisor.Estado = string.IsNullOrEmpty(fact.Emisor.Estado) ? null : fact.Emisor.Estado;
            /*
            fact.Factura.nomina.nomina.Departamento = string.IsNullOrEmpty(fact.Factura.nomina.nomina.Departamento) ? null : fact.Factura.nomina.nomina.Departamento;
            fact.Factura.nomina.nomina.NumSeguridadSocial = string.IsNullOrEmpty(fact.Factura.nomina.nomina.NumSeguridadSocial) ? null : fact.Factura.nomina.nomina.NumSeguridadSocial;
            fact.Factura.nomina.nomina.CLABE = string.IsNullOrEmpty(fact.Factura.nomina.nomina.CLABE) ? null : fact.Factura.nomina.nomina.CLABE;
            fact.Factura.nomina.nomina.Puesto = string.IsNullOrEmpty(fact.Factura.nomina.nomina.Puesto) ? null : fact.Factura.nomina.nomina.Puesto;
            fact.Factura.nomina.nomina.TipoContrato = string.IsNullOrEmpty(fact.Factura.nomina.nomina.TipoContrato) ? null : fact.Factura.nomina.nomina.TipoContrato;
            */
        //    fact.Factura.nomina.nomina.SalarioBaseCotAporSpecified = fact.Factura.nomina.nomina.SalarioBaseCotApor == -1 ? false : true;
        //    fact.Factura.nomina.nomina.SalarioBaseCotApor = fact.Factura.nomina.nomina.SalarioBaseCotApor == -1 ? 0 : fact.Factura.nomina.nomina.SalarioBaseCotApor;

        //    fact.Factura.nomina.nomina.SalarioDiarioIntegradoSpecified = fact.Factura.nomina.nomina.SalarioDiarioIntegrado == -1 ? false : true;
        //    fact.Factura.nomina.nomina.SalarioDiarioIntegrado = fact.Factura.nomina.nomina.SalarioDiarioIntegrado == -1 ? 0 : fact.Factura.nomina.nomina.SalarioDiarioIntegrado;

        //    fact.Factura.nomina.nomina.FechaInicioRelLaboralSpecified = fact.Factura.nomina.nomina.FechaInicioRelLaboral == DateTime.MinValue ? false : true;
        //    fact.Factura.nomina.nomina.AntiguedadSpecified = fact.Factura.nomina.nomina.FechaInicioRelLaboralSpecified;
        }
        //

        public bool GuardarFactura(facturas fact, List<facturasdetalle> detalles, bool enviar, List<facturasdetalle> conceptosAduana)
        {

            Logger.Debug(fact.Folio);
            NtLinkClientes nlc = new NtLinkClientes();
            clientes cliente = nlc.GetCliente(fact.idcliente);
            NtLinkEmpresa emp = new NtLinkEmpresa();
            empresa empresa = emp.GetById(fact.IdEmpresa.Value);
            NtLinkFactura fac = new NtLinkFactura(0);
            if (string.IsNullOrEmpty(empresa.RegimenFiscal))
            {
                throw new FaultException("Debes capturar el regimen fiscal de la empresa");
            }

            fact.Regimen = empresa.RegimenFiscal;
            fac.Emisor = empresa;
            fac.Receptor = cliente;
            fac.Detalles = detalles;
            fac.Factura = fact;
            fact.ConceptosAduanera = conceptosAduana;

            ValidaDatosVaciosComprobante(ref fac);

            Comprobante cfd = NtLinkFactura.GeneraCfd(fac, enviar);
            if (cfd != null)
            {
                fac.Factura.Uid = cfd.Complemento.timbreFiscalDigital.UUID;
                fac.Save();
                //VERIFY SAVE ID VENTA IN CARTA PORTE
                if (fact.ConceptosCartaPortes != null && fact.TipoDocumento == TipoDocumento.CartaPorte)
                {
                    using (var db = new NtLinkLocalServiceEntities())
                    {

                        if (fact.ConceptosCartaPortes.Count > 0)
                        {
                            long temporal = fact.ConceptosCartaPortes[0].idComprobantePdf;
                            var tempCompareCartaPorte = from p in db.ConceptosCartaPorte
                                                        where p.idComprobantePdf == temporal
                                                        select p;
                            foreach (var conceptosCartaPorte in tempCompareCartaPorte)
                            {
                                conceptosCartaPorte.idVenta = fact.idVenta;
                            }
                            db.SaveChanges();
                        }

                    }
              }
                return true;
          }
            return false;

        }

        public byte[] FacturaPreview(facturas fact, List<facturasdetalle> detalles, List<facturasdetalle> conceptosAduana)
        {
            Logger.Info("FacturaPreview");
            try
            {

                NtLinkClientes nlc = new NtLinkClientes();
                clientes cliente = nlc.GetCliente(fact.idcliente);
                NtLinkEmpresa emp = new NtLinkEmpresa();
                empresa empresa = emp.GetById(fact.IdEmpresa.Value);
                NtLinkFactura fac = new NtLinkFactura(0);
                fac.Emisor = empresa;
                fac.Receptor = cliente;
                fac.Detalles = detalles;
                fac.Factura = fact;
                fac.Factura.ConceptosAduanera = conceptosAduana;
                fac.Factura.Regimen = empresa.RegimenFiscal;

                var cfd = NtLinkFactura.GeneraPreviewRs(fac);
                return cfd;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return null;
            }
        }

        public clientes ObtenerClienteById(int idCliente)
        {
            return new NtLinkClientes().GetCliente(idCliente);
        }

        public empresa ObtenerEmpresaById(int idEmpresa)
        {
            return new NtLinkEmpresa().GetById(idEmpresa);
        }

        public List<UsuarioLocal> UsuariosLista(int idEmpresa)
        {
            var usuarios = NtLinkUsuarios.GetUserList(idEmpresa);
            return usuarios.Select(p => new UsuarioLocal
            {
                Bloqueado = p.IsLockedOut,
                Email = p.Email,
                Perfil = (NtLinkUsuarios.GetRolesForUser(p.UserName).Any() ? NtLinkUsuarios.GetRolesForUser(p.UserName)[0] : ""),
                UserId = new Guid(p.ProviderUserKey.ToString()),
                UserName = p.UserName
            }).ToList();
        }

        public List<string> ObtenerPerfiles()
        {
            var perfiles = NtLinkUsuarios.GetRoles();
            Logger.Debug(perfiles.Length.ToString());
            return perfiles.ToList();
        }

        //public bool GuardarUsuario(string userName, string password, string eMail, int idEmpresa, string perfil, string nombreCompleto, string iniciales)

        public bool GuardarUsuario(string nombreCompleto, string eMail, string password, int idEmpresa, string perfil, string userName, string iniciales)
        {
            var res = NtLinkUsuarios.CreateUser(eMail, password, eMail, idEmpresa, perfil, nombreCompleto, iniciales);
            Logger.Debug(res);
            return res;
        }


        public void SendMail(List<string> recipients, List<string> attachments, string message, string subject, string fromEmail, string fromDescription)
        {
            Mailer m = new Mailer();
            m.Send(recipients, attachments, message, subject, fromEmail, fromDescription);
        }

        public void SendMailByteArray(List<string> recipients, List<EmailAttachment> attachments, string message, string subject, string fromEmail, string fromDescription)
        {
            Mailer m = new Mailer();
            m.Send(recipients, attachments, message, subject, fromEmail, fromDescription);
        }



        public void Pagarfactura(int idVenta, DateTime fechaPago, string referenciaPago)
        {
            NtLinkFactura.Pagar(idVenta, fechaPago, referenciaPago);
        }




        public bool CambiarPassword(string userId, string password)
        {
            try
            {
                MembershipUser user = NtLinkUsuarios.GetUser(userId);
                var res = NtLinkUsuarios.UpdateUserPassword(user, password);
                empresa em = NtLinkUsuarios.GetEmpresaByUserId(userId);
                if (em != null)
                {
                    NtLinkEmpresa nle = new NtLinkEmpresa();
                    em.PrimeraVez = false;
                    nle.Save(em, null);
                }
                else
                {
                    Distribuidores dis = NtLinkUsuarios.GetDisByUserId(userId);
                    NtLinkDistribuidor nle = new NtLinkDistribuidor();
                    dis.PrimeraVez = false;
                    nle.Save(dis);
                }
                Logger.Debug(user + "->" + res);
                return res;
            }
            catch (FaultException ee)
            {
                Logger.Debug(ee);
                throw;
            }
            catch (Exception ee)
            {
                Logger.Debug(ee);
                return false;
            }

        }


        public List<Sucursales> ListaSucursales(int idEmpresa)
        {
            NtLinkSucursales suc = new NtLinkSucursales();
            return suc.GetSucursalLista(idEmpresa);

        }

        public List<Comisionistas> ListaComisionistas(int idEmpresa)
        {
            var com = new NtLinkComisionistas();
            return com.GetComisionistasLista(idEmpresa);
        }

        public Sucursales ObtenerSucursal(int idSucursal)
        {
            NtLinkSucursales suc = new NtLinkSucursales();
            return suc.GetSucursal(idSucursal);
        }

        public Comisionistas ObtenerComisionista(int idComisionista)
        {
            NtLinkComisionistas suc = new NtLinkComisionistas();
            return suc.GetComisionista(idComisionista);
        }

        public bool GuardaSucursal(Sucursales sucursal)
        {
            NtLinkSucursales suc = new NtLinkSucursales();
            return suc.SaveSucursal(sucursal);
        }

        public bool GuardaComisionista(Comisionistas comisionista)
        {
            NtLinkComisionistas com = new NtLinkComisionistas();
            return com.SaveComisionista(comisionista);
        }

        public Sistemas ObtenerSistema(string rfc)
        {
            NtLinkSistema sistema = new NtLinkSistema();
            return sistema.GetSistema(rfc);
        }

        public Sistemas ObtenerSistemaById(int idSistema)
        {
            NtLinkSistema sistema = new NtLinkSistema();
            return sistema.GetSistema(idSistema);
        }

        public List<Sistemas> ListaSistemas(string filtro)
        {
            NtLinkSistema sistema = new NtLinkSistema();
            return sistema.GetSistemasLista(filtro);
        }


        public bool GuardarSistema(Sistemas sistema, ref string resultado, string nombreCompleto, string iniciales)
        {
            NtLinkSistema sis = new NtLinkSistema();
            return sis.SaveSistema(sistema, ref resultado, nombreCompleto, iniciales);
        }

        public bool GuardarDistribuidor(Distribuidores distribuidor, ref string resultado, string nombreCompleto, string iniciales)
        {
            NtLinkDistribuidor dis = new NtLinkDistribuidor();
            return dis.SaveDistribuidor(distribuidor, ref resultado, nombreCompleto, iniciales);
        }

        public usuarios AdminLogin(string user, string password)
        {
            NtLinkUsuarios nlu = new NtLinkUsuarios();
            return nlu.LoginAdmin(user, password);
        }

        public List<UsuarioLocal> UsuariosObtenerLista(string patron)
        {
            try
            {
                var usuarios = NtLinkUsuarios.GetUserList();
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var usu_emp = db.usuarios_empresas.ToList();
                    var emp = db.empresa.ToList();
                    return
                        usuarios.Where(u => u.UserName.ToLowerInvariant().Contains(patron.ToLowerInvariant())).Select(
                            p =>
                            {
                                var guid = new Guid(p.ProviderUserKey.ToString());
                                string id = guid.ToString();
                                var em = usu_emp.FirstOrDefault(o => o.UserId == id);
                                var datos = em == null ? null : emp.FirstOrDefault(
                                    i => i.IdEmpresa == em.IdEmpresa);
                                var firstOrDefault = datos == null ? "Distribuidor" : datos.RazonSocial;
                                return firstOrDefault != null
                                           ? new UsuarioLocal
                                           {
                                               Bloqueado = p.IsLockedOut,
                                               Email = p.Email,
                                               Perfil =
                                                   (NtLinkUsuarios.GetRolesForUser(p.UserName).Any()
                                                        ? NtLinkUsuarios.GetRolesForUser(p.UserName)[0]
                                                        : ""),
                                               UserId = guid,
                                               UserName = p.UserName,
                                               RazonSocial = firstOrDefault
                                           }
                                           : null;
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }

        public void DesbloquearUsuario(string userName)
        {
            NtLinkUsuarios.DesbloquearUsuario(userName);
        }


        public bool EditarUsuario(UsuarioLocal usuario)
        {
            try
            {
                var c = NtLinkUsuarios.UpdateUser(usuario.UserId.ToString(), usuario.NombreCompleto, usuario.Iniciales, usuario.Perfil);
                Logger.Debug(c);
                return c;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }

        public bool GuardarPromotores(Promotores promotor)
        {
            Logger.Debug(promotor.IdPromotor);
            NtLinkClientes promo = new NtLinkClientes();
            var save = promo.GuardarPromotor(promotor);
            Logger.Debug(save);
            return save;
        }

        public List<Promotores> ListaPromotores(int idSistema)
        {
            NtLinkClientes list = new NtLinkClientes();
            return list.ListaPromotores(idSistema);
        }

        public Promotores ObtenerPromotor(int idPromotor)
        {
            NtLinkClientes NumProm = new NtLinkClientes();
            return NumProm.ObtenerPromotores(idPromotor);
        }


        public List<vClientesPromotores> ListaClientesPromotores(int idCliente)
        {
            NtLinkClientes list = new NtLinkClientes();
            return list.ListaClientesPromotores(idCliente);
        }

        public bool GuardarClientesPromotores(int idCliente, int idPromotor)
        {
            NtLinkClientes guardar = new NtLinkClientes();
            return guardar.GuardarClientesPromotores(idCliente, idPromotor);
        }

        public bool BorrarClientesPromotores(int idCP)
        {
            NtLinkClientes borrar = new NtLinkClientes();
            return borrar.BorrarClientesPromotores(idCP);
        }

        public bool AplicarPago(decimal importe, DateTime fecha, string observaciones, decimal pendiente, string referencia, List<FacturasPagos> facturas, int idcliente)
        {
            try
            {
                NtLinkPagos pago = new NtLinkPagos();
                Pagos p = new Pagos()
                {
                    Importe = importe,
                    Fecha = fecha,
                    Observaciones = observaciones,
                    IdCliente = idcliente,
                    Pendiente = pendiente,
                    Referencia = referencia,
                    Tipo = false
                };
                int idPago = pago.GuardarPago(p);
                pago.AplicarPago(idPago, facturas, fecha);
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }

        }

        public List<vClientesPromotores> ListaPromotoresClientes(int idCliente)
        {
            NtLinkClientes c = new NtLinkClientes();
            return c.ListaPromotoresClientes(idCliente);
        }




        public List<producto> ListaProductoGaf(int idEmpresa, string texto)
        {
            NtLinkProducto ProductoGaf = new NtLinkProducto();
            return ProductoGaf.ListaProductoGaf(idEmpresa, texto);
        }

        public void UpdateDistribuidor(int idContrato)
        {
            NtLinkDistribuidor dis = new NtLinkDistribuidor();
            dis.UpdateDistribuidor(idContrato);
        }

        public Distribuidores ObtenerDIsById(string userId)
        {
            var dis = NtLinkUsuarios.GetDisByUserId(userId);
            if (dis != null)
                Logger.Debug(dis.IdDistribuidor);
            return dis;
        }

        public string ValidarCSD(empresa empresa, byte[] cert, byte[] llave, string passwordLlave, string formatoLlave)
        {
            string result;
            NtLinkEmpresa nle = new NtLinkEmpresa();
            result = nle.ValidaCSD(empresa, cert, llave, passwordLlave, formatoLlave);
            Logger.Debug(result);
            return result;
        }
        //conecta a iserviciolocal  comision distribuidores con conexion ala base de datos
        public List<Comision_Distribuidores> listaComDis()
        {
            NtLinkDistribuidor Cm = new NtLinkDistribuidor();
            return Cm.listaComisiones();

        }

        //conect isserviciolocal 
        public List<Ctdistribuidores> lisDistribuidores()
        {
            NtLinkDistribuidor Ct = new NtLinkDistribuidor();
            return Ct.lisDistribuidores();

        }

        public List<vventas> ListaNomina(DateTime inicio, DateTime end, int idEmpresa, int idClientem, string status, string linea = null, string iniciales = null)
        {
            NtLinkVentas ventas = new NtLinkVentas();
            var lista = ventas.GetListNomina(inicio, end, idEmpresa, status, idClientem, linea, iniciales);
            Logger.Debug("Resultado: " + lista.Count + " regs.");
            return lista;
        }


        public List<vFacturasPagos> ListaAplicados(int idPago)
        {
            NtLinkPagos pagos = new NtLinkPagos();
            var lista = pagos.ListaAplicados(idPago);
            if (lista != null)
                Logger.Info(lista.Count);
            return lista;
        }

        public DatosNomina ObtenerDatosNomina(int idCLiente)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var datos = clientes.GetDatosByCliente(idCLiente);

            return datos;
        }

        public bool SaveDatosNomina(DatosNomina datos)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var res = clientes.SaveDatosNomina(datos);
            Logger.Info(res);
            return res;

        }
        public bool SaveSubcontratacionNomina(SubContratacion datos)
        {
            NtLinkClientes clientes = new NtLinkClientes();
            var res = clientes.SaveSubcontratacioNomina(datos);
            Logger.Info(res);
            return res;
        }

        public bool CancelarPago(int idPago)
        {
            NtLinkPagos pagos = new NtLinkPagos();
            var pago = pagos.CancelarPago(idPago);
            Logger.Info(idPago + "->" + pago);
            return pago;
        }

        public List<Pagos> ListaPagos(int idCliente, DateTime fechaInicial, DateTime fechaFinal)
        {
            NtLinkPagos pagos = new NtLinkPagos();
            var lista = pagos.ListaPagos(idCliente, fechaInicial, fechaFinal);
            if (lista != null)
                Logger.Info(lista.Count);
            return lista;
        }

        public bool GuardarAnticipo(Pagos pago)
        {
            NtLinkPagos pagos = new NtLinkPagos();
            var res = pagos.GuardarPago(pago);
            Logger.Info(res);
            return (res != 0);
        }



        public CentrosTrabajo ObtenerCentroById(int idCentro)
        {
            var cen = new NtLInkCentrosTrabajo();
            var centro = cen.ObtenerCentroById(idCentro);
            if (centro != null)
                Logger.Info(centro.Nombre);
            return centro;
        }

        public List<CentrosTrabajo> ListaCentros(int idEmpresa)
        {
            var centro = new NtLInkCentrosTrabajo();
            return centro.ListaCentros(idEmpresa);
        }

        public bool GuardarCentro(CentrosTrabajo centro)
        {
            var ct = new NtLInkCentrosTrabajo();
            return ct.GuardarCentro(centro);
        }

        public bool SaveChangesNtLinkCartaPorte(ConceptosCartaPorte concepto)
        {
            //CARTA PORTE .. 
            var cartaporte = new NtLinkCartaPorte();
            return cartaporte.SaveConceptoCartaPorte(concepto);
        }
    }
}
