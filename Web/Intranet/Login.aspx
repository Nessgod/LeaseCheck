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
        body.authentication-bg {
            position: relative;
            background-image: url(Css/Login/assets/images/big/fondo-inmobilaria.jpg) !important;
            background-size: cover;
            background-position: center;
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
        /* Estilos del mensaje de error */
        .mensaje-error {
            color: #d90429;
            margin-top: 10px;
            background-color: #f8d7da;
            border: 0.5px solid #d90429;
            padding: 5px 30px;
            border-radius: 4px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 18px;
            position: relative;
            top: -50px;
            left: 50%;
            transform: translateX(-50%);
            opacity: 0;
            animation: fadeIn 0.5s forwards;
        }

        @keyframes fadeIn {
            0% {
                opacity: 0;
                transform: translateX(-50%) translateY(10px);
            }

            100% {
                opacity: 1;
                transform: translateX(-50%) translateY(0);
            }
        }

        .mensaje-error i {
            margin-right: 10px;
        }

        .mensaje-error.fade-out {
            animation: fadeOut 0.5s forwards;
        }

        @keyframes fadeOut {
            0% {
                opacity: 1;
            }

            100% {
                opacity: 0;
                transform: translateX(-50%) translateY(10px);
            }
        }


        .contenedor-logo {
            min-height: 150px;
            background: #FCF7F8 !important;
        }

            .contenedor-logo img {
                max-width: 150px;
            }

        .contenedor-login {
            min-height: 400px;
        }

        h4 {
            font-size: clamp(1.5rem, 4vw, 2rem); /* clamp(MIN, PREFERRED, MAX) */
        }



        @media (min-width: 768px) {
            .contenedor-logo {
                min-height: 600px;
            }

                .contenedor-logo img {
                    max-width: 100%;
                }

            .contenedor-login {
                min-height: 600px;
                width: 400px;
            }
        }

        .btn-custom {
            background-color: #003566;
            color: white;
            border-radius: 25px;
            border: none;
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            transition: all 0.3s ease;
        }

            .btn-custom:hover {
                background-color: white;
                color: #003566;
                border: 1px solid #003566;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }


        .modern-navbar {
            background: rgba(255, 255, 255, 0.05) !important;
            backdrop-filter: blur(10px);
            border-radius: 12px;
            padding: 1rem;
            margin-top: 1rem;
        }

        /* Estilo de los links */
        .nav-item .nav-link {
            color: #ffffff !important;
            font-weight: 500;
            font-size: 1rem;
            padding: 0.5rem 1rem;
            border-radius: 8px;
            transition: all 0.3s ease;
            display: flex;
            align-items: center;
            gap: 0.5rem;
            background: transparent;
        }

            .nav-item .nav-link:hover {
                background-color: white !important;
                color: #003566 !important;
                text-decoration: none;
            }

            .nav-item .nav-link i {
                font-size: 1.2rem;
            }

        .custom-toggler {
            background-color: transparent !important;
            border: none;
            padding: 0.5rem 1rem;
            cursor: pointer;
        }

            .custom-toggler i {
                font-size: 1.8rem;
                color: white;
                transition: color 0.3s ease, transform 0.3s ease;
            }

            .custom-toggler:hover i {
                color: #0767e9;
                transform: rotate(90deg);
            }

        .navbar-toggler[aria-expanded="true"] i {
            color: #fff;
            transform: rotate(90deg);
        }

        .navbar-toggler[aria-expanded="false"] i {
            transform: rotate(0deg);
        }
    </style>
</head>

<body class="authentication-bg">
    <form id="form" runat="server">
        <!-- Navbar -->
        <div class="topbar-menu">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <nav class="navbar navbar-expand-lg navbar-light modern-navbar">
                            <div class="container-fluid">
                                <!-- Botón de colapso para móviles -->
                                <button class="navbar-toggler custom-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                                    aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <i class="fas fa-bars"></i>
                                </button>

                                <!-- Contenido del navbar -->
                                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lnkPropietario" runat="server" CssClass="nav-link" OnClick="lnkPropietario_Click">
                                                <i class="dripicons-user me-2"></i> Soy Propietario
                                            </asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lnkEmpresa" runat="server" CssClass="nav-link" OnClick="lnkEmpresa_Click">
                                                 <i class="dripicons-store me-2"></i> Soy Empresa
                                            </asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lnkContactanos" runat="server" CssClass="nav-link" OnClick="lnkComercial_Click">
                                                <i class="dripicons-message me-2"></i> Contáctenos
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </div>

        <div class="mt-5 mb-5" id="DivAccesos" runat="server" style="display: block;">
            <div class="container-fluid d-flex justify-content-start align-items-center mt-3">

                <!-- Agrupador de logo y login -->
                <div class="d-flex flex-wrap shadow rounded overflow-hidden mt-4 ml-5">

                    <!-- Logo -->
                    <div class="bg-light d-flex align-items-center justify-content-center p-4 contenedor-logo">
                        <img src="Imagen/LeaseCheck_logo_Negro.png?v1" alt="" class="logo-dark mx-auto responsive-logo">
                    </div>

                    <!-- Login -->
                    <div class="bg-white p-4 d-flex align-items-center contenedor-login">
                        <div class="w-100">
                            <div class="form-group text-center mensaje-error" id="lblMensajeContenedor" visible="false" runat="server">
                                <i class="fas fa-exclamation-triangle"></i>
                                <asp:Label ID="lblMensaje" runat="server" />
                            </div>
                            <div id="Acceso" runat="server" style="display: none;">
                                <div class="text-center mb-4">
                                    <h4 class="text-uppercase">Inicio de Sesión</h4>
                                </div>
                                <div class="form-group mb-3">
                                    <label for="txtLogin" class="form-label">Login</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" style="color:#003566;">
                                                <i class="fas fa-user"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" placeholder="Ingresa tu usuario" />
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <label for="txtPassword" class="form-label">Password</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" style="color:#003566;">
                                                <i class="fas fa-lock"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Ingresa tu password" TextMode="Password" />
                                    </div>
                                </div>
                                <div class="form-group mb-3 text-center">
                                    <asp:Button ID="btnLoginEmpresa" runat="server" Text="Ingreso" OnClick="btnLoginEmpresa_Click" CssClass="btn-custom w-50" />
                                </div>
                            </div>

                            <div id="Propietario" runat="server">
                                <div class="text-center mb-4">
                                    <h4 class="text-uppercase">Inicio de Sesión</h4>
                                </div>
                                <div class="form-group">
                                    <label for="txtCodigoPostulante" class="form-label mb-0 mr-2">Código</label>
                                    <div class="input-group justify-content-center">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" style="color:#003566;">
                                                <i class="fas fa-key"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ingresa tu código" />
                                    </div>
                                </div>
                                <div class="form-group mb-0 text-center">
                                    <asp:Button ID="btnLoginPropietario" runat="server" Text="Ingreso" OnClick="btnLoginPropietario_Click" class="btn-custom w-50" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="container" id="DivSoporteComercial" runat="server" style="display: none;">
            <div class="row justify-content-left">
                <div class="col-md-8 col-lg-6 col-xl-5">
                    <div class="text-center" style="margin-bottom: 60px">
                    </div>

                    <div class="card">

                        <div class="card-body p-4">
                            <div id="Comercial" style="display: none;" runat="server">
                                <div class="text-center mb-2">
                                    <h4 class="text-uppercase mt-0">Comercial</h4>
                                </div>
                                <div class="row text-center mb-2">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        <a href="https://api.whatsapp.com/send?phone=56950199884" alt="whatsapp" style="text-align: center;">
                                            <asp:Image ID="Image1" ImageUrl="~/Imagen/whatsappp.png" runat="server" />
                                            <asp:Label ID="Label3" runat="server" Text="+56950199884"></asp:Label>
                                        </a>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        <a href="tel:56228620047" alt="Telefono" style="margin-left: -12px; text-align: center;">
                                            <asp:Image ID="Image2" ImageUrl="~/Imagen/llamada.png" runat="server" />
                                            <asp:Label ID="Label4" runat="server" Text="+56228620047"></asp:Label>
                                        </a>
                                    </div>
                                </div>
                                <div>
                                    <label for="emailaddress">Nombre(*)</label>
                                    <asp:TextBox ID="txtNombreComercial" runat="server" class="form-control" placeholder="Ingresa tu Nombre" onkeypress="return isNumberKey(event)" />
                                    <asp:CustomValidator ID="CustomValidator5" runat="server"
                                        ControlToValidate="txtNombreComercial"
                                        ValidateEmptyText="true"
                                        Text="*obligatorio"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Comercial" />
                                </div>

                                <div>
                                    <label for="emailaddress">Empresa(*)</label>
                                    <asp:TextBox ID="txtEmpresa" runat="server" class="form-control" placeholder="Ingresa tu Empresa" onkeypress="return isNumberKey(event)" />
                                    <asp:CustomValidator ID="CustomValidator9" runat="server"
                                        ControlToValidate="txtEmpresa"
                                        ValidateEmptyText="true"
                                        Text="*obligatorio"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Comercial" />
                                </div>

                                <div>
                                    <label for="emailaddress">Cargo(*)</label>
                                    <asp:TextBox ID="txtCargo" runat="server" class="form-control" placeholder="Ingresa tu Cargo" onkeypress="return isNumberKey(event)" />
                                    <asp:CustomValidator ID="CustomValidator10" runat="server"
                                        ControlToValidate="txtCargo"
                                        ValidateEmptyText="true"
                                        Text="*obligatorio"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Comercial" />
                                </div>

                                <div>
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
                                <div>
                                    <label>Correo(*)</label>
                                    <asp:TextBox ID="txtCorreoComercial" runat="server" class="form-control" placeholder="Ej. example@gmail.com" onblur="ValidaEmailFormat()" />
                                    <asp:CustomValidator ID="CustomValidator7" runat="server"
                                        ControlToValidate="txtCorreoComercial"
                                        ValidateEmptyText="true"
                                        Text="*obligatorio"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Comercial" />
                                </div>

                                <div>
                                    <label>Mensaje(*)</label>
                                    <asp:TextBox ID="txtMensajeComercial" runat="server" class="form-control" TextMode="MultiLine" Height="60px" MaxLength="500" />
                                    <asp:CustomValidator ID="CustomValidator8" runat="server"
                                        ControlToValidate="txtMensajeComercial"
                                        ValidateEmptyText="true"
                                        Text="*obligatorio"
                                        ClientValidationFunction="validaControl"
                                        ValidationGroup="Comercial" />
                                </div>


                                <!-- CAPTCHA -->

                                <div class="alert alert-warning col-lg-12 col-md-12 col-12 text-center">
                                    <div>
                                        <b>Para comprobar que no es un bot, resuelva la adición:</b>
                                        <b>
                                            <asp:Label ID="lblValorUnoContacto" runat="server"></asp:Label></b>
                                        <b>
                                            <asp:Label ID="lblValorDosContacto" runat="server"></asp:Label></b>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-12 align-content-center">
                                        <WebControls:TextBox2 runat="server" CssClass="form-control" ID="txtResultadoContacto" placeholder="Ingrese resultado" Style="resize: none;" TextMode="Number" />
                                        <asp:CustomValidator ID="CustomValidator11" runat="server"
                                            ControlToValidate="txtResultadoContacto"
                                            ValidateEmptyText="true"
                                            ClientValidationFunction="validaControl"
                                            ValidationGroup="Contacto" />
                                    </div>
                                </div>

                                <div class="text-center">
                                    <asp:Button ID="btnEnviarComercial" runat="server" Text="Ingresar" ValidationGroup="Comercial" OnClick="btnEnviarComercial_Click" class="btn btn-custom w-50" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <script>

            $(document).ready(function () {
                $(".navbar-toggler").click(function () {
                    $("#navbarSupportedContent").toggleClass("show");
                });

                function showMensaje() {
                    var mensaje = document.getElementById('lblMensajeContenedor');
                    if (mensaje) {
                        mensaje.style.visibility = 'visible';

                        setTimeout(function () {
                            mensaje.classList.add('fade-out');
                        }, 3000); // El mensaje se empieza a desvanecer después de 3 segundos

                        setTimeout(function () {
                            mensaje.style.visibility = 'hidden';
                            mensaje.classList.remove('fade-out');
                        }, 4000); // El mensaje desaparece completamente después de 4 segundos
                    } else {
                        console.error('El elemento lblMensajeContenedor no existe en el DOM.');
                    }
                }

                // Llamada a la función showMensaje para mostrar el mensaje
                showMensaje();
            });

            function ValidaEmailFormat() {
                var txtCorreo = $('#<%=txtCorreoComercial.ClientID %>');
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

            function ValidaEmail(email) {
                var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                return regex.test(email);
            }

            function ValidaDominioCorreo(email) {
                var dominiosNoPermitidos = ["@gmail.com", "@hotmail.com"];
                email = email.toLowerCase();

                for (var i = 0; i < dominiosNoPermitidos.length; i++) {
                    if (email.endsWith(dominiosNoPermitidos[i])) {
                        return false;
                    }
                }
                return true;
            }


        </script>
    </form>

    <!-- Vendor js -->

</body>
</html>
