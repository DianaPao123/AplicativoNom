<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrCentrosTrabajo.aspx.cs" Inherits="GafLookPaid.wfrCentrosTrabajo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />--%>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h3>Centros de trabajo de la empresa: </h3><h5><asp:Label ID="lblEmpresa" runat="server"></asp:Label></h5>
    <br/>
    <br/>
     <asp:HiddenField ID="hdIdempresa" runat="server"/>
            <div class="border border-success" style=" width:95%;   background-color: #2d282808;margin:0px auto  " >
                        <asp:GridView ID="gvCentros" runat="server" AutoGenerateColumns="False" GridLines="None" 
                          ShowHeaderWhenEmpty="True" Width="100%"  AlternatingRowStyle-HorizontalAlign="Left"
                             RowStyle-HorizontalAlign="Left"
                            onrowcommand="gvSucursales_OnRowCommand"   DataKeyNames="IdCentroTrabajo"
                            CssClass="table table-hover table-striped grdViewTable"
                              onpageindexchanging="gvSucursales_OnPageIndexChanging">
                                 <rowstyle Height="6px" /><alternatingrowstyle  Height="6px"/>
                            
                     <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" HeaderStyle-HorizontalAlign="Center"  >
                 <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
             </asp:BoundField>          
            <asp:BoundField HeaderText="Registro Patronal" DataField="RegistroPatronal"  >
                  <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
            <asp:ButtonField Text="Editar" CommandName="EditarCentro" />
                 
           </Columns>
    </asp:GridView>
               </div>
    <br />
    <br />
    <div align="center">
        <asp:Button runat="server" ID="btnNuevaSucursal" Text="Nuevo Centro de Trabajo" CssClass="btn btn-outline-success" 
            onclick="btnNuevaSucursal_Click" />
    </div>
</asp:Content>
