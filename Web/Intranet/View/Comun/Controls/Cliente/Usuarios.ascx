<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Usuarios.ascx.cs" Inherits="View_Comun_Controls_Cliente_Usuarios" %>

<script type="text/javascript">
    function abrirUsuario(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteUsuario.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);
    }

    function refreshUsuarios() {
        __doPostBack("<%=gridUsuarios.ClientID %>", '')
    }

</script>


<rad:RadWindow2 ID="rwiDetalle" runat="server" />

<div class="SubTitulos">Usuarios</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>
        <rad:RadGrid2 ID="gridUsuarios" runat="server" OnItemDataBound="gridUsuarios_ItemDataBound" AllowPaging="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="usu_id">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevoUsuario" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevoUsuario_Click" />
                        <asp:LinkButton ID="lnkEliminarUsuario" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarUsuario_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                        <asp:LinkButton ID="lnkReset" runat="server" Text="Reset Password" CssClass="icono_eliminar" OnClick="lnkReset_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea resetear las claves de los usuarios seleccionados?');" />
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>
