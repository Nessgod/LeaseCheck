using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Web.UI;


//ESTE CAMBIO LO HIZO DEL GIT DE BENJAMIN
public partial class View_Clientes_Identidad_ClientePlan : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    private ClienteController controller = new ClienteController();
    ClientePlan item = new ClientePlan();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_planes.Ver;
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
                }
            }
        }

         
  
    

}

protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaDatos();
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

            item.clp_id = Id;
            item = controller.GetClientePlan(item);

            cboTipoPlan.SelectedValue = item.clp_tipo_plan.ToString();
            txtDesde.Value = item.clp_fecha_desde;
            txtHasta.Value = item.clp_fecha_hasta;
            TxtValorPlan.Value = item.valor_plan;
            txtInstalacion.Value = item.plan_propiedad;
            txtDocumentos.Value = item.plan_documento;
            txtLead.Value = item.plan_lead;

            PlanProducto planProducto = new PlanProducto();
            PlanProductoController planProductoController = new PlanProductoController();
            planProducto.plp_tipo_plan = item.clp_tipo_plan;

            // Obtener la lista de productos asociados
            List<PlanProducto> listadoProductos = planProductoController.GetListadoProductos(planProducto);

            // Construir la tabla HTML para productos y documentos
            StringBuilder htmlTable = new StringBuilder();
            htmlTable.Append("<table class='minimalist-table'>");
            htmlTable.Append("<thead>");
            htmlTable.Append("<tr>");
            htmlTable.Append("<th>Producto</th>");
            htmlTable.Append("<th>Documentos Asociados</th>");
            htmlTable.Append("</tr>");
            htmlTable.Append("</thead>");
            htmlTable.Append("<tbody>");

            foreach (var producto in listadoProductos)
            {
                // Fila para el producto
                htmlTable.Append("<tr>");
                htmlTable.AppendFormat("<td>{0}</td>", producto.producto_nombre);

                // Obtener los documentos asociados al producto
                PlanProductoDocumento planProductoDocumento = new PlanProductoDocumento();
                planProductoDocumento.prd_producto = producto.plp_producto; // ID del producto actual
                List<PlanProductoDocumento> listadoDocumentos = planProductoController.GetListadoProductosDocumento(planProductoDocumento);

                // Crear una lista de documentos en la misma fila
                if (listadoDocumentos.Count > 0)
                {
                    htmlTable.Append("<td><ul>");
                    foreach (var documento in listadoDocumentos)
                    {
                        htmlTable.AppendFormat("<li>{0}</li>", documento.tdc_nombre);
                    }
                    htmlTable.Append("</ul></td>");
                }
                else
                {
                    htmlTable.Append("<td>No hay documentos asociados</td>");
                }

                htmlTable.Append("</tr>");
            }

            htmlTable.Append("</tbody>");
            htmlTable.Append("</table>");

            // Asignar la tabla al control correspondiente
            txtPlan.Text = htmlTable.ToString();
        }

        // Configurar los campos como solo lectura
        txtDesde.ReadOnly = true;
        txtHasta.ReadOnly = true;
        cboTipoPlan.ReadOnly = true;
        TxtValorPlan.ReadOnly = true;
        txtLead.ReadOnly = true;
        txtDocumentos.ReadOnly = true;
        txtInstalacion.ReadOnly = true;
    }


    protected void btnDescargarPDF_Click(object sender, EventArgs e)
    {
        try
        {
            ClientePlan item = new ClientePlan();
            item.clp_id = Id;
            item = controller.GetClientePlan(item);

            if (item == null)
            {
                Tools.tools.ClientAlert("No se encontraron datos para generar el PDF.", "error");
                return;
            }

            // Variables cargadas directamente desde item
            string tipoPlan = item.clp_tipo_plan.ToString();
            string desde = item.clp_fecha_desde.ToShortDateString();
            string hasta = item.clp_fecha_hasta.ToShortDateString();
            string valor = item.valor_plan.ToString();
            string propiedad = item.plan_propiedad.ToString();
            string documentos = item.plan_documento.ToString();
            string lead = item.plan_lead.ToString();
            string planHtml = txtPlan.Text;

            // Crear el contenido HTML utilizando concatenación de cadenas
            string contenidoHtml = "<html>";
            contenidoHtml += "<head>";
            contenidoHtml += "<style>";
            contenidoHtml += "body { font-family: Arial, sans-serif; margin: 20px; }";
            contenidoHtml += ".minimalist-table { width: 100%; border-collapse: collapse; }";
            contenidoHtml += ".minimalist-table th, .minimalist-table td { border: 1px solid #ddd; padding: 8px; }";
            contenidoHtml += ".minimalist-table th { background-color: #f2f2f2; text-align: left; }";
            contenidoHtml += "</style>";
            contenidoHtml += "</head>";
            contenidoHtml += "<body>";
            contenidoHtml += "<h1>PLAN DETALLE</h1>";
            contenidoHtml += "<p><strong>Fecha Desde:</strong> " + desde + "</p>";
            contenidoHtml += "<p><strong>Fecha Hasta:</strong> " + hasta + "</p>";
            contenidoHtml += "<p><strong>Tipo Plan:</strong> " + tipoPlan + "</p>";
            contenidoHtml += "<p><strong>Valor:</strong> " + valor + "</p>";
            contenidoHtml += "<p><strong>Cantidad Documentos:</strong> " + documentos + "</p>";
            contenidoHtml += "<p><strong>Cantidad Instalación:</strong> " + propiedad + "</p>";
            contenidoHtml += "<p><strong>Cantidad Lead:</strong> " + lead + "</p>";
            contenidoHtml += "<h2>Contenido del Plan</h2>";
            contenidoHtml += planHtml;
            contenidoHtml += "</body>";
            contenidoHtml += "</html>";

            // Generar el PDF utilizando PdfConverterBinario
            byte[] pdfBytes = LeaseCheck.ConvertHtml.PdfConverterBinario(contenidoHtml);

            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                Tools.tools.ClientAlert("Error al generar el PDF.", "error");
                return;
            }

            // Configurar la respuesta para abrir el PDF en una nueva ventana
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.BinaryWrite(pdfBytes);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert("Error al generar el PDF: " + ex.Message, "error");
        }
    }


}