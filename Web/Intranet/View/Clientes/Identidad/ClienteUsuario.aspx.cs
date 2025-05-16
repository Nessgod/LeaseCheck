using System;
using System.Web.Script.Services;
using System.Web.Services;
using LeaseCheck.Root.Model;
using LeaseCheck.Root.Controller;
using Telerik.Web.UI;
using System.Linq;

public partial class View_Clientes_Identidad_ClienteUsuario : System.Web.UI.Page
{
    ClienteController controller = new ClienteController();

    public int Id
    {
        get { return Convert.ToInt32(ViewState["id"]); }
        set { ViewState.Add("id", value); }
    }

    public int IdDatoPago
    {
        get { return Convert.ToInt32(ViewState["IdDatoPago"]); }
        set { ViewState.Add("IdDatoPago", value); }
    }

    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_usuarios.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

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

                    case "IdDatoPago":
                        IdDatoPago = Int32.Parse(array[1].ToString());
                        break;
                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
            CargarDatosPagoUsuario();
        }
           

        Validaciones();
    }

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboPerfil":

                        string[] perfiles = LeaseCheck.Session.UsuarioPerfil().Split(',');

                        // Verificar si el usuario tiene el perfil de Ejecutivo (per_id = 7)
                        if (perfiles.Contains(Convert.ToInt32(LeaseCheck.LeaseCheck.Perfiles.Ejecutivo).ToString()))
                        {
                            // Solo carga el perfil Ejecutivo
                            ctrl.DataSource = controller.GetPerfiles().Where(p => p.per_id == 7).ToList();
                        }
                        else
                        {
                            // Carga todos los perfiles
                            ctrl.DataSource = controller.GetPerfiles();
                        }

                        ctrl.DataValueField = "per_id";
                        ctrl.DataTextField = "per_nombre";

                        ctrl.DataBind();
                        break;

                    case "cboPais":

                        ctrl.DataSource = controller.GetPaises();
                        ctrl.DataValueField = "pai_id";
                        ctrl.DataTextField = "pai_nombre";

                        ctrl.DataBind();
                        break;
                    case "cboComuna":
                        Comuna filtro1 = new Comuna();
                        var comunas = controller.GetComunas(filtro1);

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

                    case "cboEstadoCivil":
                        EstadoCivil ecivil = new EstadoCivil();
                        var estadoCivils = controller.GetEstadoCivil();

                        if (estadoCivils.Count > 0)
                        {
                            ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                            ctrl.AppendDataBoundItems = true;
                        }
                        ctrl.DataSource = estadoCivils;
                        ctrl.DataValueField = "ecl_id";
                        ctrl.DataTextField = "ecl_nombre";
                        ctrl.DataBind();
                        break;

                    case "cboProfesion":
                        var profesion = controller.GetProfesion();

                        if (profesion.Count > 0)
                        {
                            ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                            ctrl.AppendDataBoundItems = true;
                        }
                        ctrl.DataSource = profesion;
                        ctrl.DataValueField = "prf_id";
                        ctrl.DataTextField = "prf_nombre";
                        ctrl.DataBind();
                        break;

                    case "cboGenero":
                        var genero = controller.GetGenero();

                        if (genero.Count > 0)
                        {
                            ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                            ctrl.AppendDataBoundItems = true;
                        }
                        ctrl.DataSource = genero;
                        ctrl.DataValueField = "gro_id";
                        ctrl.DataTextField = "gro_nombre";
                        ctrl.DataBind();
                        break;


                    case "cboNacionalidad":
                        var nacionalidad = controller.GetNacionalidad();

                        if (nacionalidad.Count > 0)
                        {
                            ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                            ctrl.AppendDataBoundItems = true;
                        }
                        ctrl.DataSource = nacionalidad;
                        ctrl.DataValueField = "nac_id";
                        ctrl.DataTextField = "nac_nombre";
                        ctrl.DataBind();
                        break;

                    case "cboBanco":
                        var banco = controller.GetBanco();

                        if (banco.Count > 0)
                        {
                            ctrl.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                            ctrl.AppendDataBoundItems = true;
                        }
                        ctrl.DataSource = banco;
                        ctrl.DataValueField = "bnc_id";
                        ctrl.DataTextField = "bnc_nombre";
                        ctrl.DataBind();
                        break;
                }
            }
        }
    }

    protected void Validaciones()
    {
        if (Id > 0)
        {
            ragTab.Tabs[1].Visible = true;

        }
        else
        {
            lblTituloUsuario.Text = "Nuevo Usuario";
        }
    }

    protected void CargarTipoCuentaBanco()
    {

    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            hfdUsuarioID.Value = Id.ToString();

            Usuarios usuario = new Usuarios();
            usuario.usu_id = Id;
            usuario.devuelve_foto = true;

            usuario = controller.GetClienteUsuario(usuario);

            lblTituloUsuario.Text = usuario.usu_login;

            lblID.Text = Id.ToString();
            txtLogin.Text = usuario.usu_login;
            txtRUT.Text = usuario.usu_rut;
            txtCiudad.Text = usuario.usu_ciudad;
            txtCalle.Text = usuario.usu_calle;
            txtNumeroPropiedad.Text = usuario.usu_numero_propiedad;
            cboComuna.SelectedValue = usuario.usu_comuna.ToString();
            cboGenero.SelectedValue = usuario.usu_genero.ToString();
            cboEstadoCivil.SelectedValue = usuario.usu_estado_civil.ToString();
            cboNacionalidad.SelectedValue = usuario.usu_nacionalidad.ToString();
            cboProfesion.SelectedValue = usuario.usu_profesion.ToString();
            txtPassword.Text = usuario.usu_password;
            TextNombre.Text = usuario.usu_nombres;
            txtPaterno.Text = usuario.usu_apellido_paterno;
            TextMaterno.Text = usuario.usu_apellido_materno;
            txtFono.Text = usuario.usu_fono;
            txtCorreo.Text = usuario.usu_correo;
            lblUsuarioCreacion.Text = usuario.usuario_creacion;
            lblFechaCreacion.Text = usuario.usu_fecha_creacion.ToString();
            lblHostCreacion.Text = usuario.usu_host_creacion;
            lblUsuarioAct.Text = usuario.usuario_act;
            lblFechaAct.Text = usuario.usu_fecha_act.ToString();
            lblHostAct.Text = usuario.usu_host_act;
            lblUltimoLogin.Text = usuario.usu_ultimo_login.ToString();

            cboPerfil.SelectedValue = usuario.usu_perfil.ToString();
            cboPais.SelectedValue = usuario.usu_pais.ToString();

            if (bool.Parse(usuario.usu_habilitado.ToString()))
            {
                rdbSi.Checked = true;
                rdbNo.Checked = false;
            }
            else
            {
                rdbSi.Checked = false;
                rdbNo.Checked = true;
            }

            if (usuario.usu_foto != null)
            {
                string base64String = Convert.ToBase64String(usuario.usu_foto, 0, usuario.usu_foto.Length);
                imgLogo.ImageUrl = "data:image/jpeg;base64," + base64String;
            }
        }
        else
        {
            Cliente = controller.GetUsuarioCliente().cliente;
        }

        txtPassword.Attributes["type"] = "password";
    }

    protected void btnGuardar_OnClick(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            Usuarios usuario = new Usuarios();
            usuario.usu_id = Id;
            usuario.usu_login = txtLogin.Text;
            usuario.usu_password = txtPassword.Text;
            usuario.usu_nombres = TextNombre.Text;
            usuario.usu_apellido_paterno = txtPaterno.Text;
            usuario.usu_apellido_materno = TextMaterno.Text;
            usuario.usu_fono = txtFono.Text;
            usuario.usu_correo = txtCorreo.Text;
            usuario.usu_perfil = int.Parse(cboPerfil.SelectedValue);
            usuario.usu_pais = int.Parse(cboPais.SelectedValue);
            usuario.usu_comuna = int.Parse(cboComuna.SelectedValue);
            usuario.usu_genero = int.Parse(cboGenero.SelectedValue);
            usuario.usu_estado_civil = int.Parse(cboEstadoCivil.SelectedValue);
            usuario.usu_nacionalidad = int.Parse(cboNacionalidad.SelectedValue);
            usuario.usu_profesion = int.Parse(cboProfesion.SelectedValue);
            usuario.usu_rut = txtRUT.Text;
            usuario.usu_calle = txtCalle.Text;
            usuario.usu_numero_propiedad = txtNumeroPropiedad.Text;
            usuario.usu_ciudad = txtCiudad.Text;

            usuario.cliente = Cliente;

            if (rdbSi.Checked)
                usuario.usu_habilitado = true;
            else
                usuario.usu_habilitado = false;

            if (fudFoto.HasFile)
                usuario.usu_foto = LeaseCheck.LeaseCheck.ReducirImagen(fudFoto.FileBytes, 100, 100);

            if (Id > 0)
                respuesta = controller.UpdateClienteUsuario(usuario);
            else
            {
                respuesta = controller.InsertClienteUsuario(usuario);
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


    protected void CargarDatosPagoUsuario()
    {
        UsuarioDatoPago datoPago = new UsuarioDatoPago();
        datoPago.upd_id_usuario = Id;

        // Realiza la consulta primero
        datoPago = controller.GetUsuarioDatoPago(datoPago);

        // Verifica si el Id del usuario es válido y si existen datos asociados
        if (Id > 0 && datoPago != null && datoPago.upd_id > 0)
        {
            // Asignar valores a los controles
            txtOtroBanco.Text = datoPago.upd_banco_otro;
            txtTitular.Text = datoPago.upd_titular_cuenta;
            txtRutCuenta.Text = datoPago.upd_rut_cuenta;
            txtNumeroCuenta.Text = datoPago.upd_numero_cuenta;

            // Asignar el banco seleccionado
            cboBanco.SelectedValue = datoPago.upd_banco.ToString();
            cboTipoCuenta.SelectedValue = datoPago.upd_tipo_cuenta.ToString();

            // Crear un filtro para obtener los tipos de cuenta asociados al banco
            TipoCuentaBanco filtro = new TipoCuentaBanco();
            filtro.tpc_banco = datoPago.upd_banco;
            var tipoCuentaBancos = controller.GetCuentaBanco(filtro);
            // Limpiar y cargar el combo de tipos de cuenta
            cboTipoCuenta.Items.Clear();
            if (tipoCuentaBancos.Count > 0)
            {
                cboTipoCuenta.Items.Add(new RadComboBoxItem("Seleccione...", ""));
                cboTipoCuenta.AppendDataBoundItems = true;
            }
            cboTipoCuenta.DataSource = tipoCuentaBancos;
            cboTipoCuenta.DataValueField = "tpc_id";
            cboTipoCuenta.DataTextField = "tpc_nombre";
            cboTipoCuenta.DataBind();
        }
    }



    protected void btnGuardarDatosLegales_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            // Objeto con los datos ingresados por el usuario
            UsuarioDatoPago usuarioDato = new UsuarioDatoPago();
            usuarioDato.upd_id_usuario = Id;

            // Consultar si ya existe un dato de pago para este usuario
            UsuarioDatoPago datoPago = new UsuarioDatoPago();
            datoPago.upd_id_usuario = Id;
            datoPago = controller.GetUsuarioDatoPago(datoPago); // Aquí haces la consulta

            // Verificar si existe registro (si existe, se toma el ID para actualizar)
            if (datoPago != null && datoPago.upd_id > 0)
            {
                usuarioDato.upd_id = datoPago.upd_id; // Obtener el ID existente
            }

            // Asignar los datos del formulario
            usuarioDato.upd_banco_otro = txtOtroBanco.Text;
            usuarioDato.upd_banco = int.Parse(cboBanco.SelectedValue);
            usuarioDato.upd_tipo_cuenta = int.Parse(cboTipoCuenta.SelectedValue);
            usuarioDato.upd_rut_cuenta = txtRutCuenta.Text;
            usuarioDato.upd_titular_cuenta = txtTitular.Text;
            usuarioDato.upd_numero_cuenta = txtNumeroCuenta.Text;

            // Verificar si se actualiza o se inserta
            if (usuarioDato.upd_id > 0)
            {
                respuesta = controller.UpdateUsuarioDatoPago(usuarioDato);
            }
            else
            {
                respuesta = controller.InsertUsuarioDatoPago(usuarioDato);
                usuarioDato.upd_id = respuesta.codigo; // Guardar el ID nuevo
            }

            // Notificación al usuario
            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }


    protected void cboBanco_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // Obtener el ID del banco seleccionado
        int bancoId = int.Parse(cboBanco.SelectedValue);

        // Crear un filtro para obtener los tipos de cuenta asociados al banco
        TipoCuentaBanco filtro = new TipoCuentaBanco();
        filtro.tpc_banco = bancoId;
     
        // Obtener los tipos de cuenta asociados al banco
        var tipoCuenta = controller.GetCuentaBanco(filtro);

        // Limpiar y cargar el combo de tipos de cuenta
        cboTipoCuenta.Items.Clear();
        if (tipoCuenta.Count > 0)
        {
            cboTipoCuenta.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            cboTipoCuenta.AppendDataBoundItems = true;
        }
        cboTipoCuenta.DataSource = tipoCuenta;
        cboTipoCuenta.DataValueField = "tpc_id";
        cboTipoCuenta.DataTextField = "tpc_nombre";
        cboTipoCuenta.DataBind();
    }

}