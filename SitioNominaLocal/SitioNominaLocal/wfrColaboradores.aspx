<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrColaboradores.aspx.cs" Inherits="GafLookPaid.wfrColaboradores" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    
 
   
    
   
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
    </style>
   
    
 
   
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function SeleccionaBanco() {
            var txt = document.getElementById('txtClabe');
            var cmb = document.getElementById('ddlBanco');
            
            try {
                cmb.value = txt.value.toString().substr(0, 3);
                if(cmb.selectedIndex == -1) {
                    cmb.value = '000';   
                }
            } catch(e) {

            } 

        }
    </script>
    <h1>Editar Empleado</h1>
    <p>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    </p>
    <asp:TabContainer runat="server" ID="cfdTabContainer" Width="100%" 
        ActiveTabIndex="1" >
            <asp:TabPanel ID="tabGral" runat="server" HeaderText="Datos del Empleado"><ContentTemplate>
<table>
    <tr>
        <td class="auto-style1">Empresa Emisora de Recibos de Nómina:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlEmpresa" AppendDataBoundItems="True" DataTextField="RazonSocial" 
                                    DataValueField="IdEmpresa" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="ddlEmpresa" 
                                ErrorMessage="Emisora de Recibos: Dato requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1" style="color: #C0C0C0" >*Campos requeridos</td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label59" runat="server" Text="* Número de Empleado:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNumEmpleado0" runat="server" Enabled="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*RFC:</td>
        <td>
            <asp:TextBox runat="server" ID="txtRFC" Width="150px" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtRFC" ErrorMessage="RFC: Dato inválido" 
                                
                                
                                ValidationExpression="[A-Z,Ñ,&amp;amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?" 
                                SetFocusOnError="True"></asp:RegularExpressionValidator>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtRFC" ErrorMessage="RFC: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*CURP:</td>
        <td>
            <asp:TextBox runat="server" ID="txtCURP" MaxLength="24" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtCURP" ErrorMessage="CURP: Dato inválido" 
                                ValidationExpression="[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]"></asp:RegularExpressionValidator>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtCURP" ErrorMessage="CURP: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*Nombres:</td>
        <td>
            <asp:TextBox ID="txtNombres" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtNombres" 
                                ErrorMessage="Nombres: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*Apellido Paterno</td>
        <td>
            <asp:TextBox ID="txtApellidoPaterno" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                ControlToValidate="txtApellidoPaterno" 
                                ErrorMessage="Apellido paterno: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1" >*Apellido Materno:</td>
        <td >
            <asp:TextBox runat="server" ID="txtApellidoMaterno" Width="400px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                ControlToValidate="txtApellidoMaterno" 
                                ErrorMessage="Apellido materno: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Calle:</td>
        <td style="margin-left: 40px">
            <asp:TextBox runat="server" ID="txtCalle" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Número Exterior:</td>
        <td>
            <asp:TextBox ID="txtNoExt" runat="server" ClientIDMode="Static" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Número Interior:</td>
        <td>
            <asp:TextBox ID="txtNoInt" runat="server" ClientIDMode="Static" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Colonia:</td>
        <td>
            <asp:TextBox runat="server" ID="txtColonia" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Municipio:</td>
        <td>
            <asp:TextBox runat="server" ID="txtMunicipio" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Estado:</td>
        <td>
            <asp:TextBox runat="server" ID="txtEstado" Width="400px" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*País:</td>
        <td>
            <asp:TextBox runat="server" ID="txtPais" Width="400px" >México</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                ControlToValidate="txtPais" ErrorMessage="País: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">CP:</td>
        <td>
            <asp:TextBox runat="server" ID="txtCP" Width="75px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Teléfono:</td>
        <td>
            <asp:TextBox runat="server" ID="txtTelefono" Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">*Email:</td>
        <td>
            <asp:TextBox runat="server" ID="txtEmail" Width="400px" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtEmail" ErrorMessage="Email: Dato inválido" 
                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                ControlToValidate="txtEmail" ErrorMessage="Email: Campo requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">Bcc:</td>
        <td style="margin-left: 40px">
            <asp:TextBox ID="txtBcc" runat="server" Width="400px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                ControlToValidate="txtBcc" ErrorMessage="BCC:  Dato inválido" 
                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:ValidationSummary ID="ValidationSummary2" runat="server"></asp:ValidationSummary>
        </td>
        <td>&nbsp;</td>
    </tr>
