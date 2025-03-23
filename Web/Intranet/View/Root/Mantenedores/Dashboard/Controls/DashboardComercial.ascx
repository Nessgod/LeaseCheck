<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashboardComercial.ascx.cs" Inherits="View_Root_Mantenedores_Dashboard_Controls_DashboardComercial" %>

<div class="container-fluid">
    <div class="row" style="height:250px">
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="card border-left-primary" style="text-align:center">
                <h5 style="padding-bottom:0">Total Clientes</h5>
                <div>
                    <h4 style="line-height: 100px;"><asp:Label ID="lblTotalCliente" runat="server"></asp:Label></h4>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="height:300px">
            <div class="card border-left-success">
                <h5 style="text-align:center;">Clientes con más Test mes actual</h5>
                <div style="margin:0 auto;">
                    <table>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblCliente1" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblCantidad1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblCliente2" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblCantidad2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblCliente3" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblCantidad3" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblCliente4" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblCantidad4" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="card border-left-info" >
                <h5 style="padding-bottom:0; text-align:center">Próximos Vencimientos</h5>
                <div style="margin:0 auto;">
                    <table>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblVencimiento1" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblFecha1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblVencimiento2" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblFecha2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblVencimiento3" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblFecha3" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding:5px"><asp:Label ID="lblVencimiento4" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblFecha4" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>  
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="card border-left-warning" style="text-align:center">
                <h5 style="padding-bottom:0">Total Postulantes</h5>
                <div style="vertical-align:middle;">
                    <h4 style="line-height: 100px;"><asp:Label ID="lblTotalPostulante" runat="server"></asp:Label></h4>
                </div>  
            </div>
        </div>
    </div>
     <div class="row" style="height: 360px">
        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
            <div class="card border-left-info" style="height: 320px;">
                <h4 style="padding-bottom: 0; text-align: center">Total ventas por Cliente</h4>
                <div id="piechart2"></div>
            </div>
        </div>
         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
        </div>
        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
            <div class="card border-left-info" style="height: 320px;">
                <h4 style="padding-bottom: 0; text-align: center">Planes mas vendidos</h4>
                <div id="columnchart2"></div>
            </div>
        </div>
    </div>
</div>