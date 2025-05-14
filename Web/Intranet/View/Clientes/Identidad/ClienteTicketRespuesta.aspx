<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClienteTicketRespuesta.aspx.cs" Inherits="View_Clientes_Identidad_ClienteTicketRespuesta" %>

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
    </style>



</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">
                <label id="lblNumeroConsulta" runat="server" />
            </div>
            <fieldset class="border p-3 rounded">
                <legend class="w-auto px-2">Detalles de la Consulta</legend>
                <div class="form-group">
                    <b>
                        <label id="lblCreador" runat="server" />
                    </b>
                </div>
                <div class="form-group">
                    <b>
                        <label id="lblFechaCreacion" runat="server" />
                    </b>
                </div>
                <div class="form-group">
                    <label id="txtNombreMesaAyuda" runat="server"></label>
                </div>

                <div class="form-group">
                    <b>
                        <label id="lblModulo" runat="server" />
                    </b>
                </div>

                <div class="form-group">
                    <label id="txtOtroModulo" runat="server"></label>
                </div>

                <div class="form-group">
                    <label id="txtMensaje" runat="server"></label>
                </div>
            </fieldset>


            <fieldset class="border p-3 rounded mb-4" id="HistorialChat" runat="server">
                <legend class="w-auto px-2">Historial Ticket</legend>
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-7 col-md-7 col-xs-12">
                        <div id="chatContainer" class="chat-wrapper" runat="server">

                            <!-- Encabezado del chat -->
                            <div class="chat-header">
                                <div class="chat-title">Centro de Ayuda</div>
                                <div class="chat-status">Conectado a Soporte</div>
                            </div>

                            <!-- Cuerpo del chat (respuestas renderizadas con Literal) -->
                            <div class="chat-body" id="chatScrollArea">
                                <asp:Literal ID="chatTickets" runat="server" />
                            </div>

                            <!-- Barra de entrada de mensaje -->
                            <div class="chat-input-area" id="areaRespuesta" runat="server">
                                <asp:TextBox ID="txtRespuesta" runat="server" CssClass="chat-input" placeholder="Escribe tu mensaje..." TextMode="MultiLine" Rows="2" />
                                <asp:Button ID="btnEnviar" runat="server" CssClass="chat-send-button" Text="Enviar" OnClick="btnEnviarRespuesta_Click" />
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-5 col-md-5 col-xs-12">
                        <fieldset class="border p-3 rounded" id="DetalleCierre" runat="server">
                            <legend class="w-auto px-2">Detalle del Cierre</legend>
                            <div class="row form-group">
                                <label id="lblObservacionCierre" runat="server"></label>
                            </div>
                            <div class="row form-group">
                                <label id="lblUsuarioCierre" runat="server"></label>
                            </div>
                            <div class="row form-group">
                                <label id="lblFechaCierre" runat="server"></label>
                            </div>
                        </fieldset>


                        <fieldset class="border p-3 rounded" id="CerrarTicket" runat="server">
                            <legend class="w-auto px-2">Cerrar Ticket</legend>
                            <div class="row form-group">
                                <label for="txtObservacionCierre" id="Label1" runat="server">Observación de Cierre <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtObservacionCierre" runat="server" CssClass="form-control" placeholder="Ingresa observación de cierre" />
                                <asp:CustomValidator ID="CustomValidator1" runat="server"
                                    ControlToValidate="txtObservacionCierre"
                                    ValidateEmptyText="true"
                                    Text="* obligatorio"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Cierre"
                                    CssClass="text-danger small" />
                            </div>
                            <div class="text-center mt-2">
                                <WebControls:PushButton ID="PushButton1" runat="server" Text="Cerrar Ticket" ValidationGroup="Cierre"
                                    OnClick="btnCerrarTicket_Click" />
                            </div>
                        </fieldset>
                    </div>
                </div>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
