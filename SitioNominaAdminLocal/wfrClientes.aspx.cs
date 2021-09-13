using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.UI;
using ServicioLocalContract;
using System.Linq;

namespace SitioNominaAdmin
{
    public partial class wfrClientes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idEmpresaString = this.Request.QueryString["idSistema"];
                int idSistema;
                if (!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idSistema))
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    Sistemas sistema;
                    using (clienteServicio as IDisposable)
                    {
                        sistema = clienteServicio.ObtenerSistemaById(idSistema);
                        //txtConsumidos.Text = clienteServicio.ObtenerNumeroTimbresSistema(idSistema).ToString();
                        List<Contratos> v = clienteServicio.ListaContratos(idSistema);
                        if (v!= null && v.Count > 0)
                        {
                            var count = v.Sum(p => p.Timbres);
                            this.txtFolios.Text = count.ToString();
                        }
                    }
                    this.FillView(sistema);
                    ViewState["sistema"] = sistema;
                }
                else
                {
                    this.txtRFC.Enabled = true;
                }
            }
            if (ddlTipoCliente.SelectedIndex == 2)
            {
                divDistribuidores.Visible = true;
                divUsuarios.Visible = false;
                divUsers.Visible = false;
                divDis.Visible = true;
            }
            else
            {
                divDistribuidores.Visible = false;
                divUsuarios.Visible = true;
                divDis.Visible = false;
                divUsers.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrEmpresasConsulta.aspx");
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var sistema = ViewState["sistema"] as Sistemas;
            var cliente = NtLinkClientFactory.Cliente();
            if (sistema != null)
            {

                Sistemas modSistemas = this.GetEmpresaFromView();
                modSistemas.IdSistema = sistema.IdSistema;
                using (cliente as IDisposable)
                {
                    try
                    {
                        string resultado = null;
                        cliente.GuardarSistema(modSistemas, ref resultado, txtNombreAdmin.Text, txtInicialesAdmin.Text);
                        this.LblMensaje.Text = resultado;
                        btnGuardar.Enabled = false;
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
                    string resultado = null;
                    cliente.GuardarSistema(this.GetEmpresaFromView(), ref resultado, txtNombreAdmin.Text, txtInicialesAdmin.Text);
                    this.LblMensaje.Text= resultado;
                    //this.Response.Redirect("wfrClientesConsulta.aspx");
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }

        #region Helper Methods

        private void FillView(Sistemas empresa)
        {
            this.txtRFC.Text = empresa.Rfc;
            this.txtRazonSocial.Text = empresa.RazonSocial;
            this.txtCalle.Text = empresa.Calle;
            this.txtNoExt.Text = empresa.NoExt;
            this.txtNoInt.Text = empresa.NoInt;
            this.txtColonia.Text = empresa.Colonia;
            this.txtMunicipio.Text = empresa.Ciudad;
            this.txtEstado.Text = empresa.Estado;
            this.txtCP.Text = empresa.Cp;
            this.txtEmail.Text = empresa.Email;
            this.txtContacto.Text = empresa.Contacto;
            this.txtTelefono.Text = empresa.Telefono;
            this.ddlRegimen.Text = empresa.RegimenFiscal;
            this.ddlTipoCliente.SelectedValue = empresa.TipoSistema.ToString();

            this.txtTimbresContratados.Text = empresa.TimbresContratados.ToString();
        }

        private Sistemas GetEmpresaFromView()
        {
            var sistema = new Sistemas
            {
                Rfc = this.txtRFC.Text,
                RazonSocial = this.txtRazonSocial.Text,
                Calle = this.txtCalle.Text,
                NoExt = this.txtNoExt.Text,
                NoInt = this.txtNoInt.Text,
                Colonia = this.txtColonia.Text,
                Ciudad = this.txtMunicipio.Text,
                Estado = this.txtEstado.Text,
                Telefono = this.txtTelefono.Text,
                Cp = this.txtCP.Text,
                Email = this.txtEmail.Text,
                Contacto = this.txtContacto.Text,
                RegimenFiscal = this.ddlRegimen.SelectedValue,
                TipoSistema = int.Parse(ddlTipoCliente.SelectedValue),
                
            };
            if (!string.IsNullOrEmpty(txtFolios.Text))
            {
                sistema.Folios = int.Parse(txtFolios.Text);
            }
            if (!string.IsNullOrEmpty(txtTimbresContratados.Text))
            {
                sistema.TimbresContratados = int.Parse(txtTimbresContratados.Text);
            }
            return sistema;
        }

        private Distribuidores GetDisFromView()
        {
            var distribuidor = new Distribuidores();
            {
                distribuidor.Rfc = this.txtDisRfc.Text;
                distribuidor.RazonSocial = this.txtDisRazonSocial.Text;
                distribuidor.RegimenFiscal = this.ddlDisRegimen.SelectedValue;
                distribuidor.Nombre = this.txtDisNombre.Text;
                distribuidor.Direccion = this.txtDisDireccion.Text;
                distribuidor.Colonia = this.txtDisColonia.Text;
                distribuidor.Ciudad = this.txtDisMunicipio.Text;
                distribuidor.Estado = this.txtDisEstado.Text;
                distribuidor.Telefono = this.txtDisTelefono.Text;
                distribuidor.Cp = this.txtDisCp.Text;
                distribuidor.Email = this.txtDisEmail.Text;
                distribuidor.Contacto = this.txtDisContacto.Text;
                distribuidor.TipoSistema = int.Parse(ddlTipoCliente.SelectedValue);
                distribuidor.Contrato = this.txtDisContrato.Text;
                distribuidor.Distribuidor2 = this.txtID2.Text;
                distribuidor.Distribuidor3 = this.txtID3.Text;
                distribuidor.PrimeraVez = true;
            };
            return distribuidor;
        }
        #endregion

        protected void btnGuardaDis_Click(object sender, EventArgs e)
        {
            var distribuidor = ViewState["distribuidor"] as Distribuidores;
            var cliente = NtLinkClientFactory.Cliente();
            if (distribuidor != null)
            {

                Distribuidores modDist = this.GetDisFromView();
                modDist.IdDistribuidor = distribuidor.IdDistribuidor;
                using (cliente as IDisposable)
                {
                    try
                    {
                        string resultado = null;
                        cliente.GuardarDistribuidor(modDist, ref resultado, txtNombreAdmin.Text, txtInicialesAdmin.Text);
                        this.lblDistribuidor.Text = resultado;
                        btnGuardar.Enabled = false;
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
                    string resultado = null;
                    cliente.GuardarDistribuidor(this.GetDisFromView(), ref resultado, txtNombreAdmin.Text, txtInicialesAdmin.Text);
                    this.lblDistribuidor.Text = resultado;
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }

    }
}