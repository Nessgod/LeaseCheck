<%@ Page Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="ClientePropiedad.aspx.cs" Inherits="View_Clientes_Identidad_ClientePropiedad" %>

<asp:Content ID="ContenHead" ContentPlaceHolderID="chpScript" runat="server">

    <style>
        label[for='esDepaSi'], label[for='esCasaSi'] {
            cursor: pointer;
        }

        .SubTitulos label {
            font-weight: 600;
            font-size: 1.2rem;
            display: block;
            margin: 15px 0 10px;
        }

        textarea,
        .WebControls_TextArea2 {
            border-radius: 6px;
            padding: 10px;
            border: 1px solid #ccc;
            font-size: 14px;
            width: 100%;
            resize: vertical;
        }
    </style>
    <script type="text/javascript" language="javascript">
        //Cierra el RadWindow"
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
        function abrirPropiedadMedio(query) {
            var oWin = $find("<%=rwiMedios.ClientID %>");
            console.log(oWin);
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClientePropiedadMedio.aspx") %>?query=' + query);
            oWin.show();

        }


        function refresh() {
            __doPostBack("<%=GridImagenes.ClientID %>", '');
        }


        function abrirVisualizador(query) {
            var oWin = $find("<%=rwiMedios.ClientID %>");
            console.log(query);
            oWin.setUrl('<%=ResolveUrl("~/View/Clientes/Identidad/ClientePropiedadMedioVisualizador.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }




        $(document).ready(function () {
            manejarCasaDepa();
            manejarEstacionamiento();
            manejarBodega();
        });


        // Función para manejar la visibilidad del div de DEPA o CASA
        function manejarCasaDepa() {
            const chkDepaSi = $("#<%= esDepaSi.ClientID %>");
            const chkCasaSi = $("#<%= esCasaSi.ClientID %>");

            const divDepartamento = $("#<%= divDepartamentoPiso.ClientID %>");
            const txtUbicacionPiso = $("#<%= txtUbicacionPiso.ClientID %>");
            const divTipos = $("#<%= divTipos.ClientID %>");
            const divSeleccionAdicional = $("#<%= divSeleccionesDepaAdicionales.ClientID %>");
            const divCantidades = $("#<%= divCantidades.ClientID %>");
            const divSuperficies = $("#<%= divSuperficies.ClientID %>");
            const divSeleccionTipoPropiedad = $("#<%= divSeleccionTipoPropiedad.ClientID %>");


            const datosExistentes = $("#<%= hfDatosExistentes.ClientID %>").val().toLowerCase() === "true";
            console.log(datosExistentes);

            function mostrarSeccionesDepa() {
                divDepartamento.fadeIn(500);
                divTipos.fadeIn(500);
                divCantidades.fadeIn(500);
                divSuperficies.fadeIn(500);
                divSeleccionAdicional.fadeIn(500);
            }

            function mostrarSeccionesCasa() {
                divTipos.fadeIn(500);
                divCantidades.fadeIn(500);
                divSuperficies.fadeIn(500);
            }

            function ocultarSeccionesDepa() {
                divDepartamento.fadeOut(500);
                divSeleccionAdicional.fadeOut(500);
                txtUbicacionPiso.val('');
            }

            function ocultarTodo() {
                divDepartamento.fadeOut(500);
                divTipos.fadeOut(500);
                divCantidades.fadeOut(500);
                divSuperficies.fadeOut(500);
                divSeleccionAdicional.fadeOut(500);
                txtUbicacionPiso.val('');
            }

            // Estado inicial (en caso de que haya postback)
            if (datosExistentes) {
                divSeleccionTipoPropiedad.fadeOut(500);

                mostrarSeccionesDepa();
            } else if (chkDepaSi.prop("checked")) {
                mostrarSeccionesDepa();
                chkCasaSi.prop("checked", false);
            } else if (chkCasaSi.prop("checked")) {
                mostrarSeccionesCasa();
                chkDepaSi.prop("checked", false);
            } else {
                ocultarTodo();
            }

            // Al seleccionar DEPA (también se maneja al hacer clic en el label)
            chkDepaSi.change(function () {
                if ($(this).prop("checked")) {
                    chkCasaSi.prop("checked", false);
                    mostrarSeccionesDepa();
                } else {
                    ocultarTodo();
                }
            });

            // Al seleccionar CASA (también se maneja al hacer clic en el label)
            chkCasaSi.change(function () {
                if ($(this).prop("checked")) {
                    chkDepaSi.prop("checked", false);
                    ocultarSeccionesDepa();
                    mostrarSeccionesCasa();
                } else {
                    ocultarTodo();
                }
            });

            // Asegurar que el clic en las etiquetas también active los RadioButtons
            $("label[for='esDepaSi']").click(function () {
                chkDepaSi.prop("checked", true);
                chkCasaSi.prop("checked", false);
                mostrarSeccionesDepa();
            });

            $("label[for='esCasaSi']").click(function () {
                chkCasaSi.prop("checked", true);
                chkDepaSi.prop("checked", false);
                ocultarSeccionesDepa();
                mostrarSeccionesCasa();
            });
        }


        // Función para manejar la visibilidad del div de Estacionamiento
        function manejarEstacionamiento() {
            const chkEstacionamientoSi = $("#<%= rdeSi.ClientID %>");
            const chkEstacionamientoNo = $("#<%= rdeNo.ClientID %>");
            const divEstacionamiento = $("#<%= divEstacionamiento.ClientID %>");
            const txtValorEstacionamiento = $("#<%= txtValorEstacionamiento.ClientID %>");
            const txtCantidadEstacionamiento = $("#<%= txtCantidadEstacionamiento.ClientID %>");

            // Mostrar u ocultar en carga
            if (chkEstacionamientoSi.prop("checked")) {
                divEstacionamiento.fadeIn(500);
            } else {
                divEstacionamiento.fadeOut(500);
            }

            // Mostrar al seleccionar "Sí"
            chkEstacionamientoSi.change(function () {
                if ($(this).prop("checked")) {
                    divEstacionamiento.fadeIn(500);
                }
            });

            // Ocultar al seleccionar "No" y limpiar campos
            chkEstacionamientoNo.change(function () {
                if ($(this).prop("checked")) {
                    console.log("Entré al NO");
                    divEstacionamiento.fadeOut(500);
                    txtValorEstacionamiento.val('');
                    txtCantidadEstacionamiento.val('');
                }
            });
        }

        // Función para manejar la visibilidad del div de Bodega
        function manejarBodega() {
            const chkBodegaSi = $("#<%= rdbSi.ClientID %>");
            const chkBodegaNo = $("#<%= rdbNo.ClientID %>");
            const divBodega = $("#<%= divBodega.ClientID %>");
            const txtValorBodega = $("#<%= txtValorBodega.ClientID %>");
            const txtCantidadBodega = $("#<%= txtCantidadBodega.ClientID %>");

            // Mostrar u ocultar en carga
            if (chkBodegaSi.prop("checked")) {
                divBodega.fadeIn(500);
            } else {
                divBodega.fadeOut(500);
            }

            // Mostrar al seleccionar "Sí"
            chkBodegaSi.change(function () {
                if ($(this).prop("checked")) {
                    divBodega.fadeIn(500);
                }
            });

            // Ocultar al seleccionar "No" y limpiar campos
            chkBodegaNo.change(function () {
                if ($(this).prop("checked")) {
                    console.log("Entré al NO");
                    divBodega.fadeOut(500);
                    txtValorBodega.val('');
                    txtCantidadBodega.val('');
                }
            });
        }

        Sys.Application.add_load(function () {
            manejarCasaDepa();
            manejarBodega();
            manejarEstacionamiento();
        });


        function mostrarObservacionSweetAlert(observacion) {
            Swal.fire({
                title: 'Observación',
                text: observacion,
                icon: 'info',
                confirmButtonText: 'Cerrar'
            });
        }


    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <rad:RadWindow2 ID="rwiMedios" runat="server" Title="Medios" Width="1000" Height="500 " />
    <asp:UpdatePanel runat="server" ID="udPanel" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="SubTitulos">
                <asp:Label ID="lblTituloUsuario" runat="server" />
            </div>
            <rad:RadTabStrip2 ID="ragTab" runat="server" MultiPageID="MultiPage" Skin="Bootstrap" RenderMode="Lightweight" SelectedIndex="0">
                <Tabs>
                    <rad:RadTab Text="Identidad" runat="server" PageViewID="rtvIdentidad" />
                    <rad:RadTab Text="Datos Legales" runat="server" PageViewID="rtvDatosLegal" Visible="false" />
                    <rad:RadTab Text="Ficha" runat="server" PageViewID="rtvFicha" Visible="false" />
                    <rad:RadTab Text="Detalle Publicación" runat="server" PageViewID="rtvDetallePublicacion" Visible="false" />
                    <rad:RadTab Text="Imagen / Video" runat="server" PageViewID="rtvImagenVideo" Visible="false" />
                    <rad:RadTab Text="Estado de la Propiedad" runat="server" PageViewID="rtvEstadoPropiedad" Visible="false" />
                </Tabs>
            </rad:RadTabStrip2>
            <rad:RadMultiPage ID="MultiPage" runat="server" SelectedIndex="0">

                <%--  IDENTIDAD PROPIEDAD--%>
                <rad:RadPageView ID="rtvIdentidad" runat="server">
                    <br />
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>ID</label>
                        </div>
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <asp:Label ID="lblID" runat="server" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtTitulo">Titulo(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtTitulo" runat="server" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server"
                                ControlToValidate="txtTitulo"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Tipo Propiedad(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboTpoPropiedad" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Tipo Servicio(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboTpoServicio" runat="server" Width="100%" Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboServicio_SelectedIndexChanged" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Tipo Entrega(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboTpoEntrega" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtFechaEntrega">Fecha de Entrega(*)</label>
                        </div>
                        <div class="form-group col-lg-4 col-md-4 col-xs-12">
                            <WebControls:Calendar ID="txtFechaEntrega" runat="server" Calendar-To-Control="txtFechaEntrega" />
                            <asp:CustomValidator ID="CustomValidator12" runat="server"
                                ControlToValidate="txtFechaEntrega"
                                ErrorMessage="Debe elegir una fecha"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />

                        </div>
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtObservacion">Observacion</label>
                        </div>
                        <div class="form-group col-lg-4 col-md-4 col-xs-12">
                            <WebControls:TextBox2 ID="txtObservacion" runat="server" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>País(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboPais" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Región(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboRegion" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Provincia(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboProvincia" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Comuna(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboComuna" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtCalle">Calle(*)</label>
                        </div>
                        <div class="form-group col-lg-4 col-md-4 col-xs-12">
                            <WebControls:TextBox2 ID="txtCalle" runat="server" />
                            <asp:CustomValidator ID="CustomValidator7" runat="server"
                                ControlToValidate="txtCalle"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />

                        </div>
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtNumeroPropiedad">Número de Propiedad(*)</label>
                        </div>
                        <div class="form-group col-lg-4 col-md-4 col-xs-12">
                            <WebControls:TextBox2 ID="txtNumeroPropiedad" runat="server" />
                            <asp:CustomValidator ID="CustomValidator8" runat="server"
                                ControlToValidate="txtNumeroPropiedad"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtValorUf">Valor UF (*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtValorUf" runat="server" />
                            <asp:CustomValidator ID="CustomValidator2" runat="server"
                                ControlToValidate="txtValorUf"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtValorCLP">Valor Venta (*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtValorCLP" runat="server" />
                            <asp:CustomValidator ID="CustomValidator3" runat="server"
                                ControlToValidate="txtValorCLP"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtValorCLP">Valor Evaluo fiscal (*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtEvaluoFiscal" runat="server" />
                            <asp:CustomValidator ID="CustomValidator15" runat="server"
                                ControlToValidate="txtEvaluoFiscal"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="Identidad" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-xs-12">
                            <label>¿Tiene Contribucciones?</label>
                        </div>
                        <div class="col-lg-10 col-md-10 col-xs-12">
                            <asp:RadioButton ID="rdcSi" runat="server" Text="SI" GroupName="Contribucciones" />
                            <asp:RadioButton ID="rdcNo" runat="server" Text="NO" GroupName="Contribucciones" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-xs-12">
                            <label>Derecho Municipal</label>
                        </div>
                        <div class="col-lg-10 col-md-10 col-xs-12">
                            <asp:RadioButton ID="rddSi" runat="server" Text="SI" GroupName="Derecho_Municipal" />
                            <asp:RadioButton ID="rddNo" runat="server" Text="NO" GroupName="Derecho_Municipal" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-xs-12">
                            <label>¿Estacionamiento?</label>
                        </div>
                        <div class="col-lg-10 col-md-10 col-xs-12">
                            <asp:RadioButton ID="rdeSi" runat="server" Text="SI" GroupName="Estacionamiento" />
                            <asp:RadioButton ID="rdeNo" runat="server" Text="NO" GroupName="Estacionamiento" />
                        </div>
                    </div>

                    <div class="row col-lg-12 col-md-12 col-xs-12" runat="server" id="divEstacionamiento" style="display: none">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtValorEstacionamiento">Valor Estacionamiento:</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtValorEstacionamiento" runat="server" />
                        </div>


                        <div class="row col-lg-12 col-md-12 col-xs-12 ">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtCantidadEstacionamiento">Cantidad Estacionamiento:</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <rad:RadNumericBox2 ID="txtCantidadEstacionamiento" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-xs-12">
                            <label>¿Bodega?</label>
                        </div>
                        <div class="col-lg-10 col-md-10 col-xs-12">
                            <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Bodega" />
                            <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Bodega" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12" runat="server" id="divBodega" style="display: none">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtValorBodega">Valor Bodega:</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtValorBodega" runat="server" />
                        </div>


                        <div class="row col-lg-12 col-md-12 col-xs-12 ">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtCantidadBodega">Cantidad Bodega</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <rad:RadNumericBox2 ID="txtCantidadBodega" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Estado(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboEstado" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                        </br>
                            <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar"
                                ValidationGroup="Identidad"
                                OnClick="btnGuardar_OnClick" />
                    </div>
                </rad:RadPageView>

                <%--    DATOS LEGALES DE PROPIEDAD --%>
                <rad:RadPageView ID="rtvDatosLegal" runat="server">
                    <br />

                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label>Propietario(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadComboBox2 ID="cboPropietario" runat="server" Width="100%" Filter="Contains" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtCalle">Rol(*)</label>
                        </div>
                        <div class="form-group col-lg-4 col-md-4 col-xs-12">
                            <WebControls:TextBox2 ID="txtRol" runat="server" />
                            <asp:CustomValidator ID="CustomValidator22" runat="server"
                                ControlToValidate="txtRol"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtFajas">Fojas(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtFojas" runat="server" />
                            <asp:CustomValidator ID="CustomValidator4" runat="server"
                                ControlToValidate="txtFojas"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtNumeroInscripcion">Número de Inscripción(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtNumeroInscripcion" runat="server" />
                            <asp:CustomValidator ID="CustomValidator5" runat="server"
                                ControlToValidate="txtNumeroInscripcion"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtAnioInscripcion">Año Inscripción(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtAnioInscripcion" runat="server"
                                NumberFormat-DecimalSeparator="false"
                                NumberFormat-GroupSeparator="" />
                            <asp:CustomValidator ID="CustomValidator6" runat="server"
                                ControlToValidate="txtAnioInscripcion"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />
                        </div>
                    </div>


                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtNumeroSitio">Número de Sitio(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtNumeroSitio" runat="server" />
                            <asp:CustomValidator ID="CustomValidator9" runat="server"
                                ControlToValidate="txtNumeroSitio"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />

                        </div>
                    </div>

                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtNumeroManzana">Número de Manzana(*)</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtNumeroManzana" runat="server" />
                            <asp:CustomValidator ID="CustomValidator10" runat="server"
                                ControlToValidate="txtNumeroManzana"
                                ValidateEmptyText="true"
                                ClientValidationFunction="validaControl"
                                ValidationGroup="DatoLegal" />

                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtConHabitacional">Conjunto Habitacional</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtConHabitacional" runat="server" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-xs-12">
                            <label>¿Inventario?</label>
                        </div>
                        <div class="col-lg-10 col-md-10 col-xs-12">
                            <asp:RadioButton ID="rdiSi" runat="server" Text="SI" GroupName="Inventario" />
                            <asp:RadioButton ID="rdiNo" runat="server" Text="NO" GroupName="Inventario" />
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12 ">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label class="control-label" for="txtCopiaLlaves">Cantidad Copia Llaves:</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <rad:RadNumericBox2 ID="txtCopiaLlaves" runat="server" />
                        </div>
                    </div>

                    <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                        </br>
                            <WebControls:PushButton ID="btnGuardarDatoLegales" runat="server" Text="Guardar"
                                ValidationGroup="DatoLegal"
                                OnClick="btnGuardarDatoLegales_Click" />
                    </div>
                </rad:RadPageView>

                <%-- FICHA DE PROPIEDAD --%>
                <rad:RadPageView ID="rtvFicha" runat="server">
                    <br />
                    <div class="row col-12 mb-3" style="display: none" id="divSeleccionTipoPropiedad" runat="server">
                        <div class="col-12 mb-0">
                            <label class="form-label fw-bold">Seleccione el tipo de propiedad:</label>
                        </div>

                        <div class="col-12 d-flex gap-4">
                            <!-- Opción Departamento -->
                            <div class="form-check form-check-inline">
                                <asp:RadioButton ID="esDepaSi" runat="server" GroupName="TipoPropiedad" CssClass="form-check-input" />
                                <label class="form-check-label" for="esDepaSi">🏢 Departamento</label>
                            </div>

                            <!-- Opción Casa -->
                            <div class="form-check form-check-inline">
                                <asp:RadioButton ID="esCasaSi" runat="server" GroupName="TipoPropiedad" CssClass="form-check-input" />
                                <label class="form-check-label" for="esCasaSi">🏠 Casa</label>
                            </div>
                        </div>
                    </div>

                    <div style="display: none" id="divSuperficies" runat="server">
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtFajas">Superficie Util(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtSuperficieUtil" runat="server" />
                                <asp:CustomValidator ID="CustomValidator11" runat="server"
                                    ControlToValidate="txtSuperficieUtil"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>

                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtSuperficieTotal">Superficie Total(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtSuperficieTotal" runat="server" />
                                <asp:CustomValidator ID="CustomValidator13" runat="server"
                                    ControlToValidate="txtSuperficieTotal"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>
                    </div>
                    <div style="display: none" id="divCantidades" runat="server">
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtCantidadDormitorios">Cantidad de Dormitorios(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <rad:RadNumericBox2 ID="txtCantidadDormitorios" runat="server" />
                                <asp:CustomValidator ID="CustomValidator16" runat="server"
                                    ControlToValidate="txtCantidadDormitorios"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12 ">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtCantidadBanios">Cantidad de Baños(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <rad:RadNumericBox2 ID="txtCantidadBanios" runat="server" />
                                <asp:CustomValidator ID="CustomValidator17" runat="server"
                                    ControlToValidate="txtCantidadBanios"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12 ">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtCantidadPisos">Cantidad de Pisos(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <rad:RadNumericBox2 ID="txtCantidadPisos" runat="server" />
                                <asp:CustomValidator ID="CustomValidator14" runat="server"
                                    ControlToValidate="txtCantidadPisos"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-12 col-md-12 col-xs-12" style="display: none" id="divDepartamentoPiso" runat="server">
                        <div class="form-group col-lg-2 col-md-2 col-xs-12">
                            <label  id="txtUbicacionPisoEtiq" runat="server" class="control-label" for="txtUbicacionPiso">Ubicación del piso:</label>
                        </div>
                        <div class="form-group col-lg-10 col-md-10 col-xs-12">
                            <WebControls:TextBox2 ID="txtUbicacionPiso" runat="server" />
                        </div>
                    </div>
                    <div style="display: none" id="divTipos" runat="server">
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtTipoPiso">Tipo de Piso(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtTipoPiso" runat="server" />
                                <asp:CustomValidator ID="CustomValidator18" runat="server"
                                    ControlToValidate="txtTipoPiso"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtTipoVentana">Tipo de Ventana(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtTipoVentana" runat="server" />
                                <asp:CustomValidator ID="CustomValidator19" runat="server"
                                    ControlToValidate="txtTipoVentana"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtConexionCocina">Tipo de Conexión de Cocina(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtConexionCocina" runat="server" />
                                <asp:CustomValidator ID="CustomValidator20" runat="server"
                                    ControlToValidate="txtConexionCocina"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="form-group col-lg-2 col-md-2 col-xs-12">
                                <label class="control-label" for="txtConexionCocina">Tipo de Conexión de Lavadora(*)</label>
                            </div>
                            <div class="form-group col-lg-10 col-md-10 col-xs-12">
                                <WebControls:TextBox2 ID="txtConexionLavadora" runat="server" />
                                <asp:CustomValidator ID="CustomValidator21" runat="server"
                                    ControlToValidate="txtConexionLavadora"
                                    ValidateEmptyText="true"
                                    ClientValidationFunction="validaControl"
                                    ValidationGroup="Ficha" />

                            </div>
                        </div>
                    </div>
                    <div style="display: none" id="divSeleccionesDepaAdicionales" runat="server">
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-xs-12">
                                <label>🍖 Quincho</label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-xs-12">
                                <asp:RadioButton ID="quinchoSi" runat="server" Text="Sí" GroupName="Quincho" />
                                <asp:RadioButton ID="quinchoNo" runat="server" Text="No" GroupName="Quincho" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-xs-12">
                                <label>🏊 Piscina</label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-xs-12">
                                <asp:RadioButton ID="piscinaSi" runat="server" Text="Sí" GroupName="Piscina" />
                                <asp:RadioButton ID="piscinaNo" runat="server" Text="No" GroupName="Piscina" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-xs-12">
                                <label>🔥 Calefacción</label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-xs-12">
                                <asp:RadioButton ID="calefaccionSi" runat="server" Text="Sí" GroupName="Calefacción" />
                                <asp:RadioButton ID="calefaccionNo" runat="server" Text="No" GroupName="Calefacción" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-xs-12">
                                <label>💪 Gimnasio</label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-xs-12">
                                <asp:RadioButton ID="gimnasioSi" runat="server" Text="Sí" GroupName="Gimnasio" />
                                <asp:RadioButton ID="gimnasioNo" runat="server" Text="No" GroupName="Gimnasio" />
                            </div>
                        </div>
                        <div class="row col-lg-12 col-md-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-xs-12">
                                <label>🏢 Salón Múltiple</label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-xs-12">
                                <asp:RadioButton ID="salonMultipleSi" runat="server" Text="Sí" GroupName="SalonMultiple" />
                                <asp:RadioButton ID="salonMultipleNo" runat="server" Text="No" GroupName="SalonMultiple" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                        </br>
                          <WebControls:PushButton ID="btnGuardarFicha" runat="server" Text="Guardar"
                              ValidationGroup="Ficha"
                              OnClick="btnGuardarFicha_Click" />
                    </div>
                </rad:RadPageView>

                <%-- DETALLES DE PUBLICACIÓN DE PROPIEDAD --%>
                <rad:RadPageView ID="rtvDetallePublicacion" runat="server">
                    <br />
                    <!-- TOGGLE DE INSTRUCCIONES -->
                    <div class="row">
                        <div class="col-lg-12 text-start mb-3">
                            <button type="button" class="btn btn-sm btn-light border rounded-pill px-4 py-2 shadow-sm" data-toggle="collapse" data-target="#instruccionesServicios" aria-expanded="false" aria-controls="instruccionesServicios">
                                🛈 Instrucciones
                            </button>
                            <div id="instruccionesServicios" class="collapse mt-3">
                                <div class="p-3 bg-light border-start border-4 border-primary rounded shadow-sm text-start medium">
                                    <strong class="text-primary">Instrucciones:</strong><br>
                                    Ingrese los elementos en cada campo separados por comas.<br>
                                    <em>Ejemplo: Metro, Paradero, Ciclovía</em>
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- DESCRIPCIÓN DE PROPIEDAD -->
                    <div class="row">
                        <div class="form-group col-lg-2">
                            <label for="txtDescripcion">Descripción de Propiedad:</label>
                        </div>
                        <div class="form-group col-lg-10">
                            <WebControls:TextArea2 ID="txtDescripcion" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Amplio, Luminoso, Buena ubicación" />
                        </div>
                    </div>

                    <!-- SUBTÍTULO -->
                    <div class="SubTitulos mb-3">
                        <label>Servicios Cercanos</label>
                    </div>

                    <!-- BLOQUES DE SERVICIOS -->
                    <asp:PlaceHolder ID="ServiciosPlaceholder" runat="server">
                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtConectividad">Conectividad:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtConectividad" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Metro, Paradero, Ciclovía" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtComercial">Centro Comercial:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtComercial" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Mall, Supermercado, Tiendas" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtServiciosSalud">Servicios de Salud:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtServiciosSalud" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: CESFAM, Clínica, Hospital" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtEducacion">Educación:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtEducacion" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Colegio, Universidad, Jardín infantil" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtAreaVerde">Áreas Verdes:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtAreaVerde" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Plaza, Parque, Cancha" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtRestaurant">Restaurant:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtRestaurant" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Café, Pizzería, Fuente de Soda" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtSeguridad">Seguridad:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtSeguridad" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Comisaría, Guardia, Cámaras" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-2">
                                <label for="txtTransportes">Transportes:</label>
                            </div>
                            <div class="form-group col-lg-10">
                                <WebControls:TextArea2 ID="txtTransportes" runat="server" ToolTip="Separar los elementos por coma (,)" Placeholder="Ej: Colectivo, Bus, Taxi" />
                            </div>
                        </div>
                    </asp:PlaceHolder>


                    <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                        </br>
                         <WebControls:PushButton ID="btnDetallePublicacion" runat="server" Text="Guardar"
                             ValidationGroup="DetallePublicacion"
                             OnClick="btnDetallePublicacion_Click" />
                    </div>
                </rad:RadPageView>

                <%-- IMAGEN / VIDEO DE PROPIEDAD --%>
                <rad:RadPageView ID="rtvImagenVideo" runat="server">
                    <asp:UpdatePanel runat="server" ID="updGrid" UpdateMode="Conditional">
                        <ContentTemplate>
                            <rad:RadGrid2 ID="GridImagenes" runat="server" AllowPaging="true" OnItemDataBound="GridDocumentos_ItemDataBound" OnItemCreated="GridDocumentos_ItemCreated">
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="cpm_id, cpm_imagen, cpm_video">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </rad:RadPageView>

                <%-- GESTIÓN DE ESTADOS DE PROPIEDAD --%>
                <rad:RadPageView ID="rtvEstadoPropiedad" runat="server">
                    <br />
                    <!-- Filtro de fechas -->
                    <div class="form-group row col-lg-12 col-md-12 col-xs-12">
                        <label for="txtFechaInicio" class="control-label" style="margin-right: 10px;">Desde:</label>
                        <rad:RadDatePicker ID="txtFechaInicio" runat="server" Calendar-To-Control="txtFechaInicio" />

                        <label for="txtFechaFin" class="control-label" style="margin-left: 20px; margin-right: 10px;">Hasta:</label>
                        <rad:RadDatePicker ID="txtFechaFin" runat="server" Calendar-To-Control="txtFechaFin" />

                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" Style="margin-left: 20px;" />
                    </div>

                    <asp:CustomValidator ID="CustomValidator23" runat="server"
                        ControlToValidate="txtFechaInicio"
                        ErrorMessage="Debe elegir una fecha de inicio"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />

                    <asp:CustomValidator ID="CustomValidator24" runat="server"
                        ControlToValidate="txtFechaFin"
                        ErrorMessage="Debe elegir una fecha de fin"
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl"
                        ValidationGroup="Identidad" />

                    <div class="SubTitulos"></div>

                    <asp:UpdatePanel runat="server" ID="updEstados" UpdateMode="Conditional">
                        <ContentTemplate>
                            <!-- Agregar estado -->
                            <div class="form-row align-items-end">
                                <!-- Combo Estado -->
                                <div class="form-group col-lg-4 col-md-6 col-sm-12">
                                    <label for="cboEstadoPropiedad"><strong>Agregar estado</strong></label>
                                    <rad:RadComboBox2 ID="cboEstadoPropiedad" runat="server" Width="100%" Filter="Contains" />
                                </div>

                                <!-- Observación -->
                                <div class="form-group col-lg-4 col-md-6 col-sm-12">
                                    <label for="txtObservacionEstado">Observación</label>
                                    <WebControls:TextBox2 ID="txtObservacionEstado" runat="server" CssClass="form-control" />
                                </div>

                                <!-- Botón Guardar -->
                                <div class="form-group col-lg-2 col-md-6 col-sm-12">
                                    <label>&nbsp;</label>
                                    <WebControls:PushButton ID="btnGuardarEstadoPropiedad" runat="server"
                                        Text="Guardar" CssClass="btn btn-success btn-block"
                                        ValidationGroup="DetallePublicacion"
                                        OnClick="btnGuardarEstadoPropiedad_Click" />
                                </div>

                                <!-- Botón Mostrar/Ocultar -->
                                <div class="form-group col-lg-2 col-md-6 col-sm-12">
                                    <label>&nbsp;</label>
                                    <button class="btn btn-primary btn-block" type="button"
                                        data-toggle="collapse" data-target="#collapseGridEstados"
                                        aria-expanded="false" aria-controls="collapseGridEstados">
                                        Mostrar/Ocultar Tabla
                                    </button>
                                </div>
                            </div>

                            <!-- RadGrid colapsable -->
                            <div class="collapse" id="collapseGridEstados">
                                <div class="card card-body">
                                    <rad:RadGrid2 ID="GridEstados" runat="server" AllowPaging="true">
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="cea_id">
                                            <CommandItemTemplate>
                                                <div>
                                                    <asp:LinkButton ID="lnkEliminarEstado" runat="server" Text="Eliminar" CssClass="icono_eliminar"
                                                        OnClick="lnkEliminarEstado_Click"
                                                        OnClientClick="return ConfirSweetAlert(this, '', '¿Está seguro(a) que desea eliminar los registros seleccionados?');" />
                                                </div>
                                            </CommandItemTemplate>
                                        </MasterTableView>
                                        <ClientSettings>
                                        </ClientSettings>
                                    </rad:RadGrid2>
                                </div>
                            </div>

                            <!-- Estado actual -->
                            <div class="row col-lg-12 col-md-12 col-xs-12">
                                <asp:Literal ID="txtCronogramaEstadoPropiedad" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </rad:RadPageView>

            </rad:RadMultiPage>


            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                <WebControls:PushButton ID="PushButton2" runat="server" Text="Cerrar" CssClass="ButtonCerrar"
                    OnClientClick="closeWindow();" />
            </div>
            <asp:HiddenField ID="hfDatosExistentes" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
