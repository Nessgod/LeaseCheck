using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_Usuarios : System.Web.UI.UserControl
{
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }
    public int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public bool TieneRut { get; set; }

    ClienteController controller = new ClienteController();
    private ClienteDocumentoController controllerClienteDocumento = new ClienteDocumentoController();
    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;
                }
            }


            gridUsuarios.AddSelectColumn();
            gridUsuarios.AddColumn("usu_id", "", "2%", Align: HorizontalAlign.Center);
            gridUsuarios.AddColumn("nombreCompleto", "NOMBRE COMPLETO", "", HorizontalAlign.Left);
            gridUsuarios.AddColumn("perfil_nombre", "PERFIL", "", HorizontalAlign.Left);
            gridUsuarios.AddCheckboxColumn("usu_habilitado", "HABILITADO", "10%");

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
        Cliente item = new Cliente();
        item.cli_id = IdCliente;
        gridUsuarios.DataSource = controller.GetClienteUsuarios(item);
        gridUsuarios.DataBind();
    }
    protected void lnkNuevoUsuario_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + IdCliente));
        Tools.tools.ClientExecute("abrirUsuario('" + query + "')");
    }

    protected void lnkEliminarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in gridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = gridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = controller.DeleteClienteUsuario(usuario);
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

    protected void gridUsuarios_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("usu_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + IdCliente));

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

    protected void lnkReset_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in gridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = gridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = controller.ResetPassword(usuario);
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