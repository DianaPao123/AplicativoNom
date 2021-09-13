<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresa.aspx.cs" Inherits="GafLookPaid.wfrEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Empresa</h1>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <table>
        <tr>
            <td>* Campos Requeridos</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>RFC:</td>
            <td><asp:TextBox runat="server" ID="txtRFC" Width="150px" /></td>
        </tr>
        <tr>
            <td>Razón Social:</td>
            <td><asp:TextBox runat="server" ID="txtRazonSocial" Width="400px" /></td>
        </tr>
         <tr>
            <td>* Régimen Fiscal:</td>
            <td><asp:DropDownList runat="server" ID="ddlRegimen" AutoPostBack="True" 
                    onselectedindexchanged="ddlRegimen_SelectedIndexChanged" >
                    <asp:ListItem Value="Régimen General de Ley Personas Morales" Text="Régimen General de Ley Personas Morales" runat="server" />
                    <asp:ListItem Value="Personas Morales con Fines no Lucrativos" Text="Personas Morales con Fines no Lucrativos" runat="server" />
                    <asp:ListItem Value="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" Text="Régimen de las Personas Físicas con Actividades Empresariales y Profesionales" runat="server" />
                    <asp:ListItem Value="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" Text="Régimen Intermedio de las Personas Físicas con Actividades Empresariales" runat="server" />
                    <asp:ListItem Value="Régimen de Arrendamiento" Text="Régimen de Arrendamiento" runat="server" />
                    <asp:ListItem Value="Otro" Text="Otro" runat="server" />

                </asp:DropDownList></td>
                <tr>
                    <td></td>
                     <td runat="server" ID="tdRegimen" Visible="False" >
                Escribe el régimen: <asp:TextBox runat="server" ID="txtRegimen"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valRegimen" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtRegimen"></asp:RequiredFieldValidator>
            </td>

                </tr>
           
        </tr>
        <tr>
            <td>CURP: <span style="font-size: x-small">(Si aplica)</span></td>
            <td><asp:TextBox runat="server" ID="txtCURP" MaxLength="24" /></td>
        </tr>
        <tr>
            <td>* Calle:</td>
            <td><asp:TextBox runat="server" ID="txtCalle" Width="400px" />
                <asp:RequiredFieldValidator ID="valRegimen0" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtCalle"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>* Número Exterior:</td>
            <td><asp:TextBox runat="server" ID="txtNoExt" Width="400px" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="valRegimen4" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtNoExt"></asp:RequiredFieldValidator>
            </td>
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
            <td><asp:TextBox runat="server" ID="txtMunicipio" Width="400px" />
                <asp:RequiredFieldValidator ID="valRegimen1" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtMunicipio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>* Estado:</td>
            <td><asp:TextBox runat="server" ID="txtEstado" Width="400px" />
                <asp:RequiredFieldValidator ID="valRegimen2" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtEstado"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>* C.P.:</td>
            <td><asp:TextBox runat="server" ID="txtCP" Width="75px" />
                <asp:RequiredFieldValidator ID="valRegimen3" ErrorMessage="Campo requerido" 
                             runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtCP"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Teléfono:</td>
            <td><asp:TextBox runat="server" ID="txtTelefono" Width="400px" /></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox runat="server" ID="txtEmail" Width="400px" /></td>
        </tr>
        <tr>
            <td>Web:</td>
            <td><asp:TextBox runat="server" ID="txtWeb" Width="400px" /></td>
        </tr>
         <tr>
            <td>Orientación del Archivo Pdf:</td>
            <td><asp:DropDownList runat="server" ID="ddlOrientacion" >
                    <asp:ListItem Value="0" Text="Vertical" ></asp:ListItem> 
                    <asp:ListItem Value="1" Text="Horizontal" ></asp:ListItem> 
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Leyenda Encabezado:</td>
            <td><asp:TextBox runat="server" ID="txtLeyendaSuperior" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Leyenda pie de Página:</td>
            <td><asp:TextBox runat="server" ID="txtLeyendaPie" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel runat="server" ID="pnlSucursal" Visible="False">
            <tr>
                <td>Sucursal:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSucursal" Width="400px" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvSucursal" ControlToValidate="txtSucursal"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                </td>
            </tr>
            <tr>
                <td>Lugar de Expedición:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtLugarExpedicion" Width="300px" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ControlToValidate="txtLugarExpedicion"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>Contacto Administrativo:</td>
            <td><asp:TextBox runat="server" ID="txtContacto" Width="400px" /></td>
        </tr>
        <tr>
            <td>Logo Empresa:</td>
            <td>
                <asp:FileUpload runat="server" ID="fuLogoEmpresa" />
                </td>
                <td>(Máximo 50 Kb)</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td />
            <td>Debes de modificar los 3 siguientes datos en conjunto del Certificado del Sello 
                Digital.</td>
        </tr>
        
        <tr>
            <td>Certificado:</td>
            <td>
                <asp:FileUpload runat="server" ID="fuCertificado" Width="300px" />

            </td>
            <td> <asp:Label runat="server" ID="lblVencimiento"></asp:Label></td>
        </tr>
        <tr>
            <td>Llave Privada:</td>
            <td>
                <asp:FileUpload runat="server" ID="fuLlave" Width="300px" />

            </td>
            <td> <asp:Label runat="server" ID="lblAdvertencia" ForeColor="Red" ></asp:Label></td>
        </tr>
        <tr>
            <td>Password Llave:</td>
            <td>
                <asp:TextBox runat="server" ID="txtPassWordLlave" TextMode="Password" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnValidar" runat="server" Text="Validar" 
                    onclick="btnValidar_Click" />
                <asp:Label ID="Label1" runat="server" 
                    Text="Validar y Cargar de nuevo y dar  Click en Guardar"></asp:Label>
            </td>
            
        </tr>
    </table>
    
    <div align="right">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" onclick="btnCancelar_Click" CausesValidation="False" />
    </div>
</asp:Content>
