<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="MiPropiedad.aspx.cs" Inherits="View_Propietarios_MiPropiedad" %>

<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">

        function abrirPropiedad(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClientePropiedad.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }


    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Mi Propiedad
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Corredora</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblCorredora" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>RUT</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblRutCorredora" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Dirección</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblDireccion" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Email</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblEmail" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Telefono</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblFono" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Contacto Telefono</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblFonoCorredora" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Contacto Email</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblEmailCorredora" runat="server" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Propiedad</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblTituloPropiedad" runat="server" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Fecha Publicación</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblFechaPublicacion" runat="server" />
                </div>
            </div>

            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Valor UF</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblValorUF" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Valor CLP</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblValorCLP" runat="server" />
                </div>
            </div>
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <label>Estado Propiedad</label>
                </div>
                <div class="form-group col-lg-2 col-md-2 col-xs-12">
                    <asp:Label ID="lblEstado" runat="server" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
