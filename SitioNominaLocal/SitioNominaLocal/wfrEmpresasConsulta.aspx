<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresasConsulta.aspx.cs" Inherits="GafLookPaid.wfrEmpresasConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Empresas</h1>
   
    <asp:GridView runat="server" ID="gvEmpresas" AutoGenerateColumns="False" onrowcommand="gvEmpresas_RowCommand"
     DataKeyNames="IdEmpresa" AllowPaging="True" onpageindexchanging="gvEmpresas_PageIndexChanging">
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="RFC" DataField="RFC" />
            <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
            <asp:ButtonField Text="Editar" CommandName="EditarEmpresa" />
            <asp:ButtonField Text="Sucursales" CommandName="EditarSucursal" 
                Visible="False" />
            <asp:ButtonField Text="Centros de Trabajo" CommandName="CentrosTrabajo" />
        </Columns>
    </asp:GridView>
    <div align="right">
        <asp:Button runat="server" ID="btnNuevaEmpresa" Text="Nueva Empresa" onclick="btnNuevaEmpresa_Click" Visible="False" />
    </div>
</asp:Content>
