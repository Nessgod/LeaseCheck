using System;
using System.Web.Script.Services;
using System.Web.Services;
using LeaseCheck.Root.Model;
using LeaseCheck.Root.Controller;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

public partial class View_Root_Mantenedores_Cliente_ClienteUsuario : System.Web.UI.Page
{
    ClienteController controller = new ClienteController();

    public int Id
    {
        get { return Convert.ToInt32(ViewState["id"]); }
        set { ViewState.Add("id", value); }
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
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
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

                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;
                }

            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarDatos();
       
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

                        ctrl.DataSource = controller.GetPerfiles();
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
        Cliente cliente = new Cliente();
        ClienteController clientecontroller = new ClienteController();

        cliente.cli_id = Cliente;
        cliente = clientecontroller.GetCliente(cliente);

        cboPais.SelectedValue = cliente.cli_pais.ToString();

        if (Id > 0)
        {
            hfdUsuarioID.Value = Id.ToString();

            Usuarios usuario = new Usuarios();
            usuario.usu_id = Id;
            usuario.devuelve_foto = true;

            usuario = controller.GetClienteUsuario(usuario);
            Cliente clientes = new Cliente();

            clientes.cli_id = usuario.cliente;
            clientes = clientecontroller.GetCliente(clientes);

            lblTituloUsuario.Text = usuario.usu_login;

            lblID.Text = Id.ToString();
            txtLogin.Text = usuario.usu_login;
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
            Cliente cliente = new Cliente();
            ClienteController clientecontroller = new ClienteController();

            cliente.cli_id = Cliente;
            cliente = clientecontroller.GetCliente(cliente);

            
            usuario.usu_id = Id;
            usuario.usu_login = txtLogin.Text;
            usuario.usu_password = txtPassword.Text;
            usuario.usu_nombres = TextNombre.Text;
            usuario.usu_apellido_paterno = txtPaterno.Text;
            usuario.usu_apellido_materno = TextMaterno.Text;
            usuario.usu_fono = txtFono.Text;
            usuario.usu_correo = txtCorreo.Text;
            usuario.usu_perfil = int.Parse(cboPerfil.SelectedValue);
            usuario.usu_pais = cliente.cli_pais;
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