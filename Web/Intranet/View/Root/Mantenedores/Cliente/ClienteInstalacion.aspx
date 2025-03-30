<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteInstalacion.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_ClienteInstalacion" %>

<%@ Register Src="~/View/Comun/Controls/Cliente/IdentidadInstalacion.ascx" TagPrefix="wuc" TagName="IdentidadInstalacion" %>
<%@ Register Src="~/View/Comun/Controls/Cliente/UsuarioInstalacion.ascx" TagPrefix="wuc" TagName="UsuarioInstalacion" %>




<asp:Content ID="Content1" ContentPlaceHolderID="chpScript" runat="server">
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

    </script>
</asp:Content>

<asp:Content ID="ContenHead" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <rad:RadTabStrip2 ID="ragTab" runat="server" MultiPageID="MultiPage" Skin="Bootstrap" RenderMode="Lightweight" SelectedIndex="0">
                <Tabs>
                    <rad:RadTab Text="Identidad" runat="server" PageViewID="rtvIdentidad" />
                    <rad:RadTab Text="Usuario Instalación" runat="server" PageViewID="rtvUsuarioInstalacion" />
    
                </Tabs>
            </rad:RadTabStrip2>
            <rad:RadMultiPage ID="MultiPage" runat="server" SelectedIndex="0">
                <rad:RadPageView ID="rtvIdentidad" runat="server">
                    <wuc:IdentidadInstalacion runat="server" id="wucIdentidadInstalacion" />
                </rad:RadPageView>
                <rad:RadPageView ID="rtvUsuarioInstalacion" runat="server">
                    <wuc:UsuarioInstalacion runat="server" ID="wucUsuarioInstalacion" />
                </rad:RadPageView>
            </rad:RadMultiPage>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center" style="margin-top: 10px;">
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" CssClass="ButtonCerrar" Width="102px"
                    OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>