<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="PlanVigente.aspx.cs" Inherits="View_Clientes_Dashboard_PlanVigente" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeder" runat="Server">
    <style>
        .border-left-primary {
            border-left: 0.25rem solid #4e73df!important;
        }

        .border-left-success {
            border-left: 0.25rem solid #1cc88a!important;
        }

        .border-left-info {
            border-left: 0.25rem solid #36b9cc!important;
        }

        .border-left-warning {
            border-left: 0.25rem solid #f6c23e!important;
        }

          .card {
                position: relative;
                display: flex;
                flex-direction: column;
                min-width: 0;
                word-wrap: break-word;
                background-color: #fff;
                background-clip: border-box;
                border: 1px solid #e3e6f0;
                border-radius: 0.35rem;
                height:190px;
                margin-bottom:10px;
            }
       
    </style>
    <link href="../../../Css/Assets/app.min.css" rel="stylesheet" />
    <link href="../../../Css/Assets/icons.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="co" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" src="../../../Js/Loader.js"></script>
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
                ['Utilizados anteriormente',  parseFloat(hdfConsumoAnterior.val())],
                ['Utilizados este mes',  parseFloat(hdfConsumoActual.val())],
                ['Solo informes disponibles', parseFloat(hdfConsumoDisponible.val())]
            ]);
            
            var options = { 'title': '', 'width': 'auto', 'height': '' };
            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }

      
        
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:HiddenField ID="hdfConsumoAnterior" runat="server" />
    <asp:HiddenField ID="hdfConsumoActual" runat="server" />
    <asp:HiddenField ID="hdfConsumoDisponible" runat="server" />
    <asp:HiddenField ID="hdfCargo1" runat="server" />
    <asp:HiddenField ID="hdfCantidad1" runat="server" />
    <asp:HiddenField ID="hdfCargo2" runat="server" />
    <asp:HiddenField ID="hdfCantidad2" runat="server" />
    <asp:HiddenField ID="hdfCargo3" runat="server" />
    <asp:HiddenField ID="hdfCantidad3" runat="server" />

    <div class="SubTitulos" style="text-align:center">Plan Vigente</div>

    <div class="container-fluid">
    <div class="row" style="height: 250px">
        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
            <div class="card border-left-primary" style="text-align: center">
                <h5 style="padding-bottom: 0">Plan Vigente</h5>
                <div>
                    <h4 style="line-height: 100px;"><asp:Label ID="lblPlanVigente" runat="server"></asp:Label></h4>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
            <div class="card border-left-primary" style="text-align: center">
                <h5 style="padding-bottom: 0">Informes Disponibles</h5>
                <div>
                    <h4 style="line-height: 100px;"><asp:Label ID="lblInformesDisponibles" runat="server"></asp:Label></h4>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
            <div class="card border-left-warning" style="text-align: center">
                <h5 style="padding-bottom: 0">Informes Consumidos</h5>
                <div style="vertical-align: middle;">
                    <h4 style="line-height: 100px;"><asp:Label ID="lblInformesConsumidos" runat="server"></asp:Label></h4>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height: 360px">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card border-left-info" style="height: 320px;">
                <h4 style="padding-bottom: 0; text-align: center">Consumo Plan Vigente</h4>
                <div id="piechart"></div>
            </div>
        </div>
         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
        </div>
        
    </div>
</div>
</asp:Content>