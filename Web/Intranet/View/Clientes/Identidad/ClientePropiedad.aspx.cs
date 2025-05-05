using System;
using System.Web.Script.Services;
using System.Web.Services;
using LeaseCheck.Root.Model;
using LeaseCheck.Root.Controller;
using Telerik.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using AjaxControlToolkit.HTMLEditor;
using System.Linq;
using iText.Kernel.Pdf.Canvas.Wmf;
using WsCorreo;
using System.Collections.Generic;
using System.Text;



public partial class View_Clientes_Identidad_ClientePropiedad : System.Web.UI.Page
{
    #region Variables Globales
    public int Id
    {
        get { return Convert.ToInt32(ViewState["id"]); }
        set { ViewState.Add("id", value); }
    }
    public int IdDetallePublicacion
    {
        get { return Convert.ToInt32(ViewState["IdDetallePublicacion"]); }
        set { ViewState.Add("IdDetallePublicacion", value); }
    }

    public int IdDatoLegal
    {
        get { return Convert.ToInt32(ViewState["IdDatoLegal"]); }
        set { ViewState.Add("IdDatoLegal", value); }
    }

    public int IdFecha
    {
        get { return Convert.ToInt32(ViewState["IdFecha"]); }
        set { ViewState.Add("IdFecha", value); }
    }

    public int TipoServicio
    {
        get { return Convert.ToInt32(ViewState["TipoServicio"]); }
        set { ViewState.Add("TipoServicio", value); }
    }

    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    public bool esImagen
    {
        get { return Convert.ToBoolean(ViewState["esImagen"]); }
        set { ViewState.Add("esImagen", value); }
    }

    public bool esVideo
    {
        get { return Convert.ToBoolean(ViewState["esVideo"]); }
        set { ViewState.Add("esVideo", value); }
    }

    private ClienteController controller = new ClienteController();
    ClientePropiedad propiedad = new ClientePropiedad();
    ClientePropiedadDatoLegal datolegal = new ClientePropiedadDatoLegal();
    ClientePropiedadFicha ficha = new ClientePropiedadFicha();
    ClientePropiedadMedio clientePropiedadMedio = new ClientePropiedadMedio();
    ClientePropiedadDetallePublicacion detallePublicacion = new ClientePropiedadDetallePublicacion();
    ClientePropiedadController clientePropiedadController = new ClientePropiedadController();


    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_usuarios.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

