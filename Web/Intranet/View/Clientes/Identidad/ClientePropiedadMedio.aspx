<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePropiedadMedio.aspx.cs" Inherits="View_Clientes_Identidad_ClientePropiedadMedio" %>

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
        $(document).ready(function () {
            manejarVideoImagen();
        });

        function manejarVideoImagen() {
            const imagenSi = $("#<%= esImagenSi.ClientID %>");
            const videoSi = $("#<%= esVideoSi.ClientID %>");
            const divFileUpload = $("#<%= divFileUpload.ClientID %>");
            const divElementos = $("#<%= divElementos.ClientID %>");
            const divLink = $("#<%= divLink.ClientID %>");
            const txtLink = $("#<%= txtLink.ClientID %>"); // Campo de texto para el link
            const fdlImagen = $("#<%= fldImagen.ClientID %>"); // Campo para cargar imagen

            function mostrarFileUpload() {
                divElementos.fadeIn(300);
                divFileUpload.fadeIn(300);
                divLink.fadeOut(300);
            }

            function mostrarSoloLink() {
                divElementos.fadeIn(300);
                divFileUpload.fadeOut(300);
                divLink.fadeIn(300);
            }

            function ocultarTodo() {
                divFileUpload.fadeOut(300);
                divLink.fadeOut(300);
            }

            // Ejecutar lógica inicial si ya está marcado uno
            if (imagenSi.prop("checked")) {
                videoSi.prop("checked", false);
                mostrarFileUpload();
            } else if (videoSi.prop("checked")) {
                imagenSi.prop("checked", false);
                mostrarSoloLink();
            } else {
                ocultarTodo();
            }

            // Cambio a Imagen
            imagenSi.change(function () {
                if ($(this).prop("checked")) {
                    videoSi.prop("checked", false);
                    mostrarFileUpload();
                    txtLink.val(""); // Limpia el link si se cambia a imagen
                    fdlImagen.val(""); // Limpia la imagen si se cambia a imagen
                }
            });

            // Cambio a Video
            videoSi.change(function () {
                if ($(this).prop("checked")) {
                    imagenSi.prop("checked", false);
                    mostrarSoloLink();
                    fdlImagen.val(""); // Limpia la imagen si se cambia a video
                }
            });
        }




    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label></div>

            <div class="col-12 mb-0">
                <label class="form-label fw-bold">Seleccione el tipo de medio:</label>
            </div>

            <div class="col-12 d-flex gap-4">
                <!-- Opción Imagen -->
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="esImagenSi" runat="server" GroupName="esImagen" CssClass="form-check-input" />
                    <label class="form-check-label" for="esImagenSi">Imagen</label>
                </div>

                <!-- Opción Video -->
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="esVideoSi" runat="server" GroupName="TipoPreopiedad" CssClass="form-check-input" />
                    <label class="form-check-label" for="esVideoSi">Video</label>
                </div>
            </div>

            <div id="divElementos" runat="server" style="display: none">
                <div class="row col-lg-12 col-md-12 col-xs-12" id="divFileUpload" runat="server" style="display: none">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Cargar Imagen </label>
                    </div>
                    <div class="col-lg-10 col-md-10 col-xs-12">
                        <asp:FileUpload ID="fldImagen" runat="server" OnClientAdded="addTitle" />

                    </div>
                </div>
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Descripción: </label>
                    </div>
                    <div class="col-lg-10 col-md-10 col-xs-12">
                        <asp:Label ID="lblInfoFrontis" runat="server" CssClass="text-muted medium" Text="* Si desea que esta imagen sea la principal, ingrese 'Frontis' como descripción."></asp:Label>
                        <WebControls:TextBox2 ID="txtDescripcion" runat="server" MaxLength="200" UpperCase="true" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                            ControlToValidate="txtDescripcion"
                            ValidateEmptyText="false"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Documento" />
                    </div>
                </div>

                <div class="row col-lg-12 col-md-12 col-xs-12" id="divLink" style="display: none" runat="server">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Link: </label>
                    </div>
                    <div class="col-lg-10 col-md-10 col-xs-12">
                        <WebControls:TextBox2 ID="txtLink" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Documento" />
                <WebControls:PushButton ID="PushButton2" runat="server" Text="Cerrar" CssClass="ButtonCerrar" OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
