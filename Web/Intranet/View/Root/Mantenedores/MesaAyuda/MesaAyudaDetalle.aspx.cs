using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;

public partial class View_Root_Mantenedores_MesaAyuda_MesaAyudaDetalle : System.Web.UI.Page
{
    protected MesaAyudaController mesaAyudaController = new MesaAyudaController();
    
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected int Estado
    {
        get { return Convert.ToInt32(ViewState["Estado"]); }
        set { ViewState.Add("Estado", value); }
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_feriado.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {

                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;

                    case "Estado":
                        Estado = Int32.Parse(array[1].ToString());
                        break;
                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarDatos();
    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            MesaAyuda mesaAyuda = new MesaAyuda();
            mesaAyuda.mes_id = Id;
            mesaAyuda = mesaAyudaController.GetMesaAyudaDetalle(mesaAyuda);
            lblId.Text = Id.ToString();
            txtNombre.Text = mesaAyuda.mes_nombre;
            txtCorreo.Text = mesaAyuda.mes_correo;
            txtTelefono.Text = mesaAyuda.mes_telefono.ToString();
            txtFechaConsulta.Text = mesaAyuda.mes_fecha_creacion.ToString("dd-MM-yyyy");
            txtConsulta.Text = mesaAyuda.mes_mensaje;
            txtRespuesta.Text = mesaAyuda.mes_observacion_cierre;
        }

        if (Estado == 1) {
            btnGuardar.Visible = true;
            txtRespuesta.ReadOnly = false;
        } 
        else {
            btnGuardar.Visible = false;
            txtRespuesta.ReadOnly = true;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            MesaAyuda mesaAyuda = new MesaAyuda();
            mesaAyuda.mes_id = Id;
            mesaAyuda.mes_observacion_cierre = txtRespuesta.Text;


            respuesta = mesaAyudaController.UpdateMesaAyuda(mesaAyuda);

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