<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrSucursales.aspx.cs" Inherits="GafLookPaid.wfrSucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sucursal</h1>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <table>
        <tr>
            <td>* Nombre:</td>
            <td>
                <asp:HiddenField runat="server" ID="txtIdEmpresa"/>
                <asp:TextBox runat="server" ID="txtNombre" Width="400px" />
                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtNombre" />
            </td>
        </tr>
        <tr>
            <td>* Lugar de expedición:</td>
            <td>
                <asp:TextBox runat="server" ID="txtLugarExpedicion"  Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtLugarExpedicion" />
            </td>
        </tr>
        <tr>
            <td>* Domicilio:</td>
            <td>
                <asp:TextBox runat="server" ID="txtDomicilio"  Width="400px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtDomicilio" />
            </td>
        </tr>
    </table>
    <div align="right">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" 
            CausesValidation="False" onclick="btnCancelar_Click" />
    </div>
</asp:Content>
