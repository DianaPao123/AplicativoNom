<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresasConsulta.aspx.cs" Inherits="GafLookPaid.wfrEmpresasConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Empresas</h3>
            </div>
            <div class ="card-body" >
    
           <div class="border border-success" style=" width:95%;   background-color: #2d282808;margin:0px auto  " >
                        <asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="False" GridLines="None" 
                          ShowHeaderWhenEmpty="True" Width="100%"  AlternatingRowStyle-HorizontalAlign="Center"
                            onrowcommand="gvEmpresas_RowCommand" DataKeyNames="IdEmpresa" 
                            CssClass="table table-hover table-striped grdViewTable"
                             onpageindexchanging="gvEmpresas_PageIndexChanging">
                            <rowstyle Height="6px" /><alternatingrowstyle  Height="6px"/>
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
               </div>

    <div align="right">
        <asp:Button runat="server" ID="btnNuevaEmpresa" Text="Nueva Empresa" onclick="btnNuevaEmpresa_Click" CssClass="btn btn-outline-success"  Visible="False" />
    </div>
                </div>
        </div>
</asp:Content>
