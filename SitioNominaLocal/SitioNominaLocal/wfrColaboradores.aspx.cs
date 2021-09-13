using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioLocalContract;

namespace GafLookPaid
{
    public partial class wfrColaboradores : System.Web.UI.Page
    {
        private DateTime MinDateTime = DateTime.ParseExact("1800/01/01", "yyyy/MM/dd", new CultureInfo("en-US"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string idClienteString = Request.QueryString["idCliente"];

                int idCliente;
                if (!string.IsNullOrEmpty(idClienteString) && int.TryParse(idClienteString, out idCliente))
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    clientes cliente;
                    DatosNomina datos;
                    using (clienteServicio as IDisposable)
                    {
                        cliente = clienteServicio.ObtenerClienteById(idCliente);
                        var sistema = Session["idSistema"] as long?;
                        var perfil = Session["perfil"] as string;
                        this.ddlEmpresa.DataSource = clienteServicio.ListaEmpresas(perfil, cliente.idempresa.Value, sistema.Value, null);
                        this.ddlEmpresa.DataBind();
                        datos = clienteServicio.ObtenerDatosNomina(cliente.idCliente);

                    }

                    this.txtRFC.Enabled = false;
                    this.FillView(cliente, datos);
                    ViewState["cliente"] = cliente;
                    if (datos != null)
                        ViewState["idDatos"] = datos.IdDatoNomina;
                }
                else
                {
                    string idEmpresaString = Request.QueryString["idEmpresa"];
                    var sistema = Session["idSistema"] as long?;
                    int idEmpresa;
                    if (!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idEmpresa))
                    {
                        var clienteServicio = NtLinkClientFactory.Cliente();
                        using (clienteServicio as IDisposable)
                        {
                            this.ddlEmpresa.DataSource = clienteServicio.ListaEmpresas(Session["perfil"] as string, idEmpresa, sistema.Value, null);
                            this.ddlEmpresa.DataBind();
                        }
                        this.txtRFC.Enabled = true;
                    }
                    this.ddlEmpresa.SelectedValue = idEmpresaString;
                }
            }
        }   

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? idDatos = 0;
            if (ViewState["idDatos"] != null)
            {
                var iddatos = (long)ViewState["idDatos"];
                idDatos = (int) iddatos;
            }
            var cliente = ViewState["cliente"] as clientes;
            if (cliente != null)
            {
                clientes modCliente = this.GetClientFromView();
                DatosNomina datos = this.GetDatosFromView();
                datos.IdDatoNomina = idDatos.Value;
                datos.IdCliente = cliente.idCliente;
                modCliente.idCliente = cliente.idCliente;
                modCliente.idempresa = int.Parse(this.ddlEmpresa.SelectedValue);
                modCliente.idVendedor = cliente.idVendedor;
                modCliente.Tipo = cliente.Tipo;

                try
                {
                    var clienteServicio = NtLinkClientFactory.Cliente();
                    using (clienteServicio as IDisposable)
                    {
                        var cte = clienteServicio.GuardarCliente(modCliente);
                        datos.IdCliente = cte;
                        clienteServicio.SaveDatosNomina(datos);
                    }
                    Response.Redirect("wfrColaboradoresConsulta.aspx");
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
                    var idCte = 0;
                    using (clienteServicio as IDisposable)
                    {
                        var cte = clienteServicio.GuardarCliente(this.GetClientFromView());
                        DatosNomina datos = this.GetDatosFromView();
                        datos.IdDatoNomina = idDatos.Value;
                        datos.IdCliente = cte;
                        idCte = cte;
                        clienteServicio.SaveDatosNomina(datos);
                    }
                    //Response.Redirect("wfrColaboradoresConsulta.aspx");
                    Response.Redirect("~/wfrColaboradores.aspx?idCliente=" + idCte);
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrColaboradoresConsulta.aspx");
        }

        
        private void FillView(clientes cliente, DatosNomina datos)
        {
            this.ddlEmpresa.SelectedValue = cliente.idempresa.ToString();
            this.txtRFC.Text = cliente.RazonSocial;

            this.txtNombres.Text = cliente.RazonSocial;
            this.txtApellidoPaterno.Text = cliente.ApellidoPaterno;
            this.txtApellidoMaterno.Text = cliente.ApellidoMaterno;

            this.txtRFC.Text = cliente.RFC;
           this.txtCalle.Text = cliente.Calle;
           this.txtNoExt.Text = cliente.NoExt;
           this.txtNoInt.Text = cliente.NoInt;
            this.txtColonia.Text = cliente.Colonia;
            this.txtMunicipio.Text = cliente.Ciudad;
            this.txtEstado.Text = cliente.Estado;
            this.txtPais.Text = cliente.Pais;
            this.txtCP.Text = cliente.CP;
            this.txtTelefono.Text = cliente.Telefonos;
            this.txtEmail.Text = cliente.Email;
            this.txtBcc.Text = cliente.Bcc;
            //this.txtMetodoPago.Text = cliente.MetodoPago;
            this.txtCuotaDiaria.Text = cliente.CuotaDiara.ToString();
            txtCURP.Text = cliente.CURP;
            
            //Datos de la nomina
            if (datos != null)
            {
                this.txtNumEmpleado.Text = datos.NoEmpleado;
                this.txtNumEmpleado0.Text = datos.NoEmpleado;
                this.ddlRegimen.SelectedValue = datos.Regimen;
                this.txtNumSeguridadSocial.Text = datos.NoSeguridadSocial;
                this.txtDepartamento.Text = datos.Departamento;
                this.txtClabe.Text = datos.Clabe;
                this.ddlBanco.SelectedValue = string.IsNullOrEmpty(datos.Banco) ? "000" : datos.Banco.ToString();
                this.txtFechaInicialLaboral.Text = datos.FechaInicio == MinDateTime ? string.Empty : datos.FechaInicio.ToString("yyyy-MM-dd");
                this.txtPuesto.Text = datos.Puesto;

                this.txtSalarioBaseCotApor.Text = datos.SalarioBase == -1 ? string.Empty : datos.SalarioBase.ToString();
                this.txtSalarioDiarioIntegro.Text = datos.SalarioDiario == -1 ? string.Empty : datos.SalarioDiario.ToString();
                this.ddlTipoJornada.SelectedValue = datos.TipoJornada.ToString();

                if (ddlTipoContrato.Items.Contains(new ListItem(datos.TipoContrato, datos.TipoContrato)))
                {
                    this.ddlTipoContrato.SelectedValue = datos.TipoContrato;
                    this.txtTipoContratoOtro.Visible = false;
                    this.txtTipoContratoOtro.Text = "";
                    this.valTipoContrOtro.Enabled = false;
                }
                else
                {
                    this.ddlTipoContrato.SelectedValue = "Otro";
                    this.txtTipoContratoOtro.Visible = true;
                    this.txtTipoContratoOtro.Text = datos.TipoContrato;
                    this.valTipoContrOtro.Enabled = true;
                }
            }
        }

        private clientes GetClientFromView()
        {
            var cliente = new clientes
            {
                RFC = this.txtRFC.Text,
                RazonSocial = this.txtNombres.Text,
                ApellidoPaterno = this.txtApellidoPaterno.Text,
                ApellidoMaterno = this.txtApellidoMaterno.Text,
                Calle = this.txtCalle.Text,
                NoExt = this.txtNoExt.Text,
                NoInt= this.txtNoInt.Text,
                Colonia = this.txtColonia.Text,
                Ciudad = this.txtMunicipio.Text,
                Estado = this.txtEstado.Text,
                Pais = this.txtPais.Text,
                CP = this.txtCP.Text,
                Telefonos = this.txtTelefono.Text,
                Email = this.txtEmail.Text,
                Bcc = txtBcc.Text,
                idempresa = int.Parse(this.ddlEmpresa.SelectedValue),
                CURP =  txtCURP.Text,
                Tipo = 1,

                CuotaDiara = decimal.Parse(this.txtCuotaDiaria.Text)

            };
            
            return cliente;
        }


        private DatosNomina GetDatosFromView()
        {
            var fecha = MinDateTime;
            if (!string.IsNullOrEmpty(this.txtFechaInicialLaboral.Text))
                fecha = DateTime.ParseExact(txtFechaInicialLaboral.Text, "yyyy-MM-dd",
                                            System.Threading.Thread.CurrentThread.CurrentCulture);

            var datos = new DatosNomina()
            {
                Banco = (ddlBanco.SelectedValue != "000" && ddlBanco.SelectedIndex != -1) ? ddlBanco.SelectedValue : null,
                Clabe = string.IsNullOrEmpty(txtClabe.Text) ? null : txtClabe.Text,
                Departamento = txtDepartamento.Text,
                FechaInicio = fecha,
                
                NoEmpleado = txtNumEmpleado.Text,
                NoSeguridadSocial = txtNumSeguridadSocial.Text,
                Puesto = txtPuesto.Text,
               
                Regimen = ddlRegimen.SelectedValue,

                SalarioBase = string.IsNullOrEmpty(txtSalarioBaseCotApor.Text) ? -1 : decimal.Parse(txtSalarioBaseCotApor.Text),
                SalarioDiario = string.IsNullOrEmpty(txtSalarioDiarioIntegro.Text) ? -1 : decimal.Parse(txtSalarioDiarioIntegro.Text),

                TipoContrato = this.ddlTipoContrato.SelectedValue,
                TipoJornada =  this.ddlTipoJornada.SelectedValue
            };
            
            if (ddlTipoContrato.SelectedValue == "Otro")
            {
                datos.TipoContrato = txtTipoContratoOtro.Text;
            }

            return datos;

        }

        protected void ddlTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoContrato.SelectedValue == "Otro")
            {
                txtTipoContratoOtro.Visible = true;
                valTipoContrOtro.Enabled = true;
                var clienteId = ViewState["cliente"] as string;
                if(clienteId != null)
                {
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var datos = cliente.ObtenerDatosNomina(int.Parse(clienteId));
                        txtTipoContratoOtro.Text = datos.TipoContrato;
                        valTipoContrOtro.Enabled = true;
                    }
                }
            }
            else
            {
                txtTipoContratoOtro.Visible = false;
                txtTipoContratoOtro.Text = "";
                valTipoContrOtro.Enabled = false;
            }
        }

    }
}