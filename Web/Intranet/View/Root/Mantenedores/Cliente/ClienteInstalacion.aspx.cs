using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Model;
using System;
using Library;

using Telerik.Web.UI;
using LeaseCheck.Clientes.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class View_Root_Mantenedores_Cliente_ClienteInstalacion : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }


    protected int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    private ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();

    
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
                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;

                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;

                    case "IdClienteInstalacion":
                        IdClienteInstalacion = Int32.Parse(array[1].ToString());
                        break;
                }
            }
            ConfigurarGrid();
            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaDatos();
      
        if (Id == 0)
            lblTitulo.Text = "Identidad de la instalación";

        CargaDatos();
        CargarGridUsuarios();

    }

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();
            ClienteInstalacion clienteInstalacion = new ClienteInstalacion();

            clienteInstalacion.cin_id = Id;
            clienteInstalacion = clienteInstalacionController.GetClienteInstalacion(clienteInstalacion);

            lblId.Text = Id.ToString();
            txtNombre.Text = clienteInstalacion.cin_nombre;
            txtDescripcion.Text = clienteInstalacion.cin_descripcion;
            txtDireccion.Text = clienteInstalacion.cin_direccion;
            txtTelefono.Text = clienteInstalacion.cin_telefono;

            if (clienteInstalacion.cin_habilitado == false)
            {
                rdbNo.Checked = true;
                rdbSi.Checked = false;
            }
            if (clienteInstalacion.cin_habilitado == true)
            {
                rdbNo.Checked = false;
                rdbSi.Checked = true;
            }
            CargarGridUsuarios();
            pnlGrillUsuarios.Visible = true;
        }
        else
        {
            pnlGrillUsuarios.Visible = false;
            CargarGridVacia();
        }
    }

   

    //Guardar Instalacion
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            ClienteInstalacion item = new ClienteInstalacion();
            item.cin_id = Id;
            item.cin_cliente = Cliente;
            item.cin_nombre = txtNombre.Text;
            item.cin_descripcion = txtDescripcion.Text;
            item.cin_direccion = txtDireccion.Text;
            item.cin_telefono = txtTelefono.Text;


            if (rdbSi.Checked == true)
                item.cin_habilitado = true;
            else
                item.cin_habilitado = false;

            if (Id > 0)
            {
                respuesta = clienteInstalacionController.UpdateClienteInstalacion(item);
                pnlGrillUsuarios.Visible = true;
                CargarGridUsuarios();
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            }
            else
                respuesta = clienteInstalacionController.InsertClienteInstalacion(item);
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
                IdClienteInstalacion = respuesta.codigo;
                pnlGrillUsuarios.Visible = true;
                CargarGridVacia();
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void ConfigurarGrid()
    {
        GridUsuarios.Columns.Clear();
        GridUsuarios.AddSelectColumn();
        GridUsuarios.AddColumn("CIU_ID", "", Width: "5%", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("CIU_ID", "ID", Width: "5%", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("NOMBRE_COMPLETO", "NOMBRE", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("USU_CORREO", "CORREO", Align: HorizontalAlign.Left);
        GridUsuarios.AddCheckboxColumn("CIU_RESPONSABLE", "RESPONSABLE");
        GridUsuarios.AddCheckboxColumn("CIU_HABILITADO", "HABILITADO");

        Tools.tools.RegisterPostBackScript(GridUsuarios);
    }
    protected void CargarGridVacia()
    {
        List<ClienteInstalacionUsuario> items = new List<ClienteInstalacionUsuario>();

        GridUsuarios.DataSource = items;
        GridUsuarios.DataBind();
    }

    protected void CargarGridUsuarios()
    {
        ClienteInstalacionUsuario item = new ClienteInstalacionUsuario();
        ClienteInstalacionusuarioController controller = new ClienteInstalacionusuarioController();
        item.ciu_instalacion = IdClienteInstalacion;

        GridUsuarios.DataSource = controller.GetClienteInstalacionUsuarios(item);
        GridUsuarios.DataBind();
    }

    protected void GridUsuarios_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("ciu_id").ToString();

                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&IdClienteInstalacion=" + IdClienteInstalacion));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnkEditar" + id;
                //Editar.Text = "&nbsp";
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirAsociarUsuario('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell ciu_id = DataItem["ciu_id"];

                ciu_id.Controls.Add(Editar);
            }
        }
    }

    protected void lnkAñadirUsuario_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdCliente=" + Cliente + "&IdClienteInstalacion=" + IdClienteInstalacion));
        Tools.tools.ClientExecute("abrirAsociarUsuario('" + query + "')");
    }

    protected void lnkEliminarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.");
            }
            else
            {
                Respuesta respuesta = new Respuesta();
                ClienteInstalacionUsuario clienteInstalacionUsuario = new ClienteInstalacionUsuario();
                ClienteInstalacionusuarioController clienteInstalacionUsuarioController = new ClienteInstalacionusuarioController();

                foreach (string item in GridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["ciu_id"].ToString());

                    clienteInstalacionUsuario.ciu_id = id;

                    respuesta = clienteInstalacionUsuarioController.DeleteClienteInstalacionUsuario(clienteInstalacionUsuario);
                }
                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok", false);
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message);
        }
    }


}