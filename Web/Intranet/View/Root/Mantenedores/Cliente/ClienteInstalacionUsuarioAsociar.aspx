<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteInstalacionUsuarioAsociar.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_ClienteInstalacionUsuarioAsociar" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="chpScript" runat="server">

    <script type="text/javascript">


</script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">Asociar Usuarios</div>
            <rad:RadGrid2 ID="GridAsociar" runat="server" OnItemDataBound="GridAsociar_ItemDataBound">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="usu_id">
                    <CommandItemTemplate>
                        <div>
                            <asp:LinkButton ID="lnkAsociar" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkAsociar_Click" />
                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" />
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
