<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEditaCentro.aspx.cs" Inherits="GafLookPaid.wfrEditaCentro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Empresa</h1>
    <asp:UpdatePanel ID="upd1" runat="server">
    <ContentTemplate>
     <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <asp:HiddenField runat="server" ID="hfEmpresa"/>
    <table>
        <tr>
            <td>* Campos obligatorios</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                * Nombre:</td>
            <td>
                <asp:TextBox ID="txtRazonSocial" runat="server" Width="400px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtRazonSocial" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">Registro Patronal:</td>
            <td class="style1"><asp:TextBox runat="server" ID="txtRegistroPatronal" Width="400px" /></td>
            <td class="style1">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                
               
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:CheckBox ID="chkDireccionEmpresa" runat="server" AutoPostBack="True"
                    Text="El Domicilio es el mismo que la empresa" OnCheckedChanged="CheckBox1_OnCheckedChanged"
                    />
            </td>
        </tr>
        <tr>
            <td>* Calle:</td>
            <td><asp:TextBox runat="server" ID="txtCalle" Width="400px" ClientIDMode="Static" /></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtCalle" ErrorMessage="Campo Requerido" 
                    ClientIDMode="Static" Enabled="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>* Numero Exterior:</td>
            <td><asp:TextBox runat="server" ID="txtNoExt" Width="400px" ClientIDMode="Static" /></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNoExt" ErrorMessage="Campo Requerido" 
                    ClientIDMode="Static" Enabled="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Numero Interior:</td>
            <td><asp:TextBox runat="server" ID="txtNoInt" Width="400px" ClientIDMode="Static" /></td>
            <td></td>
        </tr>
        <tr>
            <td>Colonia:</td>
            <td><asp:TextBox runat="server" ID="txtColonia" Width="400px" 
                    ClientIDMode="Static" /></td>
            <td></td>
        </tr>
        <tr>
            <td>Municipio:</td>
            <td><asp:TextBox runat="server" ID="txtMunicipio" Width="400px" 
                    ClientIDMode="Static" /></td>
            <td></td>
        </tr>
        <tr>
            <td>Estado:</td>
            <td><asp:TextBox runat="server" ID="txtEstado" Width="400px" 
                    ClientIDMode="Static" /></td>
            <td></td>
        </tr>
        <tr>
            <td>*País:</td>
            <td><asp:TextBox runat="server" ID="txtPais" Width="400px" ClientIDMode="Static" /></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtPais" ErrorMessage="Campo Requerido" 
                    ClientIDMode="Static" Enabled="True"></asp:RequiredFieldValidator></td>
        </tr>


        <tr>
            <td>* Riesgo de trabajo:</td>
            <td>
            <asp:DropDownList runat="server" ID="ddlRiesgoPuesto">
                <asp:ListItem Text="Clase I" Value="1"></asp:ListItem>
                <asp:ListItem Text="Clase II" Value="2"></asp:ListItem>
                <asp:ListItem Text="Clase III" Value="3"></asp:ListItem>
                <asp:ListItem Text="Clase IV" Value="4"></asp:ListItem>
                <asp:ListItem Text="Clase V" Value="5"></asp:ListItem>
            </asp:DropDownList>


                            </td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="ddlRiesgoPuesto" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator></td>
        </tr>


    </table>

    <br />
    </ContentTemplate>
    </asp:UpdatePanel>
   
    
    
    <div align="right">
        <asp:Button runat="server" ID="btnSave" Text="Guardar" onclick="btnSave_Click" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" 
            onclick="btnCancel_Click" CausesValidation="False" />
    </div>

</asp:Content>
