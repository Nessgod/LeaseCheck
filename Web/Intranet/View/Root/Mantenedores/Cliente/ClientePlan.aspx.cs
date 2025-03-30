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
                 
                }

                if (HDFopcion.Value == "2")
                {
                    pnlPlan.Style.Add("display", "none");
                
                }

                if (HDFopcion.Value == "3")
                {
                    pnlPlan.Style.Add("display", "none");
  
                }
            }
            else
            {
                pnlPlan.Style.Add("display", "block");

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

      
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            rdoPlan.Visible = false;
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

          
                pnlPlan.Style.Add("display", "block");
            }

        }
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
        }
    }


}