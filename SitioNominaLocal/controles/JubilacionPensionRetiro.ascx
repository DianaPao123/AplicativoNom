<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JubilacionPensionRetiro.ascx.cs" Inherits="SitioNominaLocal.controles.JubilacionPensionRetiro" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<div class = "row">
     <div class = "form-group col-lg-3">
         <asp:Label ID="lblTotalSueldos" runat="server" class="control-label" Text="IngresoAcumulable"></asp:Label>
                <asp:TextBox ID="txtIngresoAcumulable" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:FilteredTextBoxExtender ID="txtIngresoAcumulable_FilteredTextBoxExtender" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtIngresoAcumulable" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" Display="Dynamic"
    ControlToValidate="txtIngresoAcumulable" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="Requerido" Display="Dynamic"
              ControlToValidate="txtIngresoAcumulable" ValidationGroup="CrearFactura" ForeColor="#FF3300"></asp:RequiredFieldValidator>
     </div>
     <div class = "form-group col-lg-3">
         <asp:Label ID="Label1" runat="server" class="control-label" Text="IngresoNoAcumulable"></asp:Label>
                   <asp:TextBox ID="txtIngresoNoAcumulable" runat="server"  CssClass="form-control"></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtIngresoNoAcumulable" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic"
    ControlToValidate="txtIngresoNoAcumulable" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="Requerido" Display="Dynamic"
              ControlToValidate="txtIngresoNoAcumulable" ValidationGroup="CrearFactura" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                
       </div>
    <div class = "form-group col-lg-3">
         <asp:Label ID="Label2" runat="server" class="control-label" Text="TotalUnaExhibicion"></asp:Label>
                            <asp:TextBox ID="txtTotalUnaExhibicion" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalUnaExhibicion" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalUnaExhibicion" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  </div>
    </div>
<div class = "row">
     <div class = "form-group col-lg-3">
         <asp:Label ID="Label3" runat="server" class="control-label" Text="TotalParcialidad"></asp:Label>
               <asp:TextBox ID="txtTotalParcialidad" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalParcialidad" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalParcialidad" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
  </div>
     <div class = "form-group col-lg-3">
         <asp:Label ID="Label4" runat="server" class="control-label" Text="MontoDiario"></asp:Label>
                   <asp:TextBox ID="txtMontoDiario" runat="server" CssClass="form-control" ></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtMontoDiario" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" Display="Dynamic"
    ControlToValidate="txtMontoDiario" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
          </div>
    </div>
<hr width="100%" align="left"> 