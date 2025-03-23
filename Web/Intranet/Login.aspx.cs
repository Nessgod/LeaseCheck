using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatosCorreo.Controller;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using Telerik.Web.UI;

public partial class Login : System.Web.UI.Page
{
    PaisesController paisesController = new PaisesController();
    public string empresa
    {
        get { return Convert.ToString(ViewState["empresa"]); }
        set { ViewState.Add("empresa", value); }
    }

    public string postulante
    {
        get { return Convert.ToString(ViewState["postulante"]); }
        set { ViewState.Add("postulante", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CargaPaises();
        if (!IsPostBack)
        {
            // Obtener el valor de la cadena de consulta 'query'
            string queryValue = Request.QueryString["query"];

            // Mostrar u ocultar secciones en función del valor del parámetro
            if (!string.IsNullOrEmpty(queryValue))
            {
                switch (queryValue)
                {
                    case "Postulante":
                        Postulante.Style.Add("display", "");
                        Acceso.Style.Add("display", "none");
                        Soporte.Style.Add("display", "none");
                        Comercial.Style.Add("display", "none");
                        break;

                    case "Empresa":
                        Postulante.Style.Add("display", "none");
                        Acceso.Style.Add("display", "");
                        Soporte.Style.Add("display", "none");
                        Comercial.Style.Add("display", "none");
                        break;

                    case "Soporte":
                        Postulante.Style.Add("display", "none");
                        Acceso.Style.Add("display", "none");
                        Soporte.Style.Add("display", "");
                        Comercial.Style.Add("display", "none");
                        break;

                    case "Comercial":
                        Postulante.Style.Add("display", "none");
                        Acceso.Style.Add("display", "none");
                        Soporte.Style.Add("display", "none");
                        Comercial.Style.Add("display", "");
                        break;
                    default:
                        Postulante.Style.Add("display", "");
                        Acceso.Style.Add("display", "none");
                        Soporte.Style.Add("display", "none");
                        Comercial.Style.Add("display", "none");
                        break;
                }
            }
        }
    }


    #region pais
    protected void CargaPaises()
    {
        if (!IsPostBack)
        {
            LeaseCheck.Session.Pais("1");
        }
        //rptPais.DataSource = paisesController.GetPaisesLogin();
        //rptPais.DataBind();

    }

    protected void seleccionarPais(object sender, CommandEventArgs e)
    {
        try
        {
            Paises paises = new Paises();
            paises.pai_id = int.Parse(e.CommandName);
            paises = paisesController.GetPaisLogin(paises);
            LeaseCheck.Session.Pais(e.CommandName.ToString());

            //if (paises.pai_imagen != null)
            //{
            //    imgPaisSeleccionado.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(paises.pai_imagen, 0, paises.pai_imagen.Length);
            //}
            //else
            //{
            //    imgPaisSeleccionado.ImageUrl = ResolveUrl("~/Imagen/no-camaras.png");
            //}
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error", true);

        }
    }

    protected void rptPais_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblPais = (Label)e.Item.FindControl("lblPais");
            Image imgPais = (Image)e.Item.FindControl("imgPais");
            lblPais.Text = DataBinder.Eval(e.Item.DataItem, "pai_nombre").ToString();
            string idPais = DataBinder.Eval(e.Item.DataItem, "pai_id").ToString();

            LinkButton lnkPais = (LinkButton)e.Item.FindControl("lnkPais");
            lnkPais.CommandName = DataBinder.Eval(e.Item.DataItem, "pai_id").ToString();

            byte[] imagen = (byte[])DataBinder.Eval(e.Item.DataItem, "pai_imagen");

            //si el pais actual de la session
            if (LeaseCheck.Session.Pais() == idPais)
            {
                //if (imagen != null)
                //{
                //    imgPaisSeleccionado.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imagen, 0, imagen.Length);
                //}
                //else
                //{
                //    imgPaisSeleccionado.ImageUrl = ResolveUrl("~/Imagen/no-camaras.png");
                //}
            }

            if (imagen != null)
            {
                imgPais.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imagen, 0, imagen.Length);
            }
            else
            {
                imgPais.ImageUrl = ResolveUrl("~/Imagen/no-camaras.png");
            }
        }
    }

    protected void rptPais_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPais = (LinkButton)e.Item.FindControl("lnkPais");
            lnkPais.Command += new CommandEventHandler(seleccionarPais);
        }
    }

    #endregion

    protected void btnLogin_Click(object sender, EventArgs e)
    {
    }

    protected void lnkPostulante_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx?query=Postulante");
    }

    protected void lnkEmpresa_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx?query=Empresa");

    }

    protected void lnkSoporte_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx?query=Soporte");
    }

    protected void lnkComercial_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx?query=Comercial");

    }

    //protected void btnLoginPostulante_Click(object sender, EventArgs e)
    //{
    //    LeaseCheck.Clientes.Controller.CargoController controller = new LeaseCheck.Clientes.Controller.CargoController();

    //    LeaseCheck.Clientes.Model.Cargo usuario = new LeaseCheck.Clientes.Model.Cargo();
    //    usuario.car_clave = txtCodigoPostulante.Text;

    //    Respuesta respuesta = controller.GetCargoClave(usuario);

    //    if (!respuesta.error)
    //    {
    //        Response.Redirect("~/View/Candidatos/Candidato/Candidato.aspx");
    //    }
    //    else
    //        lblMensajePostulante.Text = respuesta.detalle;
    //}

    protected void btnLoginEmpresa_Click(object sender, EventArgs e)
    {
        UsuarioController usuarioController = new UsuarioController();

        Usuarios usuario = new Usuarios();
        usuario.usu_login = txtLogin.Text;
        usuario.usu_password = txtPassword.Text;

        Respuesta respuesta = usuarioController.GetUsuarioLogin(usuario);

        if (!respuesta.error)
        {
            //if (LeaseCheck.Session.Usuario_Es_Cliente() == "True")
            //    Response.Redirect("~/View/Clientes/Cargo/Cargos.aspx");
            //else
            Response.Redirect("~/Default.aspx");
        }
        else
            lblMensaje.Text = respuesta.detalle;
    }

    protected void btnEnviarConsulta_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            MesaAyuda mesaAyuda = new MesaAyuda();
            MesaAyudaController mesaAyudaController = new MesaAyudaController();

            mesaAyuda.mes_nombre = txtNombreMesaAyuda.Text;
            mesaAyuda.mes_telefono = int.Parse(txtTelefono.Text);
            mesaAyuda.mes_correo = txtCorreo.Text;
            mesaAyuda.mes_mensaje = txtMensaje.Text;

            respuesta = mesaAyudaController.InsertMesaAyuda(mesaAyuda);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
                Limpiar();
            }
            else
            {
                Tools.tools.ClientAlert(respuesta.detalle, "error");
            }

        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void btnEnviarComercial_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ContactoComercial contacto = new ContactoComercial();
            EnvioCorreoController controller = new EnvioCorreoController();

            contacto.nombre = txtNombreComercial.Text;
            contacto.empresa = txtEmpresa.Text;
            contacto.cargo = txtCargo.Text;
            contacto.telefono = txtTelefonoComercial.Text;
            contacto.correo = txtCorreoComercial.Text;
            contacto.mensaje = txtMensajeComercial.Text;

            respuesta = controller.EnviarCorreo(contacto);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
                LimpiarComercial();
            }
            else
            {
                Tools.tools.ClientAlert(respuesta.detalle, "error");
            }

        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void Limpiar()
    {
        txtNombreMesaAyuda.Text = "";
        txtTelefono.Text = "";
        txtCorreo.Text = "";
        txtMensaje.Text = "";

    }

    protected void LimpiarComercial()
    {
        txtNombreComercial.Text = "";
        txtEmpresa.Text = "";
        txtCargo.Text = "";
        txtTelefonoComercial.Text = "";
        txtCorreoComercial.Text = "";
        txtMensajeComercial.Text = "";
    }

}