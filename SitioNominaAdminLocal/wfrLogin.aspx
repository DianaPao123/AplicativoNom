<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrLogin.aspx.cs" Inherits="SitioNominaAdmin.wfrLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
    <div align="center">
       
        <h1 class="auto-style1">Acceso</h1>
        <p>
            <asp:Login  class="login" ID="logMain" runat="server"  
                LoginButtonText="Iniciar Sesión" LoginButtonType="Button" LoginButtonStyle-CssClass="btn btn-primary" BorderWidth="0px"
                TextLayout="TextOnTop" Width="353px"  Height="133px"
                DisplayRememberMe="False" FailureText="* Error de Inicio de Sesión."  
                PasswordLabelText="Contraseña" PasswordRequiredErrorMessage="*Requerido" TitleText="" 
                UserNameLabelText="Nombre de usuario:" UserNameRequiredErrorMessage="* Requerido" onauthenticate="logMain_Authenticate" BorderStyle= "solid"
                DestinationPageUrl="Default.aspx" style="text-align: center"  BorderColor="#0D0D0D" ForeColor="#454444" border-shadow="2px" BackColor="#A8CF38" >
                              
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0" style="height:167px; width:353px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server"  BorderWidth="1px" Width="80%" 
                                                ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="UserName" ErrorMessage="* Requerido" ToolTip="* Requerido" 
                                                ValidationGroup="logMain">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server"  BorderWidth="1px" TextMode="Password" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                ControlToValidate="Password" ErrorMessage="*Requerido" ToolTip="*Requerido" 
                                                ValidationGroup="logMain">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="text-align: center">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                                CssClass="btn btn-primary" Text="Iniciar Sesión" ValidationGroup="logMain" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <LoginButtonStyle CssClass="btn btn-primary" />
                <TextBoxStyle BorderWidth="1px" Width="80%" />
            </asp:Login>
        </p>
    </div>
</asp:Content>
