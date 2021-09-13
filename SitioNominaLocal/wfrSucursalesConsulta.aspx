<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrSucursalesConsulta.aspx.cs" Inherits="GafLookPaid.wfrSucursalesConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sucursales de la empresa: </h1><h5><asp:Label ID="lblEmpresa" runat="server"></asp:Label></h5>
    <br/>
    <br/>
     <asp:HiddenField ID="hdIdempresa" runat="server"/>
    <asp:GridView runat="server" ID="gvSucursales" AutoGenerateColumns="False" onrowcommand="gvSucursales_RowCommand"
     DataKeyNames="IdSucursal" AllowPaging="True" onpageindexchanging="gvSucursales_PageIndexChanging">
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Lugar de Expedición" DataField="LugarExpedicion" />
            <asp:ButtonField Text="Editar" CommandName="EditarSucursal" />
        </Columns>
    </asp:GridView>
    <div align="right">
        <asp:Button runat="server" ID="btnNuevaSucursal" Text="Nueva Sucursal" onclick="btnNuevaSucursal_Click"  />
    </div>
</asp:Content>
