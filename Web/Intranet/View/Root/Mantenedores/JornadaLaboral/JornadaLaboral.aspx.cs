using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Mantenedores_JornadaLaboral_JornadaLaboral : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    private JornadasLaboralesController controller = new JornadasLaboralesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_jornada_laboral.Ver;
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
            CargaDatos();
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            JornadaLaboral item = new JornadaLaboral();
            item.jol_id = Id;

            item = controller.GetJornadaLaboral(item);
            txtNombre.Text = item.jol_nombre;

            if (item.jol_habilitado)
                rdbSi.Checked = true;
            else
                rdbNo.Checked = true;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            JornadaLaboral item = new JornadaLaboral();
            item.jol_id = Id;
            item.jol_nombre = txtNombre.Text;
            item.jol_habilitado = rdbSi.Checked;

            if (Id > 0)
                respuesta = controller.UpdateJornadaLaboral(item);
            else
                respuesta = controller.InsertJornadaLaboral(item);

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