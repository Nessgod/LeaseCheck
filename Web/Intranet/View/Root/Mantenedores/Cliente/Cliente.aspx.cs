using LeaseCheck.Root.Model;
using System;

public partial class View_Root_Mantenedores_Cliente_Cliente : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

 
    }

}