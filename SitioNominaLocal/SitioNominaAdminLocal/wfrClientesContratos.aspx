<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrClientesContratos.aspx.cs" Inherits="SitioNominaAdmin.wfrClientesContratos" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
	.mpeBack
	{
		background-color: Gray;
		filter: alpha(opacity=70);
		opacity: 0.7;
	}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server"></asp:Label></h1>
    <asp:HiddenField runat="server" ID="hidIdCliente"/>
    <div align="center">
        <asp:GridView ID="GvContratos" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="FechaContrato" HeaderText="Fecha" />
                <asp:BoundField DataField="TipoContrato" HeaderText="Tipo de Contrato" />
                <asp:BoundField DataField="Timbres" HeaderText="Timbres" />
                 <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnNuevoContrato" runat="server" Text="Nuevo Contrato" 
            onclick="btnNuevoContrato_Click"/>
    </div>
    <asp:ModalPopupExtender runat="server" ID="mpeNuevoContrato" TargetControlID="btnConceptoDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCancelar" PopupControlID="pnlBuscarConcepto" />
	<asp:Panel runat="server" ID="pnlBuscarConcepto" style="text-align: center;" Width="800px" BackColor="White">
		<h1><asp:Label runat="server" ID="lblConcepto" ></asp:Label></h1>
		 <table width="600px">
		<tr>
			<td>Fecha:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtFecha" />
                <asp:RequiredFieldValidator runat="server" ID="cmp1" ControlToValidate="txtFecha" Display="Dynamic" 
				 ErrorMessage="* Campo Requerido" ValidationGroup="contrato" />
				<asp:CompareValidator runat="server" ID="cvFechaFinal" ControlToValidate="txtFecha" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" ValidationGroup="contrato" />
				<asp:CalendarExtender runat="server" ID="ceFechaFinal" Animated="True" PopupButtonID="txtFecha" TargetControlID="txtFecha" Format="dd/MM/yyyy" />
			</td>
		</tr>
		<tr>
			<td>Tipo Contrato</td>
			<td align="left">
				<asp:DropDownList runat="server" ID="ddlTipoContrato">
				    <asp:ListItem runat="server" Text="Pre-Pago" Value="Pre-Pago"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="Post-Pago" Value="Post-Pago"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>Timbres:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtTimbres"  Width="400px"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ID="rfvTimbres" ErrorMessage="* Requerido" Display="Dynamic"
				 ControlToValidate="txtTimbres" ValidationGroup="contrato" />
			</td>
		</tr>
		<tr>
			<td>Observaciones:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtObservaciones"  Width="400px" TextMode="MultiLine" ></asp:TextBox>
				
			</td>
		</tr>
        <tr>
			<td>Distribuidor</td>
			<td align="left">
				<asp:DropDownList runat="server" ID="ddlDistribuidor">
				    <asp:ListItem runat="server" Text="Pre-Pago" Value="Pre-Pago"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="Post-Pago" Value="Post-Pago"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
        <tr>
			<td>Productos:</td>
			<td align="left">
				<asp:DropDownList ID="ddlProductos" runat="server">
                    <asp:ListItem runat="server" Text="Tera" Value="Tera"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="Giga" Value="Giga"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="Mega" Value="Mega"></asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
        <tr>
			<td>Costo:</td>
			<td align="left">
				<asp:DropDownList ID="ddlPrecios" runat="server">
                    <asp:ListItem runat="server" Text="$500.00" Value="500"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="$1,500.00" Value="1500"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="$2,000.00" Value="2000"></asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
        <tr>
			<td>Porcentaje:</td>
			<td align="left">
                <asp:DropDownList ID="ddlPorcentaje" runat="server">
                    <asp:ListItem runat="server" Text="25%" Value="25"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="30%" Value="30"></asp:ListItem>
                    <asp:ListItem runat="server"  Text="35%" Value="35"></asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
	</table>
	<div align="right">
		<asp:Button runat="server" ID="btnGuardar" Text="Guardar"  
            ValidationGroup="contrato" onclick="btnGuardar_Click"
			 />&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCancelar" Text="Cancelar" 
			CausesValidation="False" onclick="btnCancelar_Click" />
	</div>

		<br />
		
	</asp:Panel>
	<asp:Button runat="server" ID="btnConceptoDummy" style="display: none;"/>
</asp:Content>
