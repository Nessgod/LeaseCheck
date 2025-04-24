<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanesTarifarios.ascx.cs" Inherits="View_Comun_Controls_Cliente_PlanesTarifarios" %>

<script type="text/javascript">


    function abrirPlan(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClientePlan.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);
    }

    function refresh() {
        __doPostBack("<%=Grid.ClientID %>", '')
    }

</script>


<rad:RadWindow2 ID="rwiDetalle" runat="server" />

<div class="SubTitulos">Planes Tarifarios</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>

        <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound" AllowPaging="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="clp_id,tipo_dato">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevo_Click" />
                        <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>


