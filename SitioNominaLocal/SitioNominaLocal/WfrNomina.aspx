<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WfrNomina.aspx.cs" Inherits="GafLookPaid.WfrNomina" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
	.mpeBack
	{
		background-color: Gray;
		filter: alpha(opacity=70);
		opacity: 0.7;
	}
	
		.auto-style3 {
            width: 168px;
            text-align: right;
        }
	
		.auto-style4 {
            width: 143px;
        }
	
		.auto-style5 {
            height: 29px;
        }
        .auto-style6 {
            width: 168px;
        }
        .auto-style7 {
            height: 29px;
            width: 168px;
        }
	
		.auto-style11 {
            width: 209px;
        }
        	
		.auto-style12 {
            width: 122px;
        }
        	
		</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Generar Recibo de Nómina</h1>
	<p style="color: #C0C0C0">
		* Campos obligatorios</p>
       <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <table>
        <tr>
<td style="text-align: right" class="auto-style12" >
		* Empresa:</td><td class="auto-style11"><asp:DropDownList runat="server" ID="ddlEmpresa" AutoPostBack="True"
		 DataTextField="RazonSocial" DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresa_SelectedIndexChanged" />
	</td>
            </tr>
    <tr>
        <td style="text-align: right" class="auto-style12" >
        * Centro de Trabajo:</td><td class="auto-style11"><asp:DropDownList runat="server" ID="DdlCentroTrabajo" AutoPostBack="True"
                                                  DataTextField="Nombre" DataValueField="IdCentroTrabajo" onselectedindexchanged="DdlCentroTrabajo_OnSelectedIndexChanged" />
	    <asp:Label runat="server" ID="lblLugarExpedicion" Visible="False" />
		</td>
        
        </tr>
	<tr>
    <td style="text-align: right" class="auto-style12"  >

        * Tipo de Nómina:</td><td colspan="3"> <asp:DropDownList runat="server" ID="ddlTipoNomina"  
                    ClientIDMode="Static" AutoPostBack="True" 
                    onselectedindexchanged="ddlTipoNomina_SelectedIndexChanged">
                    <asp:ListItem Value="Quincenal" Text="Quincenal"></asp:ListItem>
                    <asp:ListItem Value="Catorcenal" Text="Catorcenal"></asp:ListItem>
                    <asp:ListItem Value="Semanal" Text="Semanal"></asp:ListItem>
                    <asp:ListItem Value="Otro" Text="Otro (Especificar)"></asp:ListItem>
                </asp:DropDownList>
            &nbsp;<asp:TextBox runat="server" ID="txtTipoNominaOtro" Width="400px" 
                    ClientIDMode="Static" Visible="False"/>
                    &nbsp;<asp:RequiredFieldValidator ID="valOtro" runat="server" 
                    ControlToValidate="txtTipoNominaOtro" ErrorMessage="Campo Requerido" 
                    ClientIDMode="Static" Enabled="False"></asp:RequiredFieldValidator>
            </td>
        </tr>
   
        <tr>
            <td  style="text-align: right" class="auto-style12"><asp:Label ID="Label8" runat="server" Text="Nombre:"></asp:Label></td>
            <td ><asp:TextBox runat="server" ID="txtNombreCentro" Enabled="False"></asp:TextBox></td>
            <td style="text-align: right" ><asp:Label ID="Label10" runat="server" Text="Registro Patronal:"></asp:Label></td>
            <td><asp:TextBox runat="server" ID="txtRegistroPatronal" Enabled="False"></asp:TextBox></td>
        </tr>
         <tr>
        <td  style="text-align: right" class="auto-style12"><asp:Label ID="Label21" runat="server" Text="Tipo de jornada:"></asp:Label></td>
        <td  ><asp:TextBox runat="server" Enabled="False" ToolTip="Diurna, nocturna, mixta, por hora, reducida, continua, partida, por turnos, etc." ID="txtTipoJornada"></asp:TextBox>
            </td>
        
       
        <td style="text-align: right"  ><asp:Label ID="Label24" runat="server" Text="Riesgo del puesto:"></asp:Label></td>
                        <td ><asp:DropDownList runat="server" ID="ddlRiesgoPuesto" Enabled="False">
                                <asp:ListItem Text="Clase I" Value="1" />
                                <asp:ListItem Text="Clase II" Value="2" />
                                <asp:ListItem Text="Clase III" Value="3" />
                                <asp:ListItem Text="Clase IV" Value="4"/>
                                <asp:ListItem Text="Clase V" Value="5" />
                            </asp:DropDownList>
                            </td>


    </tr>

    </table>
   

    <p>
		*
		Empleado:&nbsp;<asp:DropDownList runat="server" ID="ddlClientes" AutoPostBack="True"
		 DataTextField="NombreCompleto" DataValueField="idCliente" 
            onselectedindexchanged="ddlClientes_SelectedIndexChanged" />
		<br/>
		<asp:TextBox runat="server" TextMode="MultiLine" ID="txtRazonSocial" Width="500px" Height="75px" Enabled="False" />
	</p>
    <table width="100%">
        
                    <tr>
                        <td class="auto-style3" ><h3><asp:Label ID="Label58" runat="server" Text="Datos de Nómina" style="text-align: center"></asp:Label></h3></td>
                    </tr>
                    <tr>
                        <td class="auto-style3"><asp:Label ID="Label7" runat="server" Text="Concepto:"></asp:Label></td>
                        <td class="auto-style4">
                            <asp:DropDownList ID="ddlConcepto" runat="server">
                                <asp:ListItem Value="Pago de nómina" Text="Pago de nómina" Selected="True" />
                                <asp:ListItem Value="Aguinaldo," Text="Aguinaldo," />
                                <asp:ListItem Value="Prima vacacional" Text="Prima vacacional" />
                                <asp:ListItem Value="Fondo de Ahorro" Text="Fondo de Ahorro" />
                                <asp:ListItem Value="Liquidación" Text="Liquidación" />
                                <asp:ListItem Value="Finiquito" Text="Finiquito" />


                            </asp:DropDownList>
                            

                        </td>

                    </tr>
                    <tr>
                        
                        
                        <td style="text-align: right" class="auto-style6"  >
                            <asp:Label ID="Label12" runat="server" text="* Fecha de Pago:"></asp:Label>
                        </td>
                        <td  colspan="3">
                            <asp:TextBox runat="server" ID="txtFechaPagoNomina" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender runat="server" TargetControlID="txtFechaPagoNomina" Format="yyyy-MM-dd" ID="CalendarExtender3" />
                            <asp:RegularExpressionValidator runat="server" 
                                ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])" 
                                ControlToValidate="txtFechaPagoNomina" ErrorMessage="Dato inválido" 
                                ID="RegularExpressionValidator9"></asp:RegularExpressionValidator>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ValidationGroup="CrearFactura" ControlToValidate="txtFechaPagoNomina" 
                                ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                            <br/>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style7"  ><asp:Label ID="Label1" runat="server" Text="* Fecha de Inicio del Pago:"></asp:Label></td>
                        <td  style="text-align: left"  colspan="3" class="auto-style5">
                            <asp:TextBox runat="server" ID="txtFechaInicio" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender runat="server" TargetControlID="txtFechaInicio" Format="yyyy-MM-dd" ID="CalendarExtender2" />
                            <asp:RegularExpressionValidator runat="server" 
                                ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])" 
                                ControlToValidate="txtFechaInicio" ErrorMessage="Dato inválido" 
                                ID="RegularExpressionValidator7"></asp:RegularExpressionValidator>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="CrearFactura" ControlToValidate="txtFechaInicio" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style5">
                            </td>
                       
                    </tr>
                    <tr>
                        <td  style="text-align: right" class="auto-style6" >
                            <asp:Label ID="Label2" runat="server" text="* Fecha Final del Pago:"></asp:Label>
                        </td>
                        <td  style="text-align: left" colspan="3">
                            <asp:TextBox runat="server" ID="txtFechaFin" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender runat="server" TargetControlID="txtFechaFin" Format="yyyy-MM-dd" ID="CalendarExtender1" />
                            <asp:RegularExpressionValidator runat="server" 
                                ValidationExpression="(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])" 
                                ControlToValidate="txtFechaFin" ErrorMessage="Dato inválido" 
                                ID="RegularExpressionValidator8"></asp:RegularExpressionValidator>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="CrearFactura" ControlToValidate="txtFechaFin" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;</td>
                       

                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style6"  ><asp:Label ID="Label3" runat="server" Text="* Días Pagados:"></asp:Label></td>
                        <td  style="text-align: left" colspan="3"  >
                            <asp:TextBox runat="server" ID="txtDiasPagados" Width="100px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDias" runat="server" ControlToValidate="txtDiasPagados" ErrorMessage="Campo Obligatorio" ValidationGroup="CrearFactura"></asp:RequiredFieldValidator>

                        </td>

                    </tr>
                    <tr>
			<td  style="text-align: right" class="auto-style6"  >
		        <asp:Label ID="Label59" runat="server" Text="Status Recibo:"></asp:Label>
                        </td>
			<td class="auto-style4" >
		        <asp:DropDownList runat="server" ID="ddlStatusRecibo" AutoPostBack="True" 
                    onselectedindexchanged="ddlStatusRecibo_SelectedIndexChanged">
		            <asp:ListItem runat="server" Text="Pagado" Value="1"></asp:ListItem>
                   
		            <asp:ListItem Value="0">Pendiente</asp:ListItem>
                   
		        </asp:DropDownList>
			</td>
			<td>
                            &nbsp;</td>
			<td>
                            <asp:TextBox runat="server" ID="txtFechaPago" Visible="False"></asp:TextBox>
                            <asp:CalendarExtender runat="server" TargetControlID="txtFechaPago" Format="yyyy-MM-dd" ID="txtFechaPago_CalendarExtender" />
                            </td>
            <td style="text-align: right" >
                Tipo de Documento:</td><td>
            <asp:DropDownList runat="server" ID="ddlTipoDocumento" AutoPostBack="True" >
                <asp:ListItem runat="server" Text="Recibo de Nómina" Value="Recibo de Nómina" Selected="True" />
                <asp:ListItem runat="server" Text="Recibo de Salarios Asimilados" Value="Recibo de Salarios Asimilados" />
            </asp:DropDownList>
            </td>
			
		</tr>
		<tr>
			<td  style="text-align: right" class="auto-style3" >Serie:</td>
			<td  style="text-align: left" class="auto-style4">
				<asp:TextBox runat="server" ID="txtSerie" Width="75px" />
			</td>
			<td>Folio:</td>
			<td><asp:TextBox runat="server" ID="txtFolio" Width="75px" Enabled="False" /></td>
			<td style="text-align: right" >Moneda:</td>
			<td>
				<asp:DropDownList runat="server" ID="ddlMoneda" >
					<asp:ListItem Value="1" Text="MXN" Selected="True" />
					
				</asp:DropDownList>
			</td>
			
			
		</tr>
        <tr >
				<td style="text-align: right;" class="auto-style3" >
					Método de Pago
				    :</td>
			   <td class="auto-style4" style="text-align: left" >
					<asp:DropDownList runat="server" ID="ddlMetodoPago"  AutoPostBack="True"
                            OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged">
                            <asp:ListItem Text="01-Efectivo" Value="01" />
                            <asp:ListItem Text="02-Cheque nominativo" Value="02" /> <%--aplica--%>
                            <asp:ListItem Text="03-Transferencia electrónica de fondos" Value="03" /><%--aplica--%>
                            <asp:ListItem Text="04-Tarjeta de Crédito" Value="04" /><%--aplica--%>
                            <asp:ListItem Text="05-Monedero Electrónico" Value="05" /><%--aplica--%>
                            <asp:ListItem Text="06-Dinero Electrónico" Value="06" /><%--aplica--%>
                            <asp:ListItem Text="08-Vales de despensa" Value="08" />
                            <asp:ListItem Text="28-Tarjeta de Débito" Value="28" />
                            <asp:ListItem Text="29-Tarjeta de Servicio" Value="29" />
                            <asp:ListItem Text="NA" Value="NA" />
                            <asp:ListItem Text="99-Otros" Value="99" />
                        </asp:DropDownList>
				<asp:TextBox runat="server" ID="txtMetodoPago" Visible="False" />
                <br/>
				<asp:Label runat="server" ID="lblMetodoPago" Visible="False"></asp:Label>
				</td>
				<td >
					
				</td>
            <td ></td>
                <td style="text-align: right" >	#Cuenta o # Tarjeta <br/> (Últimos 4 dígitos)
				
                    &nbsp;</td>
            <td>
                    <asp:TextBox runat="server" ID="txtCuenta" />
                </td>

			</tr>
        <tr >
	<td style="text-align: right;" class="auto-style3">
		        Observaciones
	            :</td>
	        <td colspan="4" style="text-align: left" >
		        <asp:TextBox runat="server" ID="txtProyecto" Width="500px" />
	        </td>
	        <td>
		        &nbsp;</td>
        </tr>
      


       <%-- <tr>
            <td style="text-align: right;" class="auto-style3" >
		        Condiciones de Pago:</td><td  style="text-align: left" class="auto-style4">
		        <asp:DropDownList runat="server" ID="ddlCondicionesPago" AutoPostBack="True" 
                    onselectedindexchanged="ddlCondicionesPago_SelectedIndexChanged">
		            <asp:ListItem runat="server" Text="En una sola exhibición" Value="En una sola exhibición"></asp:ListItem>
                   
		        </asp:DropDownList>
	        </td>
        </tr>--%>


    </table>
    <%--<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>--%>
		    <table width="100%" >
		
	</table>
    

    <br/>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%">

        <asp:TabPanel ID="PanelPercepciones" runat="server" HeaderText="Percepciones">
            <ContentTemplate>
            <asp:Button runat="server" Text="Agregar Percepcion" ID="btnAgregarPercepcion" 
                    onclick="btnAgregarPercepcion_Click"/>
            <asp:GridView runat="server" ID="gvPercepciones" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvPercepciones_RowCommand" >
			<Columns>
				
				<asp:BoundField HeaderText="Clave" DataField="Clave" >
				<ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
				<asp:BoundField HeaderText="Concepto" DataField="Concepto" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Importe Gravado" DataField="ImporteGravado" DataFormatString="{0:C}"  />
				<asp:BoundField HeaderText="Importe Exento" DataField="ImporteExento" DataFormatString="{0:C}" />
                <asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/>
			</Columns>
		</asp:GridView>
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="PanelDeducciones" runat="server" HeaderText="Deducciones">
            <ContentTemplate>
            <asp:Button runat="server" Text="Agregar Deducción" ID="Button1" 
                    OnClick="Button1_Click"/>
            <asp:GridView runat="server" ID="GvDeducciones" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="GvDeducciones_RowCommand" >
			<Columns>
				
				<asp:BoundField HeaderText="Clave" DataField="Clave" >
				<ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
				<asp:BoundField HeaderText="Concepto" DataField="Concepto" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Importe Gravado" DataField="ImporteGravado" DataFormatString="{0:C}"  />
				<asp:BoundField HeaderText="Importe Exento" DataField="ImporteExento" DataFormatString="{0:C}" />
                <asp:ButtonField Text="Eliminar" CommandName="Eliminar" HeaderText="Eliminar"/>
				
			</Columns>
		</asp:GridView>
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" Visible="False" HeaderText="Incapacidades">
            <ContentTemplate>
            <asp:Button runat="server" Text="Agregar Incapacidad" ID="btnAgregarIncapacidad"/>
            <asp:GridView runat="server" ID="GridIncapacidad" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" >
			<Columns>
				<asp:BoundField HeaderText="Dias Incapacidad" DataField="DiasIncapacidad" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Tipo Incapacidad" DataField="TipoIncapacidad" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Descuento" DataField="Descuento" DataFormatString="{0:C}" />
				
			</Columns>
		</asp:GridView>
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel Visible="False" ID="TabPanel3" runat="server" HeaderText="Horas Extra">
            <ContentTemplate>
            <asp:Button runat="server" Text="Agregar Horas Extra" ID="Button2"/>
            <asp:GridView runat="server" ID="GridView2" AutoGenerateColumns="False" 
			Width="100%" ShowHeaderWhenEmpty="True" >
			<Columns>
				<asp:BoundField HeaderText="Dias Horas Extra" DataField="Dias" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Tipo Horas" DataField="TipoHoras" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Horas Extra" DataField="HorasExtra"  />
                <asp:BoundField HeaderText="Importe Pagado" DataField="ImportePagado" DataFormatString="{0:C}" />
				
			</Columns>
		</asp:GridView>
        </ContentTemplate>
        </asp:TabPanel>

    </asp:TabContainer>
        <br />
		
		   <asp:UpdateProgress AssociatedUpdatePanelID="up1" ID="UpdateProgress1" runat="server">
			<ProgressTemplate>
			<div align="center" >
			  <asp:Image ID="Image1" ImageUrl="images/ajax-loader.gif" runat="server"/> 
			  <br />
			  CFDI en proceso ..
			  </div>
			</ProgressTemplate>
			</asp:UpdateProgress>
			<asp:Label runat="server" ID="lblError" ForeColor="Red" />
	  
		
		<div style="float: right">
			 <table style="text-align:right;" >
			
			<tr>
				<td>Percepciones</td>
				<td><asp:Label runat="server" ID="lblTotalPercepciones" /></td>
			</tr>
			<tr>
				<td>Deducciones</td>
				<td><asp:Label runat="server" ID="lblTotalDeducciones" /></td>
			</tr>
            <tr>
				<td>Total</td>
				<td><asp:Label runat="server" ID="lblTotal" /></td>
			</tr>
		</table>
		</div>
	<div style="clear: both"></div>
	<p align="right">
		<asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" 
			onclick="btnLimpiar_Click"/>&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="BtnVistaPrevia" Text="Vista Previa" onclick="btnGenerarPreview_Click"
		 ValidationGroup="CrearFactura" />&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnGenerarFactura" Text="Generar Recibo" onclick="btnGenerarFactura_Click"
		 ValidationGroup="CrearFactura" />
         <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnGenerarFactura" ConfirmText="Confirma que deseas generar el comprobante"/>
	</p>
    <asp:ModalPopupExtender runat="server" ID="mpeBuscarConcepto" TargetControlID="btnConceptoDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarConcepto" PopupControlID="pnlBuscarConcepto" />
	<asp:Panel runat="server" ID="pnlBuscarConcepto" style="text-align: center;" Width="800px" BackColor="White">
		
        <h1><asp:Label Text="Agregar" ID="lblAgregar" runat="server"></asp:Label></h1>
		<p>
			<table width="100%">
			    <tr runat="server" ID="tdDeduccion">
			        <td style="width: 30%;text-align: right">* Tipo Deducción</td>
			        <td style="width: 40%;text-align: left">
			            <asp:DropDownList runat="server" ID="ddlDeduccion">
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
                    <td style="width: 30%;text-align: left">
                        
                    </td>
			    </tr>
                <tr runat="server" ID="trPercepcion">
			        <td style="width: 30%;text-align: right">* Tipo Percepción</td>
			        <td style="width: 40%;text-align: left">
			            <asp:DropDownList runat="server" ID="ddlPercepcion">
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
                            <asp:ListItem  Value="016" Text="Otros " />
                            <asp:ListItem  Value="017" Text="Subsidio para el empleo " />
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
                        </asp:DropDownList>
                    </td >
                    <td style="width: 30%;text-align: left">
                        
                    </td>
			    </tr>
                
                <tr>
			        <td style="width: 30%;text-align: right">* Importe Gravado</td>
			        <td style="width: 40%;text-align: left"><asp:TextBox ID="txtImporteGravado" Display="Dynamic" ValidationGroup="agregar" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="agregar" ControlToValidate="txtImporteGravado" ErrorMessage="Campo Obligatorio" runat="server" ></asp:RequiredFieldValidator></td>
			    </tr>
                <tr>
			        <td style="width: 30%;text-align: right">* Importe Excento</td>
			        <td style="width: 40%;text-align: left"><asp:TextBox ID="txtImporteExcento" Display="Dynamic" ValidationGroup="agregar" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="agregar" ControlToValidate="txtImporteExcento" ErrorMessage="Campo Obligatorio" runat="server" ></asp:RequiredFieldValidator>
                    </td>
			    </tr>
			</table>
            <div>
                <asp:Label runat="server" ID="lblErrorPercepcion"></asp:Label>
            </div>
        <br/>
		<%--<asp:Button runat="server" ID="btnSeleccionarConcepto" Text="Seleccionar" onclick="btnSeleccionarConcepto_Click" />&nbsp;&nbsp;--%>
        <div align="right">
            <asp:Button runat="server" ID="btnCerrarConcepto" Text="Cancelar" />
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="agregar" 
                onclick="btnAceptar_Click" />
        </div>
		
	</asp:Panel>
	<asp:Button runat="server" ID="btnConceptoDummy" style="display: none;"/>

	</ContentTemplate>
	<Triggers>
		<asp:PostBackTrigger ControlID="btnLimpiar" />
		
		<asp:PostBackTrigger ControlID="BtnVistaPrevia"/>
       
        
	</Triggers>
    </asp:UpdatePanel>
    
    

</asp:Content>
