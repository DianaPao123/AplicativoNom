<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PercepcionesTotales.ascx.cs" Inherits="SitioNominaLocal.controles.PercepcionesTotales" %>


 <div class = "row">
     <div class = "form-group col-lg-3">
         <asp:Label ID="lblTotalSueldos" runat="server" class="control-label" Text="TotalSueldos"></asp:Label>
              <asp:TextBox ID="txtTotalSueldos" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalSueldos" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalSueldos" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  </div>
      <div class = "form-group col-lg-3">
         <asp:Label ID="Label1" runat="server" class="control-label" Text="TotalSeparacionIndemnizacion"></asp:Label>
               <asp:TextBox ID="txtTotalSeparacionIndemnizacion" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            
                               <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalSeparacionIndemnizacion" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalSeparacionIndemnizacion" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  </div>
      <div class = "form-group col-lg-3">
         <asp:Label ID="Label2" runat="server" class="control-label" Text="TotalJubilacionPensionRetiro"></asp:Label>
          <asp:TextBox ID="txtTotalJubilacionPensionRetiro" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            
                               <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalJubilacionPensionRetiro" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalJubilacionPensionRetiro" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  </div>
     <div class = "form-group col-lg-3">
         <asp:Label ID="Label3" runat="server" class="control-label" Text="TotalGravado"></asp:Label>
              <asp:TextBox ID="txtTotalGravado" runat="server"  CssClass="form-control" Enabled="False"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalGravado" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalGravado" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="Requerido" Display="Dynamic"
              ControlToValidate="txtTotalGravado" ValidationGroup="CrearFactura" ForeColor="#FF3300"></asp:RequiredFieldValidator>
  </div>
     </div>
 <div class = "row">
     <div class = "form-group col-lg-3">
         <asp:Label ID="Label4" runat="server" class="control-label" Text="TotalExento"></asp:Label>
                   <asp:TextBox ID="txtTotalExento" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
            runat="server" FilterType="Numbers, Custom"
    ValidChars="." TargetControlID="txtTotalExento" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" Display="Dynamic"
    ControlToValidate="txtTotalExento" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="CrearFactura" ForeColor="#FF3300"/>
  
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="Requerido" Display="Dynamic"
              ControlToValidate="txtTotalExento" ValidationGroup="CrearFactura" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>
     </div>