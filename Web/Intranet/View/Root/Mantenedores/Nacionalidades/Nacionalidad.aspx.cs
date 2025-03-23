using System;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Nacionalidades_Nacionalidad : System.Web.UI.Page
{
    private NacionalidadesController nacionalidadesController = new NacionalidadesController();
    Nacionalidades nacionalidad = new Nacionalidades();

    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_nacionalidad.Ver;
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
        CargarDatos();
    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            try
            {
                nacionalidad.nac_id = Id;
                nacionalidad = nacionalidadesController.GetNacionalidad(nacionalidad);
                txtNombre.Text = nacionalidad.nac_nombre;

                if (nacionalidad.nac_habilitado)
                {
                    rdbSi.Checked = true;
                    rdbNo.Checked = false;
                }
                else
                {
                    rdbSi.Checked = false;
                    rdbNo.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Tools.tools.ClientExecute("Mensaje_False_Detalle_Data('" + ex.Message + "')");
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            nacionalidad.nac_id = Id;
            nacionalidad.nac_nombre = txtNombre.Text;

            if (rdbSi.Checked)
                nacionalidad.nac_habilitado = true;
            else
                nacionalidad.nac_habilitado = false;

            if (Id > 0)
            {
                respuesta = nacionalidadesController.UpdateNacionalidad(nacionalidad);
            }
            else
            {
                respuesta = nacionalidadesController.InsertNacionalidad(nacionalidad);
                Id = respuesta.codigo;
            }

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
}