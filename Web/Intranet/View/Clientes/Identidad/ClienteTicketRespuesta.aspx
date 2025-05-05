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
        .chat-container {
            max-height: 400px;
            overflow-y: auto;
            padding: 10px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .chat-message {
            margin-bottom: 15px;
            padding: 10px;
            border-radius: 5px;
        }

        .system-message {
            background-color: #e9f7ef;
            border: 1px solid #d4edda;
        }

        .client-message {
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
        }

        .message-header {
            font-size: 0.9em;
            color: #555;
            margin-bottom: 5px;
        }

        .message-author {
            font-weight: bold;
        }

        .message-date {
            float: right;
            font-style: italic;
        }

        .message-body {
            font-size: 1em;
            color: #333;
        }
    </style>

</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
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


            <div id="chatContainer" class="chat-container" runat="server">
                <asp:Literal ID="chatTickets" runat="server" />
            </div>


            <fieldset class="border p-3 rounded">
                <legend class="w-auto px-2">Responder</legend>
                <div class="form-group">
                    <label for="txtRespuesta">Respuesta <span class="text-danger">*</span></label>
                    <WebControls:TextArea2 ID="txtRespuesta" runat="server" CssClass="form-control" placeholder="Ingresar respuesta" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                        ControlToValidate="txtRespuesta"
                        ValidateEmptyText="true"
                        Text="* obligatorio"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="MesaAyuda"
                        CssClass="text-danger small" />
                </div>
                <div class="text-center mt-2">
                    <WebControls:PushButton ID="btnEnviarRespuesta" runat="server" Text="Responder" ValidationGroup="MesaAyuda"
                        OnClick="btnEnviarRespuesta_Click" />
                </div>

            </fieldset>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
