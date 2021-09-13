<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrConsultaNomina.aspx.cs" Inherits="GafLookPaid.wfrConsultaNomina" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--     <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />--%>
    
       <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Reporte de CFDI</h3>
            </div>
            <div class ="card-body" >
                  <div class = "row form-group"> 
           <div class="col-lg-12 " style="color:red;" >
                            <asp:Label ID="lblError" class="control-label" runat="server" ForeColor="Red" Font-Bold="true" style=" font-size: x-small; text-align: left; font-variant: small-caps;" Width="250px"></asp:Label>
            </div>
            </div>
             <div class = "row form-group"> 
           <div class="col-lg-12 " >
                 <a href="https://portalcfdi.facturaelectronica.sat.gob.mx" target="_blank" 
                     class="auto-style3">Sitio de cancelación del SAT</a><span class="auto-style3"> </span>
               </div>
    </div>
            <div class = "row">
                   <div class = "form-group col-lg-10">
                         <asp:Label ID="Label1" runat="server" class="control-label" Text="Empresa"></asp:Label>
        	    <asp:DropDownList runat="server" ID="ddlEmpresas" AutoPostBack="true" DataTextField="RazonSocial"
		DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged"  CssClass="form-control"/>
                       </div>
                </div>
                <div class = "row">
                   <div class = "form-group col-lg-3">
                         <asp:Label ID="Label2" runat="server" class="control-label" Text="Fecha Inicial"></asp:Label>
        		<asp:TextBox runat="server" ID="txtFechaInicial" CssClass="form-control" />
				<asp:CompareValidator runat="server" ID="cvFechaInicial" ControlToValidate="txtFechaInicial" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaInicial" Animated="False" PopupButtonID="txtFechaInicial" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy" />
			</div>
                    <div class = "form-group col-lg-3">
                         <asp:Label ID="Label3" runat="server" class="control-label" Text="Fecha Final"></asp:Label>
        		<asp:TextBox runat="server" ID="txtFechaFinal"  CssClass="form-control"/>
				<asp:CompareValidator runat="server" ID="cvFechaFinal" ControlToValidate="txtFechaFinal" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaFinal" Animated="False"
                    PopupButtonID="txtFechaFinal" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy" />
			</div>
                    </div>
		  <div class = "row">
                   <div class = "form-group col-lg-4">
                         <asp:Label ID="Label4" runat="server" class="control-label" Text="Empleados"></asp:Label>
        		<asp:DropDownList runat="server" ID="ddlClientes" AppendDataBoundItems="True" 
                    CssClass="form-control" DataTextField="NombreCompleto"
			 DataValueField="idCliente"  />
                       </div>
			       <div class = "form-group col-lg-4">
                         <asp:Label ID="Label5" runat="server" class="control-label" Text="Texto"></asp:Label>
        	<asp:TextBox runat="server" ID="txtTexto" CssClass="form-control"/>
                       </div>
              </div>
                 <div class = "row">
                   <div class = "form-group col-lg-4">
         
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" Width="239px">
					<asp:ListItem Text="Todas" Value="Todos" Selected="True"/>
				    <asp:ListItem Text="Canceladas" Value="Cancelado"/>
				
				</asp:RadioButtonList>
                       </div>
                     </div>
			 <div class = "row">
                   <div class = "form-group col-lg-6">
         
                <asp:Button runat="server"   CssClass="btn btn-outline-success" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" />
			<asp:Button runat="server" ID="btnExportar"   CssClass="btn btn-outline-success"
                Text="Exportar Excel" onclick="btnExportar_Click" />
                       </div>
                 </div>
		<br />
                 <div class="border border-success" style=" width:95%;   background-color: #2d282808;margin:0px auto  " >
                        <asp:GridView ID="gvFacturas"  DataKeyNames="Guid,IdCliente,idventa" runat="server" AutoGenerateColumns="False" GridLines="None" 
                          ShowHeaderWhenEmpty="True" Width="100%"  AlternatingRowStyle-HorizontalAlign="Left"
                             RowStyle-HorizontalAlign="Left"  onrowcommand="gvFacturas_RowCommand" onpageindexchanging="gvFacturas_PageIndexChanging" 
		  OnRowDataBound  ="gvFacturas_RowDataBound" OnPreRender="gvFacturas_PreRender" PageSize="6"
                            CssClass="table table-hover table-striped grdViewTable"    >
                                 <rowstyle Height="6px" /><alternatingrowstyle  Height="6px"/>
                          
                     <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>

		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="GreenYellow" Font-Bold="True" />
		<Columns>
			<asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Guid" />
			<asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
			<asp:BoundField HeaderText="Empleado" DataField="NombreCompleto" />
            <asp:BoundField HeaderText="RFC" DataField="Rfc" />
			<asp:BoundField HeaderText="SubTotal" DataField="Subtotal" 
                DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" >
<ItemStyle HorizontalAlign="left"></ItemStyle>
            </asp:BoundField>
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" 
                ItemStyle-HorizontalAlign="left" >
