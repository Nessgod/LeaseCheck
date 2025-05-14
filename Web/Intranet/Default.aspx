<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="Exp" TagName="DashboardClientes" Src="~/View/Root/Mantenedores/Dashboard/Controls/DashboardClientes.ascx" %>
<%@ Register TagPrefix="Exp" TagName="DashboardComercial" Src="~/View/Root/Mantenedores/Dashboard/Controls/DashboardComercial.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeder" runat="Server">
    <style>
               .card {
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            padding: 20px;
            transition: all 0.3s ease;
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            margin-bottom: 0px;
        }

            .card:hover {
                box-shadow: 0 4px 18px rgba(0, 0, 0, 0.1);
            }

        .titulo-tarjeta {
            font-size: 1.2rem;
            font-weight: 600;
            color: #333333;
        }

        .fecha-tarjeta {
            font-size: 0.9rem;
            color: #888888;
            font-weight: 500;
            display: block;
        }

        .valor-tarjeta {
            font-size: 2.2rem;
            font-weight: bold;
            color: #2a2a2a;
            margin-top: auto;
        }

        .border-left-primary {
            border-left: 5px solid #4e73df;
        }

        .border-left-success {
            border-left: 5px solid #1cc88a;
        }

        .border-left-warning {
            border-left: 5px solid #f6c23e;
        }

        .border-left-danger {
            border-left: 5px solid #e74a3b;
        }


        .subtitulo-dashboard {
            font-size: 1.6rem;
            font-weight: 700;
            text-align: center;
            margin: 20px 0;
            color: #444;
        }

        .lista-productos {
            padding-left: 20px;
            margin: 0;
            list-style-type: disc;
        }

            .lista-productos li {
                font-size: 0.9rem;
                color: #444;
                margin-bottom: 4px;
            }
    </style>

    <script src="Js/jquery.min.js"></script>

</asp:Content>

<asp:Content ID="co" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" src="Js/Loader.js"></script>
    <script type="text/javascript">

        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        google.charts.setOnLoadCallback(drawChart2);

        function drawChart() {
            var hdfConsumoAnterior = $('#<%= hdfConsumoAnterior.ClientID %>');
         var hdfConsumoActual = $('#<%= hdfConsumoActual.ClientID %>');
         var hdfConsumoDisponible = $('#<%= hdfConsumoDisponible.ClientID %>');

         var data = google.visualization.arrayToDataTable([
             ['', 'Consumo'],
             ['Creadas anteriormente', parseFloat(hdfConsumoAnterior.val())],
             ['Creadas este mes', parseFloat(hdfConsumoActual.val())],
             ['Propiedades Disponibles', parseFloat(hdfConsumoDisponible.val())]
         ]);

         var options = {
             title: '',
             width: 'auto',
             height: '',
             is3D: true,
             pieSliceText: 'percentage',
             pieSliceTextStyle: {
                 fontSize: 14,
                 color: '#fff'
             },
             legend: {
                 position: 'right',
                 textStyle: {
                     fontSize: 13,
                     color: '#444'
                 }
             }
         };

         var chart = new google.visualization.PieChart(document.getElementById('piechart'));
         chart.draw(data, options);
     }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:HiddenField ID="hdfVistaCliente" runat="server" Value="false" />
    <asp:HiddenField ID="hdfVistaComercial" runat="server" Value="false" />

    <asp:HiddenField ID="hdfConsumoAnterior" runat="server" />
    <asp:HiddenField ID="hdfConsumoActual" runat="server" />
    <asp:HiddenField ID="hdfConsumoDisponible" runat="server" />

    <div class="SubTitulos" style="text-align: center">Bienvenido a LeaseCheck</div>

    <div class="container-fluid" id="Dashboard" runat="server" visible="false">
        <div style="text-align: left; margin-bottom: 10px;">
            <asp:Label ID="lblFechaActual" runat="server" CssClass="fecha-tarjeta" />
        </div>

        <div class="row">
            <div class="col-lg-3 col-md-6 mb-4">
                <div class="card border-left-primary">
                    <span id="lblPlanVigente" runat="server" class="titulo-tarjeta"></span>
                    <asp:Label ID="lblFechaActual1" runat="server" CssClass="fecha-tarjeta" />
                    <asp:Label ID="lblProductos" runat="server" CssClass="fecha-tarjeta" />
                </div>
            </div>

            <div class="col-lg-3 col-md-6 mb-4">
                <div class="card border-left-success">
                    <span class="titulo-tarjeta">Propiedades Disponibles</span>
                    <asp:Label ID="lblFechaActual2" runat="server" CssClass="fecha-tarjeta" />
                    <asp:Label ID="lblInformesDisponibles" runat="server" CssClass="valor-tarjeta" />
                </div>
            </div>

            <div class="col-lg-3 col-md-6 mb-4">
                <div class="card border-left-warning">
                    <span class="titulo-tarjeta">Propiedades Creadas</span>
                    <asp:Label ID="lblFechaActual3" runat="server" CssClass="fecha-tarjeta" />
                    <asp:Label ID="lblInformesConsumidos" runat="server" CssClass="valor-tarjeta" />
                </div>
            </div>

            <div class="col-lg-3 col-md-6 mb-4">
                <div class="card border-left-danger">
                    <span class="titulo-tarjeta">Propiedades Creadas Anteriormente</span>
                    <asp:Label ID="lblFechaAnterior" runat="server" CssClass="fecha-tarjeta" />
                    <asp:Label ID="lblCreadasAnterior" runat="server" CssClass="valor-tarjeta" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card border-left-info">
                    <h4 class="titulo-tarjeta" style="text-align: center;">Consumo Plan Vigente - Cantidad Propiedades
               <asp:Label ID="lblFechaActual4" runat="server" CssClass="fecha-tarjeta" />
                    </h4>
                    <div id="piechart" style="height: 260px;"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
