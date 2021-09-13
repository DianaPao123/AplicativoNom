<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresa.aspx.cs" Inherits="GafLookPaid.wfrEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <%--   <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />--%>
    
          <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
      <script src="Scripts/bootstrapcdn-v3-4-0-bootstrap.min.js"></script>
     <link href="Content/Mensajes.css" rel="stylesheet" />
     <link href="Content/UpdateProgress.css" rel="stylesheet" />

    <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Empresa</h3>
            </div>
            <div class ="card-body" >
                   <div class = "row"> 
            <div class="col-lg-3 " style="color:red;" >
                            <asp:Label ID="lblError" class="control-label" runat="server" ForeColor="Red" Font-Bold="true" style=" font-size: x-small; text-align: left; font-variant: small-caps;" Width="250px"></asp:Label>
            </div>
            </div>
                 <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label8" runat="server" Text="* Campos Requeridos"></asp:Label>
        </div>
                 </div>
                 <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label2" runat="server" Text="RFC"></asp:Label>
                <asp:TextBox runat="server" ID="txtRFC"  CssClass="form-control" />
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label3" runat="server" Text="Razón Social"></asp:Label>
               <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control" />
                         </div>
                     </div>
                <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label4" runat="server" Text="* Régimen Fiscal"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlRegimen" AutoPostBack="True"  CssClass="form-control"
                    onselectedindexchanged="ddlRegimen_SelectedIndexChanged" >
                    <asp:ListItem Value="601" Text="General de Ley Personas Morales" runat="server" />
                    <asp:ListItem Value="603" Text="Personas Morales con Fines no Lucrativos" runat="server" />
                    <asp:ListItem Value="605" Text="Sueldos y Salarios e Ingresos Asimilados a Salarios" runat="server" />
                    <asp:ListItem Value="606" Text="Arrendamiento" runat="server" />
                    <asp:ListItem Value="608" Text="Demás ingresos" runat="server" />
                    <asp:ListItem Value="609" Text="Consolidación" runat="server" />
                    <asp:ListItem Value="610" Text="Residentes en el Extranjero sin Establecimiento Permanente en México" runat="server" />
                    <asp:ListItem Value="611" Text="Ingresos por Dividendos (socios y accionistas)" runat="server" />
                    <asp:ListItem Value="612" Text="Personas Físicas con Actividades Empresariales y Profesionales" runat="server" />
                    <asp:ListItem Value="614" Text="Ingresos por intereses" runat="server" />
                    <asp:ListItem Value="616" Text="Sin obligaciones fiscales" runat="server" />
                    <asp:ListItem Value="620" Text="Sociedades Cooperativas de Producción que optan por diferir sus ingresos" runat="server" />
                    <asp:ListItem Value="621" Text="Incorporación Fiscal" runat="server" />
                    <asp:ListItem Value="622" Text="Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras" runat="server" />
                    <asp:ListItem Value="623" Text="Opcional para Grupos de Sociedades" runat="server" />
                    <asp:ListItem Value="624" Text="Coordinados" runat="server" />
                    <asp:ListItem Value="628" Text="Hidrocarburos" runat="server" />
                    <asp:ListItem Value="607" Text="Régimen de Enajenación o Adquisición de Bienes" runat="server" />
                    <asp:ListItem Value="629" Text="De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales" runat="server" />
                    <asp:ListItem Value="630" Text="Enajenación de acciones en bolsa de valores" runat="server" />
                    <asp:ListItem Value="615" Text="Régimen de los ingresos por obtención de premios" runat="server" />
                </asp:DropDownList>
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label5" runat="server" Text="CURP (Si aplica)"></asp:Label>
              <asp:TextBox runat="server" ID="txtCURP" MaxLength="24" CssClass="form-control"  />
                         </div>
                     </div>

       <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label6" runat="server" Text="* Calle"></asp:Label>
                <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="valRegimen0" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtCalle">

                </asp:RequiredFieldValidator>
           
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label7" runat="server" Text="* Número Exterior"></asp:Label>
              <asp:TextBox runat="server" ID="txtNoExt"  CssClass="form-control" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="valRegimen4" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtNoExt"></asp:RequiredFieldValidator>
    
                         </div>
                     </div>
             <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label9" runat="server" Text="Numero Interior"></asp:Label>
                <asp:TextBox runat="server" ID="txtNoInt" CssClass="form-control" ClientIDMode="Static" />
           
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label10" runat="server" Text="Colonia"></asp:Label>
              <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" />
                         </div>
                     </div>
             <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label11" runat="server" Text="* Municipio"></asp:Label>
             <asp:TextBox runat="server" ID="txtMunicipio" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="valRegimen1" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtMunicipio">
                                   </asp:RequiredFieldValidator>
           
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label12" runat="server" Text="* Estado"></asp:Label>
             <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="valRegimen2" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtEstado">

                </asp:RequiredFieldValidator>
                      </div>
                     </div>
             <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label13" runat="server" Text="* C.P."></asp:Label>
              <asp:TextBox runat="server" ID="txtCP" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="valRegimen3" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtCP"></asp:RequiredFieldValidator>
        
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label14" runat="server" Text="Teléfono"></asp:Label>
             <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />   

                     </div>
                     </div>
             <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label15" runat="server" Text="Email"></asp:Label>
             <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label16" runat="server" Text="Web"></asp:Label>
             <asp:TextBox runat="server" ID="txtWeb" CssClass="form-control" />

                     </div>
                     </div>
            
           <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label17" runat="server" Text="Leyenda Encabezado"></asp:Label>
               <asp:TextBox runat="server" ID="txtLeyendaSuperior" CssClass="form-control"></asp:TextBox>

                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label18" runat="server" Text="Leyenda pie de Página"></asp:Label>
           <asp:TextBox runat="server" ID="txtLeyendaPie" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                     </div>
                     </div>
                
       
        <asp:Panel runat="server" ID="pnlSucursal" Visible="False">
          
             <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label19" runat="server" Text="Sucursal"></asp:Label>
              <asp:TextBox runat="server" ID="txtSucursal" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvSucursal" ControlToValidate="txtSucursal"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label20" runat="server" Text="Lugar de Expedición"></asp:Label>
           <asp:TextBox runat="server" ID="txtLugarExpedicion"  CssClass="form-control"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ControlToValidate="txtLugarExpedicion"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                     </div>
                     </div>
                
        </asp:Panel>
                <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label21" runat="server" Text="Contacto Administrativo"></asp:Label>
            <asp:TextBox runat="server" ID="txtContacto" CssClass="form-control" />
                </div>
                     <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label22" runat="server" Text="Orientación del Archivo Pdf"></asp:Label>
          <asp:DropDownList runat="server" ID="ddlOrientacion" CssClass="form-control">
                    <asp:ListItem Value="0" Text="Vertical" ></asp:ListItem> 
                    <asp:ListItem Value="1" Text="Horizontal" ></asp:ListItem> 
            </asp:DropDownList>
                     </div>
                     </div>
           
                <div class = "row form-group"> 
                <div class="col-lg-5 " >
                <asp:Label  class="control-label" ID="Label23" runat="server" Text="Logo Empresa (Máximo 50 Kb)"></asp:Label>
                  <asp:FileUpload runat="server" ID="fuLogoEmpresa" CssClass="form-control" />
                  </div>
      </div>
       <div class = "row form-group"> 
                <div class="col-lg-5 " >
                
                
                <asp:Button runat="server"   CssClass="btn btn-outline-success"  ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click" />
      
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar"  CssClass="btn btn-outline-success"   onclick="btnCancelar_Click" CausesValidation="False" />
   </div>
           </div>

                <div class = "card mt-2">   
            <div class = "card-header">
                  <b>  Debes de modificar los 3 siguientes datos en conjunto del Certificado del Sello 
                Digital. </b>
            </div>
                        <div class = "card-body" id="Div2" runat="server" >
                 <div class = "row">
                    <div class = "form-group col-lg-6">
                   <asp:Label ID="Label24" runat="server" class="control-label" Text="Certificado"></asp:Label>
                <asp:FileUpload runat="server" ID="fuCertificado"  CssClass="form-control" />
                        </div>
                     <div class = "form-group col-lg-6">
                   <asp:Label runat="server" ID="lblVencimiento"></asp:Label>
                         </div>
                     </div>
                               <div class = "row">
                    <div class = "form-group col-lg-6">
                   <asp:Label ID="Label25" runat="server" class="control-label" Text="Llave Privada"></asp:Label>
                <asp:FileUpload runat="server"   ID="fuLlave"  CssClass="form-control" />
                        </div>
                    <div class = "form-group col-lg-6">
            <asp:Label runat="server" ID="lblAdvertencia" ForeColor="Red" ></asp:Label>
                        </div>
                                   </div>
                               <div class = "row">
                    <div class = "form-group col-lg-6">
                   <asp:Label ID="Label26" runat="server" class="control-label" Text="Password Llave"></asp:Label>
                 <asp:TextBox runat="server" ID="txtPassWordLlave" TextMode="Password" ssClass="form-control"/>
                </div>
                                   </div>

              <div class = "row">
                    <div class = "form-group col-lg-6">
                <asp:Button ID="btnValidar" runat="server" Text="Validar y Guardar"   CssClass="btn btn-outline-success" 
                    onclick="btnValidar_Click" />
                        
               

                <asp:Label ID="Label1" runat="server"                 Text="Valida y Guarda el CSD"></asp:Label>
         </div>
                  </div>

 </div>
        </div>
                </div>
                
        </div>

    <br />
    <asp:ModalPopupExtender runat="server" ID="mpeSELLOS" TargetControlID="btngenerarDummy"
         PopupControlID="pnlMSG" BackgroundCssClass="modalBackground" />
    <asp:Panel runat="server" ID="pnlMSG" Style="display: none;" CssClass="modalPopup">
        <div class="header" >
            Mensaje
        </div>
        <div class="body" style="text-align: center;">
        <br />
        <asp:Label runat="server" ID="LblMensaje" Text="Mensaje:" Font-Size="Large" 
                            Visible="True" class="180" style="color: #F72020"/>
        <!--h1 class="style161" style="color: #000000"><strong>Comprobante generado correctamente y enviado por correo electrónico</strong></h1-->
        <br />
        <asp:Label runat="server" ID="LblSolucion" Text="Solucion:" Font-Size="Large" Height="62px" class="style161" style="color: #000000" Width="287px"/>
        <br />
        
        
        <%--<asp:Button runat="server" ID="btnSeleccionarConcepto" Text="Seleccionar" onclick="btnSeleccionarConcepto_Click" />&nbsp;&nbsp;--%>
        <asp:Button runat="server" ID="btnCerrar" Text="Aceptar"   CssClass="btn btn-outline-success"  OnClick="btnCerrar_Click" />
            </div>
    </asp:Panel>
    <asp:Button runat="server" ID="btngenerarDummy" Style="display: none;" />


     <!-- PopupAdvertencia -->

    <asp:Panel runat="server" ID="warningPopup"  Style="display: none;" CssClass="modalPopup" Font-Names ="Arial " HorizontalAlign="Center" >
    <div class="header" >
            Advertencia
        </div>
        <div class="body" style="text-align: center;">
        
        <div class="orderLabel">
        <h1>Advertencia</h1>
        Tu sesión está a punto de expirar debido a inactividad.
        <br /><br />

    <input id="btnWarningOK" type="button"  class="btn btn-outline-success" 
        value="Click para continuar." onclick="HideIddleWarning()" />
            </div>
    </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="warningMPE" runat="server"
    TargetControlID="dummyLink" PopupControlID="warningPopup"
    BehaviorID="warningMPE"  BackgroundCssClass="modalBackground" />
    <asp:HyperLink ID="dummyLink" runat="server" NavigateUrl="#"></asp:HyperLink>

    <!-- *************************************************************************** -->

   
    <!-- Cerrar sesion --> 
    <asp:Panel runat="server" ID="popupcerrar" style="display: none;" CssClass="modalPopup">
    <div class="header" >
            Sessión
        </div>
        <div class="body" style="text-align: center;">
    
        <div class="orderLabel">
        La sesión ha expirado.
    <br /><br />
    <input id="btncerrar" type="button" value="Entendido"  class="btn btn-outline-success"  onclick="HideIddleWarning2()"  />
    </div>
            </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="cerrar" runat="server"
    TargetControlID="HyperLink" PopupControlID="popupcerrar"
    BehaviorID="warningMPE2"  BackgroundCssClass="modalBackground" />
    <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl="#"></asp:HyperLink>


    <!---- **************************************************************** ---->

     
 
<!-- script inactividad -->

<script type="text/javascript">
 
    //localizar timers
    var iddleTimeoutWarning = null;
    var iddleTimeout = null;
 
    //esta funcion automaticamente sera llamada por ASP.NET AJAX cuando la pagina cargue y un postback parcial complete
    function pageLoad() {
 
        //borrar antiguos timers de postbacks anteriores
        if (iddleTimeoutWarning != null)
            clearTimeout(iddleTimeoutWarning);
        if (iddleTimeout != null)
            clearTimeout(iddleTimeout);
 
        //leer tiempos desde web.config
        var millisecTimeOutWarning = <%= int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeoutWarning"]) * 60 * 1000 %>;
        var millisecTimeOut = <%= int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"]) * 60 * 1000 %>;
 
        //establece tiempo para mostrar advertencia si el usuario ha estado inactivo
        iddleTimeoutWarning = setTimeout("DisplayIddleWarning()", millisecTimeOutWarning);
        iddleTimeout = setTimeout("TimeoutPage()", millisecTimeOut);
    }
 
    function TimeoutPage() {
        $find('warningMPE2').show(); 
        this.SessionAbandon();
    }
 
    function DisplayIddleWarning() {
        $find('warningMPE').show();
    }
 
    function HideIddleWarning() {
        $find('warningMPE').hide();
    }

    function HideIddleWarning2() {
          
        $find('warningMPE2').hide();
         
        
        
    }
 
</script>
    

</asp:Content>