        if (!IsPostBack)
        {
            //Recupero el query string
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;
                    case "IdDatoLegal":
                        IdDatoLegal = Int32.Parse(array[1].ToString());
                        break;
                }
            }

            txtFechaFin.SelectedDate = DateTime.Today;
            ConfigurarGrid();
            ConfigurarGridEstado();

        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
            CargarDatoLegal();
            CargarFicha();
            CargarDetallePublicacion();
            CargarCronogramaEstadoPropiedad(null, null);
            CargaGrid();
            CargaGridEstados();


            CargarTpoPropiedad(cboTpoPropiedad);
            CargarTpoServicio(cboTpoServicio);
            CargarTpoEntrega(cboTpoEntrega);
            CargarPaises(cboPais);
            CargarRegiones(cboRegion);
            CargarProvincias(cboProvincia);
            CargarComunas(cboComuna);
            CargarPropietarios(cboPropietario);

            updGrid.Update();

        }


        CargaGridEstados();
        CargaGrid();
        updGrid.Update();
        Validaciones();
        CargarCronogramaEstadoPropiedad(null, null);
    }

    #region Combobox Carga
    private void CargarTpoPropiedad(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var propiedad = controller.GetTipoPropiedad();
            if (propiedad.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = propiedad;
            cbo.DataValueField = "tpr_id";
            cbo.DataTextField = "tpr_nombre";
            cbo.DataBind();
        }
    }

    private void CargarTpoServicio(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var servicio = controller.GetTipoServicio();
            if (servicio.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = servicio;
            cbo.DataValueField = "tsc_id";
            cbo.DataTextField = "tsc_nombre";
            cbo.DataBind();
        }
    }

    private void CargarTpoEntrega(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var entrega = controller.GetTipoEntrega();
            if (entrega.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = entrega;
            cbo.DataValueField = "cpt_id";
            cbo.DataTextField = "cpt_nombre";
            cbo.DataBind();
        }
    }

    private void CargarPaises(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var paises = controller.GetPaises();
            if (paises.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = paises;
            cbo.DataValueField = "pai_id";
            cbo.DataTextField = "pai_nombre";
            cbo.DataBind();
        }
    }

    private void CargarRegiones(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var region = controller.GetRegiones(new LeaseCheck.Root.Model.Region());
            if (region.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = region;
            cbo.DataValueField = "rgn_id";
            cbo.DataTextField = "rgn_nombre";
            cbo.DataBind();
        }
    }

    private void CargarProvincias(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var provincia = controller.GetProvincias(new Provincia());
            if (provincia.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = provincia;
            cbo.DataValueField = "pro_id";
            cbo.DataTextField = "pro_nombre";
            cbo.DataBind();
        }
    }

    private void CargarComunas(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var comunas = controller.GetComunas(new Comuna());
            if (comunas.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = comunas;
            cbo.DataValueField = "cmn_id";
            cbo.DataTextField = "cmn_nombre";
            cbo.DataBind();
        }
    }

    private void CargarPropietarios(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var propietarios = controller.GetClienteUsuarioPropietarios();
            if (propietarios.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = propietarios;
            cbo.DataValueField = "usu_id";
            cbo.DataTextField = "NOMBRE_COMPLETO";
            cbo.DataBind();
        }
    }
    #endregion
    protected void Validaciones()
    {
        if (Id > 0)
        {
            ragTab.Tabs[1].Visible = true;
            ragTab.Tabs[2].Visible = true;
            ragTab.Tabs[3].Visible = true;
            ragTab.Tabs[4].Visible = true;
            ragTab.Tabs[5].Visible = true;

        }
        else
        {
            lblTituloUsuario.Text = "Nueva Propiedad";
        }
    }

    protected void ragTab_TabClick(object sender, RadTabStripEventArgs e)
    {
        //switch (e.Tab.Index)
        //{
        //    case 1: // Segunda pestaña
        //        CargarDatoLegal();
        //        break;
        //    case 2: // Tercera pestaña
        //        CargarFicha();
        //        break;
        //    case 3: // Cuarta pestaña
        //        CargarDetallePublicacion();
        //        break;
        //}
    }

    #region Creación de Propiedad
    protected void CargarDatos()
    {
        if (Id > 0)
        {
            #region Propiedad
            propiedad.cpd_id = Id;

            propiedad = clientePropiedadController.GetClientePropiedad(propiedad);


            TipoServicio = propiedad.cpd_tipo_servicio;
            CargarEstadosPropiedad(cboEstadoPropiedad, TipoServicio);
            CargarEstadosPropiedad(cboEstado, TipoServicio);
            lblTituloUsuario.Text = "ID: " + propiedad.cpd_id + " | " + propiedad.TIPO_PROPIEDAD + ": " + propiedad.cpd_titulo;


            cboTpoPropiedad.SelectedValue = propiedad.cpd_tipo_propiedad.ToString();
            cboTpoServicio.SelectedValue = propiedad.cpd_tipo_servicio.ToString();
            cboTpoEntrega.SelectedValue = propiedad.cpd_tipo_entrega.ToString();

            cboEstado.SelectedValue = propiedad.cpd_estado.ToString();
            cboEstado.Enabled = false;

            cboPais.SelectedValue = propiedad.cpd_pais.ToString();
            cboRegion.SelectedValue = propiedad.cpd_region.ToString();
            cboProvincia.SelectedValue = propiedad.cpd_provincia.ToString();
            cboComuna.SelectedValue = propiedad.cpd_comuna.ToString();

            txtCalle.Text = propiedad.cpd_calle.ToString();
            txtNumeroPropiedad.Text = propiedad.cpd_numero_propiedad.ToString();
            txtFechaEntrega.Value = propiedad.cpd_fecha_entrega;
            txtObservacion.Text = propiedad.cpd_fecha_entrega_detalle.ToString();

            lblID.Text = propiedad.cpd_id.ToString();
            txtTitulo.Text = propiedad.cpd_titulo;
            txtCantidadBodega.Text = propiedad.cpd_cantidad_bodega.ToString();
            txtCantidadEstacionamiento.Text = propiedad.cpd_cantidad_estacionamiento.ToString();
            txtValorEstacionamiento.Text = propiedad.cpd_valor_estacionamiento.ToString();
            txtValorBodega.Text = propiedad.cpd_valor_bodega.ToString();
            txtValorCLP.Text = propiedad.cpd_valor_venta.ToString();
            txtValorUf.Text = propiedad.cpd_valor_uf.ToString();
            txtEvaluoFiscal.Text = propiedad.cpd_valor_evaluo_fiscal.ToString();

            // Crear un filtro para obtener los tipos de cuenta asociados al banco
            Provincia filtro = new Provincia();
            filtro.pro_region = propiedad.cpd_region;
            var provincias = controller.GetProvincias(filtro);
            // Limpiar y cargar el combo de tipos de cuenta
            cboProvincia.Items.Clear();
            if (provincias.Count > 0)
            {
                cboProvincia.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                cboProvincia.AppendDataBoundItems = true;
            }
            cboProvincia.DataSource = provincias;
            cboProvincia.DataValueField = "pro_id";
            cboProvincia.DataTextField = "pro_nombre";
            cboProvincia.DataBind();

            Comuna comuna = new Comuna();
            comuna.cmn_provincia = propiedad.cpd_provincia;

            // Obtener los tipos de cuenta asociados al banco
            var comunas = controller.GetComunas(comuna);

            // Limpiar y cargar el combo de tipos de cuenta
            cboComuna.Items.Clear();
            if (comunas.Count > 0)
            {
                cboComuna.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                cboComuna.AppendDataBoundItems = true;
            }
            cboComuna.DataSource = comunas;
            cboComuna.DataValueField = "cmn_id";
            cboComuna.DataTextField = "cmn_nombre";
            cboComuna.DataBind();

            if (bool.Parse(propiedad.cpd_estacionamiento.ToString()))
            {
                rdeSi.Checked = true;
                rdeNo.Checked = false;
            }
            else
            {
                rdeSi.Checked = false;
                rdeNo.Checked = true;
            }

            if (bool.Parse(propiedad.cpd_bodega.ToString()))
            {
                rdbSi.Checked = true;
                rdbNo.Checked = false;
            }
            else
            {
                rdbSi.Checked = false;
                rdbNo.Checked = true;
            }

            if (bool.Parse(propiedad.cpd_contribucciones.ToString()))
            {
                rdcSi.Checked = true;
                rdcNo.Checked = false;
            }
            else
            {
                rdcSi.Checked = false;
                rdcNo.Checked = true;
            }

            if (bool.Parse(propiedad.cpd_derecho_municipal.ToString()))
            {
                rddSi.Checked = true;
                rddNo.Checked = false;
            }
            else
            {
                rddSi.Checked = false;
                rddNo.Checked = true;
            }
            #endregion

        }
    }


    protected void btnGuardar_OnClick(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
            ClientePropiedad propiedad = new ClientePropiedad();
            propiedad.cpd_id = Id;

            int tipoEntrega = 0;
            int tipoPropiedad = 0;
            int tipoServicio = 0;
            int pais = 0;
            int region = 0;
            int provincia = 0;
            int comuna = 0;
            int estado = 0;
            int cantBodega = 0;
            int valorBodega = 0;
            int cantEst = 0;
            int valorEst = 0;

            int.TryParse(cboTpoEntrega.SelectedValue, out tipoEntrega);
            int.TryParse(cboTpoPropiedad.SelectedValue, out tipoPropiedad);
            int.TryParse(cboTpoServicio.SelectedValue, out tipoServicio);
            int.TryParse(cboPais.SelectedValue, out pais);
            int.TryParse(cboRegion.SelectedValue, out region);
            int.TryParse(cboProvincia.SelectedValue, out provincia);
            int.TryParse(cboComuna.SelectedValue, out comuna);
            int.TryParse(cboEstado.SelectedValue, out estado);
            int.TryParse(txtCantidadBodega.Text, out cantBodega);
            int.TryParse(txtValorBodega.Text, out valorBodega);
            int.TryParse(txtCantidadEstacionamiento.Text, out cantEst);
            int.TryParse(txtValorEstacionamiento.Text, out valorEst);

            propiedad.cpd_tipo_entrega = tipoEntrega;
            propiedad.cpd_fecha_entrega = txtFechaEntrega.Value.Value;
            propiedad.cpd_fecha_entrega_detalle = txtObservacion.Text.ToString();
            propiedad.cpd_tipo_propiedad = tipoPropiedad;
            propiedad.cpd_tipo_servicio = tipoServicio;
            propiedad.cpd_pais = pais;
            propiedad.cpd_region = region;
            propiedad.cpd_provincia = provincia;
            propiedad.cpd_comuna = comuna;
            propiedad.cpd_estado = estado;
            propiedad.cpd_calle = txtCalle.Text.ToString();
            propiedad.cpd_numero_propiedad = txtNumeroPropiedad.Text.ToString();
            propiedad.cpd_titulo = txtTitulo.Text;
            propiedad.cpd_valor_uf = int.Parse(txtValorUf.Text.ToString());
            propiedad.cpd_valor_venta = int.Parse(txtValorCLP.Text.ToString());
            propiedad.cpd_valor_evaluo_fiscal = int.Parse(txtEvaluoFiscal.Text.ToString());
            propiedad.cpd_cantidad_bodega = cantBodega;
            propiedad.cpd_valor_bodega = valorBodega;
            propiedad.cpd_cantidad_estacionamiento = cantEst;
            propiedad.cpd_valor_estacionamiento = valorEst;

            propiedad.cpd_bodega = rdbSi.Checked;
            propiedad.cpd_estacionamiento = rdeSi.Checked;
            propiedad.cpd_contribucciones = rdcSi.Checked;
            propiedad.cpd_derecho_municipal = rddSi.Checked;

            if (Id > 0)
                respuesta = clientePropiedadController.UpdateClientePropiedad(propiedad);
            else
            {
                respuesta = clientePropiedadController.InsertClientePropiedad(propiedad);
                Id = respuesta.codigo;
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }
    #endregion
    #region Creación de Datos Legales Propiedad
    protected void CargarDatoLegal()
    {
        datolegal.cdl_id_propiedad = Id;

        // Realiza la consulta primero
        datolegal = clientePropiedadController.GetClientePropiedadDatoLegal(datolegal);


        // Verifica si el Id del usuario es válido y si existen datos asociados
        if (Id > 0 && datolegal != null && datolegal.cdl_id > 0)
        {
            // Asignar valores a los controles
            txtRol.Text = datolegal.cdl_rol;
            txtFojas.Text = datolegal.cdl_fajas;
            txtNumeroManzana.Text = datolegal.cdl_numero_manzana;
            txtNumeroInscripcion.Text = datolegal.cdl_numero_inscripcion;
            txtNumeroSitio.Text = datolegal.cdl_numero_sitio;
            txtAnioInscripcion.Text = datolegal.cdl_anio_inscripcion.ToString();
            txtConHabitacional.Text = datolegal.cdl_conjunto_habitacional;
            txtCopiaLlaves.Text = datolegal.cdl_copia_llaves.ToString();

            // Asignar el banco seleccionado
            cboPropietario.SelectedValue = datolegal.cdl_id_propietario.ToString();
            Usuarios filtro = new Usuarios();
            var propietarios = controller.GetClienteUsuarioPropietarios();
            cboPropietario.Items.Clear();
            if (propietarios.Count > 0)
            {
                cboPropietario.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                cboPropietario.AppendDataBoundItems = true;
            }
            cboPropietario.DataSource = propietarios;
            cboPropietario.DataValueField = "usu_id";
            cboPropietario.DataTextField = "NOMBRE_COMPLETO";
            cboPropietario.DataBind();

            if (bool.Parse(datolegal.cdl_inventario.ToString()))
            {
                rdiSi.Checked = true;
                rdiNo.Checked = false;
            }
            else
            {
                rdiSi.Checked = false;
                rdiNo.Checked = true;
            }
        }
    }
    protected void btnGuardarDatoLegales_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
            ClientePropiedadDatoLegal datoLegal = new ClientePropiedadDatoLegal();

            // Consultar si ya existe un dato de pago para este usuario
            ClientePropiedadDatoLegal dato = new ClientePropiedadDatoLegal();
            dato.cdl_id_propiedad = Id;
            dato = clientePropiedadController.GetClientePropiedadDatoLegal(dato); // Aquí haces la consulta

            // Verificar si existe registro (si existe, se toma el ID para actualizar)
            if (dato != null && dato.cdl_id > 0)
            {
                datoLegal.cdl_id = dato.cdl_id; // Obtener el ID existente
            }

            datoLegal.cdl_id = datoLegal.cdl_id;
            datoLegal.cdl_id_propiedad = Id;

            int propiedad = 0;
            int.TryParse(cboPropietario.SelectedValue, out propiedad);
            datoLegal.cdl_id_propietario = propiedad;
            datoLegal.cdl_rol = txtRol.Text.ToString();
            datoLegal.cdl_fajas = txtFojas.Text.ToString();
            datoLegal.cdl_numero_inscripcion = txtNumeroInscripcion.Text.ToString();
            datoLegal.cdl_anio_inscripcion = int.Parse(txtAnioInscripcion.Text.ToString());
            datoLegal.cdl_numero_manzana = txtNumeroManzana.Text.ToString();
            datoLegal.cdl_numero_sitio = txtNumeroSitio.Text.ToString();
            datoLegal.cdl_copia_llaves = int.Parse(txtCopiaLlaves.Text.ToString());
            datoLegal.cdl_conjunto_habitacional = txtConHabitacional.Text.ToString();


            if (rdiSi.Checked)
                datoLegal.cdl_inventario = true;
            else
                datoLegal.cdl_inventario = false;

            // Verificar si se actualiza o se inserta
            if (datoLegal.cdl_id > 0)
            {
                respuesta = clientePropiedadController.UpdateClientePropiedadDatoLegal(datoLegal);
            }
            else
            {
                respuesta = clientePropiedadController.InsertClientePropiedadDatoLegal(datoLegal);
                datoLegal.cdl_id = respuesta.codigo; // Guardar el ID nuevo
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

    #endregion
    #region Creación de Ficha de Propiedad (Caracteristicas)
    protected void CargarFicha()
    {
        ficha.cpf_id_propiedad = Id;

        // Realiza la consulta primero
        ficha = clientePropiedadController.GetClientePropiedadFicha(ficha);


        // Verifica si el Id del usuario es válido y si existen datos asociados
        if (Id > 0 && ficha != null && ficha.cpf_id > 0)
        {
            txtSuperficieUtil.Text = ficha.cpf_superficie_util;
            txtSuperficieTotal.Text = ficha.cpf_superficie_total;
            txtCantidadPisos.Text = ficha.cpf_pisos.ToString();
            txtCantidadDormitorios.Text = ficha.cpf_dormitorio.ToString();
            txtCantidadBanios.Text = ficha.cpf_baño.ToString();

            if(propiedad.TIPO_PROPIEDAD == 2.ToString())
            {
                txtUbicacionPiso.Visible = true;
                txtUbicacionPisoEtiq.Visible = true;
                txtUbicacionPiso.Text = ficha.cpf_ubicacion_piso.ToString();
            }
            else
            {
                txtUbicacionPiso.Visible = false;
                txtUbicacionPisoEtiq.Visible = false;
            }


                txtTipoPiso.Text = ficha.cpf_tipo_piso;
            txtTipoVentana.Text = ficha.cpf_tipo_ventana;
            txtConexionCocina.Text = ficha.cpf_conexion_cocina;
            txtConexionLavadora.Text = ficha.cpf_conexion_lavadora;

            #region Seleccion Adicionales DEPA
            if (bool.Parse(ficha.cpf_quincho.ToString()))
            {
                quinchoSi.Checked = true;
                quinchoNo.Checked = false;
            }
            else
            {
                quinchoSi.Checked = false;
                quinchoNo.Checked = true;
            }

            if (bool.Parse(ficha.cpf_piscina.ToString()))
            {
                piscinaSi.Checked = true;
                piscinaNo.Checked = false;
            }
            else
            {
                piscinaSi.Checked = false;
                piscinaNo.Checked = true;
            }

            if (bool.Parse(ficha.cpf_gimnasio.ToString()))
            {
                gimnasioSi.Checked = true;
                gimnasioNo.Checked = false;
            }
            else
            {
                gimnasioSi.Checked = false;
                gimnasioNo.Checked = true;
            }

            if (bool.Parse(ficha.cpf_calefaccion.ToString()))
            {
                calefaccionSi.Checked = true;
                calefaccionNo.Checked = false;
            }
            else
            {
                calefaccionSi.Checked = false;
                calefaccionNo.Checked = true;
            }

            if (bool.Parse(ficha.cpf_salon_multiple.ToString()))
            {
                salonMultipleSi.Checked = true;
                salonMultipleNo.Checked = false;
            }
            else
            {
                salonMultipleSi.Checked = false;
                salonMultipleNo.Checked = true;
            }

            #endregion


            bool datosExistentes = ficha != null && ficha.cpf_id > 0;
            hfDatosExistentes.Value = datosExistentes.ToString().ToLower();

        }
    }
    protected void btnGuardarFicha_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
            ClientePropiedadFicha ficha = new ClientePropiedadFicha();
            ficha.cpf_id = IdFecha;
            ficha.cpf_id_propiedad = Id;

            // Consultar si ya existe un dato de pago para este usuario
            ClientePropiedadFicha dato = new ClientePropiedadFicha();
            dato.cpf_id_propiedad = Id;
            dato = clientePropiedadController.GetClientePropiedadFicha(dato); // Aquí haces la consulta

            // Verificar si existe registro (si existe, se toma el ID para actualizar)
            if (dato != null && dato.cpf_id > 0)
            {
                ficha.cpf_id = dato.cpf_id; // Obtener el ID existente
            }

            ficha.cpf_id = dato.cpf_id;
            ficha.cpf_id_propiedad = Id;
            ficha.cpf_superficie_util = txtSuperficieUtil.Text.ToString();
            ficha.cpf_superficie_total = txtSuperficieTotal.Text.ToString();
            ficha.cpf_pisos = int.Parse(txtCantidadPisos.Text.ToString());
            ficha.cpf_dormitorio = int.Parse(txtCantidadDormitorios.Text.ToString());
            ficha.cpf_baño = int.Parse(txtCantidadBanios.Text.ToString());
            ficha.cpf_conexion_cocina = txtConexionCocina.Text.ToString();
            ficha.cpf_conexion_lavadora = txtConexionLavadora.Text.ToString();
            ficha.cpf_ubicacion_piso = string.IsNullOrEmpty(txtUbicacionPiso.Text) ? 0 : int.Parse(txtUbicacionPiso.Text);
            // Si es depa me indica a que piso corresponde
            ficha.cpf_tipo_piso = txtTipoPiso.Text.ToString();
            ficha.cpf_tipo_ventana = txtTipoVentana.Text.ToString();

            #region Seleccion Adicionales DEPA
            if (quinchoSi.Checked)
                ficha.cpf_quincho = true;
            else
                ficha.cpf_quincho = false;

            if (piscinaSi.Checked)
                ficha.cpf_piscina = true;
            else
                ficha.cpf_piscina = false;

            if (gimnasioSi.Checked)
                ficha.cpf_gimnasio = true;
            else
                ficha.cpf_gimnasio = false;

            if (calefaccionSi.Checked)
                ficha.cpf_calefaccion = true;
            else
                ficha.cpf_calefaccion = false;

            if (salonMultipleSi.Checked)
                ficha.cpf_salon_multiple = true;
            else
                ficha.cpf_salon_multiple = false;

            #endregion

            if (ficha.cpf_id > 0)
            {
                respuesta = clientePropiedadController.UpdateClientePropiedadFicha(ficha);
            }
            else
            {
                respuesta = clientePropiedadController.InsertClientePropiedadFicha(ficha);
                ficha.cpf_id = respuesta.codigo;
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }
    #endregion
    #region Creación de Detalle de Publicación
    protected void CargarDetallePublicacion()
    {

        detallePublicacion.cdp_id_propiedad = Id;

        // Realiza la consulta primero
        detallePublicacion = clientePropiedadController.GetClientePropiedadDetallePublicacion(detallePublicacion);


        // Verifica si el Id del usuario es válido y si existen datos asociados
        if (Id > 0 && detallePublicacion != null && detallePublicacion.cdp_id > 0)
        {
            txtConectividad.Text = detallePublicacion.cdp_conectividad;
            txtComercial.Text = detallePublicacion.cdp_centro_comercial;
            txtSeguridad.Text = detallePublicacion.cdp_seguridad;
            txtServiciosSalud.Text = detallePublicacion.cdp_servicio_salud;
            txtEducacion.Text = detallePublicacion.cdp_educacion;
            txtAreaVerde.Text = detallePublicacion.cdp_area_verde;
            txtRestaurant.Text = detallePublicacion.cdp_restaurant;
            txtTransportes.Text = detallePublicacion.cdp_transporte;
            txtDescripcion.Text = detallePublicacion.cdp_descripcion;
        }
    }
    protected void btnDetallePublicacion_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
            ClientePropiedadDetallePublicacion detallePublicacion = new ClientePropiedadDetallePublicacion();
            detallePublicacion.cdp_id = IdDetallePublicacion;
            detallePublicacion.cdp_id_propiedad = Id;

            // Consultar si ya existe un dato de pago para este usuario
            ClientePropiedadDetallePublicacion dato = new ClientePropiedadDetallePublicacion();
            dato.cdp_id_propiedad = Id;
            dato = clientePropiedadController.GetClientePropiedadDetallePublicacion(dato); // Aquí haces la consulta

            // Verificar si existe registro (si existe, se toma el ID para actualizar)
            if (dato != null && dato.cdp_id > 0)
            {
                detallePublicacion.cdp_id = dato.cdp_id; // Obtener el ID existente
            }

            detallePublicacion.cdp_id = dato.cdp_id;
            detallePublicacion.cdp_id_propiedad = Id;
            detallePublicacion.cdp_conectividad = txtConectividad.Text.ToString();
            detallePublicacion.cdp_centro_comercial = txtComercial.Text.ToString();
            detallePublicacion.cdp_servicio_salud = txtServiciosSalud.Text.ToString();
            detallePublicacion.cdp_educacion = txtEducacion.Text.ToString();
            detallePublicacion.cdp_area_verde = txtAreaVerde.Text.ToString();
            detallePublicacion.cdp_restaurant = txtRestaurant.Text.ToString();
            detallePublicacion.cdp_seguridad = txtSeguridad.Text.ToString();
            detallePublicacion.cdp_transporte = txtTransportes.Text.ToString();
            detallePublicacion.cdp_descripcion = txtDescripcion.Text.ToString();


            if (detallePublicacion.cdp_id > 0)
            {
                respuesta = clientePropiedadController.UpdateClientePropiedadDetallePublicacion(detallePublicacion);
            }
            else
            {
                respuesta = clientePropiedadController.InsertClientePropiedadDetallePublicacion(detallePublicacion);
                detallePublicacion.cdp_id = respuesta.codigo;
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }
    #endregion
    #region Subida de Medios - Imagenes, Archivo y Video
    protected void ConfigurarGrid()
    {
        GridImagenes.AddSelectColumn();
        GridImagenes.AddColumn("cpm_id", "ID", "5%", Align: HorizontalAlign.Center);
        GridImagenes.AddColumn("tipoArchivo", "TIPO", "10%", Align: HorizontalAlign.Center);
        GridImagenes.AddColumn("cpm_descripcion", "DESCRIPCION");
        GridImagenes.AddColumn("cpm_link", "LINK");
        GridImagenes.AddTemplateColumn("acciones", "", "ACCIONES", ItemPosition: HorizontalAlign.Center, Width: "10%");



        Tools.tools.RegisterPostBackScript(GridImagenes);
    }

    protected void CargaGrid()
    {

        clientePropiedadMedio.cpm_id_propiedad = Id;

        var medios = clientePropiedadController.GetClientePropiedadMedios(clientePropiedadMedio);

        foreach (var medio in medios)
        {
            if (medio.cpm_imagen)
            {
                esImagen = true;
            }
            if (medio.cpm_video)
            {
                esVideo = true;
            }

            if (esImagen && esVideo)
            {
                break;
            }
        }

        // Creamos una lista anónima con los campos + tipoArchivo
        var mediosConTipo = medios.Select(m => new
        {
            m.cpm_id,
            m.cpm_descripcion,
            m.cpm_link,
            m.cpm_imagen,
            m.cpm_video,
            tipoArchivo = m.cpm_imagen ? "IMAGEN" : m.cpm_video ? "VIDEO" : ""
        }).ToList();

        // Asignamos a la grilla
        GridImagenes.DataSource = mediosConTipo;
        GridImagenes.DataBind();
    }

    protected void lnkNuevoDocumento_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdPropiedad=" + Id));
        Tools.tools.ClientExecute("abrirPropiedadMedio('" + query + "')");
    }

    protected void lnkEliminarDocumento_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridImagenes.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
                CargaGrid();
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in GridImagenes.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridImagenes.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cpm_id"].ToString());

                    ClientePropiedadController propiedadController = new ClientePropiedadController();
                    ClientePropiedadMedio medio = new ClientePropiedadMedio();
                    medio.cpm_id = id;

                    respuesta = propiedadController.DeleteClientePropiedadMedio(medio);
                }

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
    protected void GridDocumentos_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            string cpm_id = item.GetDataKeyValue("cpm_id").ToString();

            // Botón DESCARGAR
            LinkButton lnkDescarga = new LinkButton();
            lnkDescarga.ID = "lnkDescarga";
            lnkDescarga.Text = "<i class='fa fa-download'></i>";
            lnkDescarga.CssClass = "btn btn-link";
            lnkDescarga.ToolTip = "Descargar Documento";
            lnkDescarga.CommandName = cpm_id;
            lnkDescarga.EnableViewState = true;
            lnkDescarga.Command += new CommandEventHandler(lnkDescarga_Command);

            // Botón VISUALIZAR
            LinkButton lnkVer = new LinkButton();
            lnkVer.ID = "lnkVer";
            lnkVer.Text = "<i class='fa fa-search'></i>";
            lnkVer.CssClass = "btn btn-link";
            lnkVer.ToolTip = "Visualizar Documento";
            lnkVer.CommandName = cpm_id;
            string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdPropiedad=" + Id + "&Id=" + cpm_id));
            lnkVer.Attributes.Add("onclick", "abrirVisualizador('" + query + "')");
            lnkVer.EnableViewState = true;

            // Botón EDITAR
            LinkButton lnkEditar = new LinkButton();
            lnkEditar.ID = "lnkEditar";
            lnkEditar.Text = "<i class='fa fa-edit'></i>";
            lnkEditar.CssClass = "btn btn-link";
            lnkEditar.ToolTip = "Editar Medio";
            lnkEditar.CommandName = cpm_id;
            lnkEditar.Attributes.Add("onclick", "abrirPropiedadMedio('" + query + "')");
            lnkEditar.EnableViewState = true;

            // Agregar botones a la celda "acciones"
            item["acciones"].Controls.Add(lnkDescarga);
            item["acciones"].Controls.Add(new LiteralControl("&nbsp;"));
            item["acciones"].Controls.Add(lnkVer);
            item["acciones"].Controls.Add(new LiteralControl("&nbsp;"));
            item["acciones"].Controls.Add(lnkEditar);

            // Registrar para postback
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescarga);

        }
    }

    protected void GridDocumentos_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string cpm_id = item.GetDataKeyValue("cpm_id").ToString();

                LinkButton lnkDescarga = (LinkButton)item["acciones"].FindControl("lnkDescarga");
                LinkButton lnkVer = (LinkButton)item["acciones"].FindControl("lnkVer");
                LinkButton lnkEditar = (LinkButton)item["acciones"].FindControl("lnkEditar");

                if (lnkDescarga != null)
                {
                    lnkDescarga.CommandName = cpm_id;
                    bool esImagen = Convert.ToBoolean(item.GetDataKeyValue("cpm_imagen"));
                    lnkDescarga.Visible = esImagen;
                }

                if (lnkVer != null)
                {
                    lnkVer.CommandName = cpm_id;
                    bool esImagen = Convert.ToBoolean(item.GetDataKeyValue("cpm_imagen"));
                    lnkVer.Visible = esImagen;
                }

                if (lnkEditar != null)
                {
                    lnkEditar.CommandName = cpm_id;
                    lnkEditar.Visible = true;
                }
            }
        }
    }

    private void lnkDescarga_Command(Object sender, CommandEventArgs e)
    {
        ClientePropiedadMedio propiedadMedio = new ClientePropiedadMedio();
        ClientePropiedadMedioBinario binario = new ClientePropiedadMedioBinario();
        ClientePropiedadController controller = new ClientePropiedadController();

        propiedadMedio.cpm_id = int.Parse(e.CommandName.ToString());
        binario = controller.GetClientePropiedadMedioArchivo(propiedadMedio);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "image/jpeg";
        string fileName = binario.DESCRIPCION + "_" + propiedadMedio.cpm_id.ToString() + ".jpg";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        HttpContext.Current.Response.BinaryWrite(binario.cmb_binario);
        HttpContext.Current.Response.End();
    }
    #endregion
    #region Cambiar estado de la propiedad

    private void CargarEstadosPropiedad(RadComboBox cboEstadoPropiedad, int tipoServicio)
    {
        Parametros parametros = new Parametros();
        ParametroSistemaController parametrosController = new ParametroSistemaController();

        // Determinar el código del parámetro según el tipo de servicio
        if (tipoServicio == 1)
        {
            parametros.par_codigo = "Combo_Compra";
        }
        else
        {
            parametros.par_codigo = "Combo_Arriendo";
        }

        // Obtener los parámetros
        parametros = parametrosController.GetParametros(parametros);
        string combobox = parametros.par_valor;

        // Obtener los estados de la propiedad
        var estados = controller.GetEstadosPropiedad(combobox);

        // Limpiar los elementos antes de agregar nuevos ítems
        cboEstadoPropiedad.Items.Clear();

        if (estados.Count > 0)
        {
            // Agregar ítem "Seleccione..." solo si hay datos
            cboEstadoPropiedad.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            cboEstadoPropiedad.AppendDataBoundItems = true;
        }

        // Asignar los datos al control
        cboEstadoPropiedad.DataSource = estados;
        cboEstadoPropiedad.DataValueField = "cpe_id";
        cboEstadoPropiedad.DataTextField = "cpe_nombre";
        cboEstadoPropiedad.DataBind();

        // Seleccionar el estado actual de la propiedad
        if (propiedad != null && propiedad.cpd_estado > 0)
        {
            cboEstadoPropiedad.SelectedValue = propiedad.cpd_estado.ToString();
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        DateTime? fechaInicio = null;
        DateTime? fechaFin = null;

        // Obtener las fechas seleccionadas
        if (txtFechaInicio.SelectedDate.HasValue)
            fechaInicio = txtFechaInicio.SelectedDate.Value;

        if (txtFechaFin.SelectedDate.HasValue)
            fechaFin = txtFechaFin.SelectedDate.Value;

        // Llamar al método para cargar el cronograma con las fechas filtradas
        CargarCronogramaEstadoPropiedad(fechaInicio, fechaFin);
    }

    private void CargarCronogramaEstadoPropiedad(DateTime? fechaInicio, DateTime? fechaFin)
    {
        ClientePropiedadEstadoAvance estadoAvance = new ClientePropiedadEstadoAvance();
        estadoAvance.cea_id_propiedad = Id;
        ClientePropiedadController controller = new ClientePropiedadController();
        List<ClientePropiedadEstadoAvance> listadoEstados = controller.GetListadoEstadoAvancePropiedades(estadoAvance);

        StringBuilder cronograma = new StringBuilder();

        var estadosFiltrados = listadoEstados.Where(estado =>
            (!fechaInicio.HasValue || estado.cea_fecha_creacion >= fechaInicio.Value) &&
            (!fechaFin.HasValue || estado.cea_fecha_creacion <= fechaFin.Value)).ToList();

        if (estadosFiltrados.Count == 0)
        {
            cronograma.Append("<div class='card mt-3'><div class='card-body'>");
            cronograma.Append("<h5 class='card-title text-center text-muted'>La propiedad no cuenta con estados</h5>");
            cronograma.Append("</div></div>");
        }
        else
        {
            var estadoActual = estadosFiltrados.Last();
            string estadoActualNombre = estadoActual.NOMBRE_ESTADO.Trim();
            string fechaCreacionStr = estadoActual.cea_fecha_creacion.ToString("dd/MM/yyyy HH:mm");
            string usuarioCreacion = estadoActual.NOMBRE_COMPLETO;
            string observacionActual = string.IsNullOrWhiteSpace(estadoActual.OBSERVACION_ESTADO) ? "Sin observación" : estadoActual.OBSERVACION_ESTADO.Replace("'", "\\'").Replace("\n", "\\n").Replace("\"", "\\\"");

            string iconClass = "fas fa-question-circle";
            string color = "#6c757d";

            switch (estadoActualNombre)
            {
                case "Publicado": iconClass = "fas fa-check-circle"; color = "#28a745"; break;
                case "Con Visitas": iconClass = "fas fa-eye"; color = "#17a2b8"; break;
                case "Disponible": iconClass = "fas fa-check"; color = "#007bff"; break;
                case "Reservada": iconClass = "fas fa-clock"; color = "#ffc107"; break;
                case "Esperando Firma de Promesa de Compraventa": iconClass = "fas fa-file-alt"; color = "#6f42c1"; break;
                case "Esperando Firma de Escritura": iconClass = "fas fa-signature"; color = "#fd7e14"; break;
                case "Vendida": iconClass = "fas fa-home"; color = "#20c997"; break;
                case "Esperando Firma de  Contrato de Arriendo": iconClass = "fas fa-file-signature"; color = "#6610f2"; break;
                case "Arrendada": iconClass = "fas fa-box"; color = "#6c757d"; break;
                case "No Disponible": iconClass = "fas fa-ban"; color = "#dc3545"; break;
                case "Promesada": iconClass = "fas fa-calendar-check"; color = "#fd7e14"; break;
                case "Escriturada": iconClass = "fas fa-home"; color = "#17a2b8"; break;
                case "Entregada": iconClass = "fas fa-handshake"; color = "#28a745"; break;
                case "Arrendada con disponibilidad para compra": iconClass = "fas fa-box-open"; color = "#ffc107"; break;
                case "Bloqueada": iconClass = "fas fa-ban"; color = "#dc3545"; break;
            }

            cronograma.Append("<fieldset class='border p-3 rounded mb-4 mr-4'><legend class='w-auto px-2'>Estado Actual</legend>");
            cronograma.Append("<div class='row col-lg-12 col-md-12 col-xs-12'><div class='timeline-item estado-actual'>");
            cronograma.AppendFormat("<div class='timeline-icon' style='color:{0}'><i class='{1}'></i></div>", color, iconClass);
            cronograma.Append("<div class='timeline-details'>");
            cronograma.AppendFormat("<div class='timeline-title'>{0}</div>", estadoActualNombre);
            cronograma.AppendFormat("<div class='timeline-meta'>{0} | {1}</div>", fechaCreacionStr, usuarioCreacion);
            cronograma.AppendFormat("<a href='#' onclick=\"mostrarObservacionSweetAlert('{0}')\">Ver observación</a>", observacionActual);
            cronograma.Append("</div></div></div></fieldset>");

            if (estadosFiltrados.Count > 1)
            {
                cronograma.Append("<fieldset class='border p-3 rounded'><legend class='w-auto px-2'>Cronograma de Estados</legend><div class='timeline-container'>");

                foreach (var estado in estadosFiltrados.Take(estadosFiltrados.Count - 1))
                {
                    string estadoNombre = estado.NOMBRE_ESTADO.Trim();
                    string fechaCreacion = estado.cea_fecha_creacion.ToString("dd/MM/yyyy HH:mm");
                    string usuarioCreacionAnt = estado.NOMBRE_COMPLETO;
                    string observacion = string.IsNullOrWhiteSpace(estado.OBSERVACION_ESTADO) ? "Sin observación" : estado.OBSERVACION_ESTADO.Replace("'", "\\'").Replace("\n", "\\n").Replace("\"", "\\\"");

                    string iconClassRestante = "fas fa-question-circle";
                    string colorRestante = "#6c757d";

                    switch (estadoNombre)
                    {
                        case "Publicado": iconClassRestante = "fas fa-check-circle"; colorRestante = "#28a745"; break;
                        case "Con Visitas": iconClassRestante = "fas fa-eye"; colorRestante = "#17a2b8"; break;
                        case "Disponible": iconClassRestante = "fas fa-check"; colorRestante = "#007bff"; break;
                        case "Reservada": iconClassRestante = "fas fa-clock"; colorRestante = "#ffc107"; break;
                        case "Esperando Firma de Promesa de Compraventa": iconClassRestante = "fas fa-file-alt"; colorRestante = "#6f42c1"; break;
                        case "Esperando Firma de Escritura": iconClassRestante = "fas fa-signature"; colorRestante = "#fd7e14"; break;
                        case "Vendida": iconClassRestante = "fas fa-home"; colorRestante = "#20c997"; break;
                        case "Esperando Firma de Contrato de Arriendo": iconClassRestante = "fas fa-file-signature"; colorRestante = "#6610f2"; break;
                        case "Arrendada": iconClassRestante = "fas fa-box"; colorRestante = "#6c757d"; break;
                        case "No Disponible": iconClassRestante = "fas fa-ban"; colorRestante = "#dc3545"; break;
                        case "Promesada": iconClassRestante = "fas fa-calendar-check"; colorRestante = "#fd7e14"; break;
                        case "Escriturada": iconClassRestante = "fas fa-home"; colorRestante = "#17a2b8"; break;
                        case "Entregada": iconClassRestante = "fas fa-handshake"; colorRestante = "#28a745"; break;
                        case "Arrendada con disponibilidad para compra": iconClassRestante = "fas fa-box-open"; colorRestante = "#ffc107"; break;
                        case "Bloqueada": iconClassRestante = "fas fa-ban"; colorRestante = "#dc3545"; break;
                    }

                    cronograma.Append("<div class='timeline-item estado-pasado'>");
                    cronograma.AppendFormat("<div class='timeline-icon' style='color:{0}'><i class='{1}'></i></div>", colorRestante, iconClassRestante);
                    cronograma.Append("<div class='timeline-details'>");
                    cronograma.AppendFormat("<div class='timeline-title'>{0}</div>", estadoNombre);
                    cronograma.AppendFormat("<div class='timeline-meta'>{0} | {1}</div>", fechaCreacion, usuarioCreacionAnt);
                    cronograma.AppendFormat("<a href='#' onclick=\"mostrarObservacionSweetAlert('{0}')\">Ver observación</a>", observacion);
                    cronograma.Append("</div></div>");
                }

                cronograma.Append("</div></fieldset>");
            }
        }

        txtCronogramaEstadoPropiedad.Text = cronograma.ToString();
    }


    protected void btnPubliar_Click(object sender, EventArgs e)
    {
        ClientePropiedadController controller = new ClientePropiedadController();
        Respuesta respuesta = new Respuesta();

        try
        {
            propiedad.cpd_estado = 10; // Publicado
            propiedad.cpd_id = Id;
            respuesta = controller.UpdateClientePropiedadEstado(propiedad);

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }

    }


    protected void cboEstadoPropiedad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ClientePropiedad propiedad = new ClientePropiedad();
        ClientePropiedadController controller = new ClientePropiedadController();


        int estadoPropiedad = 0;
        int.TryParse(cboPropietario.SelectedValue, out estadoPropiedad);

    }

    protected void cboServicio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int TipoServicio = 0;
        int.TryParse(cboTpoServicio.SelectedValue, out TipoServicio);
        CargarEstadosPropiedad(cboEstado, TipoServicio);
    }


    protected void ConfigurarGridEstado()
    {
        GridEstados.AddSelectColumn();
        GridEstados.Columns[0].HeaderStyle.Width = Unit.Percentage(1); // Establece el ancho de la columna de selección al 5%
        GridEstados.Columns[0].ItemStyle.Width = Unit.Percentage(1);   // Aplica el mismo ancho a las celdas de la columna
        GridEstados.AddColumn("cea_id", "ID");
        GridEstados.AddColumn("NOMBRE_ESTADO", "ESTADO");
        Tools.tools.RegisterPostBackScript(GridEstados);
    }


    protected void CargaGridEstados()
    {
        ClientePropiedadEstadoAvance avance = new ClientePropiedadEstadoAvance();
        avance.cea_id_propiedad = Id;

        var medios = clientePropiedadController.GetListadoEstadoAvancePropiedades(avance);

        // Asignamos a la grilla
        GridEstados.DataSource = medios;
        GridEstados.DataBind();
    }

    protected void lnkEliminarEstado_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridEstados.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
                CargaGridEstados();
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in GridEstados.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridEstados.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cea_id"].ToString());

                    ClientePropiedadController propiedadController = new ClientePropiedadController();
                    ClientePropiedadEstadoAvance avance = new ClientePropiedadEstadoAvance();
                    avance.cea_id = id;

                    respuesta = propiedadController.DeleteClientePropiedadEstado(avance);
                }

                if (!respuesta.error)
                {
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                    CargaGridEstados();
                    updEstados.Update();
                }

                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
    protected void btnGuardarEstadoPropiedad_Click(object sender, EventArgs e)
    {
        ClientePropiedadController controller = new ClientePropiedadController();
        Respuesta respuesta = new Respuesta();

        try
        {
            int estadoPropiedad = 0;

            int.TryParse(cboEstadoPropiedad.SelectedValue, out estadoPropiedad);

            propiedad.cpd_estado = estadoPropiedad;
            propiedad.cpd_id = Id;
            propiedad.OBSERVACION_ESTADO = txtObservacionEstado.Text.ToString();
            respuesta = controller.UpdateClientePropiedadEstado(propiedad);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok");

            }
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }


    #endregion

}