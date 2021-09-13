<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrUsuarios.aspx.cs" Inherits="GafLookPaid.wfrUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Usuario</h1>
    <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" />
    <table>
        <tr>
            <td>* Empresa:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlEmpresas" AppendDataBoundItems="True" DataValueField="idEmpresa"
                 DataTextField="RazonSocial"/>
            </td>
        </tr>
       
        <tr>
            <td>* Email (inicio de sesión)</td>
            <td>
                <asp:TextBox runat="server" ID="txtEmail" />
                <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="* Requerido" />
                <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ErrorMessage="* Direccion invalida"
                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" />
            </td>
        </tr>
         <tr>
            <td>* Nombre Completo</td>
            <td>
                <asp:TextBox runat="server" ID="txtNombreCompleto" Width="300px" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtNombreCompleto" Display="Dynamic" ErrorMessage="* Requerido" />

            </td>
        </tr>
         <tr>
            <td>* Iniciales</td>
            <td>
                <asp:TextBox runat="server" ID="txtIniciales" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIniciales" Display="Dynamic" ErrorMessage="* Requerido" />
            </td>
        </tr>
        <tr>
            <td>* Password:</td>
            <td>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword"
                    ErrorMessage="* Requerido" Display="Dynamic" />
                <asp:RegularExpressionValidator runat="server" ID="revPassword" ControlToValidate="txtPassword"
                    Display="Dynamic" ErrorMessage="* El password no cumple con las politicas de seguridad"
                    ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%+-_]).{8,20})"  />
                <asp:CompareValidator runat="server" ID="cvPassword" ControlToValidate="txtPassword" Display="Dynamic"
                    ControlToCompare="txtConfirmarPassword" ErrorMessage="* La confirmacion y el password no coinciden" Operator="Equal"  />
            </td>
        </tr>
        <tr>
            <td>Confirmar Password:</td>
            <td><asp:TextBox runat="server" ID="txtConfirmarPassword" TextMode="Password" /></td>
        </tr>

        <tr>
            <td>Perfil:</td>
            <td><asp:DropDownList runat="server" ID="ddlPerfiles" AppendDataBoundItems="True" >
                <asp:ListItem>Nomina</asp:ListItem>
                <asp:ListItem>Administrador</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div align="right">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click"/>&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" PostBackUrl="wfrUsuariosConsulta.aspx"/>
    </div>
</asp:Content>
