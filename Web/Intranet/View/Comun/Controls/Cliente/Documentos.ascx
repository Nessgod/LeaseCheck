<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Documentos.ascx.cs" Inherits="View_Comun_Controls_Cliente_Documentos" %>


<script type="text/javascript">
    function abrirDocumento(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        console.log(oWin);
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteDocumento.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);
    }

    function refresh() {
        __doPostBack("<%=GridDocumentos.ClientID %>", '')
    }
</script>

<rad:RadWindow2 ID="rwiDetalle" runat="server" Title="Documentos" Width="1000" Height="500 " />
<div class="SubTitulos">Documentos</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>

        <rad:RadGrid2 ID="GridDocumentos" runat="server" AllowPaging="false" OnItemDataBound="GridDocumentos_ItemDataBound" OnItemCreated="GridDocumentos_ItemCreated">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="cdo_id, cdo_id_cliente">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevoDocumento" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevoDocumento_Click" />
                        <asp:LinkButton ID="lnkEliminarDocumento" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarDocumento_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>

