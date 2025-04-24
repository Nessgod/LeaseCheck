using System;
using System.Web.Script.Services;
using System.Web.Services;
using LeaseCheck.Root.Model;
using LeaseCheck.Root.Controller;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

public partial class View_Sistema_Usuarios_Usuario : System.Web.UI.Page
{
   private UsuarioController usuariosController = new UsuarioController();
    private ClienteController clienteController = new ClienteController();
    public int Id
    {
        get { return Convert.ToInt32(ViewState["id"]); }
        set { ViewState.Add("id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_usuario.Ver;
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
                }

            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
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

                        Perfiles filtro = new Perfiles();
                        filtro.per_habilitado = true;

                        ctrl.DataSource = usuariosController.GetPerfiles(filtro);
                        ctrl.DataValueField = "per_id";
                        ctrl.DataTextField = "per_nombre";

                        ctrl.DataBind();
                        break;

                    case "cboPais":

                        Paises filtro2 = new Paises();
                        filtro2.pai_habilitado = true;

                        ctrl.DataSource = usuariosController.GetPaises(filtro2);
                        ctrl.DataValueField = "pai_id";
                        ctrl.DataTextField = "pai_nombre";

                        ctrl.DataBind();
                        break;

                    case "cboComuna":
                        Comuna filtro1 = new Comuna();
                        ClienteController controller = new ClienteController();
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
                        var estadoCivils = clienteController.GetEstadoCivil();

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
                        var profesion = clienteController.GetProfesion();

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
                        var genero = clienteController.GetGenero();

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
                        var nacionalidad = clienteController.GetNacionalidad();

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
                }
            }
        }
    }

    protected void Validaciones()
    {
        if (Id > 0)
        {
            //ragTab.Tabs[1].Visible = true;
            //ragTab.Tabs[2].Visible = true;
            //ragTab.Tabs[3].Visible = true;
            //ragTab.Tabs[4].Visible = true;
            //pnlNombre.Visible = true;
        }
        else 
        {
            //ragTab.Tabs[1].Visible = false;
            //ragTab.Tabs[2].Visible = false;
            //ragTab.Tabs[3].Visible = false;
            //ragTab.Tabs[4].Visible = false;
            //pnlNombre.Visible = false;
            lblTituloUsuario.Text = "Nuevo Usuario";
        }
    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            hfdUsuarioID.Value = Id.ToString();

            Usuarios usuario = new Usuarios();
            usuario.usu_id = Id;
            usuario.devuelve_foto = true;

            usuario = usuariosController.GetUsuario(usuario);

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

        txtPassword.Attributes["type"] = "password";
    }

    protected void btnGuardar_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtPassword.Text.Length < 8)
                throw new Exception("Requiere un password con un mínimo de 8 caracteres.");

            if (string.IsNullOrEmpty(Regex.Match(txtPassword.Text, @"\d+").Value))
                throw new Exception("Requiere un password que contenga al menos un número.");

            if (string.IsNullOrEmpty(Regex.Match(txtPassword.Text, "[A-Z]").Value))
                     throw new Exception("Requiere un password que contenga al menos una letra mayúscula.");


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


            if (rdbSi.Checked)
                usuario.usu_habilitado = true;
            else
                usuario.usu_habilitado = false;

            usuario.es_cliente = false;

            if (fudFoto.HasFile)
                usuario.usu_foto = LeaseCheck.LeaseCheck.ReducirImagen(fudFoto.FileBytes, 100, 100);

            if (Id > 0)
                respuesta = usuariosController.UpdateUsuario(usuario);
            else
            {
                respuesta = usuariosController.InsertUsuario(usuario);
                Id = respuesta.codigo;
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
}