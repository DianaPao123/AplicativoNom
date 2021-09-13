using System;
using System.Drawing;
using System.ServiceModel;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;
using ServicioLocalContract;


namespace GafLookPaid
{
    public partial class wfrLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    this.Response.Redirect("Default.aspx");
                }
            }
            LinkButton lbButton = logMain.FindControl("LoginLinkButton") as LinkButton;
            this.Form.DefaultButton = lbButton.UniqueID;
        }

        protected void logMain_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                UsuarioLocal res = cliente.Login(this.logMain.UserName, this.logMain.Password);
                if (res != null)
                {
                    var empresa = cliente.ObtenerEmpresaByUserId(res.UserId.ToString());
                    if (empresa != null)
                    {
                        var pantallas = cliente.ObtenerPantallasPorIdEmpresa(empresa.IdEmpresa);
                        var sistema = cliente.ObtenerSistemaById((int) empresa.idSistema.Value);
                        if (empresa.PrimeraVez || res.CambiarPassword == "1")
                        {
                            this.lblUserIdCambiarPassword.Text = res.UserId.ToString();
                            this.mpeCambiarPassword.Show();
                            e.Authenticated = false;
                            return;
                        }
                        Session["idEmpresa"] = empresa.IdEmpresa;
                        Session["idSistema"] = empresa.idSistema;
                        Session["razonSocial"] = empresa.RazonSocial;
                        Session["perfil"] = res.Perfil;
                        Session["userId"] = res.UserId;
                        Session["nombre"] = res.NombreCompleto;
                        Session["iniciales"] = res.Iniciales;
                        Session["empresa"] = empresa;
                        Session["panatallas"] = pantallas;
                        Session["TipoSistema"] = sistema.TipoSistema;
                    }
                    else
                    {
                        var dist = cliente.ObtenerDIsById(res.UserId.ToString());
                        if ((dist!= null && dist.PrimeraVez == true) || res.CambiarPassword == "1")
                        {
                            this.lblUserIdCambiarPassword.Text = res.UserId.ToString();
                            this.mpeCambiarPassword.Show();
                            e.Authenticated = false;
                            return;
                        }
                        if (dist != null)
                        {
                            Session["IdDistribuidor"] = dist.IdDistribuidor;
                            Session["razonSocial"] = dist.RazonSocial;
                            Session["tipoSistema"] = dist.TipoSistema;
                            Session["nombre"] = dist.Nombre;
                            Session["empresa"] = dist;
                        }
                        
                        Session["perfil"] = res.Perfil;
                        Session["userId"] = res.UserId;
                        
                        Session["iniciales"] = res.Iniciales;
                        
                    }

                    e.Authenticated = true;
                }
                else
                {
                    e.Authenticated = false;
                }
            }
        }

        protected void btnNo7_Click(object sender, EventArgs e)
        {
            this.logMain.Enabled = true;
            mpePasswordChange.Hide();
          //  UpdatePanel4.Update();
        }
        //protected void cb1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cb1.Checked)
        //        btnAceptarPassword.Enabled = true;
        //    else btnAceptarPassword.Enabled = false;
        //}

        protected void btnAceptarPassword_Click(object sender, EventArgs e)
        {
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                cliente.CambiarPassword(this.lblUserIdCambiarPassword.Text, this.txtPassword.Text);
            }
           

            Session.Abandon();

            this.Response.Redirect("wfrLogin.aspx");

        }

        protected void btnOlvidar_Click(object sender, EventArgs e)
        {
            this.logMain.Enabled = false;

            lblMensajePass.Text = "";
            txtOlvide.Text = "";
            txtRfcOlvide.Text = "";
            UpdatePanel5.Update();
            this.mpePasswordChange.Show();
        }

        protected void btnEnviarPass_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    if (cliente.RecuperarPassword(this.txtRfcOlvide.Text, this.txtOlvide.Text))
                    {
                        lblMensajePass.Text = "Se envió un email con la nueva contraseña";
                        lblMensajePass.Visible = true;
                    }

                }
            }
            catch (FaultException faultException)
            {
                lblMensajePass.Text = faultException.Message;
                lblMensajePass.Visible = true;
            }
        }

        protected void cb1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb1.Checked)
                btnAceptarPassword.Enabled = true;
            else btnAceptarPassword.Enabled = false;
        }

        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{



        //        if (CheckBox1.Checked == true)
        //        {
        //            this.btnAceptarPassword.Enabled = true;
        //            this.CheckBox1.Enabled = false;
        //            this.btnAceptarPassword.Focus();

        //        }

        //        lblMensajePas.Text = "Se ha cambiado correctamente su contraseña inserte de nuevo y de Click Aceptar ";
        //        lblMensajePas.Visible = true;

        //    }
        }
    }
