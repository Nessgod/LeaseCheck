using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Mantenedores_Bolsas_Bolsa : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    private BolsaController controller = new BolsaController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_bolsa.Ver;
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
        CargarBolsa();
    }

    protected void CargarBolsa()
    {
        if (Id > 0)
        {
            Bolsa bolsa = new Bolsa();
            bolsa.bls_id = Id;
            bolsa = controller.GetBolsa(bolsa);

            txtNombre.Text = bolsa.bls_nombre;
            TxtInformes.Value = bolsa.bls_cantidad;
            TxtCantAdministradores.Value = bolsa.bls_cantidad_administradores;
            ChkAdmIlimitados.Checked = bolsa.bls_administradores_ilimitados;
            TxtValorPlan.Value = bolsa.bls_valor_plan;

            if (bolsa.bls_habilitado)
                rdbSi.Checked = true;
            else
                rdbNo.Checked = true;

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
        BolsaProducto bolsaProducto = new BolsaProducto();
        bolsaProducto.blp_bolsa = Id;

        Grid.DataSource = controller.GetListado(bolsaProducto);
        Grid.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            Bolsa bolsa = new Bolsa();
            bolsa.bls_id = Id;
            bolsa.bls_nombre = txtNombre.Text;
            bolsa.bls_cantidad = int.Parse(TxtInformes.Text);
            bolsa.bls_cantidad_administradores = int.Parse(TxtCantAdministradores.Text);
            bolsa.bls_administradores_ilimitados = ChkAdmIlimitados.Checked;
            bolsa.bls_valor_plan = int.Parse(TxtValorPlan.Text);
            bolsa.bls_habilitado = rdbSi.Checked;

            if (Id > 0)
            {
                respuesta = controller.UpdateBosas(bolsa);
            }
            else
            {
                respuesta = controller.InsertBolsa(bolsa);
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok",true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");

            if (respuesta.codigo > 0)
            {
                Id = respuesta.codigo;

                List<BolsaProducto> bolsaProductos = new List<BolsaProducto>();

                Grid.DataSource = bolsaProductos;
                Grid.DataBind();

                pnlProducto.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void lnlNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdBolsa=" + Id));

        Tools.tools.ClientExecute("abrirBolsa('" + query + "')");
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
                    int id = Int32.Parse(value["blp_id"].ToString());

                    BolsaProducto bolsaProducto = new BolsaProducto();
                    bolsaProducto.blp_id = id;

                    respuesta = controller.DeleteBolsaProducto(bolsaProducto);
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