<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePlan.aspx.cs" Inherits="View_Clientes_Identidad_ClientePlan" %>

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
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container mt-1">
                <!-- Título y Botón para Descargar PDF -->
                <div class="SubTitulos">
                    <h2>Plan</h2>
                    <asp:Button ID="btnDescargarPDF" runat="server" Text="Descargar PDF" OnClick="btnDescargarPDF_Click" CssClass="btn btn-primary" />
                </div>

                <!-- Fecha Desde y Hasta -->
                <div class="row">
                    <!-- Fecha Desde -->
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="txtDesde">Fecha Desde:</label>
                        <WebControls:Calendar ID="txtDesde" runat="server" Calendar-To-Control="txtHasta" />
                        <asp:CustomValidator ID="CustomValidator12" runat="server"
                            ControlToValidate="txtDesde"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>

                    <!-- Fecha Hasta -->
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="txtHasta">Fecha Hasta:</label>
                        <WebControls:Calendar ID="txtHasta" runat="server" Calendar-From-Control="txtDesde" />
                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                            ControlToValidate="txtHasta"
                            ErrorMessage="Debe elegir una fecha"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>

                <!-- Tipo de Plan -->
                <div class="row">
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="cboTipoPlan">Tipo Plan:</label>
                        <rad:RadComboBox2 ID="cboTipoPlan" runat="server" OnLoad="LoadControls" Width="100%" />
                        <asp:CustomValidator ID="CustomValidator3" runat="server"
                            ControlToValidate="cboTipoPlan"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>

                <!-- Contenido del Plan -->
                <div class="row">
                    <div class="form-group col-lg-12 col-md-12 col-sm-12 mb-3">
                        <label class="control-label" for="txtPlan">Contenido del Plan:</label>
                        <asp:Literal ID="txtPlan" runat="server" />
                    </div>
                </div>

                <!-- Valor del Plan -->
                <div class="row">
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="TxtValorPlan">Valor:</label>
                        <rad:RadNumericBox2 ID="TxtValorPlan" runat="server" OnBlur="Sumar();" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                            ControlToValidate="TxtValorPlan"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>

                <!-- Cantidad de Documentos -->
                <div class="row">
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="txtDocumentos">Cantidad Documentos:</label>
                        <rad:RadNumericBox2 ID="txtDocumentos" runat="server" OnBlur="Sumar();" />
                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                            ControlToValidate="txtDocumentos"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>

                <!-- Cantidad de Instalación -->
                <div class="row">
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="txtInstalacion">Cantidad Propiedad:</label>
                        <rad:RadNumericBox2 ID="txtInstalacion" runat="server" OnBlur="Sumar();" />
                        <asp:CustomValidator ID="CustomValidator5" runat="server"
                            ControlToValidate="txtInstalacion"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>

                <!-- Cantidad de Leads -->
                <div class="row">
                    <div class="form-group col-lg-6 col-md-6 col-sm-12 mb-3">
                        <label class="control-label" for="txtLead">Cantidad de Potenciales Clientes:</label>
                        <rad:RadNumericBox2 ID="txtLead" runat="server" OnBlur="Sumar();" />
                        <asp:CustomValidator ID="CustomValidator6" runat="server"
                            ControlToValidate="txtLead"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
                    </div>
                </div>
            </div>



            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
