<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteDocumento.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_ClienteDocumento" %>

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

        <%--function Panel() {
            var rdoPlan = $('#<%=rdoPlan.ClientID %>');
             var rdoBolsa = $('#<%=rdoBolsa.ClientID %>');
             var rdoProducto = $('#<%=rdoProducto.ClientID %>');
             var trPlan = $('#<%=pnlPlan.ClientID %>');
             var trBolsa = $('#<%=pnlBolsas.ClientID %>');
             var trProducto = $('#<%=pnlProductos.ClientID %>');
             var HDFopcion = $('#<%=HDFopcion.ClientID %>');

            if (rdoPlan.is(':checked')) {
                trBolsa.hide();
                trProducto.hide();
                trPlan.show();
                HDFopcion.val('1');
            }
            else if (rdoBolsa.is(':checked')) {
                trPlan.hide();
                trProducto.hide();
                trBolsa.show();
                HDFopcion.val('2');
            }
            else {
                trPlan.hide();
                trBolsa.hide();
                trProducto.show();
                HDFopcion.val('3');
            }
        }--%>
    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="SubTitulos">Nuevo documento</div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Cargar Documento </label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-12">
                    <asp:FileUpload ID="fldDocumento" runat="server" OnClientAdded="addTitle" />

                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Descripción: </label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtDescripcionDocumento" runat="server" MaxLength="200" UpperCase="true" />
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="txtDescripcionDocumento"
                        ValidateEmptyText="false"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="PushButton2" runat="server" Text="Cerrar" CssClass="ButtonCerrar" OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
