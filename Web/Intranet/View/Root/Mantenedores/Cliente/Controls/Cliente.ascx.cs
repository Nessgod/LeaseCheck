using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Root_Mantenedores_Cliente_Controls_Cliente : System.Web.UI.UserControl
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

    public string URLVolverCliente
    {
        get { return Convert.ToString(ViewState["URLVolverCliente"]); }
        set { ViewState.Add("URLVolverCliente", value); }
    }
    private ClienteController controller = new ClienteController();

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
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;

                    case "ReadOnly":
                        ReadOnly = bool.Parse(array[1].ToString());
                        break;

                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargarDatos();

    }

    protected void CargarDatos()
    {
        if (IdCliente == 0)
        {
            ragTab.Tabs[1].Visible = false;
            ragTab.Tabs[2].Visible = false;
            ragTab.Tabs[3].Visible = false;
            ragTab.Tabs[4].Visible = false;
            ragTab.Tabs[5].Visible = false;

            IdCliente = wucIdentidad.IdCliente;
        }

        wucIdentidad.IdCliente = IdCliente;
        wucIdentidad.ReadOnly = ReadOnly;

        wucUsuarios.IdCliente = IdCliente;
        wucUsuarios.ReadOnly = ReadOnly;


        wucDocumentos.IdCliente = IdCliente;
        wucDocumentos.ReadOnly = ReadOnly;



        wucPlanesTarifarios.IdCliente = IdCliente;
        wucPlanesTarifarios.ReadOnly = ReadOnly;



        wucInstalaciones.IdCliente = IdCliente;
        wucInstalaciones.ReadOnly = ReadOnly;

    }

}