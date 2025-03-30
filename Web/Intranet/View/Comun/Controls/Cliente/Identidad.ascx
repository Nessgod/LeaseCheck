<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Identidad.ascx.cs" Inherits="View_Comun_Controls_Cliente_Identidad" %>

<script type="text/javascript">
    //Valido el correo
    function ValidaEmailFormat() {
        var txtCorreo = $('#<%=txtEmail.ClientID %>');
        if (txtCorreo.val() != "") {
            if (!ValidaEmail(txtCorreo.val())) {
                txtCorreo.val('');
                AlertSweet('Formato de correo invalido', '', 'alerta');
            }
        }
    }

    //Valida el Rut
    function validaRut() {
        var numero = document.getElementById('<%=txtRut.ClientID %>');
        var dv = document.getElementById('<%=txtDv.ClientID %>');

        if (numero.value == "")
            numero.className = "ErrorControl";

        if (dv.value != "") {
            var FotmateaRut = validarRut(numero.value, dv.value);
            if (!FotmateaRut) {
                alert("Rut Incorrecto");
                numero.value = "";
                dv.value = "";
                numero.className = "ErrorControl";
                dv.className = "ErrorControl";
            }
        }
    }


    $(document).ready(function () {
        MuestroImagane();
    });

    function MuestroImagane() {
        var imgLogo = $('#<%=imgLogo.ClientID %>');

        if (imgLogo.attr('src') == "")
            imgLogo.hide();
        else
            imgLogo.show();
    }

    //Controlo la visibilidad del Rut e Identificador.
    function TieneRut() {
        var rdoSi = $('#<%=rdoSi.ClientID %>');
        var trRut = $('#<%=pnlRut.ClientID %>');
        var trIdentificador = $('#<%=pnlIdentificador.ClientID %>');

        var txtRut = $('#<%=txtRut.ClientID %>');
        var txtDv = $('#<%=txtDv.ClientID %>');
        var txtIdentificador = $('#<%=txtIdentificador.ClientID %>');

        if (rdoSi.is(':checked')) {
            trIdentificador.hide();
            txtIdentificador.val('');
            txtIdentificador.removeClass('ErrorControl');
            txtIdentificador.addClass('Textbox');
            trRut.show();

            txtRut.addClass('ErrorControl');
            txtDv.addClass('ErrorControl');
            txtRut.removeClass('Textbox');
            txtDv.removeClass('Textbox');
        }
        else {
            trRut.hide();
            txtRut.val('');
            txtDv.val('');
            txtRut.removeClass('ErrorControl');
            txtDv.removeClass('ErrorControl');
            txtRut.addClass('Textbox');
            txtDv.addClass('Textbox');

            trIdentificador.show();
            txtIdentificador.removeClass('Textbox');
            txtIdentificador.addClass('ErrorControl');
        }
    }
    //Pinta en rojo con la clase ErrorControl los objetos vacíos
    function ValidaCamposVacios(obj) {
        if (obj.value != '') {
            obj.className = 'Textbox';
        }
        else {
            obj.className = 'ErrorControl';
        }
    }

    //pinto cajas de rut o identificador.
    function validaIdentificacion(obj) {
        ValidaCamposVacios(document.getElementById(obj));
    }

</script>

<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-2">
        <asp:Image ID="imgLogo" runat="server" CssClass="rounded-circle img-thumbnail avatar-md" />
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-10">
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-2">
        <label>ID</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-10">
        <asp:Label ID="lblID" runat="server" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Nombre(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtNombre" runat="server" />
        <asp:CustomValidator ID="CustomValidator1" runat="server"
            ControlToValidate="txtNombre"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Giro(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtGiro" runat="server" />
        <asp:CustomValidator ID="CustomValidator4" runat="server"
            ControlToValidate="txtGiro"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 form-col" id="trValidarut" runat="server" visible="true">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Tiene Rut</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <asp:RadioButton ID="rdoSi" runat="server" Text="Si" Checked="true" GroupName="Valida" OnClick="TieneRut()" />
        <asp:RadioButton ID="rdoNo" runat="server" Text="No" GroupName="Valida" OnClick="TieneRut()" />
    </div>
</div>
<asp:Panel runat="server" ID="pnlRut">
    <div class="row col-lg-12 col-md-12 col-xs-12 ">
        <div class="form-group col-lg-2 col-md-2 col-xs-12">
            <label>Rut(*)</label>
        </div>
        <div class="form-group col-lg-10 col-md-10 col-xs-12">
            <WebControls:TextBox2 ID="txtRut" runat="server" Width="120px" MaxLength="15" ValidaMaxLength="false" />
            <ajaxToolkit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtRut" FilterType="Numbers" />

            -
   <WebControls:TextBox2 ID="txtDv" Width="35px" runat="server" onblur="validaRut()" MaxLength="1" />

        </div>
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pnlIdentificador">
    <div class="row col-lg-12 col-md-12 col-xs-12">
        <div class="form-group col-lg-2 col-md-2 col-xs-12">
            <label>Identificación(*)</label>
        </div>
        <div class="form-group col-lg-10 col-md-10 col-xs-12">
            <WebControls:TextBox2 ID="txtIdentificador" runat="server" MaxLength="200" ValidaMaxLength="false" TabIndex="1" onblur="validaIdentificacion(this.id)" />

        </div>
    </div>
</asp:Panel>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Alias(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtAlias" runat="server" />
        <asp:CustomValidator ID="CustomValidator5" runat="server"
            ControlToValidate="txtAlias"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>

<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Pais(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <rad:RadComboBox2 ID="cboPais" runat="server" OnLoad="LoadControls" AutoPostBack="true" />
    </div>
</div>

<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Ciudad(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <rad:RadComboBox2 ID="cboComuna" runat="server" OnLoad="LoadControls" />
    </div>
</div>

<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Dirección</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtDireccion" runat="server" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Email(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtEmail" runat="server" onblur="ValidaEmailFormat()" />
        <asp:CustomValidator ID="CustomValidator8" runat="server"
            ControlToValidate="txtEmail"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Teléfono(*)</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtTelefono" runat="server" onblur="validarNumero()" />
        <asp:CustomValidator ID="CustomValidator9" runat="server"
            ControlToValidate="txtTelefono"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Logo Empresa</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <asp:FileUpload ID="fudFoto" runat="server" />
    </div>
</div>

<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Nombre Contacto</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtContactoNombre" runat="server" />
        <asp:CustomValidator ID="CustomValidator10" runat="server"
            ControlToValidate="txtTelefono"
            ValidateEmptyText="true"
            ClientValidationFunction="validaControl"
            ValidationGroup="Identidad" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Email Contacto</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtContactoEmail" runat="server" onblur="ValidaEmailFormat()" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12 ">
    <div class="form-group col-lg-2 col-md-2 col-xs-12">
        <label>Teléfono Contacto</label>
    </div>
    <div class="form-group col-lg-10 col-md-10 col-xs-12">
        <WebControls:TextBox2 ID="txtContactoTelefono" runat="server" onblur="validarNumeroContacto()" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Cliente Demo?</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <asp:CheckBox ID="chkDemo" runat="server" />
    </div>
</div>
<div class="row col-lg-12 col-md-12 col-xs-12">
    <div class="col-lg-2 col-md-2 col-xs-12">
        <label>Habilitado</label>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-12">
        <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" />
        <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" />
    </div>
</div>
<div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
    </br>
       <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Identidad" />
</div>
