using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrEmpresa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                string idEmpresaString = this.Request.QueryString["idEmpresa"];
                int idEmpresa;
                if(!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idEmpresa))
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    empresa empresa;
                    using (clienteServicio as IDisposable)
                    {
                        empresa = clienteServicio.ObtenerEmpresaById(idEmpresa);
                        var sistema = clienteServicio.ObtenerSistemaById((int)empresa.idSistema.Value);
                    }
                    this.txtRFC.Enabled = false;
                    
                    
                    this.FillView(empresa);
                    empresa.Logo = null;
                    ViewState["empresa"] = empresa;
                }
                else
                {
                    this.txtRFC.Enabled = true;
                    this.pnlSucursal.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var empresa = ViewState["empresa"] as empresa;
            var cliente = NtLinkClientFactory.Cliente();
            byte[] cert = null;
            byte[] key = null;

            string extension = "";
            if (ViewState["Extension"] != null)
                extension = ViewState["Extension"].ToString();
            if (this.fuLogoEmpresa.HasFile)
            {
                if (this.fuLogoEmpresa.FileBytes.Length > (50 * 1024))
                {
                    this.lblError.Text = "El tamaño del archivo de logo no debe exceder los 50 Kb.";
                    return;
                }
            }
            if (this.fuCertificado.HasFile)
            {
                cert = this.fuCertificado.FileBytes;
            }
            if (this.fuLlave.HasFile)
            {
                key = this.fuLlave.FileBytes;
                extension = Path.GetExtension(fuLlave.FileName).ToLower();
                
            }
            if(empresa != null)
            {                
                empresa modEmpresa = this.GetEmpresaFromView();
                if (!string.IsNullOrEmpty(this.txtPassWordLlave.Text) && this.fuCertificado.HasFile && this.fuLlave.HasFile)
                {
                    modEmpresa.PassKey = this.txtPassWordLlave.Text;
                }
                else
                {
                    modEmpresa.PassKey = empresa.PassKey;
                }
                modEmpresa.IdEmpresa = empresa.IdEmpresa;
                modEmpresa.idSistema = empresa.idSistema;
                modEmpresa.RutaKey = extension;
                using (cliente as IDisposable)
                {
                    try
                    {
                        
                        cliente.GuardarEmpresa(modEmpresa, cert, key, this.txtPassWordLlave.Text, this.GetLogoBytes(), extension);
                        this.Response.Redirect("wfrEmpresasConsulta.aspx");
                    }
                    catch (FaultException ex)
                    {
                        this.lblError.Text = ex.Message;
                    }
                }
            }
            else
            {
                try
                {
                    empresa emp = this.GetEmpresaFromView();
                    emp.Linea = "A";
                    emp.RutaKey = extension;
                    cliente.GuardarEmpresa(emp, cert, key, this.txtPassWordLlave.Text, this.GetLogoBytes(),extension );

                    var sucursal = new Sucursales
                                       {
                                           Nombre = this.txtSucursal.Text,
                                           LugarExpedicion = this.txtLugarExpedicion.Text,
                                           IdEmpresa = emp.IdEmpresa
                                       };

                    cliente.GuardaSucursal(sucursal);

                    this.Response.Redirect("wfrEmpresasConsulta.aspx");
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrEmpresasConsulta.aspx");
        }

        #region Helper Methods

        private byte[] GetLogoBytes()
        {
            return this.fuLogoEmpresa.HasFile ? fuLogoEmpresa.FileBytes : null;
        }

        private void FillView(empresa empresa)
        {
            this.txtRFC.Text = empresa.RFC;
            this.txtRazonSocial.Text = empresa.RazonSocial;
            this.txtCalle.Text = empresa.Calle;
            this.txtNoExt.Text = empresa.NoExt;
            this.txtNoInt.Text = empresa.NoInt;
            this.txtColonia.Text = empresa.Colonia;
            this.txtMunicipio.Text = empresa.Ciudad;
            this.txtEstado.Text = empresa.Estado;
            this.txtCP.Text = empresa.CP;
            this.txtEmail.Text = empresa.Email;
            this.txtWeb.Text = empresa.Pagina;
            this.txtContacto.Text = empresa.Contacto;
            this.txtTelefono.Text = empresa.Telefono;
            ListItem li = new ListItem(empresa.RegimenFiscal, empresa.RegimenFiscal);
            if (ddlRegimen.Items.Contains(li))
            {
                this.ddlRegimen.Text = empresa.RegimenFiscal;
            }
            else
            {
                this.ddlRegimen.SelectedValue = "Otro";
                this.tdRegimen.Visible = true;
                this.txtRegimen.Text = empresa.RegimenFiscal;
            }
            this.ddlOrientacion.SelectedValue = empresa.Orientacion.ToString();
            this.txtLeyendaPie.Text = empresa.LeyendaInferior;
            this.txtLeyendaSuperior.Text = empresa.LeyendaSuperior;
            this.txtCURP.Text = empresa.CURP;
            this.lblVencimiento.Text = empresa.VencimientoCert;
            ViewState["Extension"] = empresa.RutaKey;
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                IEnumerable<string> documentos = this.GetDocumentos();
                List<empresaPantalla> dbDocs = cliente.ObtenerPantallasPorIdEmpresa(empresa.IdEmpresa);

                var docs = new DataTable();
                docs.Columns.Add("Documento", typeof (string));
                docs.Columns.Add("Visible", typeof (bool));

                foreach (string menuDoc in documentos)
                {
                    DataRow row = docs.NewRow();
                    row["Documento"] = menuDoc;
                    row["Visible"] = dbDocs.Any(l => l.pantalla.Equals(menuDoc));
                    docs.Rows.Add(row);
                }
            }
        }

        private empresa GetEmpresaFromView()
        {
            var sistema = Session["idSistema"] as long?;
            var empresa = new empresa
                              {
                                  RFC = this.txtRFC.Text,
                                  RazonSocial = this.txtRazonSocial.Text,
                                  Calle = this.txtCalle.Text,
                                  NoExt = this.txtNoExt.Text,
                                  NoInt = this.txtNoInt.Text,
                                  Colonia = this.txtColonia.Text,
                                  Ciudad = this.txtMunicipio.Text,
                                  Estado = this.txtEstado.Text,
                                  Telefono = this.txtTelefono.Text,
                                  CP = this.txtCP.Text,
                                  Email = this.txtEmail.Text,
                                  Pagina = this.txtWeb.Text,
                                  Contacto = this.txtContacto.Text,
                                  PassKey = this.txtPassWordLlave.Text,
                                  RegimenFiscal = ddlRegimen.SelectedValue,
                                  idSistema = sistema.Value,
                                  LeyendaSuperior = txtLeyendaSuperior.Text,
                                  LeyendaInferior = txtLeyendaPie.Text,
                                  Orientacion = int.Parse(ddlOrientacion.SelectedValue),
                                  CURP = this.txtCURP.Text,
                                  Linea = "A"
                              };
            if (ViewState["Extension"] != null)
                empresa.RutaKey = ViewState["Extension"].ToString();
            if (ddlRegimen.SelectedValue == "Otro")
            {
                empresa.RegimenFiscal = txtRegimen.Text;
            }
            return empresa;
        }

        private IEnumerable<string> GetDocumentos()
        {
            //List<string> documentos = null;
            //MasterPage master = this.Master;
            //if(master != null)
            //{
            //    var masterMenu = master.FindControl("NavigationMenu") as Menu;
            //    if(masterMenu != null)
            //    {
            //        foreach (MenuItem menuItem in masterMenu.Items)
            //        {
            //            if(menuItem.Text.Equals("Facturación", StringComparison.CurrentCultureIgnoreCase))
            //            {
            //                documentos = (from MenuItem subMenuItem in menuItem.ChildItems select subMenuItem.Text).ToList();
            //                break;
            //            }
            //        }
            //    }
            //}
            //return documentos;
            return ConfigurationManager.AppSettings["Documentos"].Split('|').ToList();
        }

        
        #endregion

        protected void ddlRegimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegimen.SelectedValue == "Otro")
            {
                tdRegimen.Visible = true;
                valRegimen.Enabled = true;
            }
            else
            {
                tdRegimen.Visible = false;
                valRegimen.Enabled = false;
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var empresa = ViewState["empresa"] as empresa;
            var cliente = NtLinkClientFactory.Cliente();
            byte[] cert = null;
            byte[] key = null;
            if (this.fuCertificado.HasFile)
            {
                
                cert = this.fuCertificado.FileBytes;
            }
            if (this.fuLlave.HasFile)
            {
                key = this.fuLlave.FileBytes;
            }
            if (empresa != null)
            {
                empresa modEmpresa = this.GetEmpresaFromView();
                if (!string.IsNullOrEmpty(this.txtPassWordLlave.Text) && this.fuCertificado.HasFile && this.fuLlave.HasFile)
                {
                    modEmpresa.PassKey = this.txtPassWordLlave.Text;
                }
                else
                {
                    modEmpresa.PassKey = empresa.PassKey;
                }
                modEmpresa.IdEmpresa = empresa.IdEmpresa;
                modEmpresa.idSistema = empresa.idSistema;
                var extension = Path.GetExtension(fuLlave.FileName).ToLower();
                lblAdvertencia.Text = cliente.ValidarCSD(modEmpresa, cert, key, this.txtPassWordLlave.Text, extension);
            }    
        }
    }
}
