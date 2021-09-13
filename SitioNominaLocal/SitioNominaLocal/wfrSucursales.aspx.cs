using System;
using System.ServiceModel;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrSucursales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idSucursalString = this.Request.QueryString["idSucursal"];
                int idSucursal;
                if (!string.IsNullOrEmpty(idSucursalString) && int.TryParse(idSucursalString, out idSucursal) && this.Request.QueryString["idEmpresa"] != null)
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        Sucursales sucursal = cliente.ObtenerSucursal(idSucursal);
                       // txtIdEmpresa.Value = this.Request.QueryString["idEmpresa"];
                        //if (sucursal.IdEmpresa != (int) Session["idEmpresa"])
                        //{
                        //    this.Response.Write("No puedes editar esta Sucursal");
                        //    this.Response.End();
                        //}

                        this.txtNombre.Text = sucursal.Nombre;
                        this.txtLugarExpedicion.Text = sucursal.LugarExpedicion;
                        this.txtDomicilio.Text = sucursal.Direccion;
                        ViewState["sucursal"] = sucursal;
                    }
                }
                txtIdEmpresa.Value = this.Request.QueryString["idEmpresa"];
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var sucursal = ViewState["sucursal"] as Sucursales ?? new Sucursales
                                                                      {
                                                                          IdEmpresa = int.Parse(txtIdEmpresa.Value)
                                                                      };
            sucursal.Nombre = this.txtNombre.Text;
            sucursal.LugarExpedicion = this.txtLugarExpedicion.Text;
            sucursal.Direccion = this.txtDomicilio.Text;
            
             var cliente = NtLinkClientFactory.Cliente();
             using (cliente as IDisposable)
             {
                 try
                 {
                     if (cliente.GuardaSucursal(sucursal))
                     {
                         this.Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa="  + txtIdEmpresa.Value);
                     }
                     else
                     {
                         this.lblError.Text = "No se puedo guardar la sucursal";
                     }
                 }
                 catch (FaultException ex)
                 {
                     this.lblError.Text = ex.Message;
                 }
             }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa=" + txtIdEmpresa.Value);
        }
    }
}