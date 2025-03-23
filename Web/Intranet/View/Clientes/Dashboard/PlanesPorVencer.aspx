<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="PlanesPorVencer.aspx.cs" Inherits="View_Clientes_Dashboard_PlanesPorVencer" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
</script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Planes por Vencer
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro">
        <FiltroPersonalizado>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Cliente:</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12">
                    <rad:RadComboBox2 ID="cboCliente" runat="server" OnLoad="LoadControls" MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Filter="Contains" AutoPostBack="true" />
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Estado:</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12">
                    <rad:RadComboBox2 ID="cboTipo" runat="server">
                        <Items>
                            <rad:RadComboBoxItem Text="Seleccione" Value="" />
                            <rad:RadComboBoxItem Text="Habilitado" Value="1" />
                            <rad:RadComboBoxItem Text="Deshabilitado" Value="0" />
                        </Items>
                    </rad:RadComboBox2>
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Desde:</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12">
                    <WebControls:Calendar ID="txtDesde" runat="server" />
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Hasta:</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12">
                    <WebControls:Calendar ID="txtHasta" runat="server" />
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Pais:</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12">
                    <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Filter="Contains" AutoPostBack="true" />
                </div>
            </div>
        </FiltroPersonalizado>
    </wuc:Filtro>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="">
                    <CommandItemTemplate>
                        <div>
                            <span style="float: right">
                                <asp:LinkButton ID="lnkDescargarPlantilla" runat="server" Text="Descargar excel " CssClass="icono_descargar_excel" OnClick="LnkGenerar_Click" />
                            </span>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
