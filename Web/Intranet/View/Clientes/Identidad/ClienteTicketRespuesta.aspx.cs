using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class View_Clientes_Identidad_ClienteTicketRespuesta : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }
 

    MesaAyudaController controller = new MesaAyudaController();
    MesaAyuda mesaAyuda = new MesaAyuda();

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

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

        }


    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarConsulta();
            CargarHistorialRespuestas();
        }
        CargarConsulta();
        CargarHistorialRespuestas();
    }

    protected void CargarConsulta()
    {
        mesaAyuda.mes_id = Id;
        mesaAyuda = controller.GetMesaAyudaDetalle(mesaAyuda);

        if (mesaAyuda != null)
        {
            txtMensaje.InnerText = "Consulta: " + mesaAyuda.mes_mensaje.ToString();
            txtNombreMesaAyuda.InnerText = "Nombre: " + mesaAyuda.mes_nombre.ToString();
            lblModulo.InnerText = "Modulo: " + mesaAyuda.NOMBRE_MODULO.ToString();
            lblCreador.InnerText = "Creador: " + mesaAyuda.NOMBRE_CREADOR.ToString();
            lblFechaCreacion.InnerText = "Fecha Creacion: " + mesaAyuda.mes_fecha_creacion.ToString("dd/MM/yyyy HH:mm");
            if (mesaAyuda.mes_otro_modulo != null)
            {
                txtOtroModulo.Visible = true;
                txtOtroModulo.InnerText = "Otro: " + mesaAyuda.mes_otro_modulo;
            }
            else
            {
                txtOtroModulo.Visible = false;
                txtOtroModulo.InnerText = string.IsNullOrEmpty(mesaAyuda.mes_otro_modulo) ? "No especifico" : mesaAyuda.mes_otro_modulo;
            }
        }


    }
    private void CargarHistorialRespuestas()
    {
        MesaAyudaRespuesta mesaAyudaRespuesta = new MesaAyudaRespuesta();
        mesaAyudaRespuesta.mer_id_mesa_ayuda = Id;

        List<MesaAyudaRespuesta> respuestas = controller.GetMesaAyudaRespuestas(mesaAyudaRespuesta);

        StringBuilder chatHtml = new StringBuilder();

        if (respuestas == null || respuestas.Count == 0)
        {
            chatHtml.Append("<div class='card mt-3'><div class='card-body'>");
            chatHtml.Append("<h5 class='card-title text-center text-muted'>No hay respuestas registradas</h5>");
            chatHtml.Append("</div></div>");
        }
        else
        {
            int usuarioActual = Convert.ToInt32(Session["UsuarioId"]);

            chatHtml.Append("<fieldset class='border p-3 rounded mb-4'>");
            chatHtml.Append("<legend class='w-auto px-2'>Historial de Respuestas</legend>");
            chatHtml.Append("<div class='chat-container'>");

            foreach (var respuesta in respuestas)
            {
                string cssClass = respuesta.mer_tipo_respuesta == 1 ? "system-message" : "client-message";
                string autor;

                if (respuesta.QUIEN_RESPONDIO == "Sistema")
                {
                    autor = "Soporte";
                }
                else if (respuesta.QUIEN_RESPONDIO == "Cliente" && respuesta.mer_usuario_creacion == usuarioActual)
                {
                    autor = "Yo";
                }
                else
                {
                    autor = respuesta.NOMBRE_EJECUTOR;
                }

                string fecha = respuesta.mer_fecha_creacion.ToString("dd/MM/yyyy hh:mm tt");
                string mensaje = respuesta.mer_respuesta
                    .Replace("'", "\\'")
                    .Replace("\n", "<br/>")
                    .Replace("\"", "\\\"");

                chatHtml.Append("<div class='chat-message ").Append(cssClass).Append("'>");
                chatHtml.Append("<div class='message-header'>");
                chatHtml.Append("<span class='message-author'>").Append(autor).Append("</span>");
                chatHtml.Append("<span class='message-date'>").Append(fecha).Append("</span>");
                chatHtml.Append("</div>"); // message-header
                chatHtml.Append("<div class='message-body'>").Append(mensaje).Append("</div>");
                chatHtml.Append("</div>"); // chat-message
            }

            chatHtml.Append("</div></fieldset>");
        }

        chatTickets.Text = chatHtml.ToString();
    }


    protected void btnEnviarRespuesta_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            MesaAyudaRespuesta mesaAyudaRespuesta = new MesaAyudaRespuesta();
            mesaAyudaRespuesta.mer_id_mesa_ayuda = Id;
            mesaAyudaRespuesta.mer_respuesta = txtRespuesta.Text.ToString();
            mesaAyudaRespuesta.mer_tipo_respuesta = 2; // 1 = Sistema, 2 = Cliente

            respuesta = controller.InsertMesaAyudaRespuestas(mesaAyudaRespuesta);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
                udPanel.Update();
                CargarHistorialRespuestas();

            }
            else
            {
                Tools.tools.ClientAlert(respuesta.detalle, "error");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }
}