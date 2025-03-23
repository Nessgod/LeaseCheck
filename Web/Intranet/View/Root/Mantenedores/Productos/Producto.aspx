<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Producto.aspx.cs" Inherits="View_Root_Mantenedores_Productos_Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeder" Runat="Server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" Runat="Server">
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
            <div class="SubTitulos">Productos</div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Nombre(*)</label>
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
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Habilitado(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" />
                    <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" />
	            </div>
            </div>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Identidad" OnClick="btnGuardar_Click"/>
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar"/>
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>