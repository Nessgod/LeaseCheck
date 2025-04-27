<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cliente.ascx.cs" Inherits="View_Root_Mantenedores_Cliente_Controls_Cliente" %>

<%@ Register Src="~/View/Comun/Controls/FiltroAvanzado.ascx" TagPrefix="wuc" TagName="Filtro" %>
<%@ Register Src="~/View/Comun/Controls/Cliente/Identidad.ascx" TagPrefix="wuc" TagName="Identidad" %>
<%@ Register Src="~/View/Comun/Controls/Cliente/Usuarios.ascx" TagPrefix="wuc" TagName="Usuarios" %>
<%@ Register Src="~/View/Comun/Controls/Cliente/PlanesTarifarios.ascx" TagPrefix="wuc" TagName="PlanesTarifarios" %>


<script type="text/javascript">
    function closeWindow() {
        window.location = ('<%=ResolveUrl(URLVolverCliente) %>');
    }
</script>

<div class="row col-lg-12 col-md-12 col-xs-12" style="min-height: 500px">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <rad:RadTabStrip2 ID="ragTab" runat="server" MultiPageID="MultiPage" Orientation="VerticalLeft" Skin="Bootstrap" RenderMode="Lightweight" SelectedIndex="0">
            <Tabs>
                <rad:RadTab Text="Identidad" runat="server" PageViewID="rtvIdentidad" />
                <rad:RadTab Text="Planes Tarifarios" runat="server" PageViewID="rtvPlanesTarifarios" />
                <rad:RadTab Text="Usuarios" runat="server" PageViewID="rtvUsuarios" />
            </Tabs>
        </rad:RadTabStrip2>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <div class="col-lg-12 col-md-12 col-xs-12">
            <rad:RadMultiPage ID="MultiPage" runat="server" SelectedIndex="0">
                <rad:RadPageView ID="rtvIdentidad" runat="server">
                    <wuc:Identidad runat="server" ID="wucIdentidad" />
                </rad:RadPageView>
                <rad:RadPageView ID="rtvUsuarios" runat="server">
                    <wuc:Usuarios runat="server" ID="wucUsuarios" />
                </rad:RadPageView>
                <rad:RadPageView ID="rtvPlanesTarifarios" runat="server">
                    <wuc:PlanesTarifarios runat="server" ID="wucPlanesTarifarios" />
                </rad:RadPageView>
            </rad:RadMultiPage>
        </div>

        <div class="col-lg-12 col-md-12 col-xs-12 form-col-center" style="margin-top: 10px; margin-left: 6px">
            <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" CssClass="ButtonCerrar" Width="102px"
                OnClientClick="closeWindow(); return false;" />
        </div>

    </div>

</div>
