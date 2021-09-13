<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrUsuarios.aspx.cs" Inherits="GafLookPaid.wfrUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Usuario</h3>
            </div>
            <div class ="card-body" >
              <div class = "row form-group"> 
                  <div class="col-lg-12 " style="color:red;" >
                            <asp:Label ID="lblErrorMessage" class="control-label" runat="server" ForeColor="Red" Font-Bold="true" style=" font-size: x-small; text-align: left; font-variant: small-caps;" Width="250px"></asp:Label>
            </div>
            </div>
            <div class = "row">
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label2" runat="server" class="control-label" Text="* Empresa"></asp:Label>
                               <asp:DropDownList runat="server" ID="ddlEmpresas" AppendDataBoundItems="True" CssClass="form-control"
                                   DataValueField="idEmpresa"      DataTextField="RazonSocial"/>
            </div>
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label1" runat="server" class="control-label" Text="* Email (inicio de sesión)"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" Display="Dynamic"
                    ValidationGroup="Usu" ErrorMessage="* Requerido" ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" 
                    ErrorMessage="* Direccion invalida" ForeColor="Red"
                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" />
            </div>
                </div>
            <div class = "row">
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label3" runat="server" class="control-label" Text="* Nombre Completo"></asp:Label>
                <asp:TextBox runat="server" ID="txtNombreCompleto" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Usu"
                    ControlToValidate="txtNombreCompleto" Display="Dynamic" ErrorMessage="* Requerido"  ForeColor="Red"/>
              </div>
         <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label4" runat="server" class="control-label" Text="* Iniciales"></asp:Label>
                <asp:TextBox runat="server" ID="txtIniciales"  CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ValidationGroup="Usu"
                    ControlToValidate="txtIniciales" Display="Dynamic" ErrorMessage="* Requerido" ForeColor="Red"/>
          </div>
                </div>
            <div class = "row">
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label5" runat="server" class="control-label" Text="* Password"></asp:Label>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword"
                    ErrorMessage="* Requerido" Display="Dynamic" ValidationGroup="Usu" ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ID="revPassword" ControlToValidate="txtPassword"
                    Display="Dynamic" ErrorMessage="* El password no cumple con las politicas de seguridad"
                    ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%+-_]).{8,20})" ForeColor="Red" />
                <asp:CompareValidator runat="server" ID="cvPassword" ControlToValidate="txtPassword" Display="Dynamic"
                    ControlToCompare="txtConfirmarPassword" ErrorMessage="* La confirmacion y el password no coinciden" ForeColor="Red"
                    Operator="Equal"  />
          </div>
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label6" runat="server" class="control-label" Text="* Confirmar Password"></asp:Label>
       <asp:TextBox runat="server" ID="txtConfirmarPassword" TextMode="Password" CssClass="form-control"/>
        </div>
                </div>
         <div class = "row">
                                <div class = "form-group col-lg-4">
                                 <asp:Label ID="Label7" runat="server" class="control-label" Text="* Perfil"></asp:Label>
             <asp:DropDownList runat="server" ID="ddlPerfiles" AppendDataBoundItems="True" CssClass="form-control">
                <asp:ListItem>Nomina</asp:ListItem>
                <asp:ListItem>Administrador</asp:ListItem>
                </asp:DropDownList>
          </div>
             </div>

    <div align="right">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click"  ValidationGroup="Usu"
            CssClass="btn btn-outline-success" />&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar"   PostBackUrl="wfrUsuariosConsulta.aspx"  CssClass="btn btn-outline-success" />
    </div>

                </div>
         </div>
</asp:Content>
