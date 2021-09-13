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


                         var ListSub= clienteServicio.ListaSubContratacion(idCliente);
                         ViewState["SubContratacion"] = ListSub;
                         BindSubContratacionToGridView();
                   
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

        private bool ValidarFactura()
        {
            this.lblError.Text = "";
            if (!string.IsNullOrEmpty(txtClabe.Text))
            {
                if (txtClabe.Text.Count() != 10 && txtClabe.Text.Count() != 11 && txtClabe.Text.Count() != 16 && txtClabe.Text.Count() != 18)
                {
                    this.lblError.Text = "La cuenta debe contener 10,11,16 o 18 digitos";
                    return false;

                }
                if (txtClabe.Text.Count() == 18)
                {
                    if (ddlBanco.SelectedValue != "000")
                    {
                        this.lblError.Text = "El banco no debe de existir.(Cuenta de 18 digitos)";
                        return false;
                    }
                }
                else
                {
                    if (ddlBanco.SelectedValue == "000")
                    {
                        this.lblError.Text = "El banco debe de existir";
                        return false;
                    }
                }
            }
            var subContrataciones = ViewState["SubContratacion"] as List<SubContratacion>;
            if (subContrataciones != null)
            {
                if (subContrataciones.Count > 0)
                {
                    decimal? t = 0;
                    foreach (SubContratacion f in subContrataciones)
                    {
                        t += f.PorcentajeTiempo;
                    }

                    if (t != 100M)
                    {
                        this.lblError.Text = "La suma de Porcentaje de subcontratacion debe de ser 100";
                        return false;
                    }
                }
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidarFactura())
            {

                int? idDatos = 0;
                if (ViewState["idDatos"] != null)
                {
                    var iddatos = (long)ViewState["idDatos"];
                    idDatos = (int)iddatos;
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
                            clienteServicio.EliminarSubcontratacion(cte);//----

                            var subContrataciones = ViewState["SubContratacion"] as List<SubContratacion>;
                            if (subContrataciones != null)
                            {
                                if (subContrataciones.Count > 0)
                                {
                                    foreach (SubContratacion s in subContrataciones)
                                    {
                                        s.IdCliente = cte;
                                        clienteServicio.SaveSubcontratacionNomina(s);
                                    }
                                }
                            }  ///---------------------------------------
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
          /* this.txtCalle.Text = cliente.Calle;
           this.txtNoExt.Text = cliente.NoExt;
           this.txtNoInt.Text = cliente.NoInt;
            this.txtColonia.Text = cliente.Colonia;
            this.txtMunicipio.Text = cliente.Ciudad;
            this.txtEstado.Text = cliente.Estado;
            this.txtPais.Text = cliente.Pais;
            this.txtCP.Text = cliente.CP;
            this.txtTelefono.Text = cliente.Telefonos;
           */
            this.txtEmail.Text = cliente.Email;
            this.txtBcc.Text = cliente.Bcc;
            //this.txtMetodoPago.Text = cliente.MetodoPago;
            this.txtCuotaDiaria.Text = cliente.CuotaDiara.ToString();
            txtCURP.Text = cliente.CURP;
            
            //Datos de la nomina
            if (datos != null)
            {

                this.ddlTipoContrato.SelectedValue = datos.TipoContrato;
                tipocontrato();
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
                /*
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
                 */ 
                this.ddlSindicalizado.SelectedValue=datos.Sindicalizado; //nuevos
                this.ddlClaveEntFed.SelectedValue=datos.ClaveEntFed;
                //this.ddlPeriodicidadPago.SelectedValue = datos.Periodicidad;
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
                /*Calle = this.txtCalle.Text,
                NoExt = this.txtNoExt.Text,
                NoInt= this.txtNoInt.Text,
                Colonia = this.txtColonia.Text,
                Ciudad = this.txtMunicipio.Text,
                Estado = this.txtEstado.Text,
                Pais = this.txtPais.Text,
                CP = this.txtCP.Text,
                Telefonos = this.txtTelefono.Text,
                */
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
                TipoJornada =  this.ddlTipoJornada.SelectedValue,
                Sindicalizado=this.ddlSindicalizado.SelectedValue, //nuevos
                ClaveEntFed=this.ddlClaveEntFed.SelectedValue
                //Periodicidad=this.ddlPeriodicidadPago.SelectedValue

            };
            /*
            if (ddlTipoContrato.SelectedValue == "Otro")
            {
                datos.TipoContrato = txtTipoContratoOtro.Text;
            }
            */
            return datos;

        }

        private void tipocontrato()
        {
            ddlRegimen.Items.Clear();

            if (ddlTipoContrato.SelectedValue == "01" || ddlTipoContrato.SelectedValue == "02"
                    || ddlTipoContrato.SelectedValue == "03" || ddlTipoContrato.SelectedValue == "04" ||
                    ddlTipoContrato.SelectedValue == "05" || ddlTipoContrato.SelectedValue == "06" ||
                    ddlTipoContrato.SelectedValue == "07" || ddlTipoContrato.SelectedValue == "08")
            {

                ListItem oItem2 = new ListItem("Sueldos", "02");
                ddlRegimen.Items.Add(oItem2);
                ListItem oItem3 = new ListItem("Jubilados", "03");
                ddlRegimen.Items.Add(oItem3);
                ListItem oItem4 = new ListItem("Pensionados", "04");
                ddlRegimen.Items.Add(oItem4);

            }


            else
            {
                ListItem oItem5 = new ListItem("Asimilados Miembros de las Sociedades Cooperativas de Producción", "05");
                ddlRegimen.Items.Add(oItem5);
                ListItem oItem6 = new ListItem("Asimilados Integrantes de Sociedades y Asociaciones Civiles", "06");
                ddlRegimen.Items.Add(oItem6);
                ListItem oItem7 = new ListItem("Asimilados Miembros de consejos", "07");
                ddlRegimen.Items.Add(oItem7);
                ListItem oItem8 = new ListItem("Asimilados comisionistas", "08");
                ddlRegimen.Items.Add(oItem8);
                ListItem oItem9 = new ListItem("Asimilados Honorarios", "09");
                ddlRegimen.Items.Add(oItem9);
                ListItem oItem10 = new ListItem("Asimilados acciones", "10");
                ddlRegimen.Items.Add(oItem10);
                ListItem oItem11 = new ListItem("Asimilados otros", "11");
                ddlRegimen.Items.Add(oItem11);
                ListItem oItem12 = new ListItem("Jubilados o Pensionados", "12");
                ddlRegimen.Items.Add(oItem12);
                ListItem oItem13 = new ListItem("Indemnización o Separación", "13");
                ddlRegimen.Items.Add(oItem13); 
                ListItem oItem14 = new ListItem("Otro Regimen", "99");
                ddlRegimen.Items.Add(oItem14);


            }

            if (ddlTipoContrato.SelectedValue == "09" || ddlTipoContrato.SelectedValue == "10" || ddlTipoContrato.SelectedValue == "99")
            {
                txtFechaInicialLaboral.Text = "";
                txtFechaInicialLaboral.Enabled = false;
                RequiredFieldValidator17.Enabled = false;
            }
            else
            {
                RequiredFieldValidator17.Enabled = true;
                txtFechaInicialLaboral.Enabled = true;
            }
            ddlRegimen.DataBind();
            
        }

        protected void ddlTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ddlRegimen.DataBind();

            tipocontrato();
          
        }

        protected void btnAgregarSubContratacion_Click(object sender, EventArgs e)
        {
            var subContrataciones = ViewState["SubContratacion"] as List<SubContratacion>;
            if (subContrataciones == null)
            {
                subContrataciones = new List<SubContratacion>();
            }
            SubContratacion subContratacio = new SubContratacion();

            subContratacio.PorcentajeTiempo = Convert.ToDecimal(txtPorcentajeTiempo.Text);
            subContratacio.RfcLabora = txtRfcLabora.Text;


            subContrataciones.Add(subContratacio);
            ViewState["SubContratacion"] = subContrataciones;

            BindSubContratacionToGridView();

            txtPorcentajeTiempo.Text = "";
            txtRfcLabora.Text = "";

            
        }
        //----------------------------------------------
        private void BindSubContratacionToGridView()
        {
            var subContrataciones = ViewState["SubContratacion"] as List<SubContratacion>;
            if (subContrataciones == null)
            {
                subContrataciones = new List<SubContratacion>();
            }
            GridSubContratacion.DataSource = subContrataciones;
            GridSubContratacion.DataBind();


        }

        //---------------------------------------
        protected void GridSubContratacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var id = int.Parse(e.CommandArgument.ToString());
                var subContrataciones = ViewState["SubContratacion"] as List<SubContratacion>;
                if (subContrataciones != null)
                {
                    subContrataciones.RemoveAt(id);
                    ViewState["SubContratacion"] = subContrataciones;
                    BindSubContratacionToGridView();
                   
                }
            }
        }
        //------------------------------------------



    }
}