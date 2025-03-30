<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Instalacion.aspx.cs" Inherits="View_Clientes_Instalacion_Instalacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript">
        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function closeWindow() {
            var window = getRadWindow();
            if (window.BrowserWindow.refresh) window.BrowserWindow.refresh();
            window.close();
        }

    </script>
</asp:Content>

<asp:Content ID="ContenHead" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
              <asp:Panel ID="pnlInstalaciones" runat="server">
           </br>
            <div class="SubTitulos">Instalaciones</div>
           <rad:RadGrid2 ID="GridInstalaciones" runat="server" OnItemDataBound="gridInstalaciones_ItemDataBound" AllowPaging="false">
               <MasterTableView CommandItemDisplay="Top" DataKeyNames="cin_id">
                   <CommandItemTemplate>
                       <div>
                           <asp:LinkButton ID="lnkNuevaInstalacion" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevaInstalacion_Click" />
                           <asp:LinkButton ID="lnkEliminarInstalacion" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarInstalacion_Click"
                               OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />

                       </div>
                   </CommandItemTemplate>
               </MasterTableView>
               <ClientSettings>
               </ClientSettings>
           </rad:RadGrid2>
       </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>