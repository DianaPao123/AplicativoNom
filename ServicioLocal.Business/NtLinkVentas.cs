using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicioLocalContract;


namespace ServicioLocal.Business
{
    public class NtLinkVentas : NtLinkBusiness
    {
        public List<vventas> GetListCuentas(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null)
        {
            try
            {
                List<vventas> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            if(idCliente==0)
                            {
                                lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                              p.fecha <= fechaFinal && p.Linea == linea).ToList();    
                            }
                            else
                            {
                                lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Linea == linea &&
                                                          p.IdCliente == idCliente).ToList();
                            }
                            
                        }


                    }
                    else if (idCliente == 0)
                    {
                        if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Linea == linea).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventas.Where(
                               p => p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                    p.fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventas.Where(
                                  p => p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                       p.fecha <= fechaFinal && p.Linea == linea).ToList();
                        }


                    }
                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status || p.StatusFactura == "Pagado").ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }

        }
        public List<vventas> GetList(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null, string iniciales = null)
        {
            try
            {
                List<vventas> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Usuario == iniciales).OrderBy(p => p.folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).OrderBy(p => p.folio).ToList();
                        }
                        else
                        {
                            if (idCliente == 0)
                            {
                                lista = db.vventas.Where(p => p.fecha >= fechaInicial &&
                                                              p.fecha <= fechaFinal && p.Linea == linea).OrderBy(p => p.folio).ToList();
                            }
                            else
                            {
                                // order by folio
                                lista = db.vventas.Where(p => p.fecha >= fechaInicial && p.fecha <= fechaFinal && p.Linea == linea &&
                                                          p.IdCliente == idCliente).OrderBy(p => p.folio).ToList();
                            }

                        }

                        //Lista de REportes
                    }
                    else if (idCliente == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Usuario == iniciales).OrderByDescending(p => p.folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).OrderByDescending(p => p.folio).ToList();
                        }
                        else
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.folio).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventas.Where(
                               p => p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                    p.fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventas.Where(
                                  p => p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                       p.fecha <= fechaFinal && p.Linea == linea).ToList();
                        }


                    }
                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }
           
        }

        public List<vventas> GetListNomina(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null, string iniciales = null)
        {
            try
            {
                List<vventas> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 &&  p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Usuario == iniciales).OrderBy(p => p.folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).OrderBy(p => p.folio).ToList();
                        }
                        else
                        {
                            if (idCliente == 0)
                            {
                                lista = db.vventas.Where(p => p.Tipo == 1 && p.fecha >= fechaInicial &&
                                                              p.fecha <= fechaFinal && p.Linea == linea).OrderBy(p => p.folio).ToList();
                            }
                            else
                            {
                                // order by folio
                                lista = db.vventas.Where(p => p.Tipo == 1 && p.fecha >= fechaInicial && p.fecha <= fechaFinal && p.Linea == linea &&
                                                          p.IdCliente == idCliente).OrderBy(p => p.folio).ToList();
                            }

                        }

                        //Lista de REportes
                    }
                    else if (idCliente == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Usuario == iniciales).OrderByDescending(p => p.folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal).OrderByDescending(p => p.folio).ToList();
                        }
                        else
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.fecha >= fechaInicial &&
                                                          p.fecha <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.folio).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventas.Where(
                               p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                    p.fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventas.Where(
                                  p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.IdCliente == idCliente && p.fecha >= fechaInicial &&
                                       p.fecha <= fechaFinal && p.Linea == linea).ToList();
                        }


                    }
                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }

        }


        public List<vPagos> GetListCuentas(int idCliente, string folio)
        {
            try
            {
                List<vPagos> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idCliente != 0)
                    {
                        lista =
                            new List<vPagos>(
                                db.vPagos.Where(p => p.idcliente == idCliente && p.folio == folio).ToList().OrderBy(
                                    p => p.folio));
                    }
                    else
                    {
                        lista =
                            new List<vPagos>(
                                db.vPagos.Where(p => p.folio == folio).ToList().OrderBy(
                                    p => p.folio));
                    }
                }

                return lista.ToList();
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPagos>();
            }

        }
        public List<vPagos> GetListCuentas(int idventa)
        {
            try
            {
                
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var lista = db.vPagos.Where(p => p.idventa == idventa);
                    return lista.ToList();
                }
                
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPagos>();
            }

        }


        public ComprobantePdf GetComprobantePdf(string uudi)
        {
            ComprobantePdf result;
            try
            {
                using (NtLinkLocalServiceEntities db = new NtLinkLocalServiceEntities())
                {
                    ComprobantePdf lista = (from p in db.ComprobantePdf
                                            where p.timbre_UUID == uudi
                                            select p).FirstOrDefault<ComprobantePdf>();
                    result = lista;
                }
            }
            catch (Exception eee)
            {
                NtLinkBusiness.Logger.Error(eee.Message);
                result = new ComprobantePdf();
            }
            return result;
        }


        public vventas GetById(int idVenta)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var x = db.vventas.FirstOrDefault(p => p.idventa == idVenta);
                    return x;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }
        }

        public bool SaveList (List<vventas> lista)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    foreach (vventas ventas in lista)
                    {
                        vventas ventas1 = ventas;
                        facturas f = db.facturas.Where(p => p.idVenta == ventas1.idventa).First();
                        f.Cancelado = ventas.Cancelado;
                        f.FechaPago = ventas.FechaPago;
                        f.ReferenciaPago = ventas.ReferenciaPago;
                        f.Vencimiento = ventas.Vencimiento;
                        f.Proyecto = ventas.Proyecto;
                        //_entities.facturas.ApplyCurrentValues(f);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return false;
            }
            
        }

        


    }
}
