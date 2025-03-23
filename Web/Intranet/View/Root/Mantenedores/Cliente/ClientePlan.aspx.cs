using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Cliente_ClientePlan : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected string tipo_dato
    {
        get { return Convert.ToString(ViewState["tipo_dato"]); }
        set { ViewState.Add("tipo_dato", value); }
    }

    protected int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    private ClienteController controller = new ClienteController();
    private PlanProductoController planProductoController = new PlanProductoController();
    private BolsaController bolsaController = new BolsaController();
    private ProductoController productoController = new ProductoController();
    
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

                    case "tipo_dato":
                        tipo_dato = array[1].ToString();
                        break;
                }
            }
            
            Grid.AddColumn("producto_nombre", "PRODUCTO", Align: HorizontalAlign.Left);

            
            GridBolsa.AddColumn("producto_nombre", "PRODUCTO", Align: HorizontalAlign.Left);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaDatos();

        if (Id == 0)
        {
            lblTitulo.Text = "Nuevo";

            if (!string.IsNullOrEmpty(HDFopcion.Value))
            {
                if(HDFopcion.Value == "1")
                {
                    pnlPlan.Style.Add("display", "block");
                    pnlBolsas.Style.Add("display", "none");
                    pnlProductos.Style.Add("display", "none");
                }

                if (HDFopcion.Value == "2")
                {
                    pnlPlan.Style.Add("display", "none");
                    pnlBolsas.Style.Add("display", "block");
                    pnlProductos.Style.Add("display", "none");
                }

                if (HDFopcion.Value == "3")
                {
                    pnlPlan.Style.Add("display", "none");
                    pnlBolsas.Style.Add("display", "none");
                    pnlProductos.Style.Add("display", "block");
                }
            }
            else
            {
                pnlPlan.Style.Add("display", "block");
                pnlBolsas.Style.Add("display", "none");
                pnlProductos.Style.Add("display", "none");
            }
        }
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
                    case "cboTipoPlan":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione", ""));
                        ctrl.DataSource = controller.GetTiposPlanes();
                        ctrl.DataValueField = "tpl_id";
                        ctrl.DataTextField = "tpl_nombre";
                        ctrl.DataBind();

                        break;

                    case "cboBolsas":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione", ""));
                        ctrl.DataSource = controller.GetBolsas();
                        ctrl.DataValueField = "bls_id";
                        ctrl.DataTextField = "bls_nombre";
                        ctrl.DataBind();

                        break;

                    case "cboProducto":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione", ""));
                        ctrl.DataSource = controller.GetProductosAdicionales();
                        ctrl.DataValueField = "pra_id";
                        ctrl.DataTextField = "pra_nombre";
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            rdoPlan.Visible = false;
            rdoBolsa.Visible = false;
            rdoProducto.Visible = false;

            if (tipo_dato == "PLAN")
            {
                lblTitulo.Text = "Planes";

                ClientePlan item = new ClientePlan();
                item.clp_id = Id;
                item = controller.GetClientePlan(item);

                cboTipoPlan.SelectedValue = item.clp_tipo_plan.ToString();
                txtDesde.Value = item.clp_fecha_desde;
                txtHasta.Value = item.clp_fecha_hasta;
                txtCantidad.Value = item.plan_informes;
                TxtCantAdministradores.Value = item.clp_administradores;
                ChkAdmIlimitados.Checked = item.clp_administradores_ilimitados;
                TxtValorPlan.Value = item.clp_valor_plan;

                CargarGridPlan();
                pnlPlan.Style.Add("display", "block");
                pnlBolsas.Style.Add("display", "none");
                pnlProductos.Style.Add("display", "none");
            }

            if (tipo_dato == "BOLSA")
            {
                lblTitulo.Text = "Bolsas";

                ClientePlanBolsa bolsa = new ClientePlanBolsa();
                bolsa.cpb_id = Id;
                bolsa = controller.GetClientePlanBolsa(bolsa);

                cboBolsas.SelectedValue = bolsa.cpb_id_bolsa.ToString();
                txtDesdeBolsa.Value = bolsa.cpb_fecha_desde;
                txtHastaBolsa.Value = bolsa.cpb_fecha_hasta;
                txtCantidadBolsas.Value = bolsa.cpb_cantidad;
                txtAdministradoresBolsas.Value = bolsa.cpb_administradores;
                txtValorBolsa.Value = bolsa.cpb_valor_bolsa;

                CargarGridBolsa();
                pnlPlan.Style.Add("display", "none");
                pnlBolsas.Style.Add("display", "block");
                pnlProductos.Style.Add("display", "none");
            }

            if (tipo_dato == "ADICIONAL")
            {
                lblTitulo.Text = "Productos Adicionales";

                ClienteProductoAdicional producto = new ClienteProductoAdicional();
                producto.cpa_id = Id;
                producto = controller.GetClienteProductoAdicional(producto);

                cboProducto.SelectedValue = producto.cpa_producto_adicional.ToString();
                txtDesdeProducto.Value = producto.cpa_fecha_desde;
                txtHastaProducto.Value = producto.cpa_fecha_hasta;
                txtValorProducto.Value = producto.cpa_valor_producto_adicional;

                pnlPlan.Style.Add("display", "none");
                pnlBolsas.Style.Add("display", "none");
                pnlProductos.Style.Add("display", "block");
            }
        }
    }

    protected void CargarGridBolsa()
    {
        BolsaProducto bolsaProducto = new BolsaProducto();
        bolsaProducto.blp_bolsa = int.Parse(cboBolsas.SelectedValue);

        GridBolsa.DataSource = controller.GetBolsaProductos(bolsaProducto);
        GridBolsa.DataBind();
    }

    protected void CargarGridPlan()
    {
        PlanProducto planProducto = new PlanProducto();
        planProducto.plp_tipo_plan = int.Parse(cboTipoPlan.SelectedValue);

        Grid.DataSource = controller.GetPlanProductos(planProducto);
        Grid.DataBind();
        
    }

    //Guardar Plan
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            ClientePlan item = new ClientePlan();
            item.clp_id = Id;
            item.clp_cliente = Cliente;
            item.clp_fecha_desde = txtDesde.Value.Value;
            item.clp_fecha_hasta = txtHasta.Value.Value;
            item.clp_cantidad = Convert.ToInt32(txtCantidad.Value);
            item.clp_administradores = Convert.ToInt32(TxtCantAdministradores.Value);
            item.clp_administradores_ilimitados = ChkAdmIlimitados.Checked;
            item.clp_tipo_plan = int.Parse(cboTipoPlan.SelectedValue);

            item.clp_valor_plan = Convert.ToInt32(TxtValorPlan.Value);

            if (Id > 0)
                respuesta = controller.UpdateClientePlan(item);
            else
                respuesta = controller.InsertClientePlan(item);

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

    //Guardar Bolsa
    protected void btnGuardarBolsa_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            ClientePlanBolsa item = new ClientePlanBolsa();
            item.cpb_id = Id;
            item.cpb_cliente = Cliente;
            item.cpb_fecha_desde = txtDesdeBolsa.Value.Value;
            item.cpb_fecha_hasta = txtHastaBolsa.Value.Value;
            item.cpb_id_bolsa = int.Parse(cboBolsas.SelectedValue);
            item.cpb_cantidad = Convert.ToInt32(txtCantidadBolsas.Value);
            item.cpb_administradores = Convert.ToInt32(txtAdministradoresBolsas.Value);
            item.cpb_valor_bolsa = Convert.ToInt32(txtValorBolsa.Value);

            if (Id > 0)
                respuesta = controller.UpdateClientePlanBolsa(item);
            else
                respuesta = controller.InsertClientePlanBolsa(item);

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

    protected void btnGuardarProducto_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            ClienteProductoAdicional item = new ClienteProductoAdicional();
            item.cpa_id = Id;
            item.cpa_cliente = Cliente;
            item.cpa_fecha_desde = txtDesdeProducto.Value.Value;
            item.cpa_fecha_hasta = txtHastaProducto.Value.Value;
            item.cpa_producto_adicional = int.Parse(cboProducto.SelectedValue);

            item.cpa_valor_producto_adicional = Convert.ToInt32(txtValorProducto.Value);

            if (Id > 0)
                respuesta = controller.UpdateClienteProductoAdicional(item);
            else
                respuesta = controller.InsertClienteProductoAdicional(item);

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

    protected void cboTipoPlan_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        List<TipoPlan> listaPlan = new List<TipoPlan>();
        listaPlan = controller.GetTiposPlanes();

        if (!string.IsNullOrEmpty(cboTipoPlan.SelectedValue))
        {
            TipoPlan plan = listaPlan.Where(x => x.tpl_id == int.Parse(cboTipoPlan.SelectedValue)).FirstOrDefault();
            TxtValorPlan.Value = plan.tpl_valor_plan;
            txtCantidad.Value = plan.tpl_cantidad_informes;
            TxtCantAdministradores.Value = plan.tpl_cantidad_administradores;
            ChkAdmIlimitados.Checked = plan.tpl_administradores_ilimitados;
            CargarGridPlan();
        }
    }

    protected void cboBolsas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        List<Bolsa> listaBolsa = new List<Bolsa>();
        listaBolsa = controller.GetBolsas();

        if (!string.IsNullOrEmpty(cboBolsas.SelectedValue))
        {
            Bolsa bolsa = listaBolsa.Where(x => x.bls_id == int.Parse(cboBolsas.SelectedValue)).FirstOrDefault();

            txtCantidadBolsas.Value = bolsa.bls_cantidad;
            txtAdministradoresBolsas.Value = bolsa.bls_cantidad_administradores;
            txtValorBolsa.Value = bolsa.bls_valor_plan;
            CargarGridBolsa();
        }
    }

    protected void cboProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        List<ProductoAdicional> listaproducto = new List<ProductoAdicional>();
        listaproducto = controller.GetProductosAdicionales();

        if (!string.IsNullOrEmpty(cboProducto.SelectedValue))
        {
            ProductoAdicional productoAdicional = listaproducto.Where(x => x.pra_id == int.Parse(cboProducto.SelectedValue)).FirstOrDefault();

            txtValorProducto.Value = productoAdicional.pra_valor_producto;
        }
    }
}