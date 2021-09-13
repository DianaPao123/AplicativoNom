<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrUsuariosConsulta.aspx.cs" Inherits="GafLookPaid.wfrUsuariosConsulta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <%--   <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />--%>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
    <link href="Content/Mensajes.css" rel="stylesheet" />
     <link href="Content/UpdateProgress.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Usuarios</h3>
            </div>
            <div class ="card-body" >
                  <div class = "row form-group"> 
                  <div class="col-lg-12 " style="color:red;" >
                            <asp:Label ID="lblMensaje" class="control-label" runat="server" ForeColor="Red" Font-Bold="true" style=" font-size: x-small; text-align: left; font-variant: small-caps;" Width="250px"></asp:Label>
            </div>
            </div>
        
                   <div class = "row">
                                <div class = "form-group col-lg-6">
                                 <asp:Label ID="Label2" runat="server" class="control-label" Text="Empresa"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlEmpresas" AutoPostBack="True"
                        AppendDataBoundItems="True" CssClass="form-control"
     DataValueField="idEmpresa" DataTextField="RazonSocial" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" />
                                    </div>
                       </div>
    <br />
    

                 <div class="border border-success" style=" width:95%;   background-color: #2d282808;margin:0px auto  " >
                        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" GridLines="None" 
                          ShowHeaderWhenEmpty="True" Width="100%"  AlternatingRowStyle-HorizontalAlign="Left"
                             RowStyle-HorizontalAlign="Left"     onrowcommand="gvUsuarios_RowCommand" 
                            onpageindexchanging="gvUsuarios_PageIndexChanging"
                            CssClass="table table-hover table-striped grdViewTable"    >
                                 <rowstyle Height="6px" /><alternatingrowstyle  Height="6px"/>
                            
                     <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="UserId" />
            <asp:BoundField HeaderText="Nombre" DataField="UserName" />
            <asp:BoundField HeaderText="Perfil" DataField="Perfil" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Status" DataField="IsLockedOut" />
            <asp:ButtonField Text="Editar" CommandName="EditarUsuario" />
            <asp:ButtonField Text="Cambiar Password" CommandName="CambiarPassword" />
            
            <%--<asp:ButtonField Text="Editar" ButtonType="Link" CommandName="EditarUsuario" />--%>
        </Columns>
    </asp:GridView>
                     </div>
    <br />
    <div align="right">
        <asp:Button runat="server" ID="btnNuevoUsuario" Text="Nuevo Usuario"  CssClass="btn btn-outline-success" 
            onclick="btnNuevoUsuario_Click"/>
    </div>


    <asp:ModalPopupExtender runat="server" ID="mpeCambiarPassword" TargetControlID="btnPasswordDummy" BackgroundCssClass="modalBackground"
     CancelControlID="btnCerrarPassword" PopupControlID="pnlCambiarPassword" />
    <asp:Panel runat="server" ID="pnlCambiarPassword" Style="display: none;" CssClass="modalPopup" >
     <div class="header" >
            Cambiar Password
        </div>
        <div class="body" style="text-align: center;">
       <div class = "row"> 
                <div class="col-lg-12" >
                
            <asp:Label runat="server" ID="lblUserIdCambiarPassword" Visible="False" />
                    </div>
            </div>
                  <div class = "row" style="text-align:left;"> 
                 <div class="col-lg-1" >
                </div>
                <div class="col-lg-3" >
                <asp:Label  class="control-label" ID="Label4" runat="server" Text="Password"></asp:Label>
                </div>
                    <div class="col-lg-6" >
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password"  />
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword" 
                     ErrorMessage="* Requerido" ValidationGroup="CambiarPassword" Display="Dynamic" />
                    <asp:RegularExpressionValidator runat="server" ID="revPassword" ControlToValidate="txtPassword"
                     Display="Dynamic" ErrorMessage="* El password no cumple con las politicas de seguridad"
                     ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%+-_]).{8,20})" ValidationGroup="CambiarPassword" />
                    <asp:CompareValidator runat="server" ID="cvPassword" ControlToValidate="txtPassword" Display="Dynamic"
                     ControlToCompare="txtConfirmarPassword" ErrorMessage="* La confirmacion y el password no coinciden"
                      Operator="Equal" ValidationGroup="CambiarPassword" />
                </div>
                      </div>
                  <div class = "row" style="text-align:left;"> 
                 <div class="col-lg-1" >
                </div>
                <div class="col-lg-3" >
                <asp:Label  class="control-label" ID="Label6" runat="server" Text="Confirmar"></asp:Label>
                </div>
                    <div class="col-lg-6" >
                <asp:TextBox runat="server" ID="txtConfirmarPassword"  CssClass="form-control" TextMode="Password" />
                </div>
                      </div>

        <br />
            <div class = "row"> 
                <div class="col-lg-12" >
           
        <asp:Button runat="server" ID="btnAceptarPassword" Text="Cambiar" onclick="btnAceptarPassword_Click" 
            CssClass="btn btn-outline-success"
         ValidationGroup="CambiarPassword" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCerrarPassword"  CssClass="btn btn-outline-success" Text="Cancelar" />
            </div>
                </div>
        </div>


    </asp:Panel>


    <asp:Button runat="server" ID="btnPasswordDummy" style="display: none;"/>
    <asp:ModalPopupExtender runat="server" ID="mpeEditarUsuario" TargetControlID="btnDummy2" BackgroundCssClass="modalBackground"
     CancelControlID="btnCerrarEdicion" PopupControlID="panelEditar" />
    <asp:Panel runat="server" ID="panelEditar" Style="display: none;" CssClass="modalPopup">
        <div class="header" >
            Editar Usuario
        </div>
        <div class="body" style="text-align: center;">
        <div class = "row"> 
                <div class="col-lg-12" >
                
            <asp:Label runat="server" ID="Label1" Visible="False" />
                    </div>
            </div>

            <div class = "row" style="text-align:left;"> 
                 <div class="col-lg-1" >
                </div>
                <div class="col-lg-3" >
                <asp:Label  class="control-label" ID="Label10" runat="server" Text="Nombre"></asp:Label>
                </div>
                    <div class="col-lg-3" >
                
                    <asp:HiddenField runat="server" ID="txtIdUsuario" />
                    <asp:TextBox runat="server" ID="txtNombre" Width="250px" CssClass="form-control" />
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtNombre" 
                     ErrorMessage="* Requerido" ValidationGroup="EditarUsuario" Display="Dynamic" />
            
                </div>
                </div>
            <div class = "row " style="text-align:left;"> 
                               <div class="col-lg-1" >
                </div>
             <div class="col-lg-3" >
                <asp:Label  class="control-label" ID="Label5" runat="server" Text="Iniciales"></asp:Label>
                  </div>  <div class="col-lg-3" >
                
               <asp:TextBox runat="server" ID="txtIniciales" Width="250px" CssClass="form-control" />
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIniciales" 
                     ErrorMessage="* Requerido" ValidationGroup="EditarUsuario" Display="Dynamic" />
                </div>
                </div>
            <div class = "row " style="text-align:left;"> 
                               <div class="col-lg-1" >
                </div>
               <div class="col-lg-3" >
                <asp:Label  class="control-label" ID="Label3" runat="server" Text="Perfil"></asp:Label>
                </div>    <div class="col-lg-3" >
                
                   <asp:DropDownList runat="server" ID="ddlPerfil" CssClass="form-control">
                        <asp:ListItem runat="server" Text="Nomina" Value="Nomina"></asp:ListItem>
                        <asp:ListItem runat="server" Text="Administrador" Value="Administrador"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                </div>
        <br />
             <div class = "row"> 
                <div class="col-lg-12" >
           
        <asp:Button runat="server" ID="btnGuardarEdicion" CssClass="btn btn-outline-success"
            ValidationGroup="EditarUsuario" Text="Guardar" onclick="btnGuardarEdicion_Click" />
              <asp:Button runat="server" ID="btnCerrarEdicion" CssClass="btn btn-outline-success" Text="Cancelar" />
    
         
                    </div>
                 </div>
       
            </div>

    </asp:Panel>
    <asp:Button runat="server" ID="btnDummy2" style="display: none;"/>

                </div>
         </div>
   
    
</asp:Content>