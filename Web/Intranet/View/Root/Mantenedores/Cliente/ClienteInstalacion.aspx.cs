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
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }

    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }


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
                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;

                    case "IdClienteInstalacion":
                        IdClienteInstalacion = Int32.Parse(array[1].ToString());
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
        cargar();
   
    }

    protected void cargar()
    {
        if (IdClienteInstalacion == 0)
        {
            IdClienteInstalacion = wucIdentidadInstalacion.IdClienteInstalacion;
        }

        wucIdentidadInstalacion.Cliente = Cliente;
        wucIdentidadInstalacion.IdClienteInstalacion = IdClienteInstalacion;
        wucIdentidadInstalacion.ReadOnly = ReadOnly;


        wucUsuarioInstalacion.Cliente = Cliente;
        wucUsuarioInstalacion.IdClienteInstalacion = IdClienteInstalacion;
        wucUsuarioInstalacion.ReadOnly = ReadOnly;

    }

    protected void bloqueo()
    {
        if (IdClienteInstalacion == 0)
        {
            ragTab.Tabs[1].Visible = false;
            ragTab.Tabs[2].Visible = false;
     
        }
        else
        {
            ragTab.Tabs[1].Visible = true;
            ragTab.Tabs[2].Visible = true;
   
        }
    }

}