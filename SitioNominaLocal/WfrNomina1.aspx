<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WfrNomina.aspx.cs" Inherits="GafLookPaid.WfrNomina" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/controles/JubilacionPensionRetiro.ascx" TagPrefix="uc" TagName="UCJubilacionPensionRetiro" %>
<%@ Register Src="~/controles/SeparacionIndemnizacion.ascx" TagPrefix="uc" TagName="UCSeparacionIndemnizacion" %>
<%@ Register Src="~/controles/AccionesOTitulos.ascx" TagPrefix="uc" TagName="UCAccionesOTitulos" %>
<%@ Register Src="~/controles/PercepcionesTotales.ascx" TagPrefix="uc" TagName="UCPercepcionesTotales" %>
<%@ Register Src="~/controles/OtrosPagos.ascx" TagPrefix="uc" TagName="UCOtrosPagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
    <style type="text/css">


        #Background
{
position:fixed;
top:0px;
bottom:0px;
left:0px;
right:0px;
overflow:hidden;
paddin:0;
margin:0;
background-color:transparent;
filter:alpha(opacity=80);
opacity:0.8;
z-index:10000;

}
        #Progress {
            position:fixed;
            top:40%;
            left:40%;
            height:20%;
            width:20%;
            z-index: 100001;
            background-color:transparent;
            border:0px;
            background-image:url("images/ajax-loader.gif");
            background-repeat:no-repeat;
            background-position:center;   
            text-align:center;         
        }

	.mpeBack
	{
		background-color: Gray;
		filter: alpha(opacity=70);
		opacity: 0.7;
	}		
		.auto-style8 {
            width: 117px;
        }
        .auto-style12 {
            width: 124px;
            text-align: left;
        }
        .auto-style149 {
            text-align: left;
            width: 182px;
        }
        .auto-style150 {
            width: 122px;
        }
		.auto-style152 {
            text-align: left;
            width: 144px;
            height: 24px;
        }
		.auto-style153 {
            height: 24px;
        }
		.auto-style154 {
            height: 18px;
        }
		.auto-style155 {
            width: 126px;
        }
		.auto-style156 {
            width: 112px;
        }
        .auto-style158 {
            width: 111px;
        }
        .auto-style159 {
            width: 203px;
        }
        .auto-style160 {
            width: 232px;
        }
        .auto-style161 {
            width: 53%;
        }
		.auto-style162 {
            color: #666666;
        }
		</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Generar Recibo de Nómina</h1>
	
       <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <table>
        <tr>
