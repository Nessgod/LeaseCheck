using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_ClientePropiedades : System.Web.UI.Page
{
    private ClientePropiedadController controller = new ClientePropiedadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_usuarios.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

        if (!IsPostBack)
        {
            ConfigurarGrid();
        }
    }


    protected void ConfigurarGrid()
    {
        Grid.AddSelectColumn();
        Grid.AddColumn("cpd_id", "", "2%", Align: HorizontalAlign.Center);
        Grid.AddColumn("cpd_id", "CÓDIGO", "2%", Align: HorizontalAlign.Center);
        Grid.AddColumn("cpd_titulo", "TITULO", "", HorizontalAlign.Left);
        Grid.AddColumn("TIPO_PROPIEDAD", "TIPO PROPIEDAD", "", HorizontalAlign.Left);
        Grid.AddColumn("TIPO_SERVICIO", "TIPO SERVICIO", "", HorizontalAlign.Left);
        Grid.AddColumn("TIPO_ENTREGA", "TIPO ENTREGA", "", HorizontalAlign.Left);
        Grid.AddColumn("cpd_valor_uf", "VALOR UF", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
        Grid.AddColumn("cpd_valor_venta", "VALOR VENTA ($)", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
        Grid.AddColumn("GANANCIA", "GANANCIA", "", HorizontalAlign.Left, DataFormat: "{0:N0}");
        Grid.AddColumn("ESTADO", "ESTADO", "", HorizontalAlign.Left);

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGridPropiedades();
        udPanel.Update();
    }

    protected void CargaGridPropiedades()
    {
        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
            ClientePropiedad item = new ClientePropiedad();
            Grid.DataSource = controller.GetListadoPropiedades(item);
            Grid.DataBind();

        }
        else
        {
            List<ClientePropiedad> list = new List<ClientePropiedad>();
            Grid.DataSource = list;
            Grid.DataBind();

            LinkButton lnkNuevaPropiedad = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkNuevaPropiedad");
            lnkNuevaPropiedad.Visible = false;

            LinkButton lnkEliminarPropiedad = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkEliminarPropiedad");
            lnkEliminarPropiedad.Visible = false;


            wucFiltro.Visible = false;
        }
    }

    protected void lnkNuevaPropiedad_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + 0));
        Tools.tools.ClientExecute("abrirPropiedad('" + query + "')");
    }

    protected void lnkEliminarPropiedad_Click(object sender, EventArgs e)
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

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cpd_id"].ToString());

                    ClientePropiedad propiedad = new ClientePropiedad();
                    propiedad.cpd_id = id;

                    respuesta = controller.DeleteClientePropiedad(propiedad);
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

    protected void Grid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("cpd_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirPropiedad('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["cpd_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }
}