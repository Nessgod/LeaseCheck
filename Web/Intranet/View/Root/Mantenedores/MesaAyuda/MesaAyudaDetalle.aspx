<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="MesaAyudaDetalle.aspx.cs" Inherits="View_Root_Mantenedores_MesaAyuda_MesaAyudaDetalle" %>

<asp:Content ID="ContenHead" ContentPlaceHolderID="chpScript" runat="server">
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

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Consulta nº <asp:Label ID="lblId" runat="server"></asp:Label></div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Nombre</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextBox2 ID="txtNombre" runat="server" ReadOnly/>
                   
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Correo</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextBox2 ID="txtCorreo" runat="server" ReadOnly/>
                   
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Teléfono</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextBox2 ID="txtTelefono" runat="server" ReadOnly/>
                   
                </div>
            </div>

             <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Fecha Consulta</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextBox2 ID="txtFechaConsulta" runat="server" ReadOnly/>
                   
                </div>
            </div>

             <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Consulta</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextArea2 ID="txtConsulta" runat="server" ReadOnly/>
                   
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Respuesta(*)</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextArea2 id="txtRespuesta" runat="server" TextMode="MultiLine" MaxLength="200"/>
                    <asp:CustomValidator ID="CustomValidator" runat="server"
                        ControlToValidate="txtRespuesta"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Respuesta" />
                </div>
            </div>

            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Cerrar consulta" OnClick="btnGuardar_Click" ValidationGroup="Respuesta" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Volver" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>