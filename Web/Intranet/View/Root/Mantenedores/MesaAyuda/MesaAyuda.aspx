<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="MesaAyuda.aspx.cs" Inherits="View_Root_Mantenedores_MesaAyuda_MesaAyuda" %>
<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>
<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server"> 
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/MesaAyuda/MesaAyudaDetalle.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }
        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" Runat="Server">
    Mesa Ayuda
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" Runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro">
        <FiltroPersonalizado>
            <div class="col-lg-1 col-md-1 col-xs-12">
                <label>Estatus</label>
            </div>
            <div class="col-lg-2 col-md-2 col-xs-12">
                 <rad:RadComboBox2 ID="cboEstatus" runat="server" OnLoad="LoadControls">
                
                 </rad:RadComboBox2>
            </div>
        </FiltroPersonalizado>
    </wuc:Filtro>
</asp:Content>
    
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server"/>
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" onitemdatabound="Grid_ItemDataBound"> 
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="MES_ID,MES_ESTADO" >
                    <CommandItemTemplate>
                       <div class="contenedor-botones">
                                <asp:LinkButton ID="lnkDescargarPlantilla" runat="server" Text="Descargar excel " CssClass="btn_dinamico btn_excel" OnClick="LnkGenerar_Click">
                                        <span class="text">Descargar</span>
                                        <span class="icon"><i class="fas fa-file-excel"></i></span>
                                </asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </rad:RadGrid2>
        </ContentTemplate>  
    </asp:UpdatePanel>
</asp:Content>