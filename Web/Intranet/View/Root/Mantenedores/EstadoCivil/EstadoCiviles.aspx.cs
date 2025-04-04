﻿using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Mantenedores_EstadoCivil_EstadoCiviles : System.Web.UI.Page
{
    EstadoCivilesController estadoCivilesController = new EstadoCivilesController();
    EstadoCivil estadoCivil = new EstadoCivil();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_estado_civi.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("eci_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("eci_id", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("eci_nombre", "ESTADO CIVIL", "83%", HorizontalAlign.Left);
            Grid.AddCheckboxColumn("eci_habilitado", "HABILITADO", "40%");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();
        LinkButton lnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkDescargarPlantilla");
        //script que genera el excel 
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescargarPlantilla);
    }

    protected void CargaGrid()
    {
        Grid.DataSource = estadoCivilesController.GetEstadoCiviles(estadoCivil);
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("eci_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["eci_id"];
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
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["eci_id"].ToString());

                    EstadoCivil estadoCivil2 = new EstadoCivil();
                    estadoCivil2.eci_id = id;

                    respuesta = estadoCivilesController.DeleteEstadoCivil(estadoCivil2);
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
        estadoCivilesController.InformeEstadoCivil(estadoCivil);
    }
}