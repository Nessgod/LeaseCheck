<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="ClientePropiedades.aspx.cs" Inherits="View_Clientes_Identidad_ClientePropiedades" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">

        function abrirPropiedad(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClientePropiedad.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refreshUsuarios() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }

    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Propiedades
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound" AllowPaging="false">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="cpd_id">
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                            <asp:LinkButton ID="lnkNuevaPropiedad" runat="server" Text="Nuevo" CssClass="btn_dinamico btn_guardar" OnClick="lnkNuevaPropiedad_Click" ToolTip="Añadir">
                              <span class="text">Nuevo</span>
                              <span class="icon"><i class="fas fa-plus"></i></span>
                              </asp:LinkButton>

                            <asp:LinkButton ID="lnkEliminarPropiedad" runat="server" CssClass="btn_dinamico btn_eliminar" OnClick="lnkEliminarPropiedad_Click"
                                OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" ToolTip="Eliminar">
                              <span class="text">Eliminar</span>
                              <span class="icon"><i class="fas fa-trash-alt"></i></span>
                          </asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
                <ClientSettings>
                </ClientSettings>
            </rad:RadGrid2>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
