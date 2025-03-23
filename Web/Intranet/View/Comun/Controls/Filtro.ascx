<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Filtro.ascx.cs" Inherits="View_Comun_Controls_Filtro" %>
<div class="container-fluid f-busqueda">
    <div class="row">
        <div role="search">
            <div class="input-group">
                <WebControls:TextBox2 ID="txtFiltro" runat="server" />
                <div class="input-group-btn">
                    <WebControls:PushButton ID="btnFiltrar" runat="server" Text="Buscar" CssClass="Button"
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
</div>