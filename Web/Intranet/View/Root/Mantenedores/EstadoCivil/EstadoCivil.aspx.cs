using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Mantenedores_EstadoCivil_EstadoCivil : System.Web.UI.Page
{
    EstadoCivilesController estadoCivilesController = new EstadoCivilesController();
    EstadoCivil estadoCivil = new EstadoCivil();

    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_estado_civi.Ver;
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
        CargarEstadoCivil();
    }

    protected void CargarEstadoCivil()
    {
        if (Id > 0)
        {
            estadoCivil = new EstadoCivil();
            estadoCivil.eci_id = Id;

            estadoCivil = estadoCivilesController.GetEstadoCivil(estadoCivil);
            txtNombre.Text = estadoCivil.eci_nombre;

            if (bool.Parse(estadoCivil.eci_habilitado.ToString()))
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
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            estadoCivil = new EstadoCivil();
            estadoCivil.eci_id = Id;
            estadoCivil.eci_nombre = txtNombre.Text;

            if (rdbSi.Checked)
                estadoCivil.eci_habilitado = true;
            else
                estadoCivil.eci_habilitado = false;

            if (Id > 0)
                respuesta = estadoCivilesController.UpdateEstadoCivil(estadoCivil);
            else
            {
                respuesta = estadoCivilesController.InsertEstadoCivil(estadoCivil);
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