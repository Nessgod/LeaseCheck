<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="View_Sistema_Usuarios_Usuario" %>



<asp:Content ID="ContenHead" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" language="javascript">
        //Cierra el RadWindow"
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
            MuestroImagane();
        });

        function MuestroImagane() {
            var imgLogo = $('#<%=imgLogo.ClientID %>');

            if (imgLogo.attr('src') == "")
                imgLogo.hide();
            else
                imgLogo.show();
        }

        //Valido el correo
        function ValidaEmailFormat() {
            var txtCorreo = $('#<%=txtCorreo.ClientID %>');
            if (txtCorreo.val() != "") {
                if (!ValidaEmail(txtCorreo.val())) {
                    txtCorreo.val('');
                    AlertSweet('Formato de correo invalido', '', 'alerta');
                }
            }
        }
        //Validar Numero de telefono

        function validarNumero() {
            var numeroTelefono = document.getElementById('<%=txtFono.ClientID%>');
            var expresionRegular1 = /^([0-9]+){9}$/;//<--- con esto vamos a validar el numero
            var expresionRegular2 = /\s/;//<--- con esto vamos a validar que no tenga espacios en blanco

            if (numeroTelefono.value == '')
                alert('campo es obligatorio');
            else if (expresionRegular2.test(numeroTelefono.value))
                alert('error existen espacios en blanco');
            else if (!expresionRegular1.test(numeroTelefono.value))
                alert('Numero de telefono incorrecto');
        }

        $(document).ready(function () {
            // Obtén las referencias a los controles
            var $rut = $('#<%=txtRUT.ClientID %>');
               var $nombre = $('#<%=TextNombre.ClientID %>');
       var $password = $('#<%=txtPassword.ClientID %>');

       // Función para actualizar el password
       function generarPassword() {
           var rut = $rut.val().replace(/\./g, '').replace('-', ''); // Elimina puntos y guión
           var nombre = $nombre.val().toUpperCase(); // Convierte nombre a mayúsculas

           if (rut.length >= 5 && nombre.length >= 3) {
               var primerosNumeros = rut.substring(0, 5);
               var primerasLetras = nombre.substring(0, 3);
               var passwordGenerado = primerosNumeros + primerasLetras;
               $password.val(passwordGenerado);
           }
       }

       // Triggers para actualizar password al escribir
       $rut.on('keyup change', generarPassword);
       $nombre.on('keyup change', generarPassword);


               const $txtPassword = $('#<%=txtPassword.ClientID %>');
               const $toggle = $('#togglePassword');

               $toggle.on('click', function () {
                   const input = $txtPassword[0];
                   if (input.type === 'password') {
                       input.type = 'text';
                       $toggle.text('🙈');
                   } else {
                       input.type = 'password';
                       $toggle.text('👁️');
                   }
               });
        });

    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <asp:HiddenField ID="hfdUsuarioID" runat="server" />

    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="SubTitulos">
                <asp:Label ID="lblTituloUsuario" runat="server" />
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="col-lg-2 col-md-2 col-xs-2">
                    <asp:Image ID="imgLogo" runat="server" CssClass="rounded-circle img-thumbnail avatar-md" />
                </div>
                <div class="col-lg-10 col-md-10 col-xs-10">
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="col-lg-2 col-md-2 col-xs-2">
                    <label>ID</label>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-10">
                    <asp:Label ID="lblID" runat="server" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>RUT(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtRUT" runat="server" PlaceHolder="Formato: 12.345.678-9" />
                    <asp:CustomValidator ID="CustomValidator12" runat="server"
                        ControlToValidate="txtRUT"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Nombres(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="TextNombre" runat="server" />
                    <asp:CustomValidator ID="CustomValidator3" runat="server"
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
                    <WebControls:TextBox2 ID="txtPaterno" runat="server" />
                    <asp:CustomValidator ID="CustomValidator4" runat="server"
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
                    <WebControls:TextBox2 ID="TextMaterno" runat="server" />
                    <asp:CustomValidator ID="CustomValidator5" runat="server"
                        ControlToValidate="TextMaterno"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>LOGIN(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtLogin" runat="server" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                        ControlToValidate="txtLogin"
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
                    <div class="input-group">
                        <WebControls:TextBox2 ID="txtPassword" runat="server" />
                        <div class="input-group-append">
                            <span class="input-group-text" style="cursor: pointer;" id="togglePassword">👁️
                            </span>
                        </div>
                    </div>
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="txtPassword"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />

                    <!-- ✅ Aquí el mensaje que necesitas -->
                    <small class="form-text text-muted">Se generan los 4 primeros números del RUT y las 3 primeras letras del nombre automáticamente.</small>
                    <label>Mínimo 8 caracteres, incluir números y mayúsculas</label>
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Perfil(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboPerfil" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Email(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtCorreo" runat="server" onblur="ValidaEmailFormat()" />
                    <asp:CustomValidator ID="CustomValidator7" runat="server"
                        ControlToValidate="txtCorreo"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Genero(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboGenero" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Estado Civil(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboEstadoCivil" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Profesión(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboProfesion" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Pais(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Nacionalidad(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboNacionalidad" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Comuna(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <rad:RadComboBox2 ID="cboComuna" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Ciudad(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtCiudad" runat="server" />
                    <asp:CustomValidator ID="CustomValidator8" runat="server"
                        ControlToValidate="txtCiudad"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Calle(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtCalle" runat="server" />
                    <asp:CustomValidator ID="CustomValidator6" runat="server"
                        ControlToValidate="txtCalle"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Número de Propiedad(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtNumeroPropiedad" runat="server" />
                    <asp:CustomValidator ID="CustomValidator10" runat="server"
                        ControlToValidate="txtNumeroPropiedad"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Telefono(*)</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <WebControls:TextBox2 ID="txtFono" runat="server" />
                    <asp:CustomValidator ID="CustomValidator9" runat="server"
                        ControlToValidate="txtFono"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12 ">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Foto Perfil</label>
                </div>
                <div class="form-group col-lg-10 col-md-10 col-xs-12">
                    <asp:FileUpload ID="fudFoto" runat="server" />
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

            <asp:Panel ID="pnlPerfiles" runat="server" Visible="false">
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Perfil</label>
                    </div>
                    <div class="col-lg-10 col-md-10 col-xs-12">
                        <rad:RadListBox runat="server" ID="rlbPerfilesLeft" DataSortField="SortOrder"
                            Width="50%" Height="100px" AllowTransfer="true" TransferToID="rlbPerfilesRigth"
                            SelectionMode="Multiple" AllowTransferOnDoubleClick="true"
                            OnClientTransferring="transferringPerfiles">
                        </rad:RadListBox>

                        <rad:RadListBox runat="server" ID="rlbPerfilesRigth" DataSortField="SortOrder" Width="49%"
                            Height="100px" AutoPostBackOnReorder="true"
                            SelectionMode="Multiple" AllowTransferOnDoubleClick="true">
                        </rad:RadListBox>
                    </div>
                </div>

                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Usuario Creacion</label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-xs-12">
                        <asp:Label ID="lblUsuarioCreacion" runat="server" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-xs-12">
                        <asp:Label ID="lblFechaCreacion" runat="server" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <asp:Label ID="lblHostCreacion" runat="server" />
                    </div>
                </div>

                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Usuario Act.</label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-xs-12">
                        <asp:Label ID="lblUsuarioAct" runat="server" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-xs-12">
                        <asp:Label ID="lblFechaAct" runat="server" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <asp:Label ID="lblHostAct" runat="server" />
                    </div>
                </div>

                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-xs-12">
                        <label>Ultimo Login</label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-xs-12">
                        <asp:Label ID="lblUltimoLogin" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar"
                    ValidationGroup="Identidad"
                    OnClick="btnGuardar_OnClick" />
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" CssClass="ButtonCerrar"
                    OnClientClick="closeWindow();" />
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
