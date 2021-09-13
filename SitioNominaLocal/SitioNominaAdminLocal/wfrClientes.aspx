<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrClientes.aspx.cs" Inherits="SitioNominaAdmin.wfrClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server"></asp:Label></h1>
    <h1>Cliente</h1>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <table>
        <tr>
            <td>Tipo de Cliente:</td>
            <td><asp:DropDownList runat="server" ID="ddlTipoCliente" AutoPostBack="True">
                    <asp:ListItem runat="server" Text="Facturación" Value="0" ></asp:ListItem>
              <%--     <asp:ListItem runat="server" Text="Timbrado" Value="1"></asp:ListItem>
                   <asp:ListItem runat="server" Text="Distribuidor" Value="2"></asp:ListItem>--%>
                </asp:DropDownList></td>
        </tr>
    </table>
    <div runat="server" ID="divUsuarios">
    <table>
        <tr>
            <td>* RFC:</td>
            <td><asp:TextBox runat="server" ID="txtRFC" Width="150px" /></td>
        </tr>
        <tr>
            <td>* Razon Social:</td>
            <td><asp:TextBox runat="server" ID="txtRazonSocial" Width="400px" /></td>
        </tr>
         <tr>
            <td>Régimen Fiscal:</td>
            <td><asp:DropDownList runat="server" ID="ddlRegimen" >
                    <asp:ListItem Value="Régimen General de Ley Personas Morales" Text="Régimen General de Ley Personas Morales" runat="server" />
                    <asp:ListItem Value="Personas Morales con Fines no Lucrativos" Text="Personas Morales con Fines no Lucrativos" runat="server" />
                    <asp:ListItem Value="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" Text="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" runat="server" />
                    <asp:ListItem Value="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" Text="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" runat="server" />
                    <asp:ListItem Value="Régimen de Arrendamiento" Text="Régimen de Arrendamiento" runat="server" />

                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>* Calle:</td>
            <td><asp:TextBox runat="server" ID="txtCalle" Width="400px" /></td>
        </tr>
        <tr>
            <td>* Número Exterior:</td>
            <td><asp:TextBox runat="server" ID="txtNoExt" Width="400px" ClientIDMode="Static" /></td>
        </tr>
        <tr>
            <td>Numero Interior:</td>
            <td><asp:TextBox runat="server" ID="txtNoInt" Width="400px" ClientIDMode="Static" /></td>
        </tr>
        <tr>
            <td>Colonia:</td>
            <td><asp:TextBox runat="server" ID="txtColonia" Width="400px" /></td>
        </tr>
        <tr>
            <td>* Municipio:</td>
            <td><asp:TextBox runat="server" ID="txtMunicipio" Width="400px" /></td>
        </tr>
        <tr>
            <td>* Estado:</td>
            <td><asp:TextBox runat="server" ID="txtEstado" Width="400px" /></td>
        </tr>
        <tr>
            <td>* C.P.:</td>
            <td><asp:TextBox runat="server" ID="txtCP" Width="75px" /></td>
        </tr>
        <tr>
            <td>Telefono:</td>
            <td><asp:TextBox runat="server" ID="txtTelefono" Width="400px" /></td>
        </tr>
        <tr>
            <td>* Email:</td>
            <td><asp:TextBox runat="server" ID="txtEmail" Width="400px" /></td>
        </tr>
        <tr>
            <td>Contacto:</td>
            <td><asp:TextBox runat="server" ID="txtContacto" Width="400px" /></td>
        </tr>
        
        <tr>
            <td>Nombre del Administrador:</td>
            <td><asp:TextBox runat="server" ID="txtNombreAdmin" Width="400px" /></td>
        </tr>
         
        <tr>
            <td>Iniciales del Administrador:</td>
            <td><asp:TextBox runat="server" ID="txtInicialesAdmin" Width="400px" /></td>
        </tr>
        <tr>
            <td>Folios Contratados:</td>
            <td><asp:TextBox runat="server" ID="txtFolios" Width="400px" Enabled="False" />
                <asp:CompareValidator runat="server" ControlToValidate="txtFolios" ErrorMessage="Campo Inválido" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ></asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td>Folios Consumidos:</td>
            <td><asp:TextBox runat="server" ID="txtConsumidos" Width="400px" Enabled="False" />
            </td>
        </tr>

        <tr>
            <td>Timbres Contratados:</td>
            <td><asp:TextBox runat="server" ID="txtTimbresContratados" Width="400px" />
                <asp:CompareValidator runat="server" ControlToValidate="txtTimbresContratados" ErrorMessage="Campo Inválido" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ID="CompareValidator1" ></asp:CompareValidator>
            </td>
        </tr>

        <tr >
            <td colspan="2">
                <asp:Label ID="LblMensaje" runat="server" />    
            </td>
        </tr>
        </table>
        </div>
        
        <div runat="server" ID="divDistribuidores">
    <table>
        <tr>
            <td>&nbsp;RFC:</td>
            <td><asp:TextBox runat="server" ID="txtDisRfc" Width="150px" /></td>
        </tr>
        <tr>
            <td>* Razon Social:</td>
            <td><asp:TextBox runat="server" ID="txtDisRazonSocial" Width="400px" /></td>
        </tr>
         <tr>
            <td>Régimen Fiscal:</td>
            <td><asp:DropDownList runat="server" ID="ddlDisRegimen" >
                    <asp:ListItem Value="Régimen General de Ley Personas Morales" Text="Régimen General de Ley Personas Morales" runat="server" />
                    <asp:ListItem Value="Personas Morales con Fines no Lucrativos" Text="Personas Morales con Fines no Lucrativos" runat="server" />
                    <asp:ListItem Value="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" Text="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" runat="server" />
                    <asp:ListItem Value="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" Text="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" runat="server" />
                    <asp:ListItem Value="Régimen de Arrendamiento" Text="Régimen de Arrendamiento" runat="server" />

                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Usuario:</td>
            <td><asp:TextBox runat="server" ID="txtDisNombre" Width="400px" /></td>
        </tr>
        <tr>
            <td>Dirección:</td>
            <td><asp:TextBox runat="server" ID="txtDisDireccion" Width="400px" /></td>
        </tr>
        <tr>
            <td>Colonia:</td>
            <td><asp:TextBox runat="server" ID="txtDisColonia" Width="400px" /></td>
        </tr>
        <tr>
            <td>* Municipio:</td>
            <td><asp:TextBox runat="server" ID="txtDisMunicipio" Width="400px" /></td>
        </tr>
        <tr>
            <td>&nbsp;Estado:</td>
            <td><asp:TextBox runat="server" ID="txtDisEstado" Width="400px" /></td>
        </tr>
        <tr>
            <td>C.P.:</td>
            <td><asp:TextBox runat="server" ID="txtDisCp" Width="75px" /></td>
        </tr>
        <tr>
            <td>Telefono:</td>
            <td><asp:TextBox runat="server" ID="txtDisTelefono" Width="400px" /></td>
        </tr>
        <tr>
            <td>&nbsp;Email:</td>
            <td><asp:TextBox runat="server" ID="txtDisEmail" Width="400px" /></td>
        </tr>
        <tr>
            <td>Contacto:</td>
            <td><asp:TextBox runat="server" ID="txtDisContacto" Width="400px" /></td>
        </tr>
        <tr>
            <td>N. Contrato:</td>
            <td><asp:TextBox runat="server" ID="txtDisContrato" Width="400px" /></td>
        </tr>
        <tr>
            <td>ID2:</td>
            <td><asp:TextBox runat="server" ID="txtID2" Width="400px" /></td>
        </tr>
         <tr>
            <td>ID3:</td>
            <td><asp:TextBox runat="server" ID="txtID3" Width="400px" /></td>
        </tr>
        <tr >    
            <td colspan="2">
                <asp:Label ID="lblDistribuidor" runat="server" />    
            </td>
        </tr>
        </table>
        </div>
        <div align="right" runat="server" ID="divUsers">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" />&nbsp;&nbsp;
    </div>
    <div align="right" runat="server" ID="divDis">
        <asp:Button runat="server" ID="btnGuardaDis" Text="Guardar" 
            onclick="btnGuardaDis_Click" />&nbsp;&nbsp;
    </div>
</asp:Content>
