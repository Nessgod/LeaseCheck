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




    </script>

    <style>
        .minimalist-table {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
            font-size: 14px;
            color: #333;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
        }

            .minimalist-table th, .minimalist-table td {
                padding: 6px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .minimalist-table th {
                background-color: #f4f4f4;
                font-weight: bold;
                text-transform: uppercase;
            }

            .minimalist-table tr:hover {
                background-color: #f1f1f1;
            }

            .minimalist-table tr:last-child td {
                border-bottom: none;
            }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2
        ID="rwiDetalle"
        runat="server"
        Modal="true"
        VisibleOnPageLoad="false"
        Behaviors="Close, Move"
        Title="Detalle">
    </rad:RadWindow2>

    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdfTotal" runat="server" />
            <div class="SubTitulos">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>
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
                <div class="col-lg-10 col-md-10 col-xs-12">
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
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-6 col-md-6 col-xs-12">
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-4 col-xs-12">
                                    <label>Cantidad Documentos &nbsp;</label>
                                </div>
                                <div class="form-group col-lg-8 col-md-8 col-xs-12">
                                    <rad:RadNumericBox2 ID="txtCantidadDocumento" runat="server" />
                                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                                        ControlToValidate="txtCantidadDocumento"
                                        ValidateEmptyText="true"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Plan" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-4 col-xs-12">
                                    <label>Cantidad Instalaciones</label>
                                </div>
                                <div class="form-group col-lg-8 col-md-8 col-xs-12">
                                    <rad:RadNumericBox2 ID="TxtCantInstalacion" runat="server" />
                                    <asp:CustomValidator ID="CustomValidator5" runat="server"
                                        ControlToValidate="TxtCantInstalacion"
                                        ValidateEmptyText="true"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Plan" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-4 col-xs-12">
                                    <label>Cantidad de Potenciales Clientes</label>
                                </div>
                                <div class="form-group col-lg-8 col-md-8 col-xs-12">
                                    <rad:RadNumericBox2 ID="TxtCantLead" runat="server" />
                                    <asp:CustomValidator ID="CustomValidator6" runat="server"
                                        ControlToValidate="TxtCantLead"
                                        ValidateEmptyText="true"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Plan" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-4 col-xs-12">
                                    <label>Valor &nbsp;</label>
                                </div>
                                <div class="form-group col-lg-8 col-md-8 col-xs-12">
                                    <rad:RadNumericBox2 ID="TxtValorPlan" runat="server" />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                        ControlToValidate="TxtValorPlan"
                                        ValidateEmptyText="true"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Plan" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-lg-6 col-md-6 col-xs-12">
                            <asp:Panel ID="pnlPlanProducto" runat="server" Visible="false">
                                <div class="col-lg-12 col-md-12 col-xs-12">
                                    <asp:Literal ID="txtPlan" runat="server" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <br />
                <br />
                <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                    <WebControls:PushButton ID="PushButton2" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Plan" />
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
