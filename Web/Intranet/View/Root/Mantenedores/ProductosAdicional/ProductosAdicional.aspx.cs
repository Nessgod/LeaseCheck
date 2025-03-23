using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_ProductosAdicional_ProductosAdicional : System.Web.UI.Page
{
    private ProductoController controller = new ProductoController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_producto_adicional.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("pra_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("pra_id", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("pra_nombre", "NOMBRE", "50%", HorizontalAlign.Left);
            //Grid.AddColumn("pra_detalle", "DETALLE", "25%", HorizontalAlign.Left);
            //Grid.AddColumn("pra_valor_producto", "VALOR ($)", "25%", HorizontalAlign.Left, DataFormat: "{0:N0}");
            Grid.AddCheckboxColumn("pra_habilitado", "HABILITADO");
        }
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();
        LinkButton lnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkDescargarPlantilla");
        //script que genera el excel 
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescargarPlantilla);
    }
    
    protected void CargaGrid()
    {
        ProductoAdicional producto = new ProductoAdicional();
        if (wucFiltro.Filtro() != "") producto.filtro = wucFiltro.Filtro();
        Grid.DataSource = controller.GetProductosAdicional(producto);
        Grid.DataBind();
    }
    
    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("pra_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["pra_id"];
                dca_id.Controls.Add(Editar);
            }
        }
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

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["pra_id"].ToString());

                    ProductoAdicional producto = new ProductoAdicional();
                    producto.pra_id = id;

                    respuesta = controller.DeleteProductosAdicional(producto);
                }

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void lnkDescargarPlantilla_Click(object sender, EventArgs e)
    {
        ProductoAdicional producto = new ProductoAdicional();
        if (wucFiltro.Filtro() != "") producto.filtro = wucFiltro.Filtro();

        controller.InformeProductoAdicional(producto);
    }
}