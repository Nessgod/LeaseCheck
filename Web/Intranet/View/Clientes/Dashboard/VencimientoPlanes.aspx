<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="VencimientoPlanes.aspx.cs" Inherits="View_Clientes_Dashboard_VencimientoPlanes" %>
<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

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

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {

                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChart);
            }
        });

        function drawChart() {
            var hdfCargo1 = $('#<%= hdfCargo1.ClientID %>');
            var hdfCantidad1 = $('#<%= hdfCantidad1.ClientID %>');
            var hdfCargo2 = $('#<%= hdfCargo2.ClientID %>');
            var hdfCantidad2 = $('#<%= hdfCantidad2.ClientID %>');
            var hdfCargo3 = $('#<%= hdfCargo3.ClientID %>');
            var hdfCantidad3 = $('#<%= hdfCantidad3.ClientID %>');

            var data = google.visualization.arrayToDataTable([
                ['', 'Consumo'],
                [hdfCargo1.val(), parseFloat(hdfCantidad1.val())],
                [hdfCargo2.val(), parseFloat(hdfCantidad2.val())],
                [hdfCargo3.val(), parseFloat(hdfCantidad3.val())],
            ]);
            
            var options = { 'title': '', 'width': 'auto', 'height': '' };
            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }

    </script>
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <asp:HiddenField ID="hdfCargo1" runat="server" />
    <asp:HiddenField ID="hdfCantidad1" runat="server" />
    <asp:HiddenField ID="hdfCargo2" runat="server" />
    <asp:HiddenField ID="hdfCantidad2" runat="server" />
    <asp:HiddenField ID="hdfCargo3" runat="server" />
    <asp:HiddenField ID="hdfCantidad3" runat="server" />

    <div class="SubTitulos" style="text-align:center">Vencimiento Planes</div>
    <wuc:Filtro runat="server" ID="wucFiltro">
    </wuc:Filtro>

    <div class="container-fluid">
 <%--   <div class="row col-lg-12 col-md-12 col-xs-12" style="margin-left:0px;">
         <div class="row col-lg-6 col-md-6 col-xs-12 ">
            <div class="form-group col-lg-3 col-md-3 col-xs-12">
                <label>Cliente</label>
            </div>
            <div class="form-group col-lg-8 col-md-8 col-xs-12">
                <rad:RadComboBox2 ID="cboCliente" runat="server" OnLoad="LoadControls" Width="100%" Filter="Contains" AutoPostBack="true" />
            </div>
        </div>--%>
        <div class="col-lg-1 col-md-1 col-xs-12"> 
            <label>Desde</label>
        </div>
        <div class="col-lg-2 col-md-2 col-xs-12"> 
            <WebControls:Calendar ID="txtDesde" runat="server" />
        </div>
            <div class="col-lg-1 col-md-1 col-xs-12"> 
            <label>Hasta</label>
        </div>
        <div class="col-lg-2 col-md-2 col-xs-12"> 
            <WebControls:Calendar ID="txtHasta" runat="server" />
        </div>
    </div>
    <br />
    <br />
    <div class="row" style="height: 360px">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
            <div class="card border-left-info" style="height: 320px;">
                <h4 style="padding-bottom: 0; text-align: center">Consumo</h4>
                <div id="piechart"></div>
            </div>
        </div>
         <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
            <div class="card border-left-info" style="height:auto!important;" >
                <h5 style="padding-bottom:0; text-align:center">Vencimientos</h5>
                <div style="margin:0 auto;">
                    <table>
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="padding:5px"><asp:Label ID="lblVencimiento" runat="server"></asp:Label></td>
                                    <td style="padding:5px"><asp:Label ID="lblPlan" runat="server"></asp:Label></td>
                                    <td style="width:100px"><asp:Label ID="lblFecha" runat="server"></asp:Label></td>
                                </tr>
                             </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>