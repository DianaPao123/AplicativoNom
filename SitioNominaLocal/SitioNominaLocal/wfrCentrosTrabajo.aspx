<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrCentrosTrabajo.aspx.cs" Inherits="GafLookPaid.wfrCentrosTrabajo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Centros de trabajo de la empresa: </h1><h5><asp:Label ID="lblEmpresa" runat="server"></asp:Label></h5>
    <br/>
    <br/>
     <asp:HiddenField ID="hdIdempresa" runat="server"/>
    <asp:GridView runat="server" ID="gvCentros" AutoGenerateColumns="False" onrowcommand="gvSucursales_OnRowCommand"
     DataKeyNames="IdCentroTrabajo" AllowPaging="True" onpageindexchanging="gvSucursales_OnPageIndexChanging">
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Registro Patronal" DataField="RegistroPatronal" />
            
            <asp:ButtonField Text="Editar" CommandName="EditarCentro" />
        </Columns>
    </asp:GridView>
    <div align="right">
        <asp:Button runat="server" ID="btnNuevaSucursal" Text="Nuevo Centro de Trabajo" 
            onclick="btnNuevaSucursal_Click" />
    </div>
</asp:Content>
