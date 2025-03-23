<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="View_Root_Mantenedores_Reasignacion_Clientes" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Reasignacion/AsociarClientes.aspx") %>?query=' + query);
            oWin.show();
        }

        function abrirQuitar(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Reasignacion/QuitarCliente.aspx") %>?query=' + query);
            oWin.show();
        }

        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }

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

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="SubTitulos">Clientes</div>
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server"> 
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="cli_id" >
                    <CommandItemTemplate>
                        <div>
                            <asp:LinkButton ID="lnkNuevo" runat="server" Text="Asignar Clientes" CssClass="icono_guardar" OnClick="lnkNuevo_Click" />
                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar Asignación" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar la asignación de los clientes seleccionados? Deberá asignar los clientes a otro usuario para continuar.');" />
                       </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar"/>
           </div>
        </ContentTemplate>  
    </asp:UpdatePanel>
</asp:Content>