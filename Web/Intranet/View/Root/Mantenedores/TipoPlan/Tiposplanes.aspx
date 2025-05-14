<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Tiposplanes.aspx.cs" Inherits="View_Mantenedores_TipoPlan_Tiposplanes" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/TipoPlan/Tipoplan.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
        function ObtenerIdSeleccionados() {

            var masterTable = $find("<%=Grid.ClientID%>").get_masterTableView();
            var count = masterTable.get_dataItems().length;
            var checkbox;
            var item;
            for (var i = 0; i < count; i++) {
                item = masterTable.get_dataItems()[i];
                checkbox = item.findElement("ClientSelectColumnSelectCheckBox");

                if (checkbox.checked) {
                    var Id = masterTable.get_dataItems()[i].findElement("Tpl_Id");
                }
            }
        }

    </script>

</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Plan
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro">
    </wuc:Filtro>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="tpl_id">
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