</table>
</ContentTemplate>
</asp:TabPanel>
    
            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Datos Nómina"><ContentTemplate>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <table width="100%">
            <tr>
                <td  >
                    <h3>
                        <asp:Label ID="Label58" runat="server" Text="Datos de Nómina"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td  style="text-align: right"  >
                    <asp:Label ID="Label10" runat="server" Text="*Número de Empleado:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtNumEmpleado" runat="server" Enabled="False"></asp:TextBox>
                    
                    <asp:Label ID="lblNumEmpleado" runat="server" ForeColor="Red"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label13" runat="server" Text="NSS:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtNumSeguridadSocial" runat="server" MaxLength="15"></asp:TextBox>
                    
                    <asp:Label ID="lblNumSeguridadSocial" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                        ControlToValidate="txtNumSeguridadSocial" 
                        ErrorMessage="Dato inválido" 
                        ValidationExpression="[0-9]{11}"></asp:RegularExpressionValidator>
                </td>
                
            </tr>
            <tr>
                <td  style="text-align: right" >
                    <asp:Label ID="Label15" runat="server" Text="CLABE:"></asp:Label>
                </td>
                <td  >
                    <asp:TextBox ID="txtClabe" runat="server" ClientIDMode="Static" 
                        onchange="SeleccionaBanco()"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                        ControlToValidate="txtClabe" ErrorMessage="Dato inválido" 
                        ValidationExpression="[0-9]{18}"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblClabe" runat="server" ForeColor="Red"></asp:Label>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label17" runat="server" Text="*Fecha inicio laboral:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtFechaInicialLaboral" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="calExt" runat="server" Enabled="True" 
                        Format="yyyy-MM-dd" TargetControlID="txtFechaInicialLaboral">
                    </asp:CalendarExtender>
                   
                    <asp:Label ID="lblFechaInicialLaboral" runat="server" ForeColor="Red"></asp:Label>
                    &#160;<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                        ControlToValidate="txtFechaInicialLaboral" 
                        ErrorMessage="Campo requerido" SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                        ControlToValidate="txtFechaInicialLaboral" 
                        ErrorMessage="Dato inválido" 
                        ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])"></asp:RegularExpressionValidator>
                </td>
                
            </tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label19" runat="server" Text="Puesto:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtPuesto" runat="server"></asp:TextBox>
                  
                    <asp:Label ID="lblPuesto" runat="server" ForeColor="Red"></asp:Label>
                </td>

            </tr>
            <tr>
                <td style="text-align: right" >*<asp:Label ID="Label60" runat="server" Text="Cuota Diaria"></asp:Label>
                    :</td>
                <td >
                    <asp:TextBox ID="txtCuotaDiaria" runat="server">0</asp:TextBox>
                    <asp:Label ID="lblCuotaDiaria" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtCuotaDiaria" ErrorMessage="Campo requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label23" runat="server" Text="Salario Base Cotización"></asp:Label>
                    :</td>
                <td >
                    <asp:TextBox ID="txtSalarioBaseCotApor" runat="server">0</asp:TextBox>
                    <asp:Label ID="lblSalarioBaseCotApor" runat="server" ForeColor="Red"></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label25" runat="server" Text="Salario Diario Integrado:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtSalarioDiarioIntegro" runat="server">0</asp:TextBox>
                    <asp:Label ID="lblSalarioDiarioIntegro" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                </td>
               
            </tr>
           




            <tr>
                <td style="text-align: right" >
                   
                    <asp:Label ID="Label12" runat="server" Text="*Tipo de Régimen:"></asp:Label>
                </td>
                <td  >
                    <asp:DropDownList ID="ddlRegimen" runat="server" Width="200px">
                        <asp:ListItem Text="Sueldos y salarios" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Jubilados" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Pensionados" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Miembros de las Sociedades Cooperativas de Producción." Value="5"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Integrantes de Sociedades y Asociaciones Civiles" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Miembros de consejos directivos, de vigilancia, consultivos, honorarios a administradores, comisarios y gerentes generales." Value="7"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Actividad empresarial (comisionistas)" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Honorarios asimilados a salarios" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Asimilados a salarios, Ingresos acciones o títulos valor" Value="10"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblRegimen" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlRegimen" ErrorMessage="Campo requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                </tr>
            <tr>
                 <td style="text-align: right" >
                    <asp:Label ID="Label14" runat="server" Text="Departamento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDepartamento" runat="server"></asp:TextBox>
                   
                    <asp:Label ID="lblDepartamento" runat="server" ForeColor="Red"></asp:Label>
                </td>
                </tr>
            <tr>  <td style="text-align: right">
                    <asp:Label ID="Label16" runat="server" Text="Banco:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBanco" runat="server" ClientIDMode="Static">
                        <asp:ListItem Selected="True" Text="Selecciona un banco" Value="000"></asp:ListItem>
                        <asp:ListItem Text="BANAMEX " Value="002"></asp:ListItem>
                        <asp:ListItem Text="BANCOMEXT " Value="006"></asp:ListItem>
                        <asp:ListItem Text="BANOBRAS " Value="009"></asp:ListItem>
                        <asp:ListItem Text="BBVA BANCOMER " Value="012"></asp:ListItem>
                        <asp:ListItem Text="SANTANDER " Value="014"></asp:ListItem>
                        <asp:ListItem Text="BANJERCITO " Value="019"></asp:ListItem>
                        <asp:ListItem Text="HSBC " Value="021"></asp:ListItem>
                        <asp:ListItem Text="BAJIO " Value="030"></asp:ListItem>
                        <asp:ListItem Text="IXE " Value="032"></asp:ListItem>
                        <asp:ListItem Text="INBURSA " Value="036"></asp:ListItem>
                        <asp:ListItem Text="INTERACCIONES " Value="037"></asp:ListItem>
                        <asp:ListItem Text="MIFEL " Value="042"></asp:ListItem>
                        <asp:ListItem Text="SCOTIABANK " Value="044"></asp:ListItem>
                        <asp:ListItem Text="BANREGIO " Value="058"></asp:ListItem>
                        <asp:ListItem Text="INVEX " Value="059"></asp:ListItem>
                        <asp:ListItem Text="BANSI " Value="060"></asp:ListItem>
                        <asp:ListItem Text="AFIRME " Value="062"></asp:ListItem>
                        <asp:ListItem Text="BANORTE " Value="072"></asp:ListItem>
                        <asp:ListItem Text="THE ROYAL BANK " Value="102"></asp:ListItem>
                        <asp:ListItem Text="AMERICAN EXPRESS " Value="103"></asp:ListItem>
                        <asp:ListItem Text="BAMSA " Value="106"></asp:ListItem>
                        <asp:ListItem Text="TOKYO " Value="108"></asp:ListItem>
                        <asp:ListItem Text="JP MORGAN " Value="110"></asp:ListItem>
                        <asp:ListItem Text="BMONEX " Value="112"></asp:ListItem>
                        <asp:ListItem Text="VE POR MAS " Value="113"></asp:ListItem>
                        <asp:ListItem Text="ING " Value="116"></asp:ListItem>
                        <asp:ListItem Text="DEUTSCHE " Value="124"></asp:ListItem>
                        <asp:ListItem Text="CREDIT SUISSE " Value="126"></asp:ListItem>
                        <asp:ListItem Text="AZTECA " Value="127"></asp:ListItem>
                        <asp:ListItem Text="AUTOFIN " Value="128"></asp:ListItem>
                        <asp:ListItem Text="BARCLAYS " Value="129"></asp:ListItem>
                        <asp:ListItem Text="COMPARTAMOS " Value="130"></asp:ListItem>
                        <asp:ListItem Text="BANCO FAMSA " Value="131"></asp:ListItem>
                        <asp:ListItem Text="BMULTIVA " Value="132"></asp:ListItem>
                        <asp:ListItem Text="ACTINVER " Value="133"></asp:ListItem>
                        <asp:ListItem Text="WAL-MART " Value="134"></asp:ListItem>
                        <asp:ListItem Text="NAF136 " Value="135"></asp:ListItem>
                        <asp:ListItem Text="BANCOPPEL " Value="137"></asp:ListItem>
                        <asp:ListItem Text="ABC CAPITAL " Value="138"></asp:ListItem>
                        <asp:ListItem Text="UBS BANK " Value="139"></asp:ListItem>
                        <asp:ListItem Text="CONSUBANCO " Value="140"></asp:ListItem>
                        <asp:ListItem Text="VOLKSWAGEN " Value="141"></asp:ListItem>
                        <asp:ListItem Text="CIBANCO " Value="143"></asp:ListItem>
                        <asp:ListItem Text="BBASE " Value="145"></asp:ListItem>
                        <asp:ListItem Text="BANSEFI " Value="166"></asp:ListItem>
                        <asp:ListItem Text="HIPOTECARIA FEDERAL " Value="168"></asp:ListItem>
                        <asp:ListItem Text="MONEXCB " Value="600"></asp:ListItem>
                        <asp:ListItem Text="GBM " Value="601"></asp:ListItem>
                        <asp:ListItem Text="MASARI " Value="602"></asp:ListItem>
                        <asp:ListItem Text="VALUE " Value="605"></asp:ListItem>
                        <asp:ListItem Text="ESTRUCTURADORES " Value="606"></asp:ListItem>
                        <asp:ListItem Text="TIBER " Value="607"></asp:ListItem>
                        <asp:ListItem Text="VECTOR " Value="608"></asp:ListItem>
                        <asp:ListItem Text="B&amp;B " Value="610"></asp:ListItem>
                        <asp:ListItem Text="ACCIVAL " Value="614"></asp:ListItem>
                        <asp:ListItem Text="MERRILL LYNCH " Value="615"></asp:ListItem>
                        <asp:ListItem Text="FINAMEX " Value="616"></asp:ListItem>
                        <asp:ListItem Text="ORDER " Value="637"></asp:ListItem>
                        <asp:ListItem Text="AKALA " Value="638"></asp:ListItem>
                        <asp:ListItem Text="CB JPMORGAN " Value="640"></asp:ListItem>
                        <asp:ListItem Text="REFORMA " Value="642"></asp:ListItem>
                        <asp:ListItem Text="STP " Value="646"></asp:ListItem>
                        <asp:ListItem Text="TELECOMM " Value="647"></asp:ListItem>
                        <asp:ListItem Text="EVERCORE " Value="648"></asp:ListItem>
                        <asp:ListItem Text="SKANDIA " Value="649"></asp:ListItem>
                        <asp:ListItem Text="SEGMTY " Value="651"></asp:ListItem>
                        <asp:ListItem Text="ASEA " Value="652"></asp:ListItem>
                        <asp:ListItem Text="KUSPIT " Value="653"></asp:ListItem>
                        <asp:ListItem Text="SOFIEXPRESS " Value="655"></asp:ListItem>
                        <asp:ListItem Text="UNAGRA " Value="656"></asp:ListItem>
                        <asp:ListItem Text="OPCIONES EMPRESARIALES DEL NOROESTE " Value="659"></asp:ListItem>
                        <asp:ListItem Text="CLS " Value="901"></asp:ListItem>
                        <asp:ListItem Text="INDEVAL " Value="902"></asp:ListItem>
                        <asp:ListItem Text="LIBERTAD " Value="670"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:Label ID="lblBanco" runat="server" ForeColor="Red"></asp:Label>
                </td></tr>
            <tr>
                <td style="text-align: right" >
                    <asp:Label ID="Label20" runat="server" Text="*Tipo de Contrato:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoContrato" runat="server" AutoPostBack="True" ClientIDMode="Static" onselectedindexchanged="ddlTipoContrato_SelectedIndexChanged">
                        <asp:ListItem Text="Base" Value="Base"></asp:ListItem>
                        <asp:ListItem Text="Eventual" Value="Eventual"></asp:ListItem>
                        <asp:ListItem>Confianza</asp:ListItem>
                        <asp:ListItem>Sindicalizado</asp:ListItem>
                        <asp:ListItem>A Prueba</asp:ListItem>
                        <asp:ListItem Value="Otro"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtTipoContratoOtro" runat="server" Visible="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valTipoContrOtro" runat="server" ControlToValidate="txtTipoContratoOtro" ErrorMessage="Campo requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblTipoContrato" runat="server" Enabled="False" ForeColor="Red"></asp:Label>
                </td>

            </tr>
            <tr>
                <td style="text-align: right"  >
                    *Tipo de Jornada:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoJornada" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="Diurna" Value="Diurna"></asp:ListItem>
                        <asp:ListItem Text="Nocturna" Value="Nocturna"></asp:ListItem>
                        <asp:ListItem Text="Mixta" Value="Mixta"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valOtro0" runat="server" ClientIDMode="Static" ControlToValidate="ddlTipoJornada" Enabled="False" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                    <br />
                </td>
            </tr>
                <tr>
                    <td >
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    </td>
                    <td >
                        <br />
                    </td>
                   
                </tr>
            
        </table>
    
</ContentTemplate>
</asp:UpdatePanel>



</ContentTemplate>
</asp:TabPanel>
            
            
    </asp:TabContainer>
    
    
    <div align="right">
        <asp:Button runat="server" ID="btnSave" Text="Guardar" onclick="btnSave_Click" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" 
            onclick="btnCancel_Click" CausesValidation="False" />
    </div>
</asp:Content>