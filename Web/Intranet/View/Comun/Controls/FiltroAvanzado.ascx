<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiltroAvanzado.ascx.cs" Inherits="Comun_Controls_FiltroAvanzado" %>

<script>
    function fnExpandeFiltro(ObjdivPersonalizado, ObjhdfExpanded) {
        var hdfExpanded = $("#" + ObjhdfExpanded);
        if (hdfExpanded.val() == '0') {
            hdfExpanded.val('1')
        }
        else {
            hdfExpanded.val('0')
        }
        expandeFiltro(false, ObjdivPersonalizado, ObjhdfExpanded);
    }

    function expandeFiltro(isPostback, ObjdivPersonalizado, ObjhdfExpanded) {
        var hdfExpanded = $("#" + ObjhdfExpanded);
        var divPersonalizado = $("#" + ObjdivPersonalizado);

        if (hdfExpanded.val() == '1') {
            if (isPostback)
                divPersonalizado.show();
            else
                divPersonalizado.show(500);
        }
        else {
            divPersonalizado.hide(500);
        }
    }

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        expandeFiltro(true, '<%=divPersonalizado.ClientID %>', '<%=hdfExpanded.ClientID %>');
    }
</script>

<div class="row card-box filtro">
    <div class="table-responsive">
        <div class="row col-lg-12 col-md-12 col-xs-12" style="margin-left: 0px;">
            <div class="row col-lg-12 col-md-12 col-xs-12">
                <div class="col-lg-2 col-md-2 col-xs-12 filtroBusqueda">
               <a class="boton-busqueda-avanzada" role="button" data-toggle="collapse"
                       href="#buscador" aria-expanded="true" aria-controls="buscador"
                       onclick="fnExpandeFiltro('<%=divPersonalizado.ClientID %>', '<%=hdfExpanded.ClientID %>')">
                       <span class="fa fa-filter"></span> Búsqueda Avanzada
                       <asp:HiddenField ID="hdfExpanded" runat="server" Value="0" />
                    </a>

                </div>
                <div class="col-lg-9 col-md-9 col-xs-9 col-8">
                    <WebControls:TextBox2 ID="txtFiltro" runat="server" Width="100%" CssClass="search-input" />
                </div>
                <div class="col-lg-1 col-md-1 col-xs-3 col-4">
                   <WebControls:PushButton ID="btnFiltrar" CssClass="btn-search" runat="server" Text="Buscar" CausesValidation="false" />
                </div>
            </div>
            <div id="divPersonalizado" runat="server" c class="row col-lg-12 col-md-12 col-xs-12 filtroPersonalizado" style="display: none;">
                <asp:PlaceHolder ID="phPersonalizado" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</div>
