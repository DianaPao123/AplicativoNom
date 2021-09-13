<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrDistribuidorCliente.aspx.cs" Inherits="GafLookPaid.wfrDistribuidorCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Reporte de Clientes por Distribuidor</h1>
    <center>
    <asp:GridView ID="gvDistribuidor" runat="server" Font-Size="X-Small" AutoGenerateColumns="False" 
            HeaderStyle-BackColor="#CCFF33" 
        onrowdatabound="gvDistribuidor_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Rfc" HeaderText="RFC" />
                <asp:TemplateField HeaderText="Razon Social">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Contratados" HeaderText="Tipo de Contrato" />
                <asp:BoundField DataField="Timbre" HeaderText="Timbres Contratados" />
                <asp:BoundField DataField="Emitidos" HeaderText="Timbres Emitidos" />
                <asp:BoundField DataField="Cancelados" HeaderText="Cancelados" />
                <asp:TemplateField HeaderText="Porcentaje">
                    <ItemTemplate>
                        <asp:Label ID="lblPorcentaje" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

<HeaderStyle BackColor="#666666" ForeColor="#E2E2E2"></HeaderStyle>
        </asp:GridView>
        </center>
</asp:Content>
