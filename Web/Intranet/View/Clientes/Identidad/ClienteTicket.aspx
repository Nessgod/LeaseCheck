<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteTicket.aspx.cs" Inherits="View_Clientes_Identidad_ClienteTicket" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" language="javascript">
        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            var window = getRadWindow();
            if (window.BrowserWindow.refresh) window.BrowserWindow.refresh();
            window.close();
        }

        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
                oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClienteTicketRespuesta.aspx") %>?query=' + query);
                oWin.show();
                bloqueaScroll(false);
            }

    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
     <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label></div>
            <rad:RadGrid2 ID="GridMisTickets" runat="server" OnItemDataBound="GridMisTickets_ItemDataBound" AllowPaging="true">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="mes_id">
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                            <asp:LinkButton ID="lnkCerrarTicket" runat="server" CssClass="btn_dinamico btn_eliminar" OnClick="lnkCerrarTicket_Click"
                                OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" ToolTip="Eliminar">
                               <span class="text">Cerrar</span>
                               <span class="icon"><i class="fas fa-close"></i></span>
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
