<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Instalacion.aspx.cs" Inherits="View_Clientes_Instalacion_Instalacion" %>
<%@ Register Src="~/View/Clientes/Controls/Instalaciones/Identidad.ascx" TagPrefix="wuc" TagName="Identidad" %>



<asp:Content ID="Content1" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
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

    </script>
</asp:Content>

<asp:Content ID="ContenHead" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadTabStrip2 ID="ragTab" runat="server" MultiPageID="MultiPage" Skin="Bootstrap" RenderMode="Lightweight" SelectedIndex="0">
                <Tabs>
                    <rad:RadTab Text="Identidad" runat="server" PageViewID="rtvIdentidad" />

                </Tabs>
            </rad:RadTabStrip2>
            <rad:RadMultiPage ID="MultiPage" runat="server" SelectedIndex="0">
                <rad:RadPageView ID="rtvIdentidad" runat="server">
                    <wuc:Identidad runat="server" ID="wucIdentidad" />
                </rad:RadPageView>
            </rad:RadMultiPage>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center" style="margin-top: 10px;">
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" CssClass="ButtonCerrar" Width="102px"
                    OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>