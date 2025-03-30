using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_PlanesTarifarios : System.Web.UI.UserControl
{
  
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }
    public int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public bool TieneRut { get; set; }

    ClienteController controller = new ClienteController();
    private ClienteDocumentoController controllerClienteDocumento = new ClienteDocumentoController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;
                }
            }

            Grid.AddSelectColumn();
            Grid.AddColumn("clp_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("clp_id", "ID", "", HorizontalAlign.Left);
            Grid.AddColumn("tipo_dato", "TIPO", "", HorizontalAlign.Left);
            Grid.AddColumn("plan_nombre", "NOMBRE", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_fecha_desde", "DESDE", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("clp_fecha_hasta", "HASTA", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("estado", "ESTADO", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_administradores", "ADMINISTRADORES", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_cantidad", "CANTIDAD", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_valor_plan", "VALOR", "", HorizontalAlign.Left, DataFormat: "{0:N0}");

            Tools.tools.RegisterPostBackScript(Grid);
        }
    }

    public void LoadControls(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaDatos();
            CalcularTotalValor();
            udPanel.Update();
        }
        CargaDatos();
        CalcularTotalValor();
        udPanel.Update();

    }
    protected void CargaDatos()
    {
        CargaGrid();
    }

    protected void CargaGrid()
    {
        ClientePlan item = new ClientePlan();
        item.clp_cliente = IdCliente;
        List<ClientePlan> listado = new List<ClientePlan>();
        listado = controller.GetClientePlanes(item);

        Grid.DataSource = listado;
        Grid.DataBind();
        CalcularTotalValor();

    }
    private void CalcularTotalValor()
    {
        decimal total = 0;

        foreach (GridDataItem item in Grid.MasterTableView.Items)
        {
            var estado = item["estado"].Text;
            var valorPlan = item["clp_valor_plan"].Text;
            if (estado == "Activo")
            {
                decimal valor = 0;
                if (!string.IsNullOrEmpty(valorPlan))
                {
                    decimal.TryParse(valorPlan, out valor);
                }

                total += valor;
            }
        }

        lblValorTotal.Text = total.ToString("N0");
    }



    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("clp_id").ToString();
                string tipo_dato = item.GetDataKeyValue("tipo_dato").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&tipo_dato=" + tipo_dato));

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
                    int id = Int32.Parse(value["clp_id"].ToString());

                    ClientePlan plan = new ClientePlan();
                    plan.clp_id = id;

                    respuesta = controller.DeleteClientePlan(plan);
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

    protected void lnkNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + IdCliente));
        Tools.tools.ClientExecute("abrirPlan('" + query + "')");
    }


}