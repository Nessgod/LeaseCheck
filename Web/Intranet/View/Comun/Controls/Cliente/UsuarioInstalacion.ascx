<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsuarioInstalacion.ascx.cs" Inherits="View_Comun_Controls_Cliente_Usuarioinstalacion" %>

<script type="text/javascript">
    function abrirUsuario(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteInstalacionUsuario.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);

        oWin.add_close(function () {
            __doPostBack('', '');
        });
    }

    function refreshUsuarios() {
        __doPostBack("<%=gridUsuariosInstalacion.ClientID %>", '')
    }

</script>


<rad:RadWindow2 ID="rwiDetalle" runat="server" />
<div class="SubTitulos">Usuarios de la instalación</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>
        <rad:RadGrid2 ID="gridUsuariosInstalacion" runat="server" OnItemDataBound="gridUsuariosInstalacion_ItemDataBound" AllowPaging="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="ciu_id">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkAsociar" runat="server" Text="Asociar" CssClass="icono_guardar" OnClick="lnkAsociar_Click" />
                        <asp:LinkButton ID="lnkDesasociar" runat="server" Text="Desvincular" CssClass="icono_eliminar" OnClick="lnkDesasociar_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>
