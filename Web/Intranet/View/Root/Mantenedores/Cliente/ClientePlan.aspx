<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePlan.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_ClientePlan" %>

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

        function Panel() {
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
        }
    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:HiddenField ID="HDFopcion" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdfTotal" runat="server" />
            <asp:HiddenField ID="hdfTotalBalsa" runat="server" />
            <div class="SubTitulos">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12" id="trValidaPanel" runat="server" visible="true">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <asp:RadioButton ID="rdoPlan" runat="server" Text="Plan" Checked="true" GroupName="Planes" OnClick="Panel()" />
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <asp:RadioButton ID="rdoBolsa" runat="server" Text="Bolsas" GroupName="Planes" OnClick="Panel()" />
                </div>
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <asp:RadioButton ID="rdoProducto" runat="server" Text="Producto Adicional" GroupName="Planes" OnClick="Panel()" />
                </div>


            </div>
            <asp:Panel runat="server" ID="pnlPlan">
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Desde(*)</label>
                        <WebControls:Calendar ID="txtDesde" runat="server" Calendar-To-Control="txtHasta" />
                        <asp:CustomValidator ID="CustomValidator12" runat="server"
                            ControlToValidate="txtDesde"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Hasta(*)</label>
                        <WebControls:Calendar ID="txtHasta" runat="server" Calendar-From-Control="txtDesde" />
                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                            ControlToValidate="txtHasta"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>
                </div>
                <p />
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="form-group col-lg-2 col-md-2 col-xs-12">
                        <label>Plan</label>
                    </div>
                    <div class="form-group col-lg-10 col-md-10 col-xs-12">
                        <rad:RadComboBox2 ID="cboTipoPlan" runat="server" OnLoad="LoadControls" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboTipoPlan_SelectedIndexChanged" />
                        <asp:CustomValidator ID="CustomValidator3" runat="server"
                            ControlToValidate="cboTipoPlan"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>

                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12 ">
                    <div class="form-group col-lg-2 col-md-2 col-xs-12">
                        <label>Informes &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-10 col-md-10 col-xs-12">
                        <rad:RadNumericBox2 ID="txtCantidad" runat="server" />
                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                            ControlToValidate="txtCantidad"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12 ">
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <label>Administradores</label>
                    </div>
                    <div class="form-group col-lg-3 col-md-3 col-xs-3">
                        <rad:RadNumericBox2 ID="TxtCantAdministradores" runat="server" />
                        <asp:CustomValidator ID="CustomValidator5" runat="server"
                            ControlToValidate="TxtCantAdministradores"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <asp:CheckBox ID="ChkAdmIlimitados" Text=" Ilimitados" runat="server" />
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <label>Valor &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <rad:RadNumericBox2 ID="TxtValorPlan" runat="server" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                            ControlToValidate="TxtValorPlan"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Plan" />
                    </div>
                </div>
                <asp:Panel ID="pnlPlanProducto" runat="server">
                    <rad:RadGrid2 ID="Grid" runat="server">
                        <MasterTableView CommandItemDisplay="Top">
                            <CommandItemTemplate>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" ScrollHeight="320" />
                        </ClientSettings>
                    </rad:RadGrid2>
                </asp:Panel>
                <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                    <WebControls:PushButton ID="PushButton2" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Plan" />
                </div>
            </asp:Panel>
            <%--PANEL BOLSAS--%>
            <asp:Panel ID="pnlBolsas" runat="server">
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Desde(*)</label>
                        <WebControls:Calendar ID="txtDesdeBolsa" runat="server" Calendar-To-Control="txtHastaBolsa" />
                        <asp:CustomValidator ID="CustomValidator6" runat="server"
                            ControlToValidate="txtDesdeBolsa"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Hasta(*)</label>
                        <WebControls:Calendar ID="txtHastaBolsa" runat="server" Calendar-From-Control="txtDesdeBolsa" />
                        <asp:CustomValidator ID="CustomValidator7" runat="server"
                            ControlToValidate="txtHastaBolsa"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Bolsas</label>
                    </div>
                    <div class="form-group col-lg-10 col-md-10 col-xs-12">
                        <rad:RadComboBox2 ID="cboBolsas" runat="server" OnLoad="LoadControls" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboBolsas_SelectedIndexChanged" />
                        <asp:CustomValidator ID="CustomValidator8" runat="server"
                            ControlToValidate="cboBolsas"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12 ">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Informes &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-10 col-md-10 col-xs-12">
                        <rad:RadNumericBox2 ID="txtCantidadBolsas" runat="server" />
                        <asp:CustomValidator ID="CustomValidator9" runat="server"
                            ControlToValidate="txtCantidadBolsas"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12 ">
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <label>Administradores</label>
                    </div>
                    <div class="form-group col-lg-3 col-md-3 col-xs-3">
                        <rad:RadNumericBox2 ID="txtAdministradoresBolsas" runat="server" />
                        <asp:CustomValidator ID="CustomValidator10" runat="server"
                            ControlToValidate="txtAdministradoresBolsas"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>

                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <label>Valor &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <rad:RadNumericBox2 ID="txtValorBolsa" runat="server" />
                        <asp:CustomValidator ID="CustomValidator11" runat="server"
                            ControlToValidate="txtValorBolsa"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Bolsas" />
                    </div>
                </div>
                <asp:Panel ID="pnlBolsaProducto" runat="server">
                    <rad:RadGrid2 ID="GridBolsa" runat="server">
                        <MasterTableView CommandItemDisplay="Top">
                            <CommandItemTemplate>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" ScrollHeight="320" />
                        </ClientSettings>
                    </rad:RadGrid2>
                </asp:Panel>
                <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                    <WebControls:PushButton ID="PushButton1" runat="server" Text="Guardar" OnClick="btnGuardarBolsa_Click" ValidationGroup="Bolsas" />
                </div>
            </asp:Panel>
            <%--PANEL PRODUCTO ADICIONAL--%>
            <asp:Panel ID="pnlProductos" runat="server">
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Desde(*)</label>
                        <WebControls:Calendar ID="txtDesdeProducto" runat="server" Calendar-To-Control="txtHastaProducto" />
                        <asp:CustomValidator ID="CustomValidator13" runat="server"
                            ControlToValidate="txtDesdeProducto"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Producto" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-xs-12">
                        <label style="width: 100%;">Fecha Hasta(*)</label>
                        <WebControls:Calendar ID="txtHastaProducto" runat="server" Calendar-From-Control="txtDesdeProducto" />
                        <asp:CustomValidator ID="CustomValidator14" runat="server"
                            ControlToValidate="txtHastaProducto"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Producto" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Producto Adicional</label>
                    </div>
                    <div class="form-group col-lg-10 col-md-10 col-xs-12">
                        <rad:RadComboBox2 ID="cboProducto" runat="server" OnLoad="LoadControls" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboProducto_SelectedIndexChanged" />
                        <asp:CustomValidator ID="CustomValidator15" runat="server"
                            ControlToValidate="cboProducto"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Producto" />
                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12 ">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Cantidad &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-3 col-md-3 col-xs-3">
                        <rad:RadNumericBox2 ID="txtCantidadProducto" runat="server" />
                        <asp:CustomValidator ID="CustomValidator16" runat="server"
                            ControlToValidate="txtCantidadProducto"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Producto" />
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <label>Valor &nbsp;</label>
                    </div>
                    <div class="form-group col-lg-2 col-md-2 col-xs-2">
                        <rad:RadNumericBox2 ID="txtValorProducto" runat="server" />
                        <asp:CustomValidator ID="CustomValidator18" runat="server"
                            ControlToValidate="txtValorProducto"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Producto" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                        <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardarProducto_Click" ValidationGroup="Producto" />
                        <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
