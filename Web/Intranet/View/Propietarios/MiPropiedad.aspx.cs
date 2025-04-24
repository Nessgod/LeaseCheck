using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Propietarios_MiPropiedad : System.Web.UI.Page
{
    private ClientePropiedadController controller = new ClientePropiedadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_usuarios.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
         
        }
    }



    protected void Page_PreRender(object sender, EventArgs e)
    {

        udPanel.Update();
    }


    protected void CargarDatos()
    {

    }

    
}