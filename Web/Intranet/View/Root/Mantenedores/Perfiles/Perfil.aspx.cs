using System;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Root_Mantenedores_Perfiles_Perfil : System.Web.UI.Page
{
    PerfilesController perfilesController = new PerfilesController();
    Perfiles perfil = new Perfiles();

    TipoPerfil tipoPerfil = new TipoPerfil();
    TipoPerfilController tipoPerfilController = new TipoPerfilController();

    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_perfil.Ver;
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

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboCliente":
                        
                        ctrl.EmptyMessage = "Seleccione";
                        ctrl.AppendDataBoundItems = true;
                        ctrl.DataSource = perfilesController.GetCliente();
                        ctrl.DataValueField = "cli_id";
                        ctrl.DataTextField = "cli_nombre";

                        ctrl.DataBind();
                        break;

                    case "cboTipoPerfil":

                        ctrl.EmptyMessage = "Seleccione";
                        ctrl.AppendDataBoundItems = true;
                        ctrl.DataSource = tipoPerfilController.ListoTipoPerfil();
                        ctrl.DataValueField = "tpp_id";
                        ctrl.DataTextField = "tpp_nombre";

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
            try
            {
                Perfiles perfil = new Perfiles();
                perfil.per_id = Id;
                perfil = perfilesController.GetPerfiles(perfil);

                lblId.Text = Id.ToString();
                txtNombre.Text = perfil.per_nombre;
                txtDescripcion.Text = perfil.per_descripcion;
                if (perfil.per_tipo_perfil > 0) cboTipoPerfil.SelectedValue = perfil.per_tipo_perfil.ToString();
                if (perfil.per_habilitado)
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

            perfil.per_id = Id;
            perfil.per_nombre = txtNombre.Text;
            perfil.per_descripcion = txtDescripcion.Text;
           
            perfil.per_tipo_perfil = int.Parse(cboTipoPerfil.SelectedValue);

            if (rdbSi.Checked)
                perfil.per_habilitado = true;
            else
                perfil.per_habilitado = false;

            if (Id > 0)
            {
                respuesta = perfilesController.UpdateItem(perfil);
            }
            else
            {
                respuesta = perfilesController.InsertItem(perfil);
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