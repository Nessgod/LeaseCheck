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
            var trPlan = $('#<%=pnlPlan.ClientID %>');
            var HDFopcion = $('#<%=HDFopcion.ClientID %>');

            if (rdoPlan.is(':checked')) {
                trPlan.show();
                HDFopcion.val('1');
            }
            else {
                trPlan.hide();
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
            <div class="SubTitulos">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12" id="trValidaPanel" runat="server" visible="true">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <asp:RadioButton ID="rdoPlan" runat="server" Text="Plan" Checked="true" GroupName="Planes" OnClick="Panel()" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
