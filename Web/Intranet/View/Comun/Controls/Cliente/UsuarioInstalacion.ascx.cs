using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_Usuarioinstalacion : System.Web.UI.UserControl
{
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }
    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }

    ClienteInstalacionusuarioController controller = new ClienteInstalacionusuarioController();

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


            gridUsuariosInstalacion.AddSelectColumn();
            gridUsuariosInstalacion.AddColumn("CIU_ID", "ID", "2%", Align: HorizontalAlign.Center);
            gridUsuariosInstalacion.AddColumn("NOMBRE_COMPLETO", "NOMBRE COMPLETO", "", HorizontalAlign.Left);
            gridUsuariosInstalacion.AddColumn("USU_CORREO", "CORREO", "", HorizontalAlign.Left);
            gridUsuariosInstalacion.AddColumn("PERFILES", "PERFIL", "", HorizontalAlign.Left);

        }
    }

    public void LoadControls(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {

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
        ClienteInstalacionUsuario item = new ClienteInstalacionUsuario();
        item.ciu_instalacion = IdClienteInstalacion;
        gridUsuariosInstalacion.DataSource = controller.GetClienteInstalacionUsuarios(item);
        gridUsuariosInstalacion.DataBind();
    }


    protected void gridUsuariosInstalacion_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            //if (((e.Item) is GridDataItem))
            //{
            //    GridDataItem item = e.Item as GridDataItem;
            //    string id = item.GetDataKeyValue("ciu_id").ToString();
            //    string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + Cliente));

            //    HyperLink Editar = new HyperLink();
            //    Editar.ID = "lnk" + id;
            //    Editar.CssClass = "icono_Editar";
            //    Editar.NavigateUrl = "javascript:void(0)";
            //    Editar.Attributes.Add("onclick", "abrirUsuario('" + query + "')");

            //    GridDataItem DataItem = e.Item as GridDataItem;
            //    TableCell dca_id = DataItem["ciu_id"];
            //    dca_id.Controls.Add(Editar);
            //}
        }
    }


    protected void lnkAsociar_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + Cliente + "&IdClienteInstalacion=" + IdClienteInstalacion));
        Tools.tools.ClientExecute("abrirUsuario('" + query + "')");
    }

    protected void lnkDesasociar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridUsuariosInstalacion.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in gridUsuariosInstalacion.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = gridUsuariosInstalacion.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["ciu_id"].ToString());

                    ClienteInstalacionUsuario usuario = new ClienteInstalacionUsuario();
                    usuario.ciu_id = id;

                    respuesta = controller.DeleteClienteInstalacionUsuario(usuario);
                }


                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
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