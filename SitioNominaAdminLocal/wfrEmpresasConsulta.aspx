<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrEmpresasConsulta.aspx.cs" Inherits="SitioNominaAdmin.wfrEmpresasConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Empresas</h1>
   <div>
       Cliente:
       <asp:DropDownList runat="server" ID="ddlClientes" AutoPostBack="True" 
           onselectedindexchanged="ddlClientes_SelectedIndexChanged"></asp:DropDownList>
   </div>
    <asp:GridView runat="server" ID="gvEmpresas" AutoGenerateColumns="False" onrowcommand="gvEmpresas_RowCommand"
     DataKeyNames="IdEmpresa" AllowPaging="True" onpageindexchanging="gvEmpresas_PageIndexChanging">
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="RFC" DataField="RFC" />
            <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
            <asp:ButtonField Text="Editar" CommandName="EditarEmpresa" />
        </Columns>
    </asp:GridView>
</asp:Content>
