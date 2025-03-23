using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Reasignacion_Clientes : System.Web.UI.Page
{
    protected int usuario
    {
        get { return Convert.ToInt32(ViewState["usuario"]); }
        set { ViewState.Add("usuario", value); }
    }

    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_reasignacion_clientes.Ver;
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
                    case "usuario":
                        usuario = Int32.Parse(array[1].ToString());
                        break;
                }
            }

            Grid.AddSelectColumn();
            Grid.AddColumn("cli_rut_completo", "RUT", "", HorizontalAlign.Left);
            Grid.AddColumn("cli_nombre", "CLIENTE", "", HorizontalAlign.Left);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();
    }

    protected void CargaGrid()
    {
        Cliente item = new Cliente();
        item.cli_usuario = usuario;

        Grid.DataSource = controller.GetClienteReasignacion(item);
        Grid.DataBind();
    }

    protected void lnkNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("usuario=" + usuario));
        Tools.tools.ClientExecute("abrir('" + query + "')");
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                string clientes = "";

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    string id = value["cli_id"].ToString();

                    clientes += id + ",";
                }

                if (clientes.Length > 0)
                    clientes = clientes.Remove(clientes.Length - 1, 1);

                string query = Server.UrlEncode(Tools.Crypto.Encrypt("clientes=" + clientes));
                Tools.tools.ClientExecute("abrirQuitar('" + query + "')");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
}