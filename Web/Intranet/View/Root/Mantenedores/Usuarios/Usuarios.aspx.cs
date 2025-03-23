using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Root_Mantenedores_Usuarios_Usuarios : System.Web.UI.Page
{
    UsuarioController usuarioController = new UsuarioController();
    Usuarios usuario = new Usuarios();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_usuario.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("USU_ID", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("USU_ID", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign:HorizontalAlign.Center);
            Grid.AddColumn("USU_LOGIN", "LOGIN", "13%");
            Grid.AddColumn("NOMBRECOMPLETO", "NOMBRE", "25%");
            Grid.AddColumn("USU_CORREO", "CORREO", "25%");
            Grid.AddColumn("USU_FONO", "FONO", "10%");
            Grid.AddColumn("ES_CLIENTE", "TIPO", "10%");
            Grid.AddColumn("cliente_nombre", "CLIENTE", "10%");
            Grid.AddCheckboxColumn("USU_HABILITADO", "HABILITADO", "10%");
        }

        Tools.tools.RegisterPostBackScript(Grid);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargarGrid();
        udPanel.Update();
        LinkButton LnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("LnkDescargarPlantilla");
        //script que genera el excel 
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(LnkDescargarPlantilla);
    }

    public void LoadControls(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cbo":

                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));

                        PaisesController paisesController = new PaisesController();
                        ctrl.EmptyMessage = "Todos";
                        ctrl.DataSource = paisesController.GetPaises(); ;
                        ctrl.DataTextField = "PAI_NOMBRE";
                        ctrl.DataValueField = "PAI_ID";
                        ctrl.DataBind();

                        break;

                    case "cboCliente":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));

                        ClienteController clienteController = new ClienteController();
                        ctrl.EmptyMessage = "Todos";
                        ctrl.DataSource = clienteController.GetClientesUsuario();
                        ctrl.DataValueField = "cli_id";
                        ctrl.DataTextField = "cli_nombre";

                        ctrl.DataBind();

                        break;

                    case "cboPais":

                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));
                        ctrl.EmptyMessage = "Todos";
                        Paises filtro2 = new Paises();
                        UsuarioController usuariosController = new UsuarioController();
                        filtro2.pai_habilitado = true;

                        ctrl.DataSource = usuariosController.GetPaises(filtro2);
                        ctrl.DataValueField = "pai_id";
                        ctrl.DataTextField = "pai_nombre";

                        ctrl.DataBind();
                        break;
                }
            }
        }
    }

    protected void CargarGrid()
    {
        Usuarios usu = new Usuarios();
        RadComboBox2 cboTipo = (RadComboBox2)wucFiltro.FindControl("cboTipo");

        if (cboTipo.SelectedValue != "") {
            switch (cboTipo.SelectedValue) {
                case "1":
                    usu.es_cliente = true;
                    break;

                case "0":
                    usu.es_cliente = false;
                    break;
            }
        
        }

        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");

        if (wucFiltro.Filtro() != null) usu.filtro = wucFiltro.Filtro();
        if (cboCliente.SelectedValue != "") usu.filtro_Cliente = cboCliente.SelectedValue;
        if (cboPais.SelectedValue != "") usu.filtro_Pais = cboPais.SelectedValue;


        Grid.DataSource = usuarioController.GetUsuarios(usu);
        Grid.DataBind();
    }
   
    protected void Grid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("usu_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                //Creo el link
                HyperLink Editar = new HyperLink();
                Editar.ID = "lnkEdidtar" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                //Asigno el Link a la celda
                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell USU_ID = DataItem["usu_id"];

                USU_ID.Controls.Add(Editar);

                TableCell ES_CLIENTE = DataItem["ES_CLIENTE"];

                if (ES_CLIENTE.Text == "True")
                    ES_CLIENTE.Text = "Cliente";
                else
                    ES_CLIENTE.Text = "Usuario";

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
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = usuarioController.DeleteUsuario(usuario);
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

    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        Usuarios usuario = new Usuarios();

        RadComboBox2 cboTipo = (RadComboBox2)wucFiltro.FindControl("cboTipo");

        if (cboTipo.SelectedValue != "")
        {
            switch (cboTipo.SelectedValue)
            {
                case "1":
                    usuario.es_cliente = true;
                    break;

                case "0":
                    usuario.es_cliente = false;
                    break;
            }

        }

        if (wucFiltro.Filtro() != null) usuario.filtro = wucFiltro.Filtro();

        if (wucFiltro.Filtro() != null) usuario.usu_login = wucFiltro.Filtro();

        Usuarios usu = new Usuarios();
        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        if (cboCliente.SelectedValue != "") usuario.filtro_Cliente = cboCliente.SelectedValue;

        usuarioController.InformeUsuarios(usuario);
    }

    protected void lnkReset_Click(object sender, EventArgs e)
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
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = usuarioController.ResetPassword(usuario);
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