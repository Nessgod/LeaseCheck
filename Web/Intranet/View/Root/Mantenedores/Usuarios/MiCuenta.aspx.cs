using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Root_Mantenedores_Usuarios_MiCuenta : System.Web.UI.Page
{
    UsuarioController usuariosController = new UsuarioController();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    protected void CargarDatos()
    {
        if (int.Parse(LeaseCheck.Session.UsuarioId()) > 0)
        {
            Usuarios usuario = new Usuarios();
            usuario.usu_id = int.Parse(LeaseCheck.Session.UsuarioId());
            usuario.devuelve_foto = false;

            usuario = usuariosController.GetUsuario(usuario);

            lblLogin.Text = usuario.usu_login;
            TextNombre.Text = usuario.usu_nombres;
            txtPaterno.Text = usuario.usu_apellido_paterno;
            TextMaterno.Text = usuario.usu_apellido_materno;
            txtCorreo.Text = usuario.usu_correo;
            txtPassword.Text = usuario.usu_password;
            txtValidaPassword.Text = usuario.usu_password;
        }

        txtPassword.Attributes["type"] = "password";
        txtValidaPassword.Attributes["type"] = "password";
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
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
            usuario.usu_id = int.Parse(LeaseCheck.Session.UsuarioId());
            usuario.usu_login = lblLogin.Text;
            usuario.usu_password = txtPassword.Text;
            usuario.usu_nombres = TextNombre.Text;
            usuario.usu_apellido_paterno = txtPaterno.Text;
            usuario.usu_apellido_materno = TextMaterno.Text;
            usuario.usu_correo = txtCorreo.Text;

            usuario.es_cliente = false;

            respuesta = usuariosController.UpdateMiCuenta(usuario);

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