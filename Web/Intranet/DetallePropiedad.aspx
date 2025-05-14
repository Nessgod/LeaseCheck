<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetallePropiedad.aspx.cs" Inherits="DetallePropiedad" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Portal - LeaseCheck</title>
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
    <script src="Js/Library.js?V1"></script>
    <script>

</script>
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db; /* celeste único */
            --accent-color: #e74c3c;
            --text-color: #2f3542;
            --light-gray: #f1f2f6;
            --dark-gray: #57606f;
            --gold: #ffd700;
        }

        body {
            font-family: 'Helvetica Neue', Arial, sans-serif;
            color: var(--text-color);
            background-color: #fff;
            line-height: 1.6;
            margin: 0;
            padding: 0;
        }

        /* Fondo del navbar */
        .navbar {
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.08);
        }

        /* Logo */
        .navbar-brand img {
            height: 40px;
        }

        /* Links del menú */
        .navbar-nav .nav-link {
            font-weight: 500;
            color: #333;
            margin-right: 15px;
            transition: color 0.3s;
        }

            .navbar-nav .nav-link:hover {
                color: #007bff; /* Azul Bootstrap */
            }

        /* Botones derechos */
        .navbar .btn {
            font-weight: 500;
            border-radius: 20px;
            padding: 6px 16px;
        }

        /* Botón "Iniciar Sesión" */
        .navbar .btn-outline-secondary {
            color: #007bff;
            border-color: #007bff;
        }

            .navbar .btn-outline-secondary:hover {
                background-color: #007bff;
                color: #fff;
            }

        /* Botón "Publicar Propiedad" */
        .navbar .btn-primary {
            background-color: #28a745;
            border-color: #28a745;
        }

            .navbar .btn-primary:hover {
                background-color: #218838;
                border-color: #1e7e34;
            }

        /* Responsive ajuste para móviles */
        @media (max-width: 991.98px) {
            .navbar-nav .nav-link {
                margin-right: 0;
                margin-bottom: 10px;
            }

            .navbar .d-flex {
                flex-direction: column;
                gap: 10px;
                margin-top: 15px;
            }
        }


        /* HERO */
        .hero {
            background: url('Imagen/banner.png') no-repeat center center;
            background-size: cover;
            transform-origin: center;
            color: white;
            text-align: center;
            padding: 150px 20px;
        }

            .hero h1 {
                font-size: 3.2em;
                font-weight: bold;
                margin-bottom: 20px;
                text-shadow: 0 2px 4px rgba(0,0,0,0.4);
            }

            .hero p {
                font-size: 1.4em;
                text-shadow: 0 1px 2px rgba(0,0,0,0.3);
            }

        /* SEARCH BOX */
        .search-box {
            background: white;
            margin-top: -50px;
            padding: 35px 30px;
            border-radius: 12px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            position: relative;
            z-index: 10;
        }

            .search-box .form-control {
                height: 45px;
                border-radius: 6px;
                border: 1px solid #ccc;
                margin-bottom: 10px;
            }

            .search-box .btn-primary {
                background-color: var(--secondary-color);
                border: none;
                font-weight: bold;
                padding: 10px 24px;
                font-size: 15px;
                border-radius: 6px;
                transition: background-color 0.3s ease;
            }

                .search-box .btn-primary:hover {
                    background-color: var(--secondary-color);
                    opacity: 0.9;
                }

        /* PROPERTY CARD */
        .property-card {
            background: white;
            border-radius: 12px;
            overflow: hidden;
            transition: all 0.3s ease;
            box-shadow: 0 3px 12px rgba(0,0,0,0.05);
        }

            .property-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 8px 18px rgba(0,0,0,0.1);
            }

        .property-image {
            width: 100%;
            height: 240px;
            object-fit: cover;
        }

        .property-details {
            padding: 20px;
        }

        .property-title {
            font-size: 1.3em;
            font-weight: 600;
            color: var(--primary-color);
            margin-bottom: 10px;
        }

        .property-meta {
            font-size: 0.95em;
            color: #7f8c8d;
            margin-bottom: 10px;
        }

        .property-price {
            font-size: 1.4em;
            color: var(--secondary-color);
            font-weight: 700;
        }

        .btn-custom {
            background-color: var(--secondary-color);
            color: white;
            border-radius: 6px;
            padding: 10px 20px;
            font-weight: 600;
            transition: background-color 0.3s ease;
            display: inline-block;
            margin-top: 10px;
        }

            .btn-custom:hover {
                background-color: var(--secondary-color);
                opacity: 0.9;
            }

        /* FEATURED PROPERTIES */
        .featured-badge {
            position: absolute;
            top: 15px;
            left: 15px;
            background-color: var(--gold);
            color: #000;
            font-size: 0.85em;
            font-weight: bold;
            padding: 6px 10px;
            border-radius: 4px;
            z-index: 2;
        }

        .property-card.featured {
            border: 2px solid var(--gold);
            box-shadow: 0 6px 20px rgba(255, 215, 0, 0.3);
        }

        /* CTA */
        .cta-full-width {
            background: var(--secondary-color);
            color: white;
            text-align: center;
            padding: 60px 20px;
        }

        .cta-title {
            font-size: 2em;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .cta-subtitle {
            font-size: 1.1em;
            margin-bottom: 20px;
            opacity: 0.95;
        }

        .btn-white {
            background: white;
            color: var(--secondary-color);
            border: none;
            font-weight: bold;
            padding: 10px 20px;
            border-radius: 6px;
            transition: all 0.3s ease;
        }

            .btn-white:hover {
                background: #e6e6e6;
                color: var(--secondary-color);
            }

        /* FOOTER */
        .footer-full-width {
            background-color: #0f172a;
            color: #cbd5e1;
            padding: 60px 0 30px;
        }

        .footer-wrapper {
            max-width: 1400px;
            width: 90%;
            margin: 0 auto;
        }

            .footer-wrapper h5,
            .footer-wrapper h4 {
                color: white;
                font-weight: bold;
                margin-bottom: 15px;
            }

        .footer-full-width ul {
            list-style: none;
            padding-left: 0;
        }

            .footer-full-width ul li {
                margin-bottom: 10px;
                font-size: 0.95em;
                color: #94a3b8;
            }

                .footer-full-width ul li a {
                    color: #94a3b8;
                    text-decoration: none;
                }

                    .footer-full-width ul li a:hover {
                        color: white;
                    }

        .footer-social a {
            font-size: 1.2em;
            color: #94a3b8;
            margin-right: 10px;
        }

            .footer-social a:hover {
                color: white;
            }

        .brand-footer {
            font-size: 1.5em;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .copyright {
            font-size: 0.9em;
            text-align: center;
            color: #64748b;
            margin-top: 30px;
        }
    </style>
</head>

<body class="authentication-bg">
    <form id="form" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container">
                <!-- LOGO -->
                <a class="navbar-brand" href="#">
                    <img src="Imagen/LeaseCheck_logo_Negro.png" alt="LeaseCheck" height="40">
                </a>

                <!-- BOTÓN TOGGLE PARA MÓVIL -->
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarContent"
                    aria-controls="navbarContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <!-- MENÚ -->
                <div class="collapse navbar-collapse" id="navbarContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item"><a class="nav-link" href="#">Inicio</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Propiedades</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Agentes</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Nosotros</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Contacto</a></li>
                    </ul>

                    <div class="d-flex">
                        <a href="login.aspx" class="btn btn-outline-secondary me-2">Iniciar Sesión</a>
                        <a href="#" class="btn btn-primary">Publicar Propiedad</a>
                    </div>
                </div>
            </div>
        </nav>
        <!-- Contenedor general -->
        <div class="container my-5">

            <!-- FILA PRINCIPAL: Imagen + Detalle -->
            <div class="row g-4">
                <!-- IMAGEN PRINCIPAL -->
                <div class="col-md-7">
                    <asp:Image ID="imgPrincipal" runat="server" CssClass="img-fluid rounded-4 shadow-sm w-100 mb-3" />
                </div>

                <!-- DETALLE PROPIEDAD -->
                <div class="col-md-5 p-4 bg-light rounded-4 shadow-sm">
                    <h2 class="fw-bold text-primary mb-1">
                        <i class="fas fa-home me-2"></i>
                        <asp:Label ID="lblTituloPropiedad" runat="server" />
                    </h2>
                    <h4 class="text-success mb-3">
                        <i class="fas fa-dollar-sign me-1"></i>
                        <asp:Label ID="lblPrecio" runat="server" />
                    </h4>

                    <p class="mb-2 text-muted">
                        <i class="fas fa-map-marker-alt text-danger me-2"></i>
                        <asp:Label ID="lblUbicacion" runat="server" />
                    </p>

                    <hr />

                    <div class="row small text-dark">
                        <div class="col-6">
                            <p><i class="fas fa-building me-1 text-secondary"></i>Tipo:
                                <asp:Label ID="lblTipoPropiedad" runat="server" /></p>
                            <p><i class="fas fa-box-open me-1 text-secondary"></i>Entrega:
                                <asp:Label ID="lblTipoEntrega" runat="server" /></p>
                            <p><i class="fas fa-bed me-1 text-secondary"></i>Dormitorios:
                                <asp:Label ID="lblDormitorios" runat="server" /></p>
                            <p><i class="fas fa-bath me-1 text-secondary"></i>Baños:
                                <asp:Label ID="lblBanos" runat="server" /></p>
                            <p><i class="fas fa-layer-group me-1 text-secondary"></i>Pisos:
                                <asp:Label ID="lblPisos" runat="server" /></p>
                        </div>
                        <div class="col-6">
                            <p><i class="fas fa-expand me-1 text-secondary"></i>Útiles:
                                <asp:Label ID="lblMetrosUtiles" runat="server" />
                                m²</p>
                            <p><i class="fas fa-ruler-combined me-1 text-secondary"></i>Totales:
                                <asp:Label ID="lblMetrosTotales" runat="server" />
                                m²</p>
                            <p><i class="fas fa-car me-1 text-secondary"></i>Estacionamiento:
                                <asp:Label ID="lblEstacionamiento" runat="server" /></p>
                            <p><i class="fas fa-warehouse me-1 text-secondary"></i>Bodega:
                                <asp:Label ID="lblBodega" runat="server" /></p>
                        </div>
                    </div>

                    <asp:Button ID="btnContactar" runat="server" Text="📩 Contactar Agente"
                        CssClass="btn btn-primary btn-lg rounded-pill mt-3 w-100 shadow" />
                </div>
            </div>

            <!-- CARACTERÍSTICAS ADICIONALES -->
            <div class="mt-5 p-4 bg-white rounded-4 shadow-sm border">
                <h5 class="fw-semibold mb-3"><i class="fas fa-tools me-2 text-info"></i>Características adicionales</h5>
                <ul class="list-inline text-muted small">
                    <li class="list-inline-item me-4"><i class="fas fa-grip-lines-vertical me-1"></i>Piso:
                        <asp:Label ID="lblTipoPiso" runat="server" /></li>
                    <li class="list-inline-item me-4"><i class="fas fa-window-restore me-1"></i>Ventanas:
                        <asp:Label ID="lblTipoVentana" runat="server" /></li>
                    <li class="list-inline-item me-4"><i class="fas fa-fire me-1"></i>Cocina:
                        <asp:Label ID="lblConexionCocina" runat="server" /></li>
                    <li class="list-inline-item me-4"><i class="fas fa-tint me-1"></i>Lavadora:
                        <asp:Label ID="lblConexionLavadora" runat="server" /></li>
                </ul>
                <ul class="list-inline mt-2 text-dark fw-semibold">
                    <li class="list-inline-item">
                        <asp:Label ID="lblQuincho" runat="server" /></li>
                    <li class="list-inline-item">
                        <asp:Label ID="lblPiscina" runat="server" /></li>
                    <li class="list-inline-item">
                        <asp:Label ID="lblCalefaccion" runat="server" /></li>
                    <li class="list-inline-item">
                        <asp:Label ID="lblGimnasio" runat="server" /></li>
                    <li class="list-inline-item">
                        <asp:Label ID="lblSalonMultiple" runat="server" /></li>
                </ul>
            </div>

            <!-- DESCRIPCIÓN -->
            <div class="mt-4 p-4 bg-light rounded-4 shadow-sm border">
                <h5 class="fw-semibold"><i class="fas fa-file-alt me-2 text-secondary"></i>Descripción</h5>
                <p class="text-secondary" style="white-space: pre-line;">
                    <asp:Label ID="lblDescripcion" runat="server" />
                </p>
            </div>

            <!-- ENTORNO -->
            <div class="mt-4 p-4 bg-white rounded-4 shadow-sm border">
                <h5 class="fw-semibold mb-3"><i class="fas fa-tree-city me-2 text-success"></i>Entorno</h5>
                <div class="row text-muted small">
                    <div class="col-md-6">
                        <p><i class="fas fa-bus me-1"></i>Transporte:
                            <asp:Label ID="lblTransporte" runat="server" /></p>
                        <p><i class="fas fa-lock me-1"></i>Seguridad:
                            <asp:Label ID="lblSeguridad" runat="server" /></p>
                        <p><i class="fas fa-utensils me-1"></i>Restaurantes:
                            <asp:Label ID="lblRestaurant" runat="server" /></p>
                        <p><i class="fas fa-leaf me-1"></i>Áreas verdes:
                            <asp:Label ID="lblAreasVerdes" runat="server" /></p>
                    </div>
                    <div class="col-md-6">
                        <p><i class="fas fa-school me-1"></i>Educación:
                            <asp:Label ID="lblEducacion" runat="server" /></p>
                        <p><i class="fas fa-hospital me-1"></i>Salud:
                            <asp:Label ID="lblSalud" runat="server" /></p>
                        <p><i class="fas fa-shopping-cart me-1"></i>Centro Comercial:
                            <asp:Label ID="lblCentroComercial" runat="server" /></p>
                        <p><i class="fas fa-wifi me-1"></i>Conectividad:
                            <asp:Label ID="lblConectividad" runat="server" /></p>
                    </div>
                </div>
            </div>

        </div>


        <!-- FOOTER full-width -->
        <footer class="footer-full-width">
            <div class="footer-wrapper row">
                <div class="col-sm-6">
                    <h4 class="brand-footer">🏠 LeaseCheck</h4>
                    <p>Tu socio de confianza para encontrar la propiedad perfecta para tus necesidades.</p>
                    <div class="footer-social">
                        <a href="#"><i class="fab fa-facebook-f"></i></a>
                        <a href="#"><i class="fab fa-twitter"></i></a>
                        <a href="#"><i class="fab fa-instagram"></i></a>
                    </div>
                </div>

                <div class="col-sm-3">
                    <h5>Enlaces Rápidos</h5>
                    <ul class="list-unstyled">
                        <li><a href="#">Inicio</a></li>
                        <li><a href="#">Propiedades</a></li>
                        <li><a href="#">Agentes</a></li>
                        <li><a href="#">Sobre Nosotros</a></li>
                        <li><a href="#">Contacto</a></li>
                    </ul>
                </div>
                <div class="col-sm-3">
                    <h5>Contáctanos</h5>
                    <ul class="list-unstyled">
                        <li><i class="fa fa-map-marker"></i>Av. Inmobiliaria 1234, Ciudad</li>
                        <li><i class="fa fa-phone"></i>+56 9 1234 5678</li>
                        <li><i class="fa fa-envelope"></i>contacto@leasecheck.cl</li>
                    </ul>
                </div>
            </div>
            <hr />
            <p class="copyright">&copy; 2025 LeaseCheck. Todos los derechos reservados.</p>
        </footer>
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <script>
</script>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <!-- Vendor js -->
</body>
</html>
