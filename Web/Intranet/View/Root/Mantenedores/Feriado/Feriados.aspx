<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Feriados.aspx.cs" Inherits="View_Sistema_Feriado_Feriados" %>
<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHead"  ContentPlaceHolderID="cphHeder" runat="server">  
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Feriado/Feriado.aspx") %>?query=' + query);
            oWin.show();
        }
     
        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" Runat="Server">
    Feriados
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" Runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro">
        <FiltroPersonalizado>
            
                <div class="col-lg-1 col-md-1 col-xs-12"> 
                    <label>Pais</label>
                </div>
                <div class="col-lg-4 col-md-4 col-xs-12 col-12"> 
                    <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls"  />
                </div>
                <div class="col-lg-1 col-md-1 col-xs-12"> 
                    <label>Desde</label>
                </div>
                <div class="col-lg-3 col-md-3 col-xs-12"> 
                    <WebControls:Calendar ID="txtDesde" runat="server" />
                </div>
                 <div class="col-lg-1 col-md-1 col-xs-12"> 
                    <label>Hasta</label>
                </div>
                <div class="col-lg-2 col-md-2 col-xs-12"> 
                    <WebControls:Calendar ID="txtHasta" runat="server" />
                </div>
            
        </FiltroPersonalizado>
    </wuc:Filtro>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">   
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" onitemdatabound="Grid_ItemDataBound">
                <MasterTableView CommandItemDisplay="Top"  DataKeyNames="frd_id" >
                    <CommandItemTemplate>
                        <div>
                            <span style="float:left">
                                <asp:LinkButton ID="lnkNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClientClick="abrir(0);" />
                                <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                            </span>
                            <span style="float:right">
                                <asp:LinkButton ID="lnkDescargarPlantilla" runat="server" Text="Descargar excel " CssClass="icono_descargar_excel" OnClick="LnkGenerar_Click" />
                            </span>                            
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>