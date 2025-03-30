<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanesTarifarios.ascx.cs" Inherits="View_Comun_Controls_Cliente_PlanesTarifarios" %>

<script type="text/javascript">


    function abrirPlan(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClientePlan.aspx") %>?query=' + query);
        oWin.show();
        bloqueaScroll(false);
    }

    function refresh() {
        __doPostBack("<%=Grid.ClientID %>", '')
    }

    // Función para sumar el valor total
    $(document).ready(function () {
        CalculoValorTotal();
    });

    $(document).ready(function () {
        CalculoValorTotal();
    });

    function CalculoValorTotal() {

    <%-- var lblValorTotal = $("#<%= lblValorTotal.ClientID %>");

     var valorTotal = 0;

     var masterTable = $find("<%= Grid.ClientID %>").get_masterTableView();
     var row = masterTable.get_dataItems(); 
     for (var i = 0; i < row.length; i++) {
         var valorPlanParcial = row[i].findElement("clp_valor_plan");

         console.log("Valor Plan Parcial:", valorPlanParcial);
         /*console.log(row[3])*/
         console.log(lblValorTotal)

         var valorPlan = 0;

         if (valorPlanParcial && valorPlanParcial.innerHTML.trim() !== "") {
             valorPlan = parseFloat(valorPlanParcial.innerHTML.replace(/[^0-9.-]+/g, ""));
         }
         console.log("Valor Plan:", valorPlan);

         valorTotal += valorPlan;
     }


     console.log("Valor Total:", valorTotal);

     lblValorTotal.text(valorTotal);--%>
    }
</script>


<rad:RadWindow2 ID="rwiDetalle" runat="server" />

<div class="SubTitulos">Planes Tarifarios</div>
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>

        <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound" AllowPaging="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="clp_id,tipo_dato">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevo_Click" />
                        <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </rad:RadGrid2>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-11 col-md-11 col-xs-12 text-right">
        <label class="font-weight-bold">Total: $</label>
    </div>
    <div class="col-lg-1 col-md-1 col-xs-12 text-right">
        <label class="font-weight-bold">
            <asp:Label ID="lblValorTotal" runat="server" />

        </label>
    </div>
</div>
