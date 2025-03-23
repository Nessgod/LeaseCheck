using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Mantenedores_TipoPlan_Tipoplan : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    private TiposdePlanesController controller = new TiposdePlanesController();
    private PlanProductoController planProductoController = new PlanProductoController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_tipo_plan.Ver;
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
            Grid.AddColumn("producto_nombre", "PRODUCTO", Align: HorizontalAlign.Left);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaDatos();
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            TipoPlan item = new TipoPlan();
            item.tpl_id = Id;
            
            item = controller.GetTipoplan(item);

            if (item != null)
            {
                item.tpl_id = Id;
                txtNombre.Text = item.tpl_nombre;
                txtCantInformes.Value = item.tpl_cantidad_informes;
                TxtCantAdministradores.Value = item.tpl_cantidad_administradores;
                ChkAdmIlimitados.Checked = item.tpl_administradores_ilimitados;
                TxtValorPlan.Value = item.tpl_valor_plan;

                if (item.tpl_habilitado)
                    rdbSi.Checked = true;
                else
                    rdbNo.Checked = true;
            }

            CargarGrid();
            pnlProducto.Visible = true;
        }
        else
        {
            pnlProducto.Visible = false;
        }
    }

    protected void CargarGrid()
    {
        PlanProducto planProducto = new PlanProducto();
        planProducto.plp_tipo_plan = Id;

        Grid.DataSource = planProductoController.GetListado(planProducto);
        Grid.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            TipoPlan item = new TipoPlan();
            item.tpl_id = Id;
            item.tpl_nombre = txtNombre.Text;
            item.tpl_cantidad_informes = int.Parse(txtCantInformes.Text);
            item.tpl_cantidad_administradores = int.Parse(TxtCantAdministradores.Text);
            item.tpl_administradores_ilimitados = ChkAdmIlimitados.Checked;
            item.tpl_valor_plan = int.Parse(TxtValorPlan.Text);
            item.tpl_habilitado = rdbSi.Checked;

            if (Id > 0)
            {
                respuesta = controller.UpdateTipoPlan(item);
            }
            else
            {
                respuesta = controller.InsertTipoPlan(item);
                Id = respuesta.codigo;
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok",true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void lnlNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdPlan=" + Id));

        Tools.tools.ClientExecute("abrirProducto('" + query + "')");
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
                    int id = Int32.Parse(value["plp_id"].ToString());

                    PlanProducto planProducto = new PlanProducto();
                    planProducto.plp_id = id;

                    respuesta = planProductoController.DeletePlanProducto(planProducto);
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