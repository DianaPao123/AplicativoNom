using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicioLocalContract;



namespace ServicioLocal.Business
{
    public class NtLinkProducto : NtLinkBusiness
    {
        public List<producto> ListaProductoGaf(int idEmpresa, string texto)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var productosGaf = db.producto.Where(p=>p.Descripcion.Contains(texto) || p.Codigo.Contains(texto)).ToList();
                    Logger.Info(productosGaf.Count);
                    return productosGaf;
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


        public List<producto> ProductSearch(string query, int idEmpresa)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (!string.IsNullOrEmpty(query))
                    {
                        var x = db.producto.Where(p => p.IdEmpresa == idEmpresa &&(p.Descripcion.ToLower().Contains(query) ||
                                                              p.Codigo.ToLower().Contains(query) ||
                                                              p.Observaciones.ToLower().Contains(query))).ToList();
                        return x;
                    }
                    else return db.producto.Where(p=>p.IdEmpresa == idEmpresa).ToList();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }


        public producto GetProduct(int idProducto)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var x = db.producto.Where(p => p.IdProducto == idProducto).FirstOrDefault();
                    return x;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }



        public bool SaveProducto(producto prod)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (prod.IdProducto == 0)
                    {
                        //prod.Creacion = DateTime.Now;
                        prod.Creacion = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);


                        //prod.Modificacion = DateTime.Now;
                        prod.Modificacion = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);
                        //TODO: Buscar usuario de modificacion
                        prod.IdUsuario = 1;
                        db.producto.AddObject(prod);
                    }
                    else
                    {
                        var x = db.producto.FirstOrDefault(p => p.IdProducto == prod.IdProducto);
                        prod.Creacion = x.Creacion;
                        //prod.Modificacion = DateTime.Now;
                        prod.Modificacion = AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow);
                        db.producto.ApplyCurrentValues(prod);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException.Message);
                return false;
            }
        }
        
    }
}
