<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="QuitarCliente.aspx.cs" Inherits="View_Root_Mantenedores_Reasignacion_QuitarCliente" %>

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
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Reasignar Clientes</div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-3 col-md-3 col-xs-12"> 
		            <label>Usuarios</label>
	            </div>
	            <div class="form-group col-lg-9 col-md-9 col-xs-12">
		            <rad:RadComboBox2 ID="cboUsuarios" runat="server" OnLoad="LoadControls" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                            ControlToValidate="cboUsuarios"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />
	            </div>
            </div>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Reasignar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar"/>
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>