<ItemStyle HorizontalAlign="left"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="Usuario" DataField="Usuario"/>
			<asp:BoundField HeaderText="Status" DataField="StatusFactura"/>
			<asp:ButtonField ButtonType="Link" Text="XML" CommandName="DescargarXml" />
			<asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
			<asp:ButtonField ButtonType="Link" Text="Enviar Email" CommandName="EnviarEmail" />
            <asp:TemplateField  HeaderText="Cancelar">
                <ItemTemplate>
                    <asp:Button runat="server" Text='<%# (short)Eval("Cancelado") == 1 ? "Acuse Cancelacion" : "Cancelar"  %>'  CommandName='<%# (short)Eval("Cancelado") == 1 ? "Acuse" : "Cancelar"  %>' ID="btnCancelarf" CommandArgument='<%#Eval("idventa") %>' />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancelarf" ConfirmText="¿Cancelar Documento?" Enabled='<%# (short)Eval("Cancelado") != 1  %>' />
                </ItemTemplate>
            </asp:TemplateField>
		</Columns>
	</asp:GridView> 
   </div>

	<asp:GridView ID="gvFacturaCustumer" Visible="False" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="folio" HeaderText="Folio" />
            <asp:BoundField DataField="Guid" HeaderText="Folio Fiscal" />
            <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
            <asp:BoundField DataField="Rfc" HeaderText="RFC" />
            <asp:BoundField DataField="PorcentajeIva" DataFormatString="{0:F2}" 
                HeaderText="% I.V.A." />
            <asp:BoundField DataField="Subtotal" DataFormatString="{0:C}" 
                HeaderText="SubTotal" />
            <asp:BoundField DataField="IVA" DataFormatString="{0:C}" HeaderText="I.V.A." />
            <asp:BoundField DataField="Total" DataFormatString="{0:C}" HeaderText="Total" />
            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
            <asp:BoundField DataField="StatusFactura" HeaderText="Status" />
        </Columns>
    </asp:GridView>
    <br />
</div>
         </div>

	<asp:ModalPopupExtender runat="server" ID="mpePagar" TargetControlID="btnpagarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagar" PopupControlID="pnlPagar" />
	<asp:Panel runat="server" ID="pnlPagar" CssClass="ventana" BackColor="#DDDDDD" Width="600px" style="text-align: center;">
		<h1>Pagar Factura</h1>
		<asp:Label runat="server" ID="lblIdventa" Visible="False" />
		<asp:Label runat="server" ID="lblErrorPago" ForeColor="Red" />
	    No. de Folio: <asp:Label runat="server" ID="lblFolioPago" />
		<p class="auto-style1">
			Fecha Pago: <asp:TextBox runat="server" ID="txtFechaPago" CssClass="form-control2" Text='<%# ServicioLocalContract.AzureUtils.ConvertDateTimeFromUTCToMx(DateTime.UtcNow) %>' />
			<asp:CompareValidator runat="server" ID="cvFechaPago" ControlToValidate="txtFechaPago" Display="Dynamic" 
			 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Pago" />
			<asp:CalendarExtender runat="server" ID="ceFechaPago" TargetControlID="txtFechaPago" PopupButtonID="txtFechaPago" Format="dd/MM/yyyy" />
			<asp:RequiredFieldValidator runat="server" ID="rfvFechaPago" ErrorMessage="* Requerido" ControlToValidate="txtFechaPago"
			 ValidationGroup="Pago" Display="Dynamic"/>
		</p>
		<p>
			Referencia: <asp:TextBox runat="server" ID="txtReferenciaPago" Width="300px" CssClass="form-control2" />
			<asp:RequiredFieldValidator runat="server" ID="rfvReferenciaPago" ErrorMessage="* Requerido"
			 ControlToValidate="txtReferenciaPago" ValidationGroup="Pago" Display="Dynamic"/>
		</p>
		<asp:Button runat="server" ID="btnPagar" class="btn btn-primary" Text="Pagar" onclick="btnPagar_Click" ValidationGroup="Pago" />&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCerrarPagar" class="btn btn-primary" Text="Cancelar" 
            onclick="btnCerrarPagar_Click" />
		<br /><br />
	</asp:Panel>

	<asp:ModalPopupExtender runat="server" ID="mpeEmail" TargetControlID="btnEmailDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarEmail" PopupControlID="pnlEmail" />
	<asp:Panel runat="server" ID="pnlEmail" style="text-align: center; margin-right: 6px;" CssClass="ventana" Width="598px" BackColor="#DDDDDD">
		<h1>Direcciones de envio</h1>
		<asp:Label runat="server" ID="lblGuid"  Visible="False" />
		<p>
			Se enviara a: <asp:Label runat="server" ID="lblEmailCliente" />
		</p>
		<p>
			Correos adicionales:
			<asp:TextBox runat="server" ID="txtEmails" Width="250px" CssClass="form-control2"/>&nbsp;&nbsp;&nbsp;
			<span style="font-size: 8pt;">Separados por comas</span>
		</p>
		<br />
		<asp:Button runat="server" ID="btnEnviarEmail" Text="Enviar" class="btn btn-primary" onclick="btnEnviarMail_Click" />&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCerrarEmail"  class="btn btn-primary" Text="Cancelar" />
	</asp:Panel>
	<asp:Button runat="server" ID="btnEmailDummy" style="display: none;"/>
	<asp:Button runat="server" ID="btnPagarDummy" style="display: none;"/>
</asp:Content>