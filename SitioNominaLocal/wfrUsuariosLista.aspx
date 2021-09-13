<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrUsuariosLista.aspx.cs" Inherits="SitioNominaAdmin.wfrUsuariosLista" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Usuarios</h1>
     <br/>
     <table>
         <tr>
             <td>Buscar:&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td><asp:TextBox runat="server" ID="txtBuscar" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td><asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
                     onclick="btnBuscar_Click"/></td>
         </tr>
    </table>
    <p>
        <asp:Label runat="server" ID="lblMensaje" />
    </p>
    <br/>

    <asp:GridView runat="server" ID="gvUsuarios" AutoGenerateColumns="False" 
         DataKeyNames="UserName,UserId" CellPadding="8" onrowcommand="gvUsuarios_RowCommand">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Usuario" />
            <asp:TemplateField HeaderText="Bloqueado" ShowHeader="False" >
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkDesbloquear" Text='<%#((bool) Eval("Bloqueado") ) ? "Desbloquear":""  %>' CommandName="Desbloquear" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField HeaderText="Cambiar Password" Text="Cambiar" CommandName="CambiarPass"/>
            <asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" />
        </Columns>
    </asp:GridView>
    
    <asp:ModalPopupExtender runat="server" ID="mpeCambiarPassword" TargetControlID="btnPasswordDummy" BackgroundCssClass="mpeBack"
     CancelControlID="btnCerrarPassword" PopupControlID="pnlCambiarPassword" />
    <asp:Panel runat="server" ID="pnlCambiarPassword" style="text-align: center;" Width="800px" BackColor="White">
        <h1>Cambiar Password</h1>
        <asp:Label runat="server" ID="lblUserIdCambiarPassword" Visible="False" />
        <table align="center">
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPassword" Width="250px" TextMode="Password" />
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
</asp:Content>
