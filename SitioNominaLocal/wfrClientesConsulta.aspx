<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrClientesConsulta.aspx.cs" Inherits="SitioNominaAdmin.wfrClientesConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Consulta de Clientes</h1>
    <p>
        RFC o Razón Social: <asp:TextBox runat="server" ID="txtBusqueda" Width="400px" />&nbsp;
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
            onclick="btnBuscar_Click" />
    </p>
    <br />
    <asp:GridView runat="server" ID="gvClientes" AutoGenerateColumns="False" 
     DataKeyNames="IdSistema" onrowcommand="gvClientes_RowCommand" 
        onrowdatabound="gvClientes_RowDataBound" >
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="RFC" DataField="Rfc" />
            <asp:BoundField HeaderText="Razon Social" DataField="RazonSocial" />
            <%--<asp:BoundField DataField="TimbresContratados" HeaderText="Timbres Contratados" >
            <FooterStyle HorizontalAlign="Center" />
            <HeaderStyle Width="10px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="TimbresConsumidos" HeaderText="Timbres Consumidos" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
            </asp:BoundField>
            <asp:ButtonField Text="Editar" ButtonType="Link" CommandName="EditarSistema"/>
            <%--<asp:ButtonField Text="Contratos" ButtonType="Link" CommandName="Contratos"/>--%>
            <asp:ButtonField CommandName="Bloqueo" Text="Bloquear" />
        </Columns>
    </asp:GridView>
    <div align="right">
        <asp:Button runat="server" ID="btnNuevoCliente" Text="Nuevo Cliente" 
            onclick="btnNuevoCliente_Click" />
    </div>
</asp:Content>
