<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="VencimientoPlanes.aspx.cs" Inherits="View_Clientes_Dashboard_VencimientoPlanes" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeder" runat="Server">
    <style>
        .vencimientos-card {
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.06);
            padding: 24px;
            margin: 0 auto;
            font-family: 'Segoe UI', 'Roboto', sans-serif;
            transition: all 0.3s ease;
        }

        .titulo-card {
            font-size: 1.5rem;
            font-weight: 600;
            color: #007bff;
            text-align: center;
            margin-bottom: 16px;
        }

        .tabla-contenedor {
            overflow-x: auto;
        }

        .tabla-vencimientos {
            width: 100%;
            border-collapse: collapse;
        }

            .tabla-vencimientos tr {
                border-bottom: 1px solid #eaeaea;
                transition: background-color 0.2s ease;
            }

                .tabla-vencimientos tr:hover {
                    background-color: #f5faff;
                }

            .tabla-vencimientos td {
                padding: 12px 8px;
                font-size: 0.95rem;
                color: #333;
            }

            .tabla-vencimientos th {
                background-color: #f0f4f8;
                color: #333;
                text-align: left;
                padding: 12px 8px;
                font-weight: 600;
                font-size: 0.95rem;
                border-bottom: 2px solid #dee2e6;
            }
    </style>
    <link href="../../../Css/Assets/app.min.css" rel="stylesheet" />
    <link href="../../../Css/Assets/icons.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="co" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" src="../../../Js/Loader.js"></script>
    <script type="text/javascript">

</script>
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <br />
    <div class="container-fluid">
        <div class="row" style="height: 360px">
            <div class="col-lg-12 col-md-12 col-sm-12 vencimientos-card">
                <h5 class="titulo-card">Vencimientos de Planes</h5>
                <div class="tabla-contenedor">
                    <table class="tabla-vencimientos">
                        <thead>
                            <tr>
                                <th>Cliente</th>
                                <th>Plan</th>
                                <th>Fecha de Vencimiento</th>
                                <th>Propiedades</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVencimiento" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPlan" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblFecha" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPropiedades" runat="server"></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
