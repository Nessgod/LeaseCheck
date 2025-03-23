<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Cliente.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_Cliente" %>

<asp:Content ID="ContenHeder" ContentPlaceHolderID="chpScript" runat="server">
    <script type="text/javascript" language="javascript">
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

        function abrirPlan(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClientePlan.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }

        function abrirUsuario(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteUsuario.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function abrirDocumento(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteDocumento.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function refreshUsuarios() {
            __doPostBack("<%=gridUsuarios.ClientID %>", '')
        }

        function abrirInstalacion(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
              oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Cliente/ClienteInstalacion.aspx") %>?query=' + query);
              oWin.show();
              bloqueaScroll(false);
        }

        function refreshInstalacion() {
            __doPostBack("<%=GridInstalaciones.ClientID %>", '')
         }

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

        //Validar Numero de telefono

        function validarNumero() {
           <%--  var numeroTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
             var expresionRegular1 = /^([0-9]+){9}$/;//<--- con esto vamos a validar el numero
             var expresionRegular2 = /\s/;//<--- con esto vamos a validar que no tenga espacios en blanco

             if (numeroTelefono.value == '')
                 alert('campo es obligatorio');
             else if (expresionRegular2.test(numeroTelefono.value))
                 alert('error existen espacios en blanco');
             else if (!expresionRegular1.test(numeroTelefono.value))
                 alert('Numero de telefono incorrecto');--%>
        }

        //Validar Numero de telefono de contacto

        function validarNumeroContacto() {
           <%--  var numeroTelefono = document.getElementById('<%=txtContactoTelefono.ClientID%>');
             var expresionRegular1 = /^([0-9]+){9}$/;//<--- con esto vamos a validar el numero

             if (!expresionRegular1.test(numeroTelefono.value))
                 alert('Numero de telefono incorrecto');--%>
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

        // Función para sumar el valor total
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
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SubTitulos">
                <asp:Label ID="lblTituloCliente" runat="server" Text="Cliente" />
            </div>
            <rad:RadTabStrip2 ID="ragTab" runat="server" MultiPageID="MultiPage">
                <Tabs>
                    <rad:RadTab Text="Identidad" runat="server" PageViewID="rtvIdentidad" />
                    <rad:RadTab Text="Documentos adjuntos" runat="server" PageViewID="rtvDocumentosAdjuntos" />
                    <rad:RadTab Text="Planes tarifarios" runat="server" PageViewID="rtvPlanesTarifarios" />
                    <rad:RadTab Text="Usuarios" runat="server" PageViewID="rtvUsuarios" />
                    <rad:RadTab Text="Instalaciones" runat="server" PageViewID="rtvInstalaciones" />
                </Tabs>
            </rad:RadTabStrip2>

            <rad:RadMultiPage ID="MultiPage" runat="server" SelectedIndex="0" Width="100%">
                <rad:RadPageView ID="rtvIdentidad" runat="server">
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
                                <%--<asp:CustomValidator ID="CustomValidator13" runat="server"
                            ControlToValidate="txtRut"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />--%>
                        -
                    <WebControls:TextBox2 ID="txtDv" Width="35px" runat="server" onblur="validaRut()" MaxLength="1" />
                                <%--<asp:CustomValidator ID="CustomValidator14" runat="server"
                            ControlToValidate="txtDv"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" />--%>
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
                                <%--<asp:CustomValidator ID="CustomValidator3" runat="server"
                            ControlToValidate="txtIdentificador"
                            ValidateEmptyText="true"
                            ClientValidationFunction="validaControl"
                            ValidationGroup="Identidad" /> --%>
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
                        <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar" />
                    </div>
                </rad:RadPageView>
                <rad:RadPageView ID="rtvDocumentosAdjuntos" runat="server">
                    </br>
                            <div class="SubTitulos">Documentos</div>
                    <rad:RadGrid2 ID="GridDocumentos" runat="server" AllowPaging="false" OnItemDataBound="GridDocumentos_ItemDataBound" OnItemCreated="GridDocumentos_ItemCreated">
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="cdo_id, cdo_id_cliente">
                            <CommandItemTemplate>
                                <div>
                                    <asp:LinkButton ID="lnkNuevoDocumento" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevoDocumento_Click" />
                                    <asp:LinkButton ID="lnkEliminarDocumento" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarDocumento_Click"
                                        OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <ClientSettings>
                        </ClientSettings>
                    </rad:RadGrid2>
                </rad:RadPageView>
                <rad:RadPageView ID="rtvUsuarios" runat="server">
                    <asp:Panel ID="pnlUsuarios" runat="server" Visible="false">
                        </br>
                        <div class="SubTitulos">Usuarios</div>
                        <rad:RadGrid2 ID="gridUsuarios" runat="server" OnItemDataBound="gridUsuarios_ItemDataBound" AllowPaging="false">
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="usu_id">
                                <CommandItemTemplate>
                                    <div>
                                        <asp:LinkButton ID="lnkNuevoUsuario" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevoUsuario_Click" />
                                        <asp:LinkButton ID="lnkEliminarUsuario" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarUsuario_Click"
                                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                                        <asp:LinkButton ID="lnkReset" runat="server" Text="Reset Password" CssClass="icono_eliminar" OnClick="lnkReset_Click"
                                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea resetear las claves de los usuarios seleccionados?');" />
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <ClientSettings>
                            </ClientSettings>
                        </rad:RadGrid2>
                    </asp:Panel>
                </rad:RadPageView>
                <rad:RadPageView ID="rtvPlanesTarifarios" runat="server">
                    <asp:Panel ID="pnlPlan" runat="server" Visible="false">
                        </br>
                        <div class="SubTitulos">Planes Tarifarios</div>
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
                    </asp:Panel>

                </rad:RadPageView>
                <rad:RadPageView ID="rtvInstalaciones" runat="server">
                    <asp:Panel ID="pnlInstalaciones" runat="server" Visible="false">
                        </br>
                         <div class="SubTitulos">Instalaciones</div>
                        <rad:RadGrid2 ID="GridInstalaciones" runat="server" OnItemDataBound="gridInstalaciones_ItemDataBound" AllowPaging="false">
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="cin_id">
                                <CommandItemTemplate>
                                    <div>
                                        <asp:LinkButton ID="lnkNuevaInstalacion" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnkNuevaInstalacion_Click"    />
                                        <asp:LinkButton ID="lnkEliminarInstalacion" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminarInstalacion_Click"
                                            OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />

                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <ClientSettings>
                            </ClientSettings>
                        </rad:RadGrid2>
                    </asp:Panel>
                </rad:RadPageView>
            </rad:RadMultiPage>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
