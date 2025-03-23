<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePlanBolsa.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_ClientePlanBolsa" %>

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
            if (window.BrowserWindow.refreshBolsa) window.BrowserWindow.refreshBolsa();
            window.close();
         }

    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Agregar Bolsa</div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-5 col-md-5 col-xs-12"> 
		            <label>Bolsas(*)</label>
		            <rad:RadComboBox2 ID="cboBolsa" runat="server" OnLoad="LoadControls" Width="100%" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server" 
                        ControlToValidate="cboBolsa" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
                <div class="col-lg-3 col-md-3 col-xs-12">
                     <label style="width:100%;">Fecha Hasta</label>
                    <WebControls:Calendar ID="txtDesde" runat="server" />
                </div>
            </div>
            </br>
  
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>