<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="ClienteTickets.aspx.cs" Inherits="View_Clientes_Identidad_ClienteTickets" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>
<asp:Content ID="ContenHeder" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" language="javascript">

        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClienteTicket.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }


    </script>

    <style>
        .contact-icon {
            max-width: 60px;
            transition: transform 0.2s ease;
        }

            .contact-icon:hover {
                transform: scale(1.1);
            }
    </style>
</asp:Content>




<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="container">
                <!-- Botón Mis Tickets alineado a la derecha -->
                <div class="d-flex justify-content-start mb-3">
                    <asp:LinkButton ID="lnkAbirTickets" runat="server" OnClick="lnkAbrirTickets_Click"
                        CssClass="btn btn-outline-primary d-flex align-items-center gap-2" ToolTip="Ver mis tickets">
            <i class="fas fa-ticket-alt"></i>
            <span>Mis Tickets</span>
                    </asp:LinkButton>
                </div>
                <!-- Título principal -->
                <h2 class="text-center">Mesa de Ayuda</h2>

                <!-- Subtítulo -->
                <div class="text-center">
                    <h5 class="text-muted">También puedes contactarnos a través de:</h5>
                </div>

                <!-- Contacto: WhatsApp y Teléfono -->
                <div class="row justify-content-center mb-2 text-center">
                    <div class="col-12 col-md-6 mb-2">
                        <a href="https://api.whatsapp.com/send?phone=56950199884" target="_blank" class="d-block text-decoration-none text-dark">
                            <asp:Image ID="imgWsp" runat="server" ImageUrl="~/Imagen/whatsappp.png" CssClass="contact-icon" />
                            <div class="mt-2">
                                <asp:Label ID="lnlNumero" runat="server" Text="+56950199884"></asp:Label>
                            </div>
                        </a>
                    </div>
                    <div class="col-12 col-md-6 mb-3">
                        <a href="tel:56228620047" class="d-block text-decoration-none text-dark">
                            <asp:Image ID="imgLlamada" runat="server" ImageUrl="~/Imagen/llamada.png" CssClass="contact-icon" />
                            <div class="mt-2">
                                <asp:Label ID="lblNumerollamada" runat="server" Text="+56228620047"></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>

                <!-- Formulario -->
                <div class="card p-4 shadow-sm">
                    <div class="form-group">
                        <label for="txtNombreMesaAyuda">Nombre <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtNombreMesaAyuda" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" />
                        <asp:CustomValidator ID="CustomValidator3" runat="server"
                            ControlToValidate="txtNombreMesaAyuda"
                            ValidateEmptyText="true"
                            Text="* obligatorio"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="MesaAyuda"
                            CssClass="text-danger small" />
                    </div>

                    <div class="form-group">
                        <label for="cboModuloSistema"><strong>Selecciona el Modulo <span class="text-danger">*</span></strong></label>
                        <rad:RadComboBox2 ID="cboModuloSistema" runat="server" Width="100%" Filter="Contains" />
                    </div>

                    <div class="form-group">
                        <label for="txtMensaje">Otro Modulo</label>
                        <asp:TextBox ID="txtOtroModulo" runat="server" CssClass="form-control" TextMode="MultiLine"
                            placeholder="Otro..." Height="120px" MaxLength="500" />

                    </div>

                    <div class="form-group">
                        <label for="txtMensaje">Mensaje <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" TextMode="MultiLine"
                            placeholder="Describe tu problema o consulta" Height="120px" MaxLength="500" />
                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                            ControlToValidate="txtMensaje"
                            ValidateEmptyText="true"
                            Text="* obligatorio"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="MesaAyuda"
                            CssClass="text-danger small" />
                    </div>

                    <div class="text-center mt-2">
                        <asp:Button ID="btnEnviarConsulta" runat="server" Text="Enviar" ValidationGroup="MesaAyuda"
                            OnClick="btnEnviarConsulta_Click" CssClass="btn btn-primary w-50" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
