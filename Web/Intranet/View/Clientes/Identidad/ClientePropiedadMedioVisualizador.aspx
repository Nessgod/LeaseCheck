<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePropiedadMedioVisualizador.aspx.cs" Inherits="View_Clientes_Identidad_ClientePropiedadMedioVisualizador" %>

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
        .imageStyle {
            width: 60%;
            object-fit: cover;
            display: block;
            margin: 0 auto;
        }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">
                <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
            </div>

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="CardImagen">
                    <div class="imageContainer">
                        <!-- Imagen -->
                        <asp:Image ID="imgPropiedad" runat="server" CssClass="imageStyle" />
                    </div>
                </div>
            </div>



            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                <WebControls:PushButton ID="PushButton2" runat="server" Text="Cerrar" CssClass="ButtonCerrar" OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
