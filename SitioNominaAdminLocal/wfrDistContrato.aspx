<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrDistContrato.aspx.cs" Inherits="SitioNominaAdmin.wfrDistContrato" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
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
			<td>Costo:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtCosto"  Width="400px"></asp:TextBox>
			</td>
		</tr>
        <tr>
			<td>Porcentaje:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtPorcentaje"  Width="400px"></asp:TextBox>
			</td>
		</tr>
        <tr>
			<td>
                 
			</td>
			<td>
		<asp:Button ID="btnRegresar" runat="server" CausesValidation="False" 
            Text="Regresar" onclick="btnRegresar_Click" />
		&nbsp;&nbsp;&nbsp;&nbsp;
			    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                    onclick="btnGuardar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                    onclick="btnCancelar_Click" />
			</td>
		</tr>
	</table>
    </center>
</asp:Content>
