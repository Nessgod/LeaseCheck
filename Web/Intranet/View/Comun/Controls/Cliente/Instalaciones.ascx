<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Instalaciones.ascx.cs" Inherits="View_Comun_Controls_Cliente_Instalaciones" %>

<script type="text/javascript">

    function abrirInstalacion(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteInstalacion.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);
    }

    function refreshInstalacion() {
        __doPostBack("<%=GridInstalaciones.ClientID %>", '')
    }

</script>


<rad:RadWindow2 ID="rwiDetalle" runat="server" />

<div class="SubTitulos">Instalaciones</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>
        <rad:RadGrid2 ID="GridInstalaciones" runat="server" OnItemDataBound="gridInstalaciones_ItemDataBound" AllowPaging="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="cin_id">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevaInstalacion" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevaInstalacion_Click" />
                        <asp:LinkButton ID="lnkEliminarInstalacion" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarInstalacion_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />

                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>

