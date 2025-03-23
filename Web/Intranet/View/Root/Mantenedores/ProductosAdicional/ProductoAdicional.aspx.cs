using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Root_Mantenedores_ProductosAdicional_ProductoAdicional : System.Web.UI.Page
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
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_producto_adicional.Ver;
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
            CargarProductoAdicional();
    }
    
    protected void CargarProductoAdicional()
    {
        if (Id > 0)
        {
            ProductoAdicional producto = new ProductoAdicional();
            producto.pra_id = Id;
            producto = controller.GetProductoAdicional(producto);

            txtNombre.Text = producto.pra_nombre;
            //TxtValorProducto.Value = producto.pra_valor_producto;
            txtDetalle.Text = producto.pra_detalle;

            if (producto.pra_habilitado)
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

            ProductoAdicional producto = new ProductoAdicional();
            producto.pra_id = Id;
            producto.pra_nombre = txtNombre.Text;
            producto.pra_valor_producto = 0; // Convert.ToInt32(TxtValorProducto.Value.Value);
            producto.pra_habilitado = rdbSi.Checked;
            producto.pra_detalle = txtDetalle.Text;

            if (Id > 0)
                respuesta = controller.UpdateProductosAdicional(producto);
            else
                respuesta = controller.InsertProductoAdicional(producto);

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