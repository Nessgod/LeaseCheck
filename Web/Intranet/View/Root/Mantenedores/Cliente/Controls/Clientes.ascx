<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Clientes.ascx.cs" Inherits="View_Root_Mantenedores_Cliente_Controls_Clientes" %>

<%@ Register Src="~/View/Comun/Controls/FiltroAvanzado.ascx" TagPrefix="wuc" TagName="Filtro" %>


<script type="text/javascript">
    function abrirClientes(query) {
        window.location = ('<%=ResolveUrl(URLNuevoCliente) %>?query=' + query);
        console.log(URLNuevoCliente);
    }

    function refresh() {
        __doPostBack("<%=Grid.ClientID %>", '')
    }
</script>

<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-12 col-md-12 col-xs-12">
        <wuc:Filtro runat="server" ID="wucFiltro">
            <FiltroPersonalizado>
                <div class="row col-lg-12 col-md-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-12">
                        <label>Habilitado</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-12">
                        <rad:RadComboBox2 ID="cboHabilitados" runat="server" Filter="Contains" Width="100%">
                            <Items>
                                <rad:RadComboBoxItem Text="Todos" Value="" />
                                <rad:RadComboBoxItem Text="Habilitados" Value="True" />
                                <rad:RadComboBoxItem Text="Deshabilitados" Value="False" />
                            </Items>
                        </rad:RadComboBox2>
                    </div>
                    <div class="col-lg-1 col-md-1 col-12"></div>
                    <div class="col-lg-2 col-md-2 col-12">
                        <label>Pais</label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-12">
                        <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" MarkFirstMatch="true" EnableLoadOnDemand="true" Width="80%" Filter="Contains" AutoPostBack="true" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-12">
                        <label>Usuario</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-12">
                        <WebControls:TextBox2 ID="txtUsuario" runat="server" MaxLength="200" Width="100%" />
                    </div>
                </div>
            </FiltroPersonalizado>
        </wuc:Filtro>
    </div>
    <div class="col-lg-12 col-md-12 col-xs-12">
        <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
            <ContentTemplate>
                <rad:RadGrid2 ID="Grid" runat="server" OnItemDataBound="Grid_ItemDataBound">
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="cli_id">
                        <CommandItemTemplate>
                            <div style="margin-bottom: 5px;">
                                <asp:LinkButton ID="lnkNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClientClick="abrirClientes(0)" />
                                <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" />
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </rad:RadGrid2>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
