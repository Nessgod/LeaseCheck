<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="MiCuenta.aspx.cs" Inherits="View_Root_Mantenedores_Usuarios_MiCuenta" %>

<asp:Content ID="Content3" ContentPlaceHolderID="chpScript" Runat="Server">
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

        function ValidaEmailFormat() {
            var txtCorreo = $('#<%=txtCorreo.ClientID %>');
            if (txtCorreo.val() != "") {
                if (!ValidaEmail(txtCorreo.val())) {
                    txtCorreo.val('');
                    AlertSweet('Formato correo invalido', '', 'alerta');
                }
            }
        }

        function ComparaPass() {
            var txtPassword = $('#<%=txtPassword.ClientID %>');
            var txtConfirmacionPassword = $('#<%=txtValidaPassword.ClientID %>');

            var btnGuardar = $('#<%=btnGuardar.ClientID %>');

            if (txtPassword.val() != "" & txtConfirmacionPassword.val() != "") {
                if (txtPassword.val() != txtConfirmacionPassword.val()) {
                    txtConfirmacionPassword.val('');
                    AlertSweet('Las contraseñas no coinciden.', '', 'alerta');
                    btnGuardar.attr("disabled", true);
                    $('#<%=txtValidaPassword.ClientID %>').focus();
                }
                else {
                    btnGuardar.attr("disabled", false);
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Mi Cuenta</div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="col-lg-2 col-md-2 col-xs-2"> 
                    <label>Login</label>
                </div>
	            <div class="col-lg-10 col-md-10 col-xs-10"> 
                    <asp:Label ID="lblLogin" runat="server" />  
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Nombres(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:Textbox2 ID="TextNombre" runat="server"/>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="TextNombre" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Apellido Paterno(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:Textbox2 ID="txtPaterno" runat="server" />
                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ControlToValidate="txtPaterno" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
                        
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Apellido Materno(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:Textbox2 ID="TextMaterno" runat="server"/>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" 
                        ControlToValidate="TextMaterno" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Email(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:Textbox2 ID="txtCorreo" runat="server" onblur="ValidaEmailFormat()"/>
                    <asp:CustomValidator ID="CustomValidator7" runat="server" 
                        ControlToValidate="txtCorreo" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
                    <label>Password(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
                    <WebControls:Textbox2 ID="txtPassword" runat="server" />
                    <asp:CustomValidator ID="CustomValidator5" runat="server" 
                        ControlToValidate="txtPassword" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" /> <label>Mínimo 8 caracteres, incluir números y mayúsculas</label>
	            </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Validar Password(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12" style="width:200px !important"> 
		            <WebControls:TextBox2 ID="txtValidaPassword" runat="server" onblur="ComparaPass()"/>
                    <asp:CustomValidator ID="CustomValidator6" runat="server" 
                        ControlToValidate="txtValidaPassword" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
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