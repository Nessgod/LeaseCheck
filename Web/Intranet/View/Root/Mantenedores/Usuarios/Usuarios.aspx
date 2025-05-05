<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="View_Root_Mantenedores_Usuarios_Usuarios" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Usuarios/Usuario.aspx") %>?query=' + query);
            oWin.show();
        }

    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Usuarios
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro">
        <FiltroPersonalizado>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-1 col-md-1 col-xs-12">
                    <label>Tipo</label>
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <rad:RadComboBox2 ID="cboTipo" runat="server" Width="100%">
                        <Items>
                            <rad:RadComboBoxItem Text="Seleccione" Value="" />
                            <rad:RadComboBoxItem Text="Cliente" Value="1" />
                            <rad:RadComboBoxItem Text="Usuario" Value="0" />
                        </Items>
                    </rad:RadComboBox2>
                </div>
                <div class="col-lg-1 col-md-1 col-xs-12">
                    <label>Cliente:</label>
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <rad:RadComboBox2 ID="cboCliente" runat="server" OnLoad="LoadControls" MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" />
                </div>
                <div class="col-lg-1 col-md-1 col-xs-12">
                    <label>Pais:</label>
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" MarkFirstMatch="true" EnableLoadOnDemand="true" />
                </div>
            </div>
        </FiltroPersonalizado>
    </wuc:Filtro>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="usu_id">
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                           
                                <asp:LinkButton ID="lnkNuevo" runat="server" CssClass="btn_dinamico btn_guardar" OnClientClick="abrir(0); return false;" ToolTip="Añadir">
                                 <span class="text">Nuevo</span>
                                 <span class="icon"><i class="fas fa-plus"></i></span>
                                </asp:LinkButton>


                                <asp:LinkButton ID="lnkEliminar" runat="server" CssClass="btn_dinamico btn_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" ToolTip="Eliminar">
                                     <span class="text">Eliminar</span>
                                     <span class="icon"><i class="fas fa-trash-alt"></i></span>
                                 </asp:LinkButton>

                
                                <asp:LinkButton ID="lnkDescargarPlantilla" runat="server" Text="Descargar excel " CssClass="btn_dinamico btn_excel" OnClick="LnkGenerar_Click">
                                        <span class="text">Descargar</span>
                                        <span class="icon"><i class="fas fa-file-excel"></i></span>
                                </asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