<td class="auto-style155" >
		* Empresa:</td><td class="auto-style159"><asp:DropDownList runat="server" ID="ddlEmpresa" AutoPostBack="True"
		 DataTextField="RazonSocial" DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresa_SelectedIndexChanged" Enabled="False" Height="18px" Width="187px" />
	</td>
            <td class="auto-style156" >Serie:</td>
			<td  style="text-align: left" class="auto-style160">
				<asp:TextBox runat="server" ID="txtSerie" Width="144px" Height="15px" />
			</td>
			<td class="auto-style158">Folio:</td>
			<td class="auto-style34"><asp:TextBox runat="server" ID="txtFolio" Width="72px" Enabled="False" /></td>
            
            
            </tr>
        <tr>
             <td class="auto-style155" >
        * Centro de Trabajo:</td><td class="auto-style159"><asp:DropDownList runat="server" ID="DdlCentroTrabajo" AutoPostBack="True"
                                                  DataTextField="Nombre" DataValueField="IdCentroTrabajo" onselectedindexchanged="DdlCentroTrabajo_OnSelectedIndexChanged" Height="16px" Width="126px" />
	    <asp:Label runat="server" ID="lblLugarExpedicion" Visible="False" />
		</td>
           <td style="text-align: left" class="auto-style156" ><asp:Label ID="Label10" runat="server" Text="Registro Patronal:"></asp:Label></td>
            <td class="auto-style160"><asp:TextBox runat="server" ID="txtRegistroPatronal" Enabled="False" EnableTheming="True" Width="145px"></asp:TextBox></td>
             <td class="auto-style158"  >

        * Tipo de Jornada:</td><td class="auto-style35"> <asp:TextBox ID="txtTipoJornada" runat="server" Enabled="False" ToolTip="Diurna, nocturna, mixta, por hora, reducida, continua, partida, por turnos, etc." Width="72px"></asp:TextBox>
                 </td>  

        </tr>
	 <tr>
         <td class="auto-style155">Periodicidad</td>
       <td class="auto-style159"><asp:DropDownList runat="server" ID="ddlPeriodicidad"  
                     AutoPostBack="True" 
                    onselectedindexchanged="ddlPeriodicidad_SelectedIndexChanged">
                      <asp:ListItem Text="Diario" Value="01" />
                      <asp:ListItem Text="Semanal" Value="02" />
                      <asp:ListItem Text="Catorcenal" Value="03" />
                     <asp:ListItem Text="Quincenal" Value="04" />
                      <asp:ListItem Text="Mensual" Value="05" />
                      <asp:ListItem Text="Bimestral" Value="06" />
                      <asp:ListItem Text="Unidad obra" Value="07" />
                       <asp:ListItem Text="Comisión" Value="08" />
                      <asp:ListItem Text="Precio alzado" Value="09" />
                      <asp:ListItem Text="Precio alzado" Value="10" />
                      <asp:ListItem Text="Otra Periodicidad" Value="99" />
                         
       </asp:DropDownList>
            
            <td class="auto-style156"  >

        * Tipo de Nómina:</td>
           <td class="auto-style160"> <asp:DropDownList runat="server" ID="ddlTipoNomina"  
                   AutoPostBack="True" 
                    onselectedindexchanged="ddlTipoNomina_SelectedIndexChanged" style="text-align: center; margin-left: 0px">
                    <asp:ListItem Value="O" Text="Nómina ordinaria"></asp:ListItem>
                    <asp:ListItem Value="E" Text="Nómina extraordinaria"></asp:ListItem>
                   
                </asp:DropDownList></td>
         <td class="auto-style158"  ><asp:Label ID="Label24" runat="server" Text="Riesgo del puesto:" style="text-align: right"></asp:Label></td>
                        <td ><asp:DropDownList runat="server" ID="ddlRiesgoPuesto" Enabled="False" style="margin-left: 0px" Height="16px" Width="78px">
                                <asp:ListItem Text="Clase I" Value="1" />
                                <asp:ListItem Text="Clase II" Value="2" />
                                <asp:ListItem Text="Clase III" Value="3" />
                                <asp:ListItem Text="Clase IV" Value="4"/>
                                <asp:ListItem Text="Clase V" Value="5" />
                            </asp:DropDownList>
                            </td>
        </tr>
  <tr><td style="text-align: right">

      Exclusivo

      </td>
      <td>Dependencias Federales</td>
  </tr>
            <tr>
           <td class="auto-style24">OrigenRecurso</td>
           <td class="auto-style11"><asp:DropDownList runat="server" ID="ddlOrigenRecurso"  
                    ClientIDMode="Static" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrigenRecurso_SelectedIndexChanged" style="margin-left: 0px">
                 <asp:ListItem Text="Selecciona" Value="0" />      
                 <asp:ListItem Text="Ingresos propios" Value="IP" />
                                <asp:ListItem Text="Ingreso federales" Value="IF" />
                                <asp:ListItem Text="Ingresos mixtos" Value="IM" />
                </asp:DropDownList></td>
           <td class="auto-style11"><asp:Label ID="lblMontoRecursoPropio" runat="server" Visible="false" Text="MontoRecursoPropio"></asp:Label>
           </td>
           <td class="auto-style11">
               <asp:TextBox ID="txtMontoRecursoPropio" runat="server" Visible="false" Enabled="false"></asp:TextBox>

                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtMontoRecursoPropio" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator13" runat="server" Display="Dynamic"
    ControlToValidate="txtMontoRecursoPropio" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ErrorMessage="Requerido" Display="Dynamic"
              ControlToValidate="txtMontoRecursoPropio" ValidationGroup="CrearFactura" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                
           
           </td>
       </tr>

    </table>
  <!-- <table>
       <tr>
           <td class="auto-style29"><asp:Label ID="Label8" runat="server" Text="Nombre:"></asp:Label></td>
            <td ><asp:TextBox runat="server" ID="txtNombreCentro" Enabled="False"></asp:TextBox></td>
          
          
       </tr>
   </table>-->
    <p>
		*
		Empleado:&nbsp;<asp:DropDownList runat="server" ID="ddlClientes" AutoPostBack="True"
		 DataTextField="NombreCompleto" DataValueField="idCliente" 
            onselectedindexchanged="ddlClientes_SelectedIndexChanged" style="margin-left: 50px" />
		<asp:TextBox runat="server"  ID="txtRazonSocial"   Text-align="center" TextMode="MultiLine" Width ="399px" Height="16px" Enabled="False" />
	   		<table>
                  
                    <caption>
                        <h1>
                            <asp:Label ID="Label58" runat="server" Text="Datos de Nómina"></asp:Label>
                        </h1>
                        <tr>
                            <td class="auto-style153" style="text-align:left">
                                <asp:Label ID="Label12" runat="server" text="* Fecha de Pago:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaPagoNomina" runat="server" Width="113px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFechaPagoNomina" />
                            </td>
                            <td class="auto-style153"></td>
                            <td class="auto-style153" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Text="* Fecha de Inicio del Pago:"></asp:Label>
                            </td>
                            <td class="auto-style152">
                                <asp:TextBox ID="txtFechaInicio" runat="server" Width="113px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFechaInicio" />
                               
                            </td>
                             <td class="auto-style153" style="text-align:left">Observaciones :</td>
                            <td colspan="2" style="text-align: left" class="auto-style153">
                                <asp:TextBox ID="txtProyecto"  TextMode="MultiLine" runat="server" Width="152px" Height="16px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style154"><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtFechaPagoNomina" ErrorMessage="Dato inválido" ForeColor="#FF3300" ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])"></asp:RegularExpressionValidator></td>
                            <td class="auto-style154"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFechaPagoNomina" ErrorMessage="Campo obligatorio" ForeColor="#FF3300" ValidationGroup="CrearFactura"></asp:RequiredFieldValidator></td>
                        <td class="auto-style154"></td>
                            <td class="auto-style154"><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtFechaInicio" ErrorMessage="Dato inválido" ForeColor="#FF3300" ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])"></asp:RegularExpressionValidator></td>
                        <td class="auto-style154"> 
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaInicio" ErrorMessage="Campo obligatorio" ForeColor="#FF3300" ValidationGroup="CrearFactura"></asp:RequiredFieldValidator>
                            </td> </tr>
                        <caption>
                           </tr>
                            <tr>
                                <td class="auto-style67" style="text-align: left">
                                    <asp:Label ID="Label3" runat="server" Text="* Días Pagados:"></asp:Label>
                                </td>
                                <td class="auto-style68" style="text-align: left">
                                    <asp:TextBox ID="txtDiasPagados" runat="server" Width="113px"></asp:TextBox>
                                    
                                </td>
                                <td class="auto-style69">
                                    <asp:TextBox ID="txtFechaPago" runat="server" Height="16px" Visible="False" Width="16px"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaPago_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFechaPago" />
                                </td>
                                <td class="auto-style70" style="text-align: left">
                                    <asp:Label ID="Label2" runat="server" text="* Fecha Final del Pago:"></asp:Label>
                                </td>
                                <td class="auto-style71">
                                    <asp:TextBox ID="txtFechaFin" runat="server" Width="113px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFechaFin" />
                                   </td>
                            </tr>
                            <tr>
                                <td class="auto-style95">&nbsp;</td>
                                <td class="auto-style64"><asp:RequiredFieldValidator ID="rfvDias" runat="server" ControlToValidate="txtDiasPagados" ErrorMessage="Campo Obligatorio" ForeColor="#FF3300" ValidationGroup="CrearFactura" style="text-align: right"></asp:RequiredFieldValidator></td>
                                <td></td>
                                <td class="auto-style65"><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="Dato inválido" ForeColor="#FF3300" ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])"></asp:RegularExpressionValidator></td>
                                <td class="auto-style72"> 
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="Campo obligatorio" ForeColor="#FF3300" ValidationGroup="CrearFactura"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <%-- <tr>
            <td style="text-align: right;"  >
		        Condiciones de Pago:</td><td  style="text-align: left" class="auto-style4">
		        <asp:DropDownList runat="server" ID="ddlCondicionesPago" AutoPostBack="True" 
                    onselectedindexchanged="ddlCondicionesPago_SelectedIndexChanged">
		            <asp:ListItem runat="server" Text="En una sola exhibición" Value="En una sola exhibición"></asp:ListItem>
                   
		        </asp:DropDownList>
	        </td>
        </tr>--%>
                        </caption>
                    </caption>
        </table>
	</p>
    <%--<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>--%>
	

    
            <div style="width:100%">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" Width="100%" style="color: #0000FF; text-decoration: blink; font-weight: 700; font-family: Arial, Helvetica, sans-serif; text-align: left;">

        <asp:TabPanel ID="PanelPercepciones" runat="server"  CssClass="page3"  HeaderText="Percepciones"><ContentTemplate><table  ><tr><td style="font-weight: 700; font-family: Arial, Helvetica, sans-serif"><asp:CheckBox ID="ChPercepciones" runat="server" Text="Percepciones" CssClass="page2" AutoPostBack="True" OnCheckedChanged="ChPercepciones_CheckedChanged" /></td><td><asp:CheckBox ID="ChJubilacionPensionRetiro" runat="server" CssClass="page2" OnCheckedChanged="ChJubilacionPensionRetiro_CheckedChanged"  Text="JubilacionPensionRetiro" AutoPostBack="True" style="font-weight: 700; font-family: Arial, Helvetica, sans-serif" Enabled="False"/></td><td><asp:CheckBox ID="ChSeparacionIndemnizacio" runat="server"  CssClass="page2" OnCheckedChanged="ChSeparacionIndemnizacio_CheckedChanged"  Text="SeparacionIndemnizacio" AutoPostBack="True" style="font-weight: 700; font-family: Arial, Helvetica, sans-serif"/></td></tr></table><br /><uc:UCJubilacionPensionRetiro id="JubilacionPensionRetiro" runat="server"></uc:UCJubilacionPensionRetiro><uc:UCSeparacionIndemnizacion id="SeparacionIndemnizacion" runat="server"></uc:UCSeparacionIndemnizacion><uc:UCPercepcionesTotales id="PercepcionesTotales" runat="server"></uc:UCPercepcionesTotales><asp:Panel ID="Panel2" runat="server" BorderStyle="Outset" CssClass="page2" HorizontalAlign="Center" Width="100%" ><table width="100%"><tr runat="server" ID="Tr1">
            <td runat="server" class="auto-style149" >* Clave</td><td style="text-align: left" runat="server" class="auto-style161">
            <asp:TextBox ID="txtClave" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtClave" ErrorMessage="Campo Obligatorio" ValidationGroup="AgregarPersepcion" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                        ControlToValidate="txtClave" 
            ValidationGroup="AgregarPersepcion"            
            ErrorMessage="Dato inválido" 
                        ValidationExpression="([A-Z]|[a-z]|[0-9]|Ñ|ñ|!|&quot;|%|&amp;|&apos;|´|-|:|;|>|=|&lt;|@|_|,|\{|\}|`|~|&#225;|&#233;|&#237;|&#243;|&#250;|&#193;|&#201;|&#205;|&#211;|&#218;|&#252;|&#220;){3,15}" ForeColor="#FF3300"></asp:RegularExpressionValidator></td ><td style="width: 30%;text-align: left" runat="server"></td></tr><tr runat="server" ID="tr2"><td runat="server" class="auto-style149">* Tipo Percepción</td><td style="text-align: left" runat="server" class="auto-style161">
                            <asp:DropDownList runat="server" ID="ddlPercepcion" AutoPostBack="True" OnSelectedIndexChanged="ddlPercepcion_SelectedIndexChanged" style="margin-left: 0px">
                                <asp:ListItem  Value="001" Text="Sueldos, Salarios  Rayas y Jornales" />
                                <asp:ListItem  Value="002" Text="Gratificación Anual -Aguinaldo-" />
                                <asp:ListItem  Value="003" Text="Participación de los Trabajadores en las Utilidades PTU" />
                                <asp:ListItem  Value="004" Text="Reembolso de Gastos Médicos Dentales y Hospitalarios" />
                                <asp:ListItem  Value="005" Text="Fondo de Ahorro" />
                                <asp:ListItem  Value="006" Text="Caja de ahorro" />
                                <asp:ListItem  Value="009" Text="Contribuciones a Cargo del Trabajador Pagadas por el Patrón" />
                                <asp:ListItem  Value="010" Text="Premios por puntualidad" />
                                <asp:ListItem  Value="011" Text="Prima de Seguro de vida" />
                                <asp:ListItem  Value="012" Text="Seguro de Gastos Médicos Mayores" />
                                <asp:ListItem  Value="013" Text="Cuotas Sindicales Pagadas por el Patrón" />
                                <asp:ListItem  Value="014" Text="Subsidios por incapacidad" />
                                <asp:ListItem  Value="015" Text="Becas para trabajadores y-o hijos" />
                                <asp:ListItem  Value="019" Text="Horas extra" />
                                <asp:ListItem  Value="020" Text="Prima dominical" />
                                <asp:ListItem  Value="021" Text="Prima vacacional" />
                                <asp:ListItem  Value="022" Text="Prima por antigüedad" />
                                <asp:ListItem  Value="023" Text="Pagos por separación" />
                                <asp:ListItem  Value="024" Text="Seguro de retiro" />
                                <asp:ListItem  Value="025" Text="Indemnizaciones" />
                                <asp:ListItem  Value="026" Text="Reembolso por funeral" />
                                <asp:ListItem  Value="027" Text="Cuotas de seguridad social pagadas por el patrón" />
                                <asp:ListItem  Value="028" Text="Comisiones" />
                                <asp:ListItem  Value="029" Text="Vales de despensa" />
                                <asp:ListItem  Value="030" Text="Vales de restaurante" />
                                <asp:ListItem  Value="031" Text="Vales de gasolina" />
                                <asp:ListItem  Value="032" Text="Vales de ropa" />
                                <asp:ListItem  Value="033" Text="Ayuda para renta" />
                                <asp:ListItem  Value="034" Text="Ayuda para artículos escolares" />
                                <asp:ListItem  Value="035" Text="Ayuda para anteojos" />
                                <asp:ListItem  Value="036" Text="Ayuda para transporte" />
                                <asp:ListItem  Value="037" Text="Ayuda para gastos de funeral" />
                                <asp:ListItem  Value="038" Text="Otros ingresos por salarios" />
                                <asp:ListItem  Value="039" Text="Jubilaciones, pensiones o haberes de retiro" />
                                <asp:ListItem  Value="044" Text="Jubilaciones, pensiones o haberes de retiro en parcialidades" />
                                <asp:ListItem  Value="045" Text="Ingresos en acciones o títulos valor que representan bienes" />
                                <asp:ListItem  Value="046" Text="Ingresos asimilados a salarios" />
                                <asp:ListItem  Value="047" Text="Alimentación" />
                                <asp:ListItem  Value="048" Text="Habitación" />
                                <asp:ListItem  Value="049" Text="Premios por asistencia" />
                                <asp:ListItem  Value="050" Text="Viáticos" />
                                <asp:ListItem  Value="051" Text="Pagos por gratificaciones, primas, compensaciones, recompensas u otros a extrabajadores derivados de jubilación en parcialidades" />
                                <asp:ListItem  Value="052" Text="Pagos que se realicen a extrabajadores que obtengan una jubilación en parcialidades derivados de la ejecución de resoluciones judicial o de un laudo" />
                                <asp:ListItem  Value="053" Text="Pagos que se realicen a extrabajadores que obtengan una jubilación en una sola exhibición derivados de la ejecución de resoluciones judicial o de un laudo" />
                            </asp:DropDownList>
                                                                                                                                                                                                                                                                                                                                                                                                                                              
                               </td ><td style="text-align: left" runat="server" class="auto-style135"><asp:Button runat="server" Text="Agregar Percepcion" ID="btnAgregarPercepcion" class="btn btn-primary" ValidationGroup="AgregarPersepcion"
                    onclick="btnAgregarPercepcion_Click"/></td></tr><tr><td style="text-align: left" class="auto-style149" >* Importe Gravado</td><td style="text-align: left" class="auto-style161"><asp:TextBox ID="txtImporteGravado" Display="Dynamic" ValidationGroup="AgregarPersepcion" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="AgregarPersepcion" ControlToValidate="txtImporteGravado" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImporteGravado" BehaviorID="_content_FilteredTextBoxExtender3" /><asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" Display="Dynamic"
    ControlToValidate="txtImporteGravado" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarPersepcion" ForeColor="#FF3300"/></td></tr><tr><td style="text-align: left" class="auto-style149" >* Importe Excento</td><td style="text-align: left" class="auto-style161"><asp:TextBox ID="txtImporteExcento" Display="Dynamic" ValidationGroup="AgregarPersepcion" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="AgregarPersepcion" ControlToValidate="txtImporteExcento" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImporteExcento" BehaviorID="_content_FilteredTextBoxExtender4" /><asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" Display="Dynamic"
    ControlToValidate="txtImporteExcento" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarPersepcion" ForeColor="#FF3300"/></td><td><asp:CheckBox ID="ChAccionesOTitulos" runat="server"  Text="AccionesOTitulos" AutoPostBack="True" OnCheckedChanged="ChAccionesOTitulos_CheckedChanged" style="text-align: left; font-weight: 700; font-family: Arial, Helvetica, sans-serif" Enabled="False"/></td></tr><tr>
            <td colspan="2">
                <uc:UCAccionesOTitulos ID="AccionesOTitulos" runat="server" />
            </td>
            </tr></table><br /><asp:GridView runat="server" CssClass="page1" ID="gvPercepciones" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvPercepciones_RowCommand" ><Columns><asp:BoundField HeaderText="Clave" DataField="Clave" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Tipo Percepcion" DataField="TipoPercepcion" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Concepto" DataField="Concepto" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Importe Gravado" DataField="ImporteGravado" DataFormatString="{0:C}"  /><asp:BoundField HeaderText="Importe Exento" DataField="ImporteExento" DataFormatString="{0:C}" /><asp:BoundField HeaderText="Valor Mercado" DataField="ValorMercado" DataFormatString="{0:C}" /><asp:BoundField HeaderText="Precio Al Otorgarse" DataField="PrecioAlOtorgarse" DataFormatString="{0:C}" /><asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/></Columns></asp:GridView><br /><table style="width: 76%"><tr>
            <td class="auto-style12">* Clave</td><td style="text-align: left" class="auto-style127">
            <asp:DropDownList ID="ddlClave" runat="server" style="margin-left: 0px"></asp:DropDownList><asp:Label ID="lblClaveError" runat="server" ForeColor="#FF3300" Text="Campo Obligatorio" Visible="False"></asp:Label></td></tr><tr><td class="auto-style12">* Dias</td><td style="text-align: left" class="auto-style127"><asp:TextBox ID="txtDias" runat="server"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
            runat="server" FilterType="Numbers"  TargetControlID="txtDias" BehaviorID="_content_FilteredTextBoxExtender1" /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="AgregarHorasExtra" ControlToValidate="txtDias" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator></td><td><asp:Button runat="server" Text="Agregar Horas Extras" ID="btnAgregarHorasExtra" class="btn btn-primary" ValidationGroup="AgregarHorasExtra"
                    onclick="btnAgregarHorasExtra_Click"/></td></tr><tr><td class="auto-style12">* TipoHoras</td><td style="text-align: left" class="auto-style127"><asp:DropDownList runat="server" ID="ddlTipoHoras" style="margin-left: 0px"><asp:ListItem  Value="01" Text="Dobles" /><asp:ListItem  Value="02" Text="Triples" /><asp:ListItem  Value="03" Text="Simples" /></asp:DropDownList></td></tr><tr><td class="auto-style12">* HorasExtra</td><td style="text-align: left" class="auto-style127"><asp:TextBox ID="txtHorasExtra" runat="server"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
            runat="server" FilterType="Numbers"  TargetControlID="txtHorasExtra" BehaviorID="_content_FilteredTextBoxExtender2" /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="AgregarHorasExtra" ControlToValidate="txtHorasExtra" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator></td></tr><tr><td class="auto-style12">* ImportePagado</td><td style="text-align: left" class="auto-style127"><asp:TextBox ID="txtImportePagado" runat="server"></asp:TextBox></td><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImportePagado" BehaviorID="_content_FilteredTextBoxExtender4" /><asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic"
    ControlToValidate="txtImportePagado" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarHorasExtra" ForeColor="#FF3300"/><asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="AgregarHorasExtra" ControlToValidate="txtImportePagado" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator></tr></table><br /><br /><asp:GridView runat="server" ID="gvHorasExtra" CssClass="page1"  AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvHorasExtra_RowCommand" ><Columns><asp:BoundField HeaderText="Clave" DataField="clave" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Dias" DataField="Dias" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Tipo Horas" DataField="TipoHoras" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Horas Extra" DataField="HoraExtra" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="ImportePagado" DataField="ImportePagado" DataFormatString="{0:C}"  /><asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/></Columns></asp:GridView></asp:Panel></ContentTemplate></asp:TabPanel>
        <asp:TabPanel ID="PanelDeducciones" runat="server" CssClass="page3"  HeaderText ="Deducciones"><ContentTemplate><table><tr><td style="font-weight: 700; font-family: Arial, Helvetica, sans-serif" class="auto-style120"><asp:CheckBox ID="ChDeducciones" runat="server" CssClass="page2" Text="Deducciones" AutoPostBack="True" OnCheckedChanged="ChDeducciones_CheckedChanged" /></td></tr>
                                      <tr>
                                <td style="text-align: left" class="auto-style120">TotalOtrasDeducciones</td>
                                <td class="auto-style121">
                                    <asp:TextBox ID="txtTotalOtrasDeducciones" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" BehaviorID="_content_FilteredTextBoxExtender4" FilterType="Custom, Numbers" TargetControlID="txtTotalOtrasDeducciones" ValidChars="." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTotalOtrasDeducciones" Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="#FF3300" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura"></asp:RegularExpressionValidator>
                                </td>
                                <td class="auto-style20">TotalImpuestosRetenidos</td>
                                <td>
                                    <asp:TextBox ID="txtTotalImpuestosRetenidos" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" BehaviorID="_content_FilteredTextBoxExtender4" FilterType="Custom, Numbers" TargetControlID="txtTotalImpuestosRetenidos" ValidChars="." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTotalImpuestosRetenidos" Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="#FF3300" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
            
                 
            </table><br /><table width="100%"><tr runat="server" ID="Tr3"><td runat="server" class="auto-style123">&nbsp;* Clave</td><td style="width: 40%;text-align: left" runat="server"><asp:TextBox ID="txtClaveDed" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtClaveDed" ErrorMessage="Campo Obligatorio" ValidationGroup="AgregarDeduccion" ForeColor="Red"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                        ControlToValidate="txtClaveDed" 
            ValidationGroup="AgregarDeduccion"            
            ErrorMessage="Dato inválido" 
                        ValidationExpression="([A-Z]|[a-z]|[0-9]|Ñ|ñ|!|&quot;|%|&amp;|&apos;|´|-|:|;|>|=|&lt;|@|_|,|\{|\}|`|~|&#225;|&#233;|&#237;|&#243;|&#250;|&#193;|&#201;|&#205;|&#211;|&#218;|&#252;|&#220;){3,15}" ForeColor="#FF3300"></asp:RegularExpressionValidator></td ></tr><tr runat="server" ID="tr4"><td runat="server" class="auto-style123">* Tipo Deduccion</td><td style="width: 40%;text-align: left" runat="server">
                            <asp:DropDownList ID="ddlTipoDed" runat="server" style="margin-left: 0px" Height="20px" Width="524px">
                                <asp:ListItem Text="Seguridad social" Value="001" />
                                <asp:ListItem Text="ISR" Value="002" />
                                <asp:ListItem Text="Aportaciones a retiro, cesantía en edad avanzada y vejez. " Value="003" />
                                <asp:ListItem Text="Otros " Value="004" />
                                <asp:ListItem Text="Aportaciones a Fondo de vivienda " Value="005" />
                                <asp:ListItem Text="Descuento por incapacidad" Value="006" />
                                <asp:ListItem Text="Pensión alimenticia" Value="007" />
                                <asp:ListItem Text="Renta" Value="008" />
                                <asp:ListItem Text="Préstamos provenientes del Fondo Nacional de la Vivienda para los Trabajadores" Value="009" />
                                <asp:ListItem Text="Pago por crédito de vivienda" Value="010" />
                                <asp:ListItem Text="Pago de abonos INFONACOT" Value="011" />
                                <asp:ListItem Text="Anticipo de salarios" Value="012" />
                                <asp:ListItem Text="Pagos hechos con exceso al trabajador" Value="013" />
                                <asp:ListItem Text="Errores" Value="014" />
                                <asp:ListItem Text="Pérdidas" Value="015" />
                                <asp:ListItem Text="Averías" Value="016" />
                                <asp:ListItem Text="Adquisición de artículos producidos por la empresa o establecimiento " Value="017" />
                                <asp:ListItem Text="Cuotas para la constitución y fomento de sociedades cooperativas y de cajas de ahorro" Value="018" />
                                <asp:ListItem Text="Cuotas sindicales" Value="019" />
                                <asp:ListItem Text="Ausencia (Ausentismo)" Value="020" />
                                <asp:ListItem Text="Cuotas obrero patronales" Value="021" />
                                <asp:ListItem Text="Impuestos Locales" Value="022" />
                                <asp:ListItem Text="Aportaciones voluntarias" Value="023" />
                                <asp:ListItem Text="Ajuste en Gratificación Anual (Aguinaldo) Exento"	 Value="024" />
                                <asp:ListItem Text="Ajuste en Gratificación Anual (Aguinaldo) Gravado"	 Value="025" />
                                <asp:ListItem Text="Ajuste en Participación de los Trabajadores en las Utilidades PTU Exento" Value="026" />
                                <asp:ListItem Text="Ajuste en Participación de los Trabajadores en las Utilidades PTU Gravado"	 Value="027" />
                                <asp:ListItem Text="Ajuste en Reembolso de Gastos Médicos Dentales y Hospitalarios Exento"	 Value="028" />
                                <asp:ListItem Text="Ajuste en Fondo de ahorro Exento"	 Value="029" />
