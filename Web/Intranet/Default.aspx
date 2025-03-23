<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="Exp" TagName="DashboardClientes" Src="~/View/Root/Mantenedores/Dashboard/Controls/DashboardClientes.ascx" %>
<%@ Register TagPrefix="Exp" TagName="DashboardComercial" Src="~/View/Root/Mantenedores/Dashboard/Controls/DashboardComercial.ascx" %>

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

    <script src="Js/jquery.min.js"></script>

</asp:Content>

<asp:Content ID="co" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" src="Js/Loader.js"></script>
    <script type="text/javascript">

        google.charts.load('current', { 'packages': ['corechart'] });

        if ($('#<%= hdfVistaCliente.ClientID %>').val() == 'true'){
            google.charts.setOnLoadCallback(drawChart);
            google.charts.setOnLoadCallback(drawChart2);
        }

        if ($('#<%= hdfVistaComercial.ClientID %>').val() == 'true') {
            google.charts.setOnLoadCallback(drawChart3);
            google.charts.setOnLoadCallback(drawChart4);
        }

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
        
      

        function drawChart3() {
            var hdfCliente1 = $('#<%= hdfCliente1.ClientID %>');
            var hdfVenta1 = $('#<%= hdfVenta1.ClientID %>');
            var hdfCliente2 = $('#<%= hdfCliente2.ClientID %>');
            var hdfVenta2 = $('#<%= hdfVenta2.ClientID %>');
            var hdfCliente3 = $('#<%= hdfCliente3.ClientID %>');
            var hdfVenta3 = $('#<%= hdfVenta3.ClientID %>');
            var hdfCliente4 = $('#<%= hdfCliente4.ClientID %>');
            var hdfVenta4 = $('#<%= hdfVenta4.ClientID %>');
        
            var data = google.visualization.arrayToDataTable([
                ['', 'Total Venta'],
                [hdfCliente1.val(), parseFloat(hdfVenta1.val())],
                [hdfCliente2.val(), parseFloat(hdfVenta2.val())],
                [hdfCliente3.val(), parseFloat(hdfVenta3.val())],
                [hdfCliente4.val(), parseFloat(hdfVenta4.val())]
            ]);
            
            var options = { 'title': '', 'width': 'auto', 'height': '' };
            var chart = new google.visualization.PieChart(document.getElementById('piechart2'));
            chart.draw(data, options);
        }
        
        function drawChart4() {
            var hdfPlan1 = $('#<%= hdfPlan1.ClientID %>');
            var hdfPlanCantidad1 = $('#<%= hdfPlanCantidad1.ClientID %>');
            var hdfPlan2 = $('#<%= hdfPlan2.ClientID %>');
            var hdfPlanCantidad2 = $('#<%= hdfPlanCantidad2.ClientID %>');
            var hdfPlan3 = $('#<%= hdfPlan3.ClientID %>');
            var hdfPlanCantidad3 = $('#<%= hdfPlanCantidad3.ClientID %>');
            var hdfPlan4 = $('#<%= hdfPlan4.ClientID %>');
            var hdfPlanCantidad4 = $('#<%= hdfPlanCantidad4.ClientID %>');

            var data = google.visualization.arrayToDataTable([
                ['', 'Plan'],
                [hdfPlan1.val(), parseFloat(hdfPlanCantidad1.val())],
                [hdfPlan2.val(), parseFloat(hdfPlanCantidad2.val())],
                [hdfPlan3.val(), parseFloat(hdfPlanCantidad3.val())],
                [hdfPlan4.val(), parseFloat(hdfPlanCantidad4.val())],
            ]);
            
            var options = { 'title': '', 'width': 'auto', 'height': '' };
            var chart = new google.visualization.ColumnChart(document.getElementById('columnchart2'));
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

    <asp:HiddenField ID="hdfCargo1" runat="server" />
    <asp:HiddenField ID="hdfCantidad1" runat="server" />
    <asp:HiddenField ID="hdfCargo2" runat="server" />
    <asp:HiddenField ID="hdfCantidad2" runat="server" />
    <asp:HiddenField ID="hdfCargo3" runat="server" />
    <asp:HiddenField ID="hdfCantidad3" runat="server" />

    <asp:HiddenField ID="hdfCliente1" runat="server" />
    <asp:HiddenField ID="hdfVenta1" runat="server" />
    <asp:HiddenField ID="hdfCliente2" runat="server" />
    <asp:HiddenField ID="hdfVenta2" runat="server" />
    <asp:HiddenField ID="hdfCliente3" runat="server" />
    <asp:HiddenField ID="hdfVenta3" runat="server" />
    <asp:HiddenField ID="hdfCliente4" runat="server" />
    <asp:HiddenField ID="hdfVenta4" runat="server" />

     <asp:HiddenField ID="hdfPlan1" runat="server" />
     <asp:HiddenField ID="hdfPlanCantidad1" runat="server" />
     <asp:HiddenField ID="hdfPlan2" runat="server" />
     <asp:HiddenField ID="hdfPlanCantidad2" runat="server" />
     <asp:HiddenField ID="hdfPlan3" runat="server" />
     <asp:HiddenField ID="hdfPlanCantidad3" runat="server" />
     <asp:HiddenField ID="hdfPlan4" runat="server" />
     <asp:HiddenField ID="hdfPlanCantidad4" runat="server" />

    <div class="SubTitulos" style="text-align:center">Bienvenido a LeaseCheck</div>

    <%--CLIENTES--%>
    <Exp:DashboardClientes runat="server" ID="wucDashboardClientes" Visible="false"/> 

    <%--COMERCIAL--%>
    <Exp:DashboardComercial runat="server" ID="wucDashboardComercial" Visible="false" /> 
</asp:Content>