<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEditaCentro.aspx.cs" Inherits="GafLookPaid.wfrEditaCentro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
          <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="Content/bootstrap.css" rel="stylesheet" />
      <script src="Scripts/chosen.jquery.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
      <div  class = "card mt-2">   
            <div class="card-header">
               <h3>Empresa</h3>
            </div>
            <div class ="card-body" >


    <asp:UpdatePanel ID="upd1" runat="server">
    <ContentTemplate>
     <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <asp:HiddenField runat="server" ID="hfEmpresa"/>

          <div class = "row"> 
                <div class="col-lg-12 " >
                        <asp:Label  class="control-label" ID="Label8" runat="server" Text="* Campos obligatorios"></asp:Label>
               </div>
    </div>
          <div class = "form-group row"> 
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label1" runat="server" Text="* Nombre"></asp:Label>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtRazonSocial" ErrorMessage="Campo Requerido" style="color: #FF0000; text-align: center"></asp:RequiredFieldValidator>
      
            </div>
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label2" runat="server" Text="Registro Patronal"></asp:Label>
                      <asp:TextBox runat="server" ID="txtRegistroPatronal"  CssClass="form-control"/></td>
                </div>
              </div>
          <div class = "row"> 
                <div class="col-lg-12 " >
                <asp:CheckBox ID="chkDireccionEmpresa" runat="server" AutoPostBack="True"
                    Text="El Domicilio es el mismo que la empresa" OnCheckedChanged="CheckBox1_OnCheckedChanged" style="color: #0000FF; text-align: left;"
                    />
           </div>
              </div>
         <div class = "form-group row"> 
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label3" runat="server" Text="* Calle"></asp:Label>
                      <asp:TextBox runat="server" ID="txtCalle" Width="200px" CssClass="form-control" ClientIDMode="Static" />
                </div>
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label4" runat="server" Text="CP"></asp:Label>
                          <asp:TextBox ID="txtCP" runat="server"  CssClass="form-control" MaxLength="5" ></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="filterInt" runat="server" FilterType="Numbers"
              TargetControlID="txtCP" ></asp:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCP" ErrorMessage="Campo Requerido" style="color: #FF0000; text-align: center"></asp:RequiredFieldValidator>
            </div>
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label5" runat="server" Text="* Numero Exterior"></asp:Label>
                       <asp:TextBox runat="server" ID="txtNoExt"  CssClass="form-control" ClientIDMode="Static" />
              </div>
                <div class="col-lg-3 " >
                        <asp:Label  class="control-label" ID="Label6" runat="server" Text="Numero Interior"></asp:Label>
             <asp:TextBox runat="server" ID="txtNoInt"  CssClass="form-control" ClientIDMode="Static" />
            </div>
             </div>
         <div class = "form-group row"> 
                <div class="col-lg-3 " >
                  <asp:Label  class="control-label" ID="Label7" runat="server" Text="Colonia"></asp:Label>
              <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control"
                    ClientIDMode="Static" />
                    </div>
                <div class="col-lg-3 " >
                  <asp:Label  class="control-label" ID="Label9" runat="server" Text="Alcaldia"></asp:Label>
             <asp:TextBox runat="server" ID="txtMunicipio"  CssClass="form-control"
                    ClientIDMode="Static" />
                    </div>
                <div class="col-lg-3 " >
                  <asp:Label  class="control-label" ID="Label10" runat="server" Text="Estado"></asp:Label>
                  <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control"
                    ClientIDMode="Static" />
                    </div>
           <div class="col-lg-3 " >
                  <asp:Label  class="control-label" ID="Label11" runat="server" Text="* País"></asp:Label>
              <asp:TextBox runat="server" ID="txtPais" CssClass="form-control"  ClientIDMode="Static" />
               </div>
             </div>
        <div class = "form-group row"> 
                <div class="col-lg-3 " >
                  <asp:Label  class="control-label" ID="Label12" runat="server" Text="* Riesgo de trabajo"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlRiesgoPuesto"  CssClass="form-control">
                <asp:ListItem Text="Clase I" Value="1"></asp:ListItem>
                <asp:ListItem Text="Clase II" Value="2"></asp:ListItem>
                <asp:ListItem Text="Clase III" Value="3"></asp:ListItem>
                <asp:ListItem Text="Clase IV" Value="4"></asp:ListItem>
                <asp:ListItem Text="Clase V" Value="5"></asp:ListItem>
            </asp:DropDownList>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="ddlRiesgoPuesto" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
        </div>
</div>        


    <br />
    </ContentTemplate>
    </asp:UpdatePanel>
   
      <div class = "form-group row"> 
                <div class="col-lg-6" >
        <asp:Button runat="server" ID="btnSave" Text="Guardar" CssClass="btn btn-outline-success"  onclick="btnSave_Click" />&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="btn btn-outline-success"
            onclick="btnCancel_Click" CausesValidation="False" />
    </div>

                </div>
          </div>
    </div>

</asp:Content>
