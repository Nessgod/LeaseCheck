<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Perfiles.aspx.cs" Inherits="View_Root_Mantenedores_Perfiles_Perfiles" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function abrirPerfil(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Perfiles/Perfil.aspx") %>?query=' + query);
            oWin.show();
        }


        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Perfiles
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="PER_ID">
                    <CommandItemTemplate>
                       <div class="contenedor-botones">
                                <asp:LinkButton ID="lnkNuevo" runat="server" CssClass="btn_dinamico btn_guardar" OnClientClick="abrirPerfil(0); return false;" ToolTip="Añadir">
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
