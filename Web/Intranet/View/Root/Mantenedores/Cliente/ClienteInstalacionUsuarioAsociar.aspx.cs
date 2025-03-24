using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WsCorreo;

public partial class View_Root_Mantenedores_Cliente_ClienteInstalacionUsuarioAsociar : System.Web.UI.Page
{

    public int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    public int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
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
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;
                    case "IdClienteInstalacion":
                        IdClienteInstalacion = Int32.Parse(array[1].ToString());
                        break;
              
                }

            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConfigurarGrid();
        }

        CargarGrid();
        udPanel.Update();
        GridAsociar.DataBind();
    }

    protected void ConfigurarGrid()
    {
        GridAsociar.AddSelectColumn();

        GridAsociar.AddColumn("USU_ID", "", Width: "2%");
        GridAsociar.AddColumn("USU_ID", "ID", Width: "6%");
        GridAsociar.AddColumn("NOMBRE_COMPLETO", "NOMBRE", Width: "38%");
        GridAsociar.AddColumn("USU_CORREO", "CORREO", Width: "30%");

        Tools.tools.RegisterPostBackScript(GridAsociar);

    }

    protected void CargarGrid()
    {
        ClienteInstalacionUsuario clienteInstalacionUsuario = new ClienteInstalacionUsuario();
        ClienteInstalacionusuarioController clienteInstalacionUsuarioController = new ClienteInstalacionusuarioController();
        clienteInstalacionUsuario.cli_id=  IdCliente;

        GridAsociar.DataSource = clienteInstalacionUsuarioController.GetClienteInstalacionUsuariosAsociar(clienteInstalacionUsuario);
    }


    protected void lnkAsociar_Click(object sender, EventArgs e)
    {
        if (GridAsociar.SelectedIndexes.Count == 0)
        {
            Tools.tools.ClientAlert("Debe seleccionar al menos un registro.");
        }
        else
        {
            try
            {
                ClienteInstalacionUsuario clienteInstalacionUsuario = new ClienteInstalacionUsuario();
                ClienteInstalacionusuarioController clienteInstalacionUsuarioController = new ClienteInstalacionusuarioController();
                Respuesta respuesta = new Respuesta();

                foreach (int item in GridAsociar.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridAsociar.MasterTableView.DataKeyValues[item];
                    int id = Int32.Parse(value["usu_id"].ToString());

                    clienteInstalacionUsuario.usu_id = id;
                    clienteInstalacionUsuario.ciu_instalacion = IdClienteInstalacion;

                    respuesta = clienteInstalacionUsuarioController.InsertClienteInstalacionUsuario(clienteInstalacionUsuario);
                }

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
            catch (Exception ex)
            {
                Tools.tools.ClientAlert(ex.ToString(), "error");
            }
        }
    }


    protected void lnkEliminar_Click(object sender, EventArgs e)
    {

    }

    protected void GridAsociar_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }


}