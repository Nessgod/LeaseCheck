<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Tipoplan.aspx.cs" Inherits="View_Mantenedores_TipoPlan_Tipoplan" %>

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

        function abrirProducto(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/TipoPlan/ProductoPlan.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }

   
    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdfTotal" runat="server" />
            <div class="SubTitulos">Definición Tipo de Plan</div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Nombre Plan(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtNombre" runat="server" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                        ControlToValidate="txtNombre"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-2">
                    <label>Cantidad Documentos(*)</label>
                </div>
                <div class="form-group col-lg-3 col-md-3 col-xs-3">
                    <rad:RadNumericBox2 ID="txtCantDocumento" runat="server" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                        ControlToValidate="txtCantDocumento"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-2">
                    <label>Cantidad Propiedad(*)</label>
                </div>
                <div class="form-group col-lg-3 col-md-3 col-xs-3">
                    <rad:RadNumericBox2 ID="TxtCantPropiedad" runat="server" />
                    <asp:CustomValidator ID="CustomValidator5" runat="server"
                        ControlToValidate="TxtCantPropiedad"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Cantidad de Potenciales Clientes(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadNumericBox2 ID="TxtCantLead" runat="server" />
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="TxtCantLead"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Valor</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadNumericBox2 ID="TxtValorPlan" runat="server" />
                    <asp:CustomValidator ID="CustomValidator4" runat="server"
                        ControlToValidate="TxtValorPlan"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Habilitado</label>
                </div>
                <div class="form-group col-lg-3 col-md-3 col-xs-3">
                    <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" />
                    <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" />
                </div>
            </div>
            <asp:Panel ID="pnlProducto" runat="server" Visible="true">
                <br />
                <rad:RadGrid2 ID="Grid" runat="server">
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="plp_id">
                        <CommandItemTemplate>
                            <div>
                                <asp:LinkButton ID="lnlNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnlNuevo_Click" />
                                <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="True" ScrollHeight="320" />
                    </ClientSettings>
                </rad:RadGrid2>
                <br />
            </asp:Panel>
            <br />
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
