<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="ClienteUsuarios.aspx.cs" Inherits="View_Clientes_Identidad_ClienteUsuarios" %>
<%@ Register TagPrefix="wuc" TagName="Filtro" Src="~/View/Comun/Controls/FiltroAvanzado.ascx" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="cphHeder" runat="server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">

        function abrirUsuario(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClienteUsuario.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refreshUsuarios() {
            __doPostBack("<%=gridUsuarios.ClientID %>", '')
        }

    </script>
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" Runat="Server">
    Usuarios
</asp:Content>

<asp:Content ID="ContentFiltro" ContentPlaceHolderID="cphFiltro" runat="Server">
    <wuc:Filtro runat="server" ID="wucFiltro" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <rad:RadGrid2 ID="gridUsuarios" runat="server" onitemdatabound="gridUsuarios_ItemDataBound" AllowPaging="false" >
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="usu_id" >
                    <CommandItemTemplate>
                        <div class="contenedor-botones">
                           <asp:LinkButton ID="lnkNuevoUsuario" runat="server" Text="Nuevo" CssClass="btn_dinamico btn_guardar" OnClick="lnkNuevoUsuario_Click" ToolTip="Añadir">
                             <span class="text">Nuevo</span>
                             <span class="icon"><i class="fas fa-plus"></i></span>
                             </asp:LinkButton>


                           <asp:LinkButton ID="lnkEliminarUsuario" runat="server" CssClass="btn_dinamico btn_eliminar" OnClick="lnkEliminarUsuario_Click"
                               OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" ToolTip="Eliminar">
                                 <span class="text">Eliminar</span>
                                 <span class="icon"><i class="fas fa-trash-alt"></i></span>
                             </asp:LinkButton>


                           <asp:LinkButton ID="lnkReset" runat="server" CssClass="btn_dinamico_largo btn_reset_password" OnClick="lnkReset_Click"
                               OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea resetear las claves de los usuarios seleccionados?');" >
                               <span class="text">Reset Password</span>
                            <span class="icon"><i class="fas fa-sync-alt"></i></span> 
                           </asp:LinkButton>
                       </div>
                    </CommandItemTemplate>
                </MasterTableView>
                <ClientSettings>
                </ClientSettings>  
            </rad:RadGrid2>
        </ContentTemplate>  
    </asp:UpdatePanel>
</asp:Content>