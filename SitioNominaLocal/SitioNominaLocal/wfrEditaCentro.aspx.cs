using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;
using System.ServiceModel;

namespace GafLookPaid
{
    public partial class wfrEditaCentro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idCentroStr = this.Request.QueryString["idCentro"];
                string idEmpresaStr = this.Request.QueryString["idEmpresa"];
                int idCentro, idEmpresa;
                if (!string.IsNullOrEmpty(idEmpresaStr) && int.TryParse(idEmpresaStr, out idEmpresa))
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    empresa empresa;
                    using (clienteServicio as IDisposable)
                    {
                        if (!string.IsNullOrEmpty(idCentroStr) && int.TryParse(idCentroStr, out idCentro))
                        {
                            CentrosTrabajo centro;
                            centro = clienteServicio.ObtenerCentroById(idCentro);
                            this.FillView(centro);
                            ViewState["centro"] = idCentroStr;
                        }
                        else
                        {
                            var centro = new CentrosTrabajo();
                            FillView(centro);
                        }
                        ViewState["empresa"] = idEmpresaStr;
                    }
                }
            }
        }

        private void FillView(CentrosTrabajo centro)
        {
            this.txtRazonSocial.Text = centro.Nombre;
            this.txtRegistroPatronal.Text = string.IsNullOrEmpty(centro.RegistroPatronal) ? string.Empty : centro.RegistroPatronal;
            
            this.chkDireccionEmpresa.Checked = centro.DireccionEmpresa;

            this.txtCalle.Text = centro.Calle;
            this.txtNoExt.Text = centro.NoExt;
            this.txtNoInt.Text = centro.NoInt;
            this.txtColonia.Text = centro.Colonia;
            this.txtMunicipio.Text = centro.Municipio;
            this.txtEstado.Text = centro.Estado;
            this.txtPais.Text = centro.Pais;
            this.ddlRiesgoPuesto.SelectedValue = centro.RiesgoTrabajo;

            //this.ddlTipoJornada.SelectedValue = centro.TipoJornada;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfrCentrosTrabajo.aspx?idEmpresa=" + this.Request.QueryString["idEmpresa"]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var centro = ViewState["centro"] as string;
            var empresa = ViewState["empresa"] as string;
            if (centro != null)
            {
                CentrosTrabajo modCentro= this.GetCentroFromView();
                modCentro.IdCentroTrabajo = int.Parse(centro);
                modCentro.IdEmpresa = int.Parse(empresa);
               
                modCentro.RiesgoTrabajo = modCentro.RiesgoTrabajo;
               
                try
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    using (clienteServicio as IDisposable)
                    {
                        var cntro = clienteServicio.GuardarCentro(modCentro);
                    }
                    Response.Redirect("wfrCentrosTrabajo.aspx?idEmpresa=" + this.Request.QueryString["idEmpresa"]);
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }

            }
            else
            {
                try
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    using (clienteServicio as IDisposable)
                    {
                        var cntro = clienteServicio.GuardarCentro(this.GetCentroFromView());
                    }
                    Response.Redirect("wfrCentrosTrabajo.aspx?idEmpresa=" + this.Request.QueryString["idEmpresa"]);
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }

        private CentrosTrabajo GetCentroFromView()
        {
            var empresa = ViewState["empresa"] as string;
            var centro = new CentrosTrabajo()
            {

                IdEmpresa = int.Parse(empresa),               
                Nombre = this.txtRazonSocial.Text,
                RegistroPatronal = string.IsNullOrEmpty(txtRegistroPatronal.Text) ? null : this.txtRegistroPatronal.Text,
               
                
                Calle = this.txtCalle.Text,
                NoExt = this.txtNoExt.Text,
                NoInt = this.txtNoInt.Text,
                Colonia = this.txtColonia.Text,
                Municipio = this.txtMunicipio.Text,
                Estado = this.txtEstado.Text,
                RiesgoTrabajo = this.ddlRiesgoPuesto.SelectedValue,
                Pais = this.txtPais.Text,

                //TipoJornada = this.ddlTipoJornada.SelectedValue,
                DireccionEmpresa = chkDireccionEmpresa.Checked
            };
           

            return centro;
        }

        protected void CheckBox1_OnCheckedChanged(object sender, EventArgs e)
        {
            var empresa = ViewState["empresa"] as string;
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                var centro = cliente.ObtenerEmpresaById(int.Parse(empresa));
                this.txtCalle.Text = centro.Calle;
                this.txtNoExt.Text = centro.NoExt;
                this.txtNoInt.Text = centro.NoInt;
                this.txtColonia.Text = centro.Colonia;
                this.txtMunicipio.Text = centro.Ciudad;
                this.txtEstado.Text = centro.Estado;
                this.txtPais.Text = "México";
            }
        }

        
    }
}