using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Root_Mantenedores_Productos_Producto : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    private ProductoController controller = new ProductoController();
    private PlanProductoController controllerDocumento = new PlanProductoController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_producto.Ver;
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

            Grid.AddSelectColumn();
            Grid.AddColumn("tdc_nombre", "TIPO DOCUMENTO", Align: HorizontalAlign.Left);

            GridServicio.AddSelectColumn();
            GridServicio.AddColumn("tsc_nombre", "SERVICIO", Align: HorizontalAlign.Left);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarProducto();
        else
            CargarProducto();
    }


    protected void CargarProducto()
    {
        if (Id > 0)
        {
            Producto producto = new Producto();
            producto.pro_id = Id;
            producto = controller.GetProducto(producto);

            txtNombre.Text = producto.pro_producto;

            if (producto.pro_habilitado)
                rdbSi.Checked = true;
            else
                rdbNo.Checked = true;

            CargarGridDocumento();
            CargarGridServicio();
            pnlDocumento.Visible = true;
            pnlServicios.Visible = true;
        }
        else
        {
            pnlDocumento.Visible = false;
            pnlServicios.Visible = false;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            Producto producto = new Producto();
            producto.pro_id = Id;
            producto.pro_producto = txtNombre.Text;
            producto.pro_habilitado = rdbSi.Checked;

            if (Id > 0)
                respuesta = controller.UpdateProductos(producto);
            else
                respuesta = controller.InsertProducto(producto);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
                Id = respuesta.codigo;
            }
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }



    }

    protected void CargarGridDocumento()
    {
        PlanProductoDocumento planProductoDocumento = new PlanProductoDocumento();

        planProductoDocumento.prd_producto = Id;

        Grid.DataSource = controllerDocumento.GetListadoProductosDocumento(planProductoDocumento);
        Grid.DataBind();
    }

    protected void CargarGridServicio()
    {
        ClientePropiedadTipoServicio servicio = new ClientePropiedadTipoServicio();

        servicio.psc_producto = Id;

        GridServicio.DataSource = controllerDocumento.GetListadoProductosServicio(servicio);
        GridServicio.DataBind();
    }

    protected void lnlNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdProducto=" + Id));

        Tools.tools.ClientExecute("abrirDocumento('" + query + "')");
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
                    int id = Int32.Parse(value["prd_id"].ToString());

                    PlanProductoDocumento planProductoDocumento = new PlanProductoDocumento();
                    planProductoDocumento.prd_id = id;

                    respuesta = controllerDocumento.DeleteProductoDocumento(planProductoDocumento);
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

    protected void lnlNuevoServicio_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdProducto=" + Id));

        Tools.tools.ClientExecute("abrirServicio('" + query + "')");
    }

    protected void lnlEliminarServicio_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridServicio.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in GridServicio.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridServicio.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["psc_id"].ToString());

                    ClientePropiedadTipoServicio servicio = new ClientePropiedadTipoServicio();
                    servicio.psc_id = id;

                    respuesta = controllerDocumento.DeleteProductoServicio(servicio);
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
}