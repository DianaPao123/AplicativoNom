<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrUsuariosConsulta.aspx.cs" Inherits="GafLookPaid.wfrUsuariosConsulta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
    .mpeBack
    {
	    background-color: Gray;
	    filter: alpha(opacity=70);
	    opacity: 0.7;
    }
    </style>
    <h1>Usuarios</h1>
    <p>
        <asp:Label runat="server" ID="lblMensaje" />
    </p>
    Empresa:
    <asp:DropDownList runat="server" ID="ddlEmpresas" AutoPostBack="True" AppendDataBoundItems="True"
     DataValueField="idEmpresa" DataTextField="RazonSocial" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" />
    <br />

    <asp:GridView runat="server" ID="gvUsuarios" AutoGenerateColumns="False" onrowcommand="gvUsuarios_RowCommand"
      AllowPaging="True" onpageindexchanging="gvUsuarios_PageIndexChanging">
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
    <br />
    <div align="right">
        <asp:Button runat="server" ID="btnNuevoUsuario" Text="Nuevo Usuario" 
            onclick="btnNuevoUsuario_Click"/>
    </div>
    <asp:ModalPopupExtender runat="server" ID="mpeCambiarPassword" TargetControlID="btnPasswordDummy" BackgroundCssClass="mpeBack"
     CancelControlID="btnCerrarPassword" PopupControlID="pnlCambiarPassword" />
    <asp:Panel runat="server" ID="pnlCambiarPassword" style="text-align: center;" Width="800px" BackColor="White">
        <h1>Cambiar Password</h1>
        <asp:Label runat="server" ID="lblUserIdCambiarPassword" Visible="False" />
        <table align="center">
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPassword" Width="250px" TextMode="Password"  />
                </td>
            </tr>
            <tr>
                <td />
                <td>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword" 
                     ErrorMessage="* Requerido" ValidationGroup="CambiarPassword" Display="Dynamic" />
                    <asp:RegularExpressionValidator runat="server" ID="revPassword" ControlToValidate="txtPassword"
                     Display="Dynamic" ErrorMessage="* El password no cumple con las politicas de seguridad"
                     ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%+-_]).{8,20})" ValidationGroup="CambiarPassword" />
                    <asp:CompareValidator runat="server" ID="cvPassword" ControlToValidate="txtPassword" Display="Dynamic"
                     ControlToCompare="txtConfirmarPassword" ErrorMessage="* La confirmacion y el password no coinciden"
                      Operator="Equal" ValidationGroup="CambiarPassword" />
                </td>
            </tr>
            <tr>
                <td>Confirmar:</td>
                <td><asp:TextBox runat="server" ID="txtConfirmarPassword" Width="250px" TextMode="Password" /></td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="btnAceptarPassword" Text="Cambiar" onclick="btnAceptarPassword_Click"
         ValidationGroup="CambiarPassword" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCerrarPassword" Text="Cancelar" />
    </asp:Panel>
    <asp:Button runat="server" ID="btnPasswordDummy" style="display: none;"/>
    <asp:ModalPopupExtender runat="server" ID="mpeEditarUsuario" TargetControlID="btnDummy2" BackgroundCssClass="mpeBack"
     CancelControlID="btnCerrarEdicion" PopupControlID="panelEditar" />
    <asp:Panel runat="server" ID="panelEditar" style="text-align: center;" Width="800px" BackColor="White">
        <h1>Editar Usuario</h1>
        <asp:Label runat="server" ID="Label1" Visible="False" />
        <table align="center">
            <tr>
                <td>Nombre:</td>
                <td>
                <asp:HiddenField runat="server" ID="txtIdUsuario" />
                    <asp:TextBox runat="server" ID="txtNombre" Width="250px" />
                   
                </td>
                <td>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtNombre" 
                     ErrorMessage="* Requerido" ValidationGroup="EditarUsuario" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td>Iniciales:</td>
                <td><asp:TextBox runat="server" ID="txtIniciales" Width="250px" /></td>
                 <td>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIniciales" 
                     ErrorMessage="* Requerido" ValidationGroup="EditarUsuario" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>Perfil:</td>
                
                 <td style="text-align: left;">
                    <asp:DropDownList runat="server" ID="ddlPerfil">
                        <asp:ListItem runat="server" Text="Nomina" Value="Nomina"></asp:ListItem>
                        <asp:ListItem runat="server" Text="Administrador" Value="Administrador"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="btnGuardarEdicion" 
            ValidationGroup="EditarUsuario" Text="Guardar" onclick="btnGuardarEdicion_Click"
         />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCerrarEdicion" Text="Cancelar" />
    </asp:Panel>
    <asp:Button runat="server" ID="btnDummy2" style="display: none;"/>


</asp:Content>