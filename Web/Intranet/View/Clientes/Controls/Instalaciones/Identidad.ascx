<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Identidad.ascx.cs" Inherits="View_Comun_Controls_Instalaciones_Identidad" %>


<script type="text/javascript">
    function abrirAsociarUsuario(query) {
        var oWin = $find("<%=rwiDetalle.ClientID %>");
        oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteInstalacionUsuarioAsociar.aspx") %>?query=' + query);
        oWin.show();
        //oWin.maximize();
        bloqueaScroll(false);
    }

    function refresh() {
        __doPostBack("<%=GridUsuarios.ClientID %>", '')
    }
</script>

<rad:RadWindow2 ID="rwiDetalle" runat="server" />

<div class="SubTitulos">Identidad de la instalación</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>ID</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <asp:Label ID="lblId" runat="server"></asp:Label>
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Nombre(*):</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtNombre" runat="server" MaxLength="200" />
        <asp:CustomValidator ID="CustomValidator3" runat="server"
            ControlToValidate="txtNombre"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Instalacion" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Descripción(*):</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtDescripcion" runat="server" MaxLength="2000" />
        <asp:CustomValidator ID="CustomValidator2" runat="server"
            ControlToValidate="txtDescripcion"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Instalacion" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Ubicacion(*):</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtDireccion" runat="server" MaxLength="200" />
        <asp:CustomValidator ID="CustomValidator1" runat="server"
            ControlToValidate="txtDireccion"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Instalacion" />
    </div>
</div>

<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Habilitado(*):</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" Checked="true" ValidationGroup="Instalacion" />
        <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" ValidationGroup="Instalacion" />
    </div>
</div>

<asp:Panel ID="pnlGrillUsuarios" runat="server" Visible="true">
    <div class="SubTitulos col-lg-12 col-md-12 col-xs-12">
        Notifica a
    </div>
    <rad:RadGrid2 ID="GridUsuarios" runat="server" OnItemDataBound="GridUsuarios_ItemDataBound">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ciu_id">
            <CommandItemTemplate>
                <div>
                    <asp:LinkButton ID="lnkAñadirUsuario" runat="server" Text="Añadir" CssClass="icono_guardar" OnClick="lnkAñadirUsuario_Click" />
                    <asp:LinkButton ID="lnkEliminarUsuario" runat="server" Text="Quitar" CssClass="icono_eliminar" OnClick="lnkEliminarUsuario_Click"
                        OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro que desea eliminar los registros seleccionados?');" />
                </div>
            </CommandItemTemplate>
        </MasterTableView>
        <ClientSettings>
            <Scrolling AllowScroll="True" ScrollHeight="320" />
        </ClientSettings>
    </rad:RadGrid2>
</asp:Panel>

<div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
    <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Instalacion" />
</div>

