using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_ClienteUsuarios : System.Web.UI.Page
{
    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_usuarios.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            gridUsuarios.AddSelectColumn();
            gridUsuarios.AddColumn("usu_id", "", "2%", Align: HorizontalAlign.Center);
            gridUsuarios.AddColumn("nombreCompleto", "NOMBRE", "", HorizontalAlign.Left);
            gridUsuarios.AddColumn("perfil_nombre", "PERFIL", "", HorizontalAlign.Left);
            gridUsuarios.AddColumn("nombre_propiedad", "PROPIEDAD", "", HorizontalAlign.Left);
            gridUsuarios.AddCheckboxColumn("usu_habilitado", "HABILITADO", "10%");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGridUsuarios();
        udPanel.Update();
    }

    protected void CargaGridUsuarios()
    {
        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
            Usuarios item = new Usuarios();
            if (wucFiltro.Filtro() != "") item.filtro = wucFiltro.Filtro();
            string[] perfiles = LeaseCheck.Session.UsuarioPerfil().Split(',');

            // Verificar si el usuario tiene el perfil de Ejecutivo (per_id = 7)
            if (perfiles.Contains(Convert.ToInt32(LeaseCheck.LeaseCheck.Perfiles.Ejecutivo).ToString()))
            {
                item.perfiles = "7";
                gridUsuarios.DataSource = controller.GetClienteUsuariosIdentidad(item);
                gridUsuarios.DataBind();

                LinkButton lnkReset = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkReset");
                lnkReset.Visible = false;
                LinkButton lnkEliminarUsuario = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkEliminarUsuario");
                lnkEliminarUsuario.Visible = false;
            }
            else
            {
                gridUsuarios.DataSource = controller.GetClienteUsuariosIdentidad(item);
                gridUsuarios.DataBind();
            }
        }
        else
        {
            List<ClientePlan> list = new List<ClientePlan>();
            gridUsuarios.DataSource = list;
            gridUsuarios.DataBind();

            LinkButton lnkNuevoUsuario = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkNuevoUsuario");
            lnkNuevoUsuario.Visible = false;

            LinkButton lnkEliminarUsuario = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkEliminarUsuario");
            lnkEliminarUsuario.Visible = false;


            LinkButton lnkReset = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkReset");
            lnkReset.Visible = false;


            //LinkButton lnkReset = (LinkButton)gridUsuarios.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkReset");
            //lnkReset.Visible = false;

            wucFiltro.Visible = false;
        }
    }

    protected void lnkNuevoUsuario_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + 0));
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
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

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
                    Tools.tools.ClientAlert(respuesta.detalle + "la nueva contraseña provisoria es 123456", "ok");
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