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

        function Sumar() {

            var hdfTotal = $('#<%= hdfTotal.ClientID %>');
            var lblTotal = $('#<%= lblTotal.ClientID %>');
            var TxtValorPlan = $('#<%= TxtValorPlan.ClientID %>');
      
            if (TxtValorPlan.val() == '') TxtValorPlan.val(0);

            var plan = parseFloat(TxtValorPlan.val().split('.').join(''));

            hdfTotal.val(plan);
            lblTotal.text('$' + formatMillones(plan))
        }

    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdfTotal" runat="server" />
            <asp:HiddenField ID="hdfTotalBalsa" runat="server" />
            <div class="SubTitulos">Plan</div>
            <div style="text-align: right;" class="SubTitulos">
                <label>Total Plan:</label>
                <asp:Label ID="lblTotal" runat="server" />
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-6 col-md-6 col-xs-12">
                    <label style="width: 100%;">Fecha Desde</label>
                    <WebControls:Calendar ID="txtDesde" runat="server" Calendar-To-Control="txtHasta" />
                    <asp:CustomValidator ID="CustomValidator12" runat="server"
                        ControlToValidate="txtDesde"
                        ErrorMessage="Debe elegir una fecha"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
                <div class="col-lg-6 col-md-6 col-xs-12">
                    <label style="width: 100%;">Fecha Hasta</label>
                    <WebControls:Calendar ID="txtHasta" runat="server" Calendar-From-Control="txtDesde" />
                    <asp:CustomValidator ID="CustomValidator4" runat="server"
                        ControlToValidate="txtHasta"
                        ErrorMessage="Debe elegir una fecha"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <p />
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-6 col-md-6 col-xs-12">
                    <label>Tipo Plan</label>
                    <rad:RadComboBox2 ID="cboTipoPlan" runat="server" OnLoad="LoadControls" Width="100%" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                        ControlToValidate="cboTipoPlan"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
                <div class="col-lg-6 col-md-6 col-xs-12">
                    <label>Valor &nbsp;</label>
                    <rad:RadNumericBox2 ID="TxtValorPlan" runat="server" OnBlur="Sumar();" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                        ControlToValidate="TxtValorPlan"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />

                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
            </div>

            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
