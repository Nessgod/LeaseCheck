<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Cliente.aspx.cs" Inherits="View_Clientes_Identidad_Cliente" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="chpScript" runat="server">
     <script type="text/javascript" language="javascript">

        //Valido el correo
        function ValidaEmailFormat() {
            var txtCorreo = $('#<%=txtEmail.ClientID %>');
            if (txtCorreo.val() != "") {
                if (!ValidaEmail(txtCorreo.val())) {
                    txtCorreo.val('');
                    alert('Formato correo invalido');
                }
            }
         }

         //Valida el Rut
        function validaRut() {
            var numero = document.getElementById('<%=txtRut.ClientID %>');
            var dv = document.getElementById('<%=txtDv.ClientID %>');

            if (numero.value == "")
                numero.className = "ErrorControl";

            if(dv.value != ""){
                var FotmateaRut = validarRut(numero.value, dv.value);
                if (!FotmateaRut) {
                    alert("Rut Incorrecto");
                    numero.value = "";
                    dv.value = "";
                    numero.className = "ErrorControl";
                    dv.className = "ErrorControl";
                }
            }
         }

        $(document).ready(function () {
            MuestroImagane();
        });

        function MuestroImagane() {
            var imgLogo = $('#<%=imgLogo.ClientID %>');

            if (imgLogo.attr('src') == "")
                imgLogo.hide();
            else
                imgLogo.show();
        }
       
    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Identidad</div>
            
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-2"> 
                    <asp:Image ID="imgLogo" runat="server" CssClass="rounded-circle img-thumbnail avatar-md"    />
                </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-10"> 
                           
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-2"> 
                    <label>ID</label>
                </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-10"> 
                    <asp:Label ID="lblID" runat="server" />  
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Nombre</label>
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
		            <label>Giro</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtGiro" runat="server" />
                    <asp:CustomValidator ID="CustomValidator4" runat="server" 
                        ControlToValidate="txtGiro" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Rut</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtRut" runat="server" Width="120px" MaxLength="15" ValidaMaxLength="false"/>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtRut" FilterType="Numbers" />
                    <asp:CustomValidator ID="CustomValidator13" runat="server" 
                        ControlToValidate="txtRut" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
                    -
                    <WebControls:TextBox2 ID="txtDv" Width="35px" runat="server" onblur="validaRut()" MaxLength="1" />
                    <asp:CustomValidator ID="CustomValidator14" runat="server" 
                        ControlToValidate="txtDv" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Alias</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtAlias" runat="server" />
                    <asp:CustomValidator ID="CustomValidator5" runat="server" 
                        ControlToValidate="txtAlias" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Pais</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12">
		            <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" />
	            </div>
            </div>

              <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Comuna</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12">
		            <rad:RadComboBox2 ID="cboComuna" runat="server" OnLoad="LoadControls" />
	            </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Dirección</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtDireccion" runat="server" />
                    <asp:CustomValidator ID="CustomValidator7" runat="server" 
                        ControlToValidate="txtDireccion" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Email</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtEmail" runat="server" onblur="ValidaEmailFormat()" />
                    <asp:CustomValidator ID="CustomValidator8" runat="server" 
                        ControlToValidate="txtEmail" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Teléfono</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtTelefono" runat="server" />
                    <asp:CustomValidator ID="CustomValidator9" runat="server" 
                        ControlToValidate="txtTelefono" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
        <%--    <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Logo Empresa</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <asp:FileUpload ID="fudFoto" runat="server" />  
	            </div>
            </div>--%>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Nombre Contacto</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtContactoNombre" runat="server" />
                    <asp:CustomValidator ID="CustomValidator10" runat="server" 
                        ControlToValidate="txtTelefono" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Email Contacto</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtContactoEmail" runat="server" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Teléfono Contacto</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtContactoTelefono" runat="server" />
	            </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12">
                    <label>Cliente Demo?</label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-12">
                    <asp:Label ID="chkDemo" runat="server" />
                </div>
            </div>

           <%-- <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
            </div>--%>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>