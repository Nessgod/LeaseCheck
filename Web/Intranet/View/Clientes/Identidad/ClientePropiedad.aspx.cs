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

            ConfigurarGrid();

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
            CargarEstadoPropiedad();
            CargaGrid();
            updGrid.Update();
        }

        CargaGrid();
        updGrid.Update();
        Validaciones();
    }

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (sender is RadComboBox)
        {
            RadComboBox ctrl = (RadComboBox)sender;

            switch (ctrl.ID)
            {
                #region Identidad
                case "cboTpoPropiedad":
                    var propiedad = controller.GetTipoPropiedad();
                    if (propiedad.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = propiedad;
                    ctrl.DataValueField = "tpr_id";
                    ctrl.DataTextField = "tpr_nombre";
                    ctrl.DataBind();
                    break;

                case "cboTpoServicio":
                    var servicio = controller.GetTipoServicio();
                    if (servicio.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = servicio;
                    ctrl.DataValueField = "tsc_id";
                    ctrl.DataTextField = "tsc_nombre";
                    ctrl.DataBind();
                    break;

                case "cboTpoEntrega":
                    var entrega = controller.GetTipoEntrega();
                    if (entrega.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = entrega;
                    ctrl.DataValueField = "cpt_id";
                    ctrl.DataTextField = "cpt_nombre";
                    ctrl.DataBind();
                    break;

                case "cboPais":
                    var paises = controller.GetPaises();
                    if (paises.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = paises;
                    ctrl.DataValueField = "pai_id";
                    ctrl.DataTextField = "pai_nombre";
                    ctrl.DataBind();

                    break;

                case "cboRegion":
                    LeaseCheck.Root.Model.Region filtro = new LeaseCheck.Root.Model.Region();
                    var region = controller.GetRegiones(filtro);
                    if (region.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = region;
                    ctrl.DataValueField = "rgn_id";
                    ctrl.DataTextField = "rgn_nombre";
                    ctrl.DataBind();
                    break;

                case "cboProvincia":
                    Provincia pro = new Provincia();
                    var provincia = controller.GetProvincias(pro);
                    if (provincia.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = provincia;
                    ctrl.DataValueField = "pro_id";
                    ctrl.DataTextField = "pro_nombre";
                    ctrl.DataBind();
                    break;


                case "cboComuna":
                    Comuna comuna = new Comuna();
                    var comunas = controller.GetComunas(comuna);
                    if (comunas.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }

                    ctrl.DataSource = comunas;
                    ctrl.DataValueField = "cmn_id";
                    ctrl.DataTextField = "cmn_nombre";
                    ctrl.DataBind();
                    break;


                case "cboEstado":
                    var estados = controller.GetEstadosPropiedad();
                    if (estados.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }
                    cboEstado.Items.Clear();
                    ctrl.DataSource = estados;
                    ctrl.DataValueField = "cpe_id";
                    ctrl.DataTextField = "cpe_nombre";
                    ctrl.DataBind();
                    break;
                #endregion

                #region Datos Legales
                case "cboPropietario":
                    var propietarios = controller.GetClienteUsuarioPropietarios();
                    if (propietarios.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }
                    ctrl.DataSource = propietarios;
                    ctrl.DataValueField = "usu_id";
                    ctrl.DataTextField = "NOMBRE_COMPLETO";
                    ctrl.DataBind();
                    break;
                #endregion

                #region Estado de Propiedad
                case "cboEstadoPropiedad":
                    var estados2 = controller.GetEstadosPropiedad();
                    if (estados2.Count > 0)
                    {
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                        ctrl.AppendDataBoundItems = true;
                    }
                    cboEstadoPropiedad.Items.Clear();
                    ctrl.DataSource = estados2;
                    ctrl.DataValueField = "cpe_id";
                    ctrl.DataTextField = "cpe_nombre";
                    ctrl.DataBind();
                    break;
                    #endregion
            }
        }
    }

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


            ClientePropiedad propiedad = new ClientePropiedad();
            ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
            propiedad.cpd_id = Id;

            propiedad = clientePropiedadController.GetClientePropiedad(propiedad);

            lblTituloUsuario.Text = "ID: " + propiedad.cpd_id + " | " + propiedad.TIPO_PROPIEDAD + ": " + propiedad.cpd_titulo;


            cboTpoPropiedad.SelectedValue = propiedad.cpd_tipo_propiedad.ToString();
            cboTpoServicio.SelectedValue = propiedad.cpd_tipo_servicio.ToString();
            cboTpoEntrega.SelectedValue = propiedad.cpd_tipo_entrega.ToString();

            cboEstado.SelectedValue = propiedad.cpd_estado.ToString();

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
            propiedad.cpd_valor_evaluo_fiscal= int.Parse(txtEvaluoFiscal.Text.ToString());
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

        ClientePropiedadDatoLegal datolegal = new ClientePropiedadDatoLegal();
        ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
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
        ClientePropiedadFicha ficha = new ClientePropiedadFicha();
        ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
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
            txtUbicacionPiso.Text = ficha.cpf_ubicacion_piso.ToString();
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
        ClientePropiedadDetallePublicacion detallePublicacion = new ClientePropiedadDetallePublicacion();
        ClientePropiedadController clientePropiedadController = new ClientePropiedadController();
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

            btnPubliar.Visible = true;


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
                btnPubliar.Visible = true;
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
        ClientePropiedadMedio clientePropiedadMedio = new ClientePropiedadMedio();
        ClientePropiedadController propiedadController = new ClientePropiedadController();
        clientePropiedadMedio.cpm_id_propiedad = Id;

        var medios = propiedadController.GetClientePropiedadMedios(clientePropiedadMedio);

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
    private void CargarEstadoPropiedad()
    {
        ClientePropiedad propiedad = new ClientePropiedad();
        ClienteController controller = new ClienteController();

        propiedad.cpd_id = Id;
        propiedad = controller.GetEstadoPropiedad(propiedad);

        string estado = propiedad.ESTADO != null ? propiedad.ESTADO.Trim() : "";
        string emoji = "";
        string cssClass = "badge badge-pill estado-propiedad";

        switch (estado)
        {
            case "Publicado":
                emoji = "🟢";
                cssClass += " badge-success";
                break;
            case "Con Visitas":
                emoji = "👀";
                cssClass += " badge-info";
                break;
            case "Disponible":
                emoji = "✅";
                cssClass += " badge-success";
                break;
            case "Reservada":
                emoji = "🟠";
                cssClass += " badge-warning";
                break;
            case "En Proceso de Promesa de Compraventa":
                emoji = "📑";
                cssClass += " badge-primary";
                break;
            case "Esperando Firma de Escritura":
                emoji = "✍️";
                cssClass += " badge-primary";
                break;
            case "Vendida":
                emoji = "🏠✅";
                cssClass += " badge-dark";
                break;
            case "En Proceso de Contrato de Arriendo":
                emoji = "📝";
                cssClass += " badge-primary";
                break;
            case "Arrendada":
                emoji = "📦";
                cssClass += " badge-secondary";
                break;
            case "No Disponible":
                emoji = "⛔";
                cssClass += " badge-danger";
                break;
            default:
                emoji = "❓";
                cssClass += " badge-light";
                break;
        }

        lblEstadoPropiedad.Text = "Estado Actual: " + emoji + " " + estado;
        lblEstadoPropiedad.CssClass = cssClass;
    }

    #endregion

    protected void btnPubliar_Click(object sender, EventArgs e)
    {
        ClientePropiedad propiedad = new ClientePropiedad();
        ClientePropiedadController controller = new ClientePropiedadController();

        propiedad.cpd_estado = 10; // Publicado
        propiedad.cpd_id = Id;
        controller.UpdateClientePropiedadEstado(propiedad);

    }



    protected void cboEstadoPropiedad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ClientePropiedad propiedad = new ClientePropiedad();
        ClientePropiedadController controller = new ClientePropiedadController();


        int estadoPropiedad = 0;
        int.TryParse(cboPropietario.SelectedValue, out estadoPropiedad);

    }
}