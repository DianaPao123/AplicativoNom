using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicioLocalContract;

namespace ServicioLocal.Business
{
    public class NtLInkCentrosTrabajo : NtLinkBusiness
    {
        public List<CentrosTrabajo> ListaCentros(int idEmpresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var centros = db.CentrosTrabajo.Where(p => p.IdEmpresa == idEmpresa);
                    return centros.ToList();
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

        public CentrosTrabajo ObtenerCentroById(int idCentro)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var centros = db.CentrosTrabajo.FirstOrDefault((p) => p.IdCentroTrabajo == idCentro);
                    return centros;
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


        public bool GuardarCentro(CentrosTrabajo centro)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (centro.IdCentroTrabajo == 0)
                    {
                        db.CentrosTrabajo.AddObject(centro);
                    }
                    else
                    {
                        var c = db.CentrosTrabajo.FirstOrDefault(p => p.IdCentroTrabajo == centro.IdCentroTrabajo);
                        db.CentrosTrabajo.ApplyCurrentValues(centro);
                    }
                    db.SaveChanges();
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

    }
}
