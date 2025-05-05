using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_ClientePlanes : System.Web.UI.Page
{
    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_planes.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddColumn("clp_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("plan_nombre", "NOMBRE", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_fecha_desde", "DESDE", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("clp_fecha_hasta", "HASTA", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("plan_documento", "CANTIDAD DOCUMENTOS", "", HorizontalAlign.Left);
            Grid.AddColumn("plan_propiedad", "CANTIDAD PROPIEDADES", "", HorizontalAlign.Left);
            Grid.AddColumn("plan_lead", "CANTIDAD DE POTENCIALES CLIENTES", "", HorizontalAlign.Left);
            Grid.AddColumn("valor_plan", "VALOR", "", HorizontalAlign.Left, DataFormat: "{0:N0}");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
    }

    protected void CargaGrid()
    {
        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
            ClientePlan item = new ClientePlan();
            Grid.DataSource = controller.GetClientePlanesIdentidad();
           
        }
        else
        {
            List<ClientePlan> list = new List<ClientePlan>();
            Grid.DataSource = list;
        }

        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("clp_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirPlan('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["clp_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }
}