<asp:ListItem Text="Ajuste en Caja de ahorro Exento"	 Value="030" />
<asp:ListItem Text="Ajuste en Contribuciones a Cargo del Trabajador Pagadas por el Patrón Exento" Value="031" />
<asp:ListItem Text="Ajuste en Premios por puntualidad Gravado"	 Value="032" />
<asp:ListItem Text="Ajuste en Prima de Seguro de vida Exento"	 Value="033" />
<asp:ListItem Text="Ajuste en Seguro de Gastos Médicos Mayores Exento"	 Value="034" />
<asp:ListItem Text="Ajuste en Cuotas Sindicales Pagadas por el Patrón Exento"	 Value="035" />
<asp:ListItem Text="Ajuste en Subsidios por incapacidad Exento"	 Value="036" />
<asp:ListItem Text="Ajuste en Becas para trabajadores y/o hijos Exento"	 Value="037" />
<asp:ListItem Text="Ajuste en Horas extra Exento"	 Value="038" />
<asp:ListItem Text="Ajuste en Horas extra Gravado"	 Value="039" />
<asp:ListItem Text="Ajuste en Prima dominical Exento"	 Value="040" />
<asp:ListItem Text="Ajuste en Prima dominical Gravado"	 Value="041" />
<asp:ListItem Text="Ajuste en Prima vacacional Exento"	 Value="042" />
<asp:ListItem Text="Ajuste en Prima vacacional Gravado"	 Value="043" />
<asp:ListItem Text="Ajuste en Prima por antigüedad Exento"	 Value="044" />
<asp:ListItem Text="Ajuste en Prima por antigüedad Gravado"	 Value="045" />
<asp:ListItem Text="Ajuste en Pagos por separación Exento"	 Value="046" />
<asp:ListItem Text="Ajuste en Pagos por separación Gravado"	 Value="047" />
<asp:ListItem Text="Ajuste en Seguro de retiro Exento"	 Value="048" />
<asp:ListItem Text="Ajuste en Indemnizaciones Exento"	 Value="049" />
<asp:ListItem Text="Ajuste en Indemnizaciones Gravado"	 Value="050" />
<asp:ListItem Text="Ajuste en Reembolso por funeral Exento"	 Value="051" />
<asp:ListItem Text="Ajuste en Cuotas de seguridad social pagadas por el patrón Exento"	 Value="052" />
<asp:ListItem Text="Ajuste en Comisiones Gravado"	 Value="053" />
<asp:ListItem Text="Ajuste en Vales de despensa Exento"	 Value="054" />
<asp:ListItem Text="Ajuste en Vales de restaurante Exento"	 Value="055" />
<asp:ListItem Text="Ajuste en Vales de gasolina Exento"	 Value="056" />
<asp:ListItem Text="Ajuste en Vales de ropa Exento"	 Value="057" />
<asp:ListItem Text="Ajuste en Ayuda para renta Exento"	 Value="058" />
<asp:ListItem Text="Ajuste en Ayuda para artículos escolares Exento" Value="059" />
<asp:ListItem Text="Ajuste en Ayuda para anteojos Exento"	 Value="060" />
<asp:ListItem Text="Ajuste en Ayuda para transporte Exento"	 Value="061" />
<asp:ListItem Text="Ajuste en Ayuda para gastos de funeral Exento"	 Value="062" />
<asp:ListItem Text="Ajuste en Otros ingresos por salarios Exento"	 Value="063" />
<asp:ListItem Text="Ajuste en Otros ingresos por salarios Gravado"	 Value="064" />
<asp:ListItem Text="Ajuste en Jubilaciones, pensiones o haberes de retiro en una sola exhibición Exento"	 Value="065" />
<asp:ListItem Text="Ajuste en Jubilaciones, pensiones o haberes de retiro en una sola exhibición Gravado"	 Value="066" />
<asp:ListItem Text="Ajuste en Pagos por separación Acumulable"  Value="067" />
<asp:ListItem Text="Ajuste en Pagos por separación No acumulable"  Value="068" />
<asp:ListItem Text="Ajuste en Jubilaciones, pensiones o haberes de retiro en parcialidades Exento"  Value="069" />
<asp:ListItem Text="Ajuste en Jubilaciones, pensiones o haberes de retiro en parcialidades Gravado"  Value="070" />
<asp:ListItem Text="Ajuste en Subsidio para el empleo (efectivamente entregado al trabajador)"  Value="071" />
<%--<asp:ListItem Text="Ajuste en Ingresos en acciones o títulos valor que representan bienes Exento"  Value="072" />--%>
<asp:ListItem Text="Ajuste en Ingresos en acciones o títulos valor que representan bienes Gravado"  Value="073" />
<asp:ListItem Text="Ajuste en Alimentación Exento"  Value="074" />
<asp:ListItem Text="Ajuste en Alimentación Gravado"  Value="075" />
<asp:ListItem Text="Ajuste en Habitación Exento"  Value="076" />
<asp:ListItem Text="Ajuste en Habitación Gravado"  Value="077" />
<asp:ListItem Text="Ajuste en Premios por asistencia"  Value="078" />
<asp:ListItem Text="Ajuste en Pagos distintos a los listados y que no deben considerarse como ingreso por sueldos, salarios o ingresos asimilados."  Value="079" />
<asp:ListItem Text="Ajuste en Viáticos gravados"  Value="080" />
<asp:ListItem Text="Ajuste en Viáticos (entregados al trabajador)"  Value="081" />
<asp:ListItem Text="Ajuste en Fondo de ahorro Gravado"  Value="082" />
<asp:ListItem Text="Ajuste en Caja de ahorro Gravado"  Value="083" />
<asp:ListItem Text="Ajuste en Prima de Seguro de vida Gravado"  Value="084" />
<asp:ListItem Text="Ajuste en Seguro de Gastos Médicos Mayores Gravado"  Value="085" />
<asp:ListItem Text="Ajuste en Subsidios por incapacidad Gravado"  Value="086" />
<asp:ListItem Text="Ajuste en Becas para trabajadores y/o hijos Gravado"  Value="087" />
<asp:ListItem Text="Ajuste en Seguro de retiro Gravado"  Value="088" />
<asp:ListItem Text="Ajuste en Vales de despensa Gravado"  Value="089" />
<asp:ListItem Text="Ajuste en Vales de restaurante Gravado"  Value="090" />
<asp:ListItem Text="Ajuste en Vales de gasolina Gravado"  Value="091" />
<asp:ListItem Text="Ajuste en Vales de ropa Gravado"  Value="092" />
<asp:ListItem Text="Ajuste en Ayuda para renta Gravado"  Value="093" />
<asp:ListItem Text="Ajuste en Ayuda para artículos escolares Gravado"  Value="094" />
<asp:ListItem Text="Ajuste en Ayuda para anteojos Gravado"  Value="095" />
<asp:ListItem Text="Ajuste en Ayuda para transporte Gravado"  Value="096" />
<asp:ListItem Text="Ajuste en Ayuda para gastos de funeral Gravado"  Value="097" />
<asp:ListItem Text="Ajuste a ingresos asimilados a salarios gravados"  Value="098" />
<asp:ListItem Text="Ajuste a ingresos por sueldos y salarios gravados"  Value="099" />
<asp:ListItem Text="Ajuste en Viáticos exentos"  Value="100" />
                                <asp:ListItem Text="ISR Retenido de ejercicio anterior"  Value="101" />
                                <asp:ListItem Text="Ajuste a pagos por gratificaciones, primas, compensaciones, recompensas u otros a extrabajadores derivados de jubilación en parcialidades, gravados"  Value="102" />
                                <asp:ListItem Text="Ajuste a pagos que se realicen a extrabajadores que obtengan una jubilación en parcialidades derivados de la ejecución de una resolución judicial o de un laudo gravados"  Value="103" />
                                <asp:ListItem Text="Ajuste a pagos que se realicen a extrabajadores que obtengan una jubilación en parcialidades derivados de la ejecución de una resolución judicial o de un laudo exentos"  Value="104" />
                                <asp:ListItem Text="Ajuste a pagos que se realicen a extrabajadores que obtengan una jubilación en una sola exhibición derivados de la ejecución de una resolución judicial o de un laudo gravados"  Value="105" />
                                <asp:ListItem Text="Ajuste a pagos que se realicen a extrabajadores que obtengan una jubilación en una sola exhibición derivados de la ejecución de una resolución judicial o de un laudo exentos"  Value="106" />
                                </asp:DropDownList>

                                                                                                                                                                                                                                                                                                                                                                                   </td ></tr><tr><td class="auto-style123">* Concepto</td><td><asp:TextBox ID="txtConceptpDed" runat="server" Width="415px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtConceptpDed" ErrorMessage="Campo Obligatorio" ValidationGroup="AgregarDeduccion" ForeColor="Red"></asp:RequiredFieldValidator></td></tr><tr><td class="auto-style123">* Importe </td><td style="width: 40%;text-align: left"><asp:TextBox ID="txtImporteDed" Display="Dynamic" ValidationGroup="AgregarDeduccion" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="AgregarDeduccion" ControlToValidate="txtImporteDed" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImporteDed" BehaviorID="_content_FilteredTextBoxExtender3" /><asp:RegularExpressionValidator id="RegularExpressionValidator6" runat="server" Display="Dynamic"
    ControlToValidate="txtImporteDed" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarDeduccion" ForeColor="#FF3300"/></td><td><asp:Button runat="server" Text="Agregar Deducción" ID="AgregarDeduccion" class="btn btn-primary" ValidationGroup="AgregarDeduccion"
                    OnClick="AgregarDeduccion_Click"/></td></tr><tr><td class="auto-style123">&nbsp;</td><td style="width: 40%;text-align: left">&nbsp;</td></tr></table><asp:GridView runat="server" ID="GvDeducciones" CssClass="page1" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="GvDeducciones_RowCommand" ><Columns><asp:BoundField HeaderText="Clave" DataField="Clave" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Tipo De duccion" DataField="TipoDeduccion" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Concepto" DataField="Concepto" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Importe" DataField="Importe" DataFormatString="{0:C}"  /><asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
       
        <asp:TabPanel Visible="true" ID="TabPanel3" CssClass="page3" runat="server" HeaderText="Otros Pagos"><ContentTemplate><br /><br /><table><tr runat="server" ID="Tr5"><td runat="server" class="auto-style124">&nbsp;* Clave</td><td style="text-align: left" runat="server" class="auto-style117"><asp:TextBox ID="txtClaveOtros" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtClaveOtros" ErrorMessage="Campo Obligatorio" ValidationGroup="AgregarOtros" ForeColor="Red"></asp:RequiredFieldValidator></td ></tr><tr runat="server" ID="tr6"><td style="text-align: left" runat="server" class="auto-style125">* Tipo Otro Pago</td><td style="text-align: left" runat="server" class="auto-style117"><asp:DropDownList ID="ddlTipoOtros" runat="server" style="margin-left: 0px" Height="16px" Width="555px"><asp:ListItem Text="Reintegro de ISR pagado en exceso (siempre que no haya sido enterado al SAT)" Value="001" /><asp:ListItem Text="Subsidio para el empleo (efectivamente entregado al trabajador)" Value="002" /><asp:ListItem Text="Viáticos (entregados al trabajador)" Value="003" /><asp:ListItem Text="Aplicación de saldo a favor por compensación anual" Value="004" /><asp:ListItem Text="Pagos distintos a los listados y que no deben considerarse como ingreso por sueldos, salarios o ingresos asimilados" Value="999" /></asp:DropDownList></td ></tr><tr>
            <td style="text-align: left" class="auto-style125">* Concepto</td><td class="auto-style117"><asp:TextBox ID="txtConceptoOtros" runat="server" Width="415px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtConceptoOtros" ErrorMessage="Campo Obligatorio" ValidationGroup="AgregarOtros" ForeColor="Red"></asp:RequiredFieldValidator></td></tr><tr><td class="auto-style124">* Importe </td><td style="text-align: left" class="auto-style117"><asp:TextBox ID="txtImporteOtros" Display="Dynamic" ValidationGroup="AgregarOtros" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="AgregarOtros" ControlToValidate="txtImporteOtros" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImporteOtros" BehaviorID="_content_FilteredTextBoxExtender3" /><asp:RegularExpressionValidator id="RegularExpressionValidator10" runat="server" Display="Dynamic"
    ControlToValidate="txtImporteOtros" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarOtros" ForeColor="#FF3300"/></td></tr><tr><td style="text-align: left" class="auto-style125">SubsidioCausado</td><td style="text-align: left" class="auto-style117"><asp:TextBox ID="txtSubsidio" runat="server"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtSubsidio" BehaviorID="_content_FilteredTextBoxExtender3" /><asp:RegularExpressionValidator id="RegularExpressionValidator11" runat="server" Display="Dynamic"
    ControlToValidate="txtSubsidio" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarOtros" ForeColor="#FF3300"/></td><td><asp:CheckBox ID="ChCompensacionSaldosAFavor" Text="CompensacionSaldosAFavor" CssClass="page2" runat="server" AutoPostBack="True" OnCheckedChanged="ChCompensacionSaldosAFavor_CheckedChanged" /></td></tr><tr><td colspan="2"></td></tr></table><uc:UCOtrosPagos id="UCOtrosPagos" runat="server"></uc:UCOtrosPagos><br /><asp:Button runat="server" Text="Agregar Otros Pagos" ID="AgregarOtros" class="btn btn-primary" OnClick="AgregarOtros_Click" ValidationGroup="AgregarOtros"/><br /><br /><asp:GridView runat="server" ID="GvOtrosPagos" AutoGenerateColumns="False" CssClass="page1"  onrowcommand="GvOtrosPagos_RowCommand"
			Width="100%" ShowHeaderWhenEmpty="True" ><Columns><asp:BoundField HeaderText="Clave" DataField="Clave" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Tipo Otro Pago" DataField="TipoOtroPago" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Concepto" DataField="Concepto" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Importe" DataField="Importe" DataFormatString="{0:C}"  /><asp:BoundField HeaderText="Subsidio Causado" DataField="SubsidioCausado" DataFormatString="{0:C}"  /><asp:BoundField HeaderText="Saldo A Favor" DataField="SaldoAFavor" DataFormatString="{0:C}"  /><asp:BoundField HeaderText="RemanenteSalFav" DataField="RemanenteSalFav" DataFormatString="{0:C}"  /><asp:BoundField HeaderText="Año" DataField="Año" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
         <asp:TabPanel ID="TabPanel2" runat="server" CssClass="page3"  Visible="true" HeaderText="Incapacidades"><ContentTemplate><table align="center"><tr><td class="auto-style118">DiasIncapacidad</td><td class="auto-style119"><asp:TextBox ID="txtDiasIncapacidad" runat="server"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
            runat="server" FilterType="Numbers" TargetControlID="txtDiasIncapacidad" BehaviorID="_content_FilteredTextBoxExtender12" /></td></tr><tr><td class="auto-style118">TipoIncapacidad</td><td class="auto-style119"><asp:DropDownList ID="ddlTipoIncapacidad" runat="server" style="margin-left: 0px"><asp:ListItem Text="Riesgo de trabajo" Value="01" /><asp:ListItem Text="Enfermedad en general" Value="02" /><asp:ListItem Text="Maternidad" Value="03 " /></asp:DropDownList></td></tr><tr><td class="auto-style118">ImporteMonetario</td><td class="auto-style119"><asp:TextBox ID="txtImporteMonetario" runat="server"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtImporteMonetario" BehaviorID="_content_FilteredTextBoxExtender3" /><asp:RegularExpressionValidator id="RegularExpressionValidator12" runat="server" Display="Dynamic"
    ControlToValidate="txtImporteMonetario" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarIncapacidad" ForeColor="#FF3300"/></td><td><asp:Button runat="server" Text="Agregar Incapacidad" ID="btnAgregarIncapacidad" class="btn btn-primary"  ValidationGroup="AgregarIncapacidad" OnClick="btnAgregarIncapacidad_Click"/></td></tr></table><br /><asp:GridView runat="server" ID="GridIncapacidad"  CssClass="page1" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" OnRowCommand="GridIncapacidad_RowCommand" ><Columns><asp:BoundField HeaderText="Dias Incapacidad" DataField="DiasIncapacidad" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="Tipo Incapacidad" DataField="TipoIncapacidad" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField><asp:BoundField HeaderText="ImporteMonetario" DataField="ImporteMonetario" DataFormatString="{0:C}" /><asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
    </asp:TabContainer>
                </div>
        <br />
		
		    <asp:UpdateProgress AssociatedUpdatePanelID="up1" ID="UpdateProgress1"  runat="server">
			<ProgressTemplate>
			<div id="Background" ></div>
                <div id="Progress">
				    <br/><br/><br/><br/>
				    <br/>
				    <br>
				</br/>  CFDI en proceso ..
			  </div>
			</ProgressTemplate>
			</asp:UpdateProgress>
			<asp:Label runat="server" ID="lblError" ForeColor="Red" />
	  
		
		<div style="float: right">
			 <table style="text-align:right;" >
			
			<tr class="auto-style162">
				<td>Percepciones</td>
				<td><asp:Label runat="server" ID="lblTotalPercepciones" >$0.00</asp:Label>
                </td>
			</tr>
			<tr class="auto-style162">
				<td>Deducciones</td>
				<td><asp:Label runat="server" ID="lblTotalDeducciones" >$0.00</asp:Label>
                </td>
			</tr>
                 <tr class="auto-style162">
				<td>OtrosPagos</td>
				<td><asp:Label runat="server" ID="lblTotalOtrosPagos" >$0.00</asp:Label>
                     </td>
                    
			</tr>
            <tr class="auto-style162">
				<td>Total</td>
				<td><asp:Label runat="server" ID="lblTotal" >$0.00</asp:Label>
                </td>
			</tr>
		</table>
            <td></td>
		</div>
	<div style="clear: both"></div>
	<p align="right">
		<asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" class="btn btn-primary"
			onclick="btnLimpiar_Click"/>&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="BtnVistaPrevia" Text="Vista Previa" onclick="btnGenerarPreview_Click" class="btn btn-primary"
		 ValidationGroup="CrearFactura" />&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnGenerarFactura" Text="Generar Recibo" onclick="btnGenerarFactura_Click" class="btn btn-primary"
		 ValidationGroup="CrearFactura" />
         <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnGenerarFactura" ConfirmText="Confirma que deseas generar el comprobante"/>
	</p>

    <asp:ModalPopupExtender runat="server" ID="mpeBuscarConcepto" TargetControlID="btnConceptoDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarConcepto" PopupControlID="pnlBuscarConcepto" />
	<asp:Panel runat="server" ID="pnlBuscarConcepto" style="text-align: center;" Width="800px" BackColor="White">
		
        <h1><asp:Label Text="Agregar" ID="lblAgregar" runat="server"></asp:Label></h1>
		<p>
			<table>
			    <tr runat="server" ID="tdDeduccion2" style="margin-left: 10px">
			        <td style="text-align: left" class="auto-style150">* Tipo Deducción</td>
			        <td style="width: 40%;text-align: left">
			            <asp:DropDownList runat="server" ID="ddlDeduccion" style="margin-left: 0px">
			                <asp:ListItem Value="001" Text="Seguridad social" />
                            <asp:ListItem Value="002" Text="ISR" />
                            <asp:ListItem Value="003" Text="Aportaciones a retiro, cesantía en edad avanzada y vejez. " />
                            <asp:ListItem Value="004" Text="Otros " />
                            <asp:ListItem Value="005" Text="Aportaciones a Fondo de vivienda " />
			                <asp:ListItem Value="006" Text="Descuento por incapacidad" />
                            <asp:ListItem Value="007" Text="Pensión alimenticia" />
                            <asp:ListItem Value="008" Text="Renta" />
                            <asp:ListItem Value="009" Text="Préstamos provenientes del Fondo Nacional de la Vivienda para los Trabajadores" />
                            <asp:ListItem Value="010" Text="Pago por crédito de vivienda" />
                            <asp:ListItem Value="011" Text="Pago de abonos INFONACOT" />
			                <asp:ListItem Value="012" Text="Anticipo de salarios"/>
                            <asp:ListItem Value="013" Text="Pagos hechos con exceso al trabajador" />
                            <asp:ListItem Value="014" Text="Errores" />
                            <asp:ListItem Value="015" Text="Pérdidas" />
                            <asp:ListItem Value="016" Text="Averías" />
                            <asp:ListItem Value="017" Text="Adquisición de artículos producidos por la empresa o establecimiento " />
                            <asp:ListItem Value="018" Text="Cuotas para la constitución y fomento de sociedades cooperativas y de cajas de ahorro" />
                            <asp:ListItem Value="019" Text="Cuotas sindicales" />
                            <asp:ListItem Value="020" Text="Ausencia (Ausentismo)" />
                            <asp:ListItem Value="021" Text="Cuotas obrero patronales" />
                        </asp:DropDownList>
                    </td >
                    
			    </tr>
                <tr runat="server" class="page2" ID="trPercepcion">
			        <td style="text-align: left" class="auto-style150">* Tipo Percepción</td>
			        <td style="width: 40%;text-align: left">
			            <asp:DropDownList runat="server" ID="ddlPercepcion2" style="margin-left: 0px">
			                <asp:ListItem  Value="001" Text="Sueldos, Salarios  Rayas y Jornales " />
                            <asp:ListItem  Value="002" Text="Gratificación Anual (Aguinaldo) " />
                            <asp:ListItem  Value="003" Text="Participación de los Trabajadores en las Utilidades PTU " />
                            <asp:ListItem  Value="004" Text="Reembolso de Gastos Médicos Dentales y Hospitalarios " />
                            <asp:ListItem  Value="005" Text="Fondo de Ahorro " />
                            <asp:ListItem  Value="006" Text="Caja de ahorro " />
                            <asp:ListItem  Value="009" Text="Contribuciones a Cargo del Trabajador Pagadas por el Patrón " />
                            <asp:ListItem  Value="010" Text="Premios por puntualidad " />
                            <asp:ListItem  Value="011" Text="Prima de Seguro de vida " />
                            <asp:ListItem  Value="012" Text="Seguro de Gastos Médicos Mayores " />
                            <asp:ListItem  Value="013" Text="Cuotas Sindicales Pagadas por el Patrón " />
                            <asp:ListItem  Value="014" Text="Subsidios por incapacidad " />
                            <asp:ListItem  Value="015" Text="Becas para trabajadores y/o hijos " />
                            <asp:ListItem  Value="019" Text="Horas extra " />
                            <asp:ListItem  Value="020" Text="Prima dominical " />
                            <asp:ListItem  Value="021" Text="Prima vacacional " />
                            <asp:ListItem  Value="022" Text="Prima por antigüedad " />
                            <asp:ListItem  Value="023" Text="Pagos por separación " />
                            <asp:ListItem  Value="024" Text="Seguro de retiro " />
                            <asp:ListItem  Value="025" Text="Indemnizaciones " />
                            <asp:ListItem  Value="026" Text="Reembolso por funeral " />
                            <asp:ListItem  Value="027" Text="Cuotas de seguridad social pagadas por el patrón " />
                            <asp:ListItem  Value="028" Text="Comisiones " />
                            <asp:ListItem  Value="029" Text="Vales de despensa " />
                            <asp:ListItem  Value="030" Text="Vales de restaurante " />
                            <asp:ListItem  Value="031" Text="Vales de gasolina " />
                            <asp:ListItem  Value="032" Text="Vales de ropa " />
                            <asp:ListItem  Value="033" Text="Ayuda para renta " />
                            <asp:ListItem  Value="034" Text="Ayuda para artículos escolares " />
                            <asp:ListItem  Value="035" Text="Ayuda para anteojos " />
                            <asp:ListItem  Value="036" Text="Ayuda para transporte " />
                            <asp:ListItem  Value="037" Text="Ayuda para gastos de funeral " />
                            <asp:ListItem  Value="038" Text="Otros ingresos por salarios " />
                            <asp:ListItem  Value="039" Text="Jubilaciones, pensiones o haberes de retiro " />
                            <asp:ListItem  Value="044" Text="Jubilaciones, pensiones o haberes de retiro en parcialidades " />
                            <asp:ListItem  Value="045" Text="Ingresos en acciones o títulos valor que representan bienes " />
                            <asp:ListItem  Value="046" Text="Ingresos asimilados a salarios " />
                            <asp:ListItem  Value="047" Text="Alimentación " />
                            <asp:ListItem  Value="048" Text="Habitación " />
                            <asp:ListItem  Value="049" Text="Premios por asistencia " />
                      
                        </asp:DropDownList>
                    </td >
                   
			    </tr>
                
                <tr>
			        <td style="text-align: left" class="auto-style150">* Importe Gravado</td>
			        <td style="width: 40%;text-align: left"><asp:TextBox ID="txtImporteGravado2" Display="Dynamic" ValidationGroup="agregar" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="agregar" ControlToValidate="txtImporteGravado2" ErrorMessage="Campo Obligatorio" runat="server" ></asp:RequiredFieldValidator></td>
			    </tr>
                <tr>
			        <td style="text-align: left" class="auto-style150">* Importe Excento</td>
			        <td style="width: 40%;text-align: left"><asp:TextBox ID="txtImporteExcento2" Display="Dynamic" ValidationGroup="agregar" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="agregar" ControlToValidate="txtImporteExcento2" ErrorMessage="Campo Obligatorio" runat="server" ></asp:RequiredFieldValidator>
                    </td>
			    </tr>
			</table>
            <div>
                <asp:Label runat="server" ID="lblErrorPercepcion"></asp:Label>
            </div>
        <br/>
		<%--<asp:Button runat="server" ID="btnSeleccionarConcepto" Text="Seleccionar" onclick="btnSeleccionarConcepto_Click" />&nbsp;&nbsp;--%>
        <div align="right">
            <asp:Button runat="server" ID="btnCerrarConcepto" class="btn btn-primary" Text="Cancelar" OnClick="btnCerrarConcepto_Click" />
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="agregar" class="btn btn-primary"
                onclick="btnAceptar_Click" />
        </div><br />
		
	</asp:Panel>

 
	<asp:Button runat="server" ID="btnConceptoDummy" style="display: none;"/>
 
	</ContentTemplate>
	<Triggers>
		<asp:PostBackTrigger ControlID="btnLimpiar" />
		
		<asp:PostBackTrigger ControlID="BtnVistaPrevia"/>
       
        
	</Triggers>
    </asp:UpdatePanel>
    
    

</asp:Content>
