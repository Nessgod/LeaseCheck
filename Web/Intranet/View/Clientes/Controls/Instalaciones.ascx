﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Instalaciones.ascx.cs" Inherits="View_Comun_Controls_Cliente_Instalaciones" %>
<script type="text/javascript">
         function abrirClienteInstalacion(query) {
             var oWin = $find("<%=rwiDetalle.ClientID %>");
             oWin.setUrl('<%=ResolveUrl("~/View/Comun/Clientes/NuevaInstalacion.aspx") %>?query=' + query);
             oWin.show();
             //oWin.maximize();
             bloqueaScroll(false);
         }

        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
</script>

<rad:RadWindow2 ID="rwiDetalle" runat="server" />
<asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="SubTitulos">Instalaciones</div>
        <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="rgrClienteInstalacion_ItemDataBound">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="cin_id">
                <CommandItemTemplate>
                    <div>
                        <asp:LinkButton ID="lnkNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevoClienteInstalacion_Click" />
                        <%--<asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                            OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" />--%>
                    </div>
                </CommandItemTemplate>
            </MasterTableView>
        </rad:RadGrid2>
       
    </ContentTemplate>
</asp:UpdatePanel>
