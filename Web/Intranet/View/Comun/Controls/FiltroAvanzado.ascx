<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiltroAvanzado.ascx.cs" Inherits="Comun_Controls_FiltroAvanzado" %>

<script>
     function fnExpandeFiltro() {

         var hdfExpanded = $("#<%=hdfExpanded.ClientID %>");

         if (hdfExpanded.val() == '0') {
             hdfExpanded.val('1')
         }
         else {
             hdfExpanded.val('0')
         }

         expandeFiltro(false);
     }

    function expandeFiltro(isPostback) {
        var hdfExpanded = $("#<%=hdfExpanded.ClientID %>");
        var divPersonalizado = $("#divPersonalizado");
        
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
         expandeFiltro(true);
     }
</script>

<style>
    .filtro {
        margin-bottom:4px;
        padding:10px 5px;
    }
    .filtroBusqueda {
        padding:8px;
    }
    .filtroPersonalizado {
        margin-right:5px;
        padding-right:1px !important;
    }
   

</style>

<div class="row card-box filtro">
    <div class="table-responsive">
        <div class="row col-lg-12 col-md-12 col-xs-12" style="margin-left:0px;">
            <div class="row col-lg-11 col-md-11 col-xs-11">
                <div class="col-lg- col-md-1 col-xs-1 filtroBusqueda"> 
                    <a style="color:#53b4c7; text-transform:uppercase; font-size:12px;" role="button" data-toggle="collapse" 
                        href="#buscador" aria-expanded="true" aria-controls="buscador" onclick="fnExpandeFiltro()">
                        <span class="fa fa-filter"></span> Busqueda
                        <asp:HiddenField ID="hdfExpanded" runat="server" Value="0" />
                    </a>
                </div>
                <div class="col-lg-10 col-md-10 col-xs-10"> 
                    <WebControls:TextBox2 ID="txtFiltro" runat="server" Width="98%"  /> 
                </div>
            </div>
            <div id="divPersonalizado" class="row col-lg-11 col-md-11 col-xs-11 filtroPersonalizado" style="display:none;">
                <asp:PlaceHolder ID="phPersonalizado" runat="server"></asp:PlaceHolder>
            </div> 
            <div class="row col-lg-1 col-md-1 col-xs-1"> 
                <WebControls:PushButton ID="btnFiltrar" runat="server" Text="Buscar" CssClass="ButtonFilter" 
                    CausesValidation="false"/>
            </div>
        
        </div>
        
    </div>
</div>
