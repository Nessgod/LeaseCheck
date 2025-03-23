<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Feriado.aspx.cs" Inherits="View_Sistema_Feriado_Feriado" %>

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
            <div class="SubTitulos">Feriado</div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Fecha Feriado</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:Calendar ID="txtFecha" runat="server" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="txtFecha"
                        errormessage="Debe elegir una fecha"                        
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Descripción(*)</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <WebControls:TextBox2 id="txtDescripcion" runat="server" />
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="txtDescripcion"
                        errormessage="Ingrese una descripción"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-xs-12">
                    <label>Pais(*)</label>
                </div>
                <div class="col-lg-9 col-md-9 col-xs-12">
                    <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" Filter="Contains" Width="100%"  />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                        ControlToValidate="cboPais"
                        errormessage="seleccione un Pais"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            
           

            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>