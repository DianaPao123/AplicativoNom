<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccionesOTitulos.ascx.cs" Inherits="SitioNominaLocal.controles.AccionesOTitulos" %>

        <div class = "row">
                                <div class = "form-group col-lg-3">
                                 <asp:Label ID="lblValorMercado" runat="server" class="control-label" Text="* Valor Mercado"></asp:Label>
                           <asp:TextBox ID="txtValorMercado" Display="Dynamic" CssClass="form-control" ValidationGroup="AgregarPersepcion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="AgregarPersepcion" 
                        ControlToValidate="txtValorMercado" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator>
                   
                               <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtValorMercado" BehaviorID="_content_FilteredTextBoxExtender2" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" Display="Dynamic"
    ControlToValidate="txtValorMercado" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarPersepcion" ForeColor="#FF3300"/>
  
                         </div>
                                <div class = "form-group col-lg-3">
                                 <asp:Label ID="lblPrecioAlOtorgarse" runat="server" class="control-label" Text="* Precio Al Otorgarse"></asp:Label>
                         <asp:TextBox ID="txtPrecioAlOtorgarse" Display="Dynamic" CssClass="form-control"
                             ValidationGroup="AgregarPersepcion" runat="server" style="margin-left: 0px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="AgregarPersepcion" ControlToValidate="txtPrecioAlOtorgarse" ErrorMessage="Campo Obligatorio" runat="server" ForeColor="#FF3300" ></asp:RequiredFieldValidator>
                    
                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
    ValidChars="." TargetControlID="txtPrecioAlOtorgarse" BehaviorID="_content_FilteredTextBoxExtender1" />
    <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic"
    ControlToValidate="txtPrecioAlOtorgarse" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?" ValidationGroup="AgregarPersepcion" ForeColor="#FF3300"/>
  </div>
            </div>