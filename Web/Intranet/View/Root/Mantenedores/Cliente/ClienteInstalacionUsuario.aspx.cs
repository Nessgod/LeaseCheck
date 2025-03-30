using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Cliente_Cliente : System.Web.UI.Page
{
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }

    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }



    ClienteController controller = new ClienteController();
    ClienteInstalacionusuarioController controllerInstalacionUsuario = new ClienteInstalacionusuarioController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
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
                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;
                    case "IdClienteInstalacion":
                        IdClienteInstalacion = Int32.Parse(array[1].ToString());
                        break;
                }
            }

            Grid.AddSelectColumn();
            Grid.AddColumn("usu_id", "ID", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("NOMBRE_COMPLETO", "NOMBRE COMPLETO", "", HorizontalAlign.Left);
            Grid.AddColumn("usu_correo", "CORREO", "", HorizontalAlign.Left);
            Grid.AddColumn("PERFILES", "PERFIL", "", HorizontalAlign.Left);

        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaDatos();
            udPanel.Update();
        }
        CargaDatos();
        udPanel.Update();

    }

    protected void CargaDatos()
    {
        CargaGridUsuarios();
    }

    protected void CargaGridUsuarios()
    {
        Cliente item = new Cliente();
        ClienteInstalacionUsuario clienteInstalacionUsuario = new ClienteInstalacionUsuario();

        clienteInstalacionUsuario.cli_id = Cliente;
        clienteInstalacionUsuario.ciu_instalacion = IdClienteInstalacion;
        Grid.DataSource = controllerInstalacionUsuario.GetClienteInstalacionUsuariosAsociar(clienteInstalacionUsuario);
        Grid.DataBind();
    }


    protected void gridUsuariosInstalacion_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("usu_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + Cliente));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirUsuario('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["usu_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }


    protected void btnAsociar_Click(object sender, EventArgs e)
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
                ClienteInstalacionUsuario usuario = new ClienteInstalacionUsuario();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["usu_id"].ToString());

                   
                    usuario.ciu_usuario = id;
                    usuario.ciu_instalacion = IdClienteInstalacion;

                    respuesta = controllerInstalacionUsuario.InsertClienteInstalacionUsuario(usuario);
                }


                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");

            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }
}
