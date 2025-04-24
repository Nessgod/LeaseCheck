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

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" Runat="Server">
    Propiedades
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" onitemdatabound="Grid_ItemDataBound" AllowPaging="false" >
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="cpd_id" >
                    <CommandItemTemplate>
                        <div>
                            <asp:LinkButton ID="lnkNuevaPropiedad" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevaPropiedad_Click"/>
                            <asp:LinkButton ID="lnkEliminarPropiedad" runat="server" Text="Eliminar" CssClass="icono_eliminar"  OnClick="lnkEliminarPropiedad_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');"/>
    
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
                <ClientSettings>
                </ClientSettings>  
            </rad:RadGrid2>
        </ContentTemplate>  
    </asp:UpdatePanel>
</asp:Content>