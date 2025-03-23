using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using Telerik.Web.UI;

public partial class View_Sistema_Feriado_Feriado : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected FeriadoController controller = new FeriadoController();

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
                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarDatos();
    }

    public void LoadControls(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboPais":
                        PaisesController paisesController = new PaisesController();
                        Paises filtro = new Paises();
                        filtro.pai_habilitado = true;

                        ctrl.EmptyMessage = "Seleccione...";
                        ctrl.DataSource = paisesController.GetPaises(filtro);
                        ctrl.DataTextField = "PAI_NOMBRE";
                        ctrl.DataValueField = "PAI_ID";
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            Feriado item = new Feriado();
            item.frd_id = Id;
            item = controller.GetFeriado(item);

            txtFecha.Value = item.frd_fecha_feriados;
            cboPais.SelectedValue = item.frd_pais.ToString();
            txtDescripcion.Text = item.frd_descripcion;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            Feriado item = new Feriado();
            item.frd_id = Id;
            item.frd_fecha_feriados = txtFecha.Value.Value;
            item.frd_descripcion = txtDescripcion.Text;
            item.frd_pais = int.Parse(cboPais.SelectedValue);

            if (Id > 0)
            {
                respuesta = controller.UpdateFeriado(item);
            }
            else
            {
                respuesta = controller.InsertFeriado(item);
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