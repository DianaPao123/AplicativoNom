<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WfrColaboradoresConsulta.aspx.cs" Inherits="GafLookPaid.WfrColaboradoresConsulta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <%--  <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />--%>
          <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Consulta de EMPLEADOS</h3>
            </div>
            <div class ="card-body" >
                      <div class = "row form-line"> 
                <div class="col-lg-4 " >
                <asp:Label  class="control-label" ID="Label8" runat="server" Text="Empresa"></asp:Label>
             <asp:DropDownList runat="server" ID="ddlEmpresa" AppendDataBoundItems="True" DataTextField="RazonSocial"
         DataValueField="IdEmpresa" CssClass="form-control" Enabled="False" />
                   </div>
                <div class="col-lg-4 " >
                <asp:Label  class="control-label" ID="Label1" runat="server" Text="Nombre"></asp:Label>
            <asp:TextBox runat="server" ID="txtBusqueda"  CssClass="form-control" />&nbsp;
        </div>
                </div>
                         <div class = "row form-line"> 
                <div class="col-lg-6" >
           <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-outline-success" Text="Buscar" onclick="btnBuscar_Click"/>
     
            <asp:Button ID="Button1" runat="server" Text="Nuevo Empleado" CssClass="btn btn-outline-success"
            OnClick="btnNuevoCliente_Click"/> 
    </div>
        </div>
                         <div class = "row form-line"> 
                <div class="col-lg-12" >
           
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    </div>
                </div>                 
    <br />
                  <div class="border border-success" style=" width:95%;   background-color: #2d282808;margin:0px auto  " >
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" GridLines="None" 
                          ShowHeaderWhenEmpty="True" Width="100%"  AlternatingRowStyle-HorizontalAlign="Left"
                             RowStyle-HorizontalAlign="Left"
                            onrowcommand="gvClientes_RowCommand"   DataKeyNames="idCliente"
                            CssClass="table table-hover table-striped grdViewTable"
                              onpageindexchanging="gvClientes_PageIndexChanging" onselectedindexchanged="gvClientes_SelectedIndexChanged" >
                                 <rowstyle Height="6px" /><alternatingrowstyle  Height="6px"/>
                            
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
    </asp:GridView><br />
    

            
        </div>
   </div>
        </div>
</asp:Content>