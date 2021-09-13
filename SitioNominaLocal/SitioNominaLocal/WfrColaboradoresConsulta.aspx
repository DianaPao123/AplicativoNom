<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WfrColaboradoresConsulta.aspx.cs" Inherits="GafLookPaid.WfrColaboradoresConsulta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Consulta de EMPLEADOS</h1>
    <p>
        Empresa: <asp:DropDownList runat="server" ID="ddlEmpresa" AppendDataBoundItems="True" DataTextField="RazonSocial"
         DataValueField="IdEmpresa" Enabled="False" />
    </p>
    <p>
        Nombre: <asp:TextBox runat="server" ID="txtBusqueda" Width="400px" />&nbsp;
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" onclick="btnBuscar_Click"/>
    </p>
    <asp:GridView runat="server" ID="gvClientes" AutoGenerateColumns="False" onrowcommand="gvClientes_RowCommand"
     DataKeyNames="idCliente" AllowPaging="True" 
        onpageindexchanging="gvClientes_PageIndexChanging" 
        onselectedindexchanged="gvClientes_SelectedIndexChanged" >
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="RFC" DataField="RFC" />
            <asp:BoundField HeaderText="Nombre" DataField="RazonSocial" />
            <asp:BoundField HeaderText="Apellido Paterno" DataField="ApellidoPaterno" />
            <asp:BoundField HeaderText="Apellido Materno" DataField="ApellidoMaterno" />
            <asp:ButtonField Text="Editar" ButtonType="Link" CommandName="EditarCliente"/>
            <asp:TemplateField>
             <ItemTemplate>
                <asp:LinkButton Text="Eliminar" runat="server" ID="btnEliminarCliente" CommandArgument='<%# Eval("idCliente") %>' CommandName="Eliminar"/>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnEliminarCliente" ConfirmText="Confirma que deseas eliminar el registro"/>
             </ItemTemplate>
            </asp:TemplateField>
            </Columns>
    </asp:GridView>
    <p><asp:Button ID="Button1" runat="server" Text="Nuevo Empleado" 
            OnClick="btnNuevoCliente_Click"/> </p>
     <p>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    </p>
</asp:Content>