<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrPaquetes.aspx.cs" Inherits="SitioNominaAdmin.wfrPaquetes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Paquetes de Venta</h1>
   <br/>
   <br/>
   <br/>
    <asp:GridView runat="server" ID="gvPaquetes" AutoGenerateColumns="False" 
     DataKeyNames="IdPaquete" >
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:ButtonField Text="Editar" ButtonType="Link" CommandName="EditarSistema"/>
        </Columns>
    </asp:GridView>
    <div align="right">
        <asp:Button runat="server" ID="btnNuevoPaquete" Text="Nuevo Paquete" />
    </div>
</asp:Content>
