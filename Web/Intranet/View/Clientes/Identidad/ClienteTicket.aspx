<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteTicket.aspx.cs" Inherits="View_Clientes_Identidad_ClienteTicket" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>
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
            <wuc:Filtro runat="server" ID="wucFiltro">
                <FiltroPersonalizado>
                    <div class="col-lg-1 col-md-1 col-xs-12">
                        <label>Estatus</label>
                    </div>
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <rad:RadComboBox2 ID="cboEstatus" runat="server" OnLoad="LoadControls">
                        </rad:RadComboBox2>
                    </div>
                </FiltroPersonalizado>
            </wuc:Filtro>

            <div class="SubTitulos">
                Mis Tickets
            </div>
            <rad:RadGrid2 ID="GridMisTickets" runat="server" OnItemDataBound="GridMisTickets_ItemDataBound" AllowPaging="true">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="mes_id">
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                            <asp:LinkButton ID="lnkDescargarPlantilla" runat="server" Text="Descargar excel " CssClass="btn_dinamico btn_excel" OnClick="LnkGenerar_Click">
                        <span class="text">Descargar</span>
                        <span class="icon"><i class="fas fa-file-excel"></i></span>
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
