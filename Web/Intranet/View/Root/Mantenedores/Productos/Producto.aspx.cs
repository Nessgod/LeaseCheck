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
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
}