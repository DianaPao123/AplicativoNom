<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="wfrDistribuidores.aspx.cs" Inherits="SitioNominaAdmin.wfrDistribuidores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server">LISTA DE DISTRIBUIDORES</asp:Label></h1>
    <p>Distribuidores:
        <asp:DropDownList ID="ddlDistribuidores" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlDistribuidores_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <center>
        <asp:GridView ID="gvDistribuidor" runat="server" Font-Size="X-Small" AutoGenerateColumns="False"
            onrowdatabound="gvDistribuidor_RowDataBound" 
            onselectedindexchanging="gvDistribuidor_SelectedIndexChanging" 
            onrowediting="gvDistribuidor_RowEditing" 
            HeaderStyle-BackColor="#CCFF33">
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
                <asp:TemplateField HeaderText="Razon Social">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Rfc" HeaderText="RFC" />
                <asp:BoundField DataField="Contratados" HeaderText="Tipo de Contrato" />
                <asp:BoundField DataField="Timbre" HeaderText="Timbres Contratados" />
                <asp:BoundField DataField="Observacones" HeaderText="Observaciones" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}"/>
                <asp:BoundField DataField="Porcentaje" HeaderText="Comision %" />
                <asp:BoundField DataField="Comision" HeaderText="Comision $" DataFormatString="{0:C}"/>
                <asp:TemplateField HeaderText="Estatus">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Emitidos" HeaderText="Timbres Emitidos" />
                <asp:BoundField DataField="Cancelados" HeaderText="Cancelados" />
                <asp:BoundField DataField="Distribuidor" HeaderText="Nombre del Distribuidor" />
                <asp:CommandField SelectText="Pagar" ShowSelectButton="True" />
                <asp:CommandField EditText="EditarContrato" ShowEditButton="True" 
                    AccessibleHeaderText="EditarContrato" />
            </Columns>

<HeaderStyle BackColor="#666666" ForeColor="#E2E2E2"></HeaderStyle>

        </asp:GridView>
      <br
      br />
        <asp:GridView ID="gvComisonAD" runat="server"  Font-Size="XX-Small" 
            AutoGenerateColumns="False" Width="917px" 
            >
            <Columns>
                <asp:BoundField DataField="PaquetesdeFacturacion" HeaderText="Paquetes de Facturación" 
                    SortExpression="PaquetesdeFacturacion" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="Totalconiva" 
                    HeaderText="Total con I.V.A." SortExpression="Totalconiva" 
                    DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NtlinkPorcentaje1" HeaderText="Ntlink 70%" 
                    SortExpression="NtlinkPorcentaje1" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="Promotores" HeaderText="Comisión30%" 
                    SortExpression="Promotores" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorPrimario1" HeaderText="Tera 15%" 
                    SortExpression="PromotorPrimario1" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorSecundario1" HeaderText="Giga 35%" 
                    SortExpression="PromotorSecundario1" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorTerciario1" 
                    HeaderText="Mega 50%" SortExpression="PromotorTerciario1" 
                    DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NtlinkPorcentaje2" HeaderText="NtLink  70%" 
                    SortExpression="NtlinkPorcentaje2" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="Promotor2" HeaderText="Comisión 30%" 
                    SortExpression="Promotor2" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorPrimario2" HeaderText="Tera 15%" 
                    SortExpression="PromotorPrimario2" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorSecundario2" HeaderText="Giga 35%" 
                    SortExpression="PromotorSecundario2" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorTerciario2" 
                    HeaderText="Mega 50%" SortExpression="PromotorTerciario2" 
                    DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NtlinkPorcentaje3" HeaderText="NtLink 77.5%" 
                    SortExpression="NtlinkPorcentaje3" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="Promotor3" HeaderText="Comisión 22.5%" 
                    SortExpression="Promotor3" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorPrimario3" HeaderText="Tera 15%" 
                    SortExpression="PromotorPrimario3" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorSecundario3" HeaderText="Giga 35%" 
                    SortExpression="PromotorSecundario3" DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="PromotorTerciario3" 
                    HeaderText="Mega 50%" SortExpression="PromotorTerciario3" 
                    DataFormatString="{0:C}" >
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="GvCtdistri" runat="server" Font-Size="X-Small" AutoGenerateColumns="False"    >
            <Columns>
                <asp:BoundField DataField="PAQUETEDETIMBRADO" HeaderText="Paquete de timbrado">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="TOTALCONIVA" DataFormatString="{0:C}" 
                    HeaderText="Total con I.V.A.">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NTLINK70" DataFormatString="{0:C}" 
                    HeaderText="Ntlink 70%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORP" DataFormatString="{0:C}" 
                    HeaderText="Comisión 30%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORP" DataFormatString="{0:C}" 
                    HeaderText="Tera 10%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDOR2" DataFormatString="{0:C}" 
                    HeaderText="Giga 30">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDOR3" DataFormatString="{0:C}" 
                    HeaderText="Mega 60%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NTLINK" DataFormatString="{0:C}" 
                    HeaderText="NtLink 70%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORS" DataFormatString="{0:C}" 
                    HeaderText="Comisión 25%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORUNO" DataFormatString="{0:C}" 
                    HeaderText="Tera 15%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORDOS" DataFormatString="{0:C}" 
                    HeaderText="Giga 35%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORTRES" DataFormatString="{0:C}" 
                    HeaderText="Mega 50%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="NTLINK75" DataFormatString="{0:C}" 
                    HeaderText="NtLink 75%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORTER" DataFormatString="{0:C}" 
                    HeaderText="Comisión 22.5%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                <ItemStyle BackColor="#66FF99" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORU" DataFormatString="{0:C}" 
                    HeaderText="Tera 50%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORD" DataFormatString="{0:C}" 
                    HeaderText="Giga 50%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
                <asp:BoundField DataField="DISTRIBUIDORT" DataFormatString="{0:C}" 
                    HeaderText="Mega 50%">
                <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br>
       
    </center>
         
</asp:Content>
