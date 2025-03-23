<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Pais.aspx.cs" Inherits="View_Root_Mantenedores_Paises_Pais" %>

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

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>            
            <div class="SubTitulos">País</div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Nombre(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtNombre" runat="server" MaxLength="200" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                        ControlToValidate="txtNombre"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Pais" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Diferencia(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <asp:RadioButtonList ID="rbtDiferencia" runat="server" RepeatDirection="Horizontal" Width="100px" Font-Size="Larger">
                        <asp:ListItem Text="+" Value="1"></asp:ListItem>
                        <asp:ListItem Text="-" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rbtValidator1" ErrorMessage="Seleccione diferencia horaria"
                        ControlToValidate="rbtDiferencia" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="Pais" />
                </div>
            </div>            
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Hora(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadNumericBox2 ID="txtHora" runat="server" MinValue="0" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                        ControlToValidate="txtHora"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Pais" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Habilitado</label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-12">
                    <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" />
                    <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">     
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Imagen</label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-12">
                     <asp:FileUpload ID="fldImagen" runat="server" />
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" 
                    ValidationGroup="Pais" 
                    OnClick="btnGuardar_Click" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" CssClass="ButtonCerrar"
                    OnClientClick="closeWindow();" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
