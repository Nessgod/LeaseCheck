<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Paises.aspx.cs" Inherits="View_Root_Mantenedores_Paises_Paises" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server"> 
    <script type="text/javascript">
        function abrir(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Paises/Pais.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }
        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }
    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" Runat="Server">
    Paises
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" Runat="Server">
</asp:Content>
    
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server"/>
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="Grid" runat="server" onitemdatabound="Grid_ItemDataBound"> 
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="PAI_ID" >
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                                 <asp:LinkButton ID="lnkNuevo" runat="server" CssClass="btn_dinamico btn_guardar" OnClientClick="abrir(0); return false;" ToolTip="Añadir">
                                 <span class="text">Nuevo</span>
                                 <span class="icon"><i class="fas fa-plus"></i></span>
                                 </asp:LinkButton>


                                 <asp:LinkButton ID="lnkEliminar" runat="server" CssClass="btn_dinamico btn_eliminar" OnClick="lnkEliminar_Click"
                                     OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" ToolTip="Eliminar">
                                     <span class="text">Eliminar</span>
                                     <span class="icon"><i class="fas fa-trash-alt"></i></span>
                                 </asp:LinkButton>

                
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