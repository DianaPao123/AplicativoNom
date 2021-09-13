<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="GafLookPaid._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent" >
    <style type="text/css">
    .modalBackground {
        background-color: #666;
        filter: alpha(opacity=70);
        opacity: 0 ;
    }
    .modalPopup
    {
      background-color: #AACE48;
      border-width: 2px;
      border-style: solid;
      border-color: #000;
      padding: 8px;
      width: 300px;
      text-align: center;
      
    }
</style>
     <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
     
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" t>
    

    
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <asp:Image runat="server" Width="100%" Height="100%"  ID ="ImgInicio" ImageUrl="images/bienvenido.png"/>

    
   <script type="text/javascript"> 
       function cuenta() {
           document.forms[0].caracteres.value = document.forms[0].texto.value.length
       } 
</script>
   

    <!-- PopupAdvertencia -->

    <asp:Panel runat="server" ID="warningPopup" style="display: none;" CssClass="modalPopup" Font-Names ="Arial " HorizontalAlign="Center" >
    <div class="orderLabel">
        <h1><b>Advertencia</b></h1>
        Tu sesión está a punto de expirar debido a inactividad.
        <br /><br />

    <input id="btnWarningOK" type="button" value="Click para continuar." onclick="HideIddleWarning()" />
    </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="warningMPE" runat="server"
    TargetControlID="dummyLink" PopupControlID="warningPopup"
    BehaviorID="warningMPE" BackgroundCssClass="modalBackground" />
    <asp:HyperLink ID="dummyLink" runat="server" NavigateUrl="#"></asp:HyperLink>

    <!-- *************************************************************************** -->

   
    <!-- Cerrar sesion --> 
    <asp:Panel runat="server" ID="popupcerrar" style="display: none;" CssClass="modalPopup">
    <div class="orderLabel">
        La sesión ha expirado.
    <br /><br />
    <input id="btncerrar" type="button" value="Entendido" onclick="HideIddleWarning2()"  />
    </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="cerrar" runat="server"
    TargetControlID="HyperLink" PopupControlID="popupcerrar"
    BehaviorID="warningMPE2" BackgroundCssClass="modalBackground" />
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



