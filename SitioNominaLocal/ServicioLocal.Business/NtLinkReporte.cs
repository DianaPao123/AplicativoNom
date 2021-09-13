using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicioLocalContract;

namespace ServicioLocal.Business
{
    public class NtLinkReporte : NtLinkBusiness
    {

       


       
        public static List<ElementoReporte> ObtenerReportePorEmisor(int mes, int anio, int idEmpresa)
        {
            var listaReporte = new List<ElementoReporte>();
            using (var db = new NtLinkLocalServiceEntities())
            {
                var emisor = db.empresa.First(e => e.IdEmpresa == idEmpresa);
                var elementoReporte = new ElementoReporte
                                          {
                                              Cancelados = db.vventas.Count(
                                                    f =>
                                                    f.fecha.Year == anio &&
                                                    f.RfcEmisor == emisor.RFC &&
                                                    f.Cancelado == 1),
                                              Cliente = emisor.RazonSocial,
                                              Rfc = emisor.RFC,
                                              
                                          };
                listaReporte.Add(elementoReporte);
            }
            return listaReporte;
        }

        public static List<ElementoReporte> ObtenerReporteFullEmisor(int mes, int anio, int idSistema)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var emisor = db.empresa.Where(e => e.idSistema == idSistema).ToList();
                    var ventas = db.vventas.OrderBy(p => p.IdEmpresa);
                    if (mes != 0)
                    {
                        return emisor.Select(p =>
                                             new ElementoReporte()
                                                 {
                                                     Cancelados = ventas.Count(
                                                         f =>
                                                         f.fecha.Year == anio &&
                                                         f.fecha.Month == mes &&
                                                         f.RfcEmisor.Equals(p.RFC,
                                                                            StringComparison.
                                                                                InvariantCultureIgnoreCase) &&
                                                         f.Cancelado == 1),
                                                     Cliente = p.RazonSocial,
                                                     Rfc = p.RFC,
                                                     
                                                 }).ToList();
                    }
                    else
                    {
                        return emisor.Select(p =>
                                         new ElementoReporte()
                                         {
                                             Cancelados = ventas.Count(
                                                    f =>
                                                    f.RfcEmisor.Equals(p.RFC,
                                                                        StringComparison.
                                                                            InvariantCultureIgnoreCase) &&
                                                    f.Cancelado == 1),
                                             Cliente = p.RazonSocial,
                                             Rfc = p.RFC,
                                            
                                         }).ToList();
                    }

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

        
    }
}
