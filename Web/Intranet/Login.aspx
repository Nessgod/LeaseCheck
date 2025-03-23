<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>LeaseCheck</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="Imagen/favicon_leasecheck.png">

    <!-- Bootstrap Css -->
    <link href="Css/Login/assets/css/bootstrap.min.css" id="bootstrap-stylesheet" rel="stylesheet" type="text/css" />
    <!-- Icons Css -->
    <link href="Css/Login/assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <!-- App Css-->
    <link href="Css/Login/assets/css/app.min.css?vrs=1" id="app-stylesheet" rel="stylesheet" type="text/css" />


    <!-- SweetAlert CSS -->
    <link href="Css/SweetAlert/sweetalert.css" rel="stylesheet" />
    <link href="Css/SweetAlert/sweetalert2.min.css" rel="stylesheet" />

    <script src="Js/jquery-1.11.3.min.js"></script>
    <script src="Css/SweetAlert/sweetalert2.min.js"></script>

    <script src="Css/Login/assets/js/vendor.min.js"></script>

    <%--        <!-- App js -->
        <script src="Css/Login/assets/js/app.min.js"></script>--%>

    <script src="Js/Library.js?V1"></script>
    <script>
        function ValidaEmailFormat() {
            var txtCorreo = $('#<%=txtCorreo.ClientID %>');
            if (txtCorreo.val() != "") {
                if (!ValidaEmail(txtCorreo.val())) {
                    txtCorreo.val('');
                    AlertSweet('Formato correo invalido', '', 'alerta');
                }
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;

            return false;
        }

        function ValidaEmailFormat() {
            var txtCorreo = $('#<%=txtCorreoComercial.ClientID %>');
            if (txtCorreo.val() != "") {
                if (!ValidaEmail(txtCorreo.val())) {
                    txtCorreo.val('');
                    AlertSweet('Formato correo invalido', '', 'alerta');
                }
            }
        }
    </script>
    <style>
        body {
            padding-bottom: 150px !important;
        }

            body.authentication-bg {
                position: relative;
                background-image: url(Css/Login/assets/images/big/fondo-inmobilaria.jpg) !important;
                background-size: cover;
                background-position: center;
            }

                body.authentication-bg::before {
                    content: "";
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background: rgba(0, 0, 0, 0.5); 
                    backdrop-filter: blur(1px); 
                    z-index: 0;
                }


        .btn-primary {
            background-color: #04377c;
            border-color: #04377c;
        }

            .btn-primary:hover {
                background-color: #04377c;
                border-color: #04377c;
            }

            .btn-primary:focus, .btn-primary.focus {
                background-color: #04377c;
                border-color: #04377c;
                -webkit-box-shadow: 0 0 0 0.15rem rgba(150, 4, 202, 0.5);
                box-shadow: 0 0 0 0.15rem rgba(150, 4, 202, 0.5);
            }

        .navigation-menu li a {
            white-space: nowrap;
        }

        .white-text {
            color: #fff !important;
        }

        .responsive-logo {
            width: 400px;
        }

        @media (max-width: 768px) {
            .responsive-logo {
                width: 280px;
            }
        }
    </style>
</head>

<body class="authentication-bg">
    <form id="form" runat="server">
        <div class="topbar-menu">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-10 col-md-9 col-sm-8 col-xs-7 col-7">
                        <nav class="navbar navbar-expand-lg navbar-light col-md-9 col-sm-8 col-xl-8">

                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="navbar-toggler-icon"></span>
                            </button>

                            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                <ul class="navigation-menu">
                                    <li class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkPostulante" runat="server" OnClick="lnkPostulante_Click" CssClass="white-text">
                                        <i class=" dripicons-user"></i>Soy Cliente<div class="arrow-down"></div>
                                        </asp:LinkButton>
                                    </li>


                                    <li class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkEmpresa" runat="server" OnClick="lnkEmpresa_Click" CssClass="white-text"><i class="dripicons-store"></i>Soy Empresa<div class="arrow-down"></div>
                                        </asp:LinkButton>
                                    </li>

                                    <li class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkSoporte" runat="server" OnClick="lnkSoporte_Click" CssClass="white-text"><i data-icon="~"></i>Necesito Ayuda<div class="arrow-down"></div>
                                        </asp:LinkButton>
                                    </li>

                                    <li class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="lnkComercial" runat="server" OnClick="lnkComercial_Click" CssClass="white-text"><i class="dripicons-user-id"></i>Comercial<div class="arrow-down"></div>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </nav>
                        <!-- Navigation Menu-->

                        <!-- End navigation menu -->

                        <div class="clearfix"></div>
                    </div>
                    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

                </div>
                <!-- end #navigation -->
            </div>
            <!-- end container -->
        </div>
        <!-- end navbar-custom -->

        <div class="account-pages mt-5 mb-5">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="text-center" style="margin-bottom: 20px">
                            <img src="Imagen/LeaseCheck_logo_Blanco.png?v1" alt="" class="logo-dark mx-auto responsive-logo">
                        </div>

                        <div class="card">

                            <div class="card-body p-4">

                                <div id="Acceso" style="display: none" runat="server">
                                    <div class="text-center mb-4">
                                        <h4 class="text-uppercase mt-0">INICIAR SESIÓN</h4>
                                    </div>
                                    <div class="form-group">
                                        <label for="fullname">Usuario</label>
                                        <asp:TextBox ID="txtLogin" runat="server" class="form-control" placeholder="Ingresa tu usuario" />
                                    </div>
                                    <div class="form-group">
                                        <label for="fullname">Contraseña</label>
                                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Ingresa tu contraseña" TextMode="Password" />
                                    </div>
                                    <div class="form-group mb-0 text-center">
                                        <asp:Button ID="btnLoginEmpresa" runat="server" Text="Ingresar" OnClick="btnLoginEmpresa_Click" class="btn btn-primary btn-block" />
                                    </div>
                                    <div class="form-group mb-0 text-center">
                                        <br />
                                        <asp:Label ID="lblMensaje" runat="server" Style="color: red" />
                                    </div>
                                </div>

                                <div id="Postulante" runat="server">
                                    <div class="text-center mb-4">
                                        <h4 class="text-uppercase mt-0">Postulante</h4>
                                    </div>
                                    <div class="form-group">
                                        <label for="fullname">Código</label>
                                        <asp:TextBox ID="txtCodigoPostulante" runat="server" class="form-control" placeholder="Ingresa tu código" />
                                    </div>
                                    <%--  <div class="form-group mb-0 text-center">
                                        <asp:Button ID="btnLoginPostulante" runat="server" Text="Ingreso" OnClick="btnLoginPostulante_Click" class="btn btn-primary btn-block" />
                                    </div>--%>
                                    <div class="form-group mb-0 text-center">
                                        <br />
                                        <asp:Label ID="lblMensajePostulante" runat="server" Style="color: red" />
                                    </div>
                                </div>

                                <div id="Soporte" style="display: none;" runat="server">
                                    <div class="text-center mb-4">
                                        <h4 class="text-uppercase mt-0">Soporte</h4>
                                    </div>
                                    <div class="form-group mb-3">
                                        <label for="emailaddress">Nombre(*)</label>
                                        <asp:TextBox ID="txtNombreMesaAyuda" runat="server" class="form-control" placeholder="Ingresa tu nombre" onkeypress="return isNumberKey(event)" />
                                        <asp:CustomValidator ID="CustomValidator3" runat="server"
                                            ControlToValidate="txtNombreMesaAyuda"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="MesaAyuda" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label>Telefono(*)</label>
                                        <br />
                                        <rad:RadNumericBox2 ID="txtTelefono" runat="server" class="form-control" placeholder="Ej. 9 928 388 49" NumberFormat-GroupSeparator="" MaxLength="9" Style="text-align: left" Width="100%" />
                                        <br />
                                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                                            ControlToValidate="txtTelefono"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="MesaAyuda" />
                                    </div>
                                    <div class="form-group mb-3">
                                        <label>Correo(*)</label>
                                        <asp:TextBox ID="txtCorreo" runat="server" class="form-control" placeholder="Ej. example@gmail.com" onblur="ValidaEmailFormat()" />
                                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                                            ControlToValidate="txtCorreo"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="MesaAyuda" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label>Mensaje(*)</label>
                                        <asp:TextBox ID="txtMensaje" runat="server" class="form-control" placeholder="Describe tu problema o consulta" TextMode="MultiLine" Height="200px" MaxLength="500" />
                                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                                            ControlToValidate="txtMensaje"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="MesaAyuda" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <asp:Button ID="btnEnviarConsulta" runat="server" Text="Enviar" ValidationGroup="MesaAyuda" OnClick="btnEnviarConsulta_Click" class="btn btn-primary btn-block" />
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <a href="https://api.whatsapp.com/send?phone=56950199884" alt="whatsapp" style="text-align: center;">
                                                <asp:Image ID="imgWsp" ImageUrl="~/Imagen/whatsappp.png" runat="server" />
                                                <asp:Label ID="lnlNumero" runat="server" Text="+56950199884"></asp:Label>
                                            </a>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <a href="tel:56228620047" alt="Telefono" style="margin-left: -12px; text-align: center;">
                                                <asp:Image ID="imgLlamada" ImageUrl="~/Imagen/llamada.png" runat="server" />
                                                <asp:Label ID="lblNumerollamada" runat="server" Text="+56228620047"></asp:Label>
                                            </a>
                                        </div>

                                    </div>
                                </div>

                                <div id="Comercial" style="display: none;" runat="server">
                                    <div class="text-center mb-4">
                                        <h4 class="text-uppercase mt-0">Comercial</h4>
                                    </div>
                                    <div class="form-group mb-3">
                                        <label for="emailaddress">Nombre(*)</label>
                                        <asp:TextBox ID="txtNombreComercial" runat="server" class="form-control" placeholder="Ingresa tu Nombre" onkeypress="return isNumberKey(event)" />
                                        <asp:CustomValidator ID="CustomValidator5" runat="server"
                                            ControlToValidate="txtNombreComercial"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label for="emailaddress">Empresa(*)</label>
                                        <asp:TextBox ID="txtEmpresa" runat="server" class="form-control" placeholder="Ingresa tu Empresa" onkeypress="return isNumberKey(event)" />
                                        <asp:CustomValidator ID="CustomValidator9" runat="server"
                                            ControlToValidate="txtEmpresa"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label for="emailaddress">Cargo(*)</label>
                                        <asp:TextBox ID="txtCargo" runat="server" class="form-control" placeholder="Ingresa tu Cargo" onkeypress="return isNumberKey(event)" />
                                        <asp:CustomValidator ID="CustomValidator10" runat="server"
                                            ControlToValidate="txtCargo"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label>Telefono(*)</label>
                                        <br />
                                        <rad:RadNumericBox2 ID="txtTelefonoComercial" runat="server" class="form-control" placeholder="Ej. 9 928 388 49" NumberFormat-GroupSeparator="" MaxLength="9" Style="text-align: left" Width="100%" />

                                        <asp:CustomValidator ID="CustomValidator6" runat="server"
                                            ControlToValidate="txtTelefonoComercial"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>
                                    <div class="form-group mb-3">
                                        <label>Correo(*)</label>
                                        <asp:TextBox ID="txtCorreoComercial" runat="server" class="form-control" placeholder="Ej. example@gmail.com" onblur="ValidaEmailFormat()" />
                                        <asp:CustomValidator ID="CustomValidator7" runat="server"
                                            ControlToValidate="txtCorreoComercial"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <label>Mensaje(*)</label>
                                        <asp:TextBox ID="txtMensajeComercial" runat="server" class="form-control" TextMode="MultiLine" Height="200px" MaxLength="500" />
                                        <asp:CustomValidator ID="CustomValidator8" runat="server"
                                            ControlToValidate="txtMensajeComercial"
                                            ValidateEmptyText="true"
                                            Text="*obligatorio"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Comercial" />
                                    </div>

                                    <div class="form-group mb-3">
                                        <asp:Button ID="btnEnviarComercial" runat="server" Text="Ingresar" ValidationGroup="Comercial" OnClick="btnEnviarComercial_Click" class="btn btn-primary btn-block" />
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <a href="https://api.whatsapp.com/send?phone=56950199884" alt="whatsapp" style="text-align: center;">
                                                <asp:Image ID="Image1" ImageUrl="~/Imagen/whatsappp.png" runat="server" />
                                                <asp:Label ID="Label1" runat="server" Text="+56950199884"></asp:Label>
                                            </a>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <a href="tel:56228620047" alt="Telefono" style="margin-left: -12px; text-align: center;">
                                                <asp:Image ID="Image2" ImageUrl="~/Imagen/llamada.png" runat="server" />
                                                <asp:Label ID="Label2" runat="server" Text="+56228620047"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card -->
                        <!-- end row -->

                    </div>
                    <!-- end col -->
                </div>
                <!-- end row -->
            </div>
            <!-- end container -->
        </div>
        <!-- end page -->
    </form>

    <!-- Vendor js -->

</body>
</html>
