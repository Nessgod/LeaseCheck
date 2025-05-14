using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class View_Root_Mantenedores_MesaAyuda_MesaAyudaDetalle : System.Web.UI.Page
{
    protected MesaAyudaController mesaAyudaController = new MesaAyudaController();

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
            lblNumeroConsulta.InnerText = "N° Consulta: " + mesaAyuda.mes_id.ToString(); 
            txtMensaje.InnerText = "Consulta: " + mesaAyuda.mes_mensaje.ToString();
            txtNombreMesaAyuda.InnerText = "Nombre: " + mesaAyuda.mes_nombre.ToString();
            lblModulo.InnerText = "Modulo: " + mesaAyuda.NOMBRE_MODULO.ToString();
            lblCreador.InnerText = "Creador: " + mesaAyuda.NOMBRE_CREADOR.ToString();
            lblFechaCreacion.InnerText = "Fecha Creacion: " + mesaAyuda.mes_fecha_creacion.ToString("dd/MM/yyyy HH:mm");
            lblCliente.InnerText = "Cliente: " + mesaAyuda.NOMBRE_CLIENTE.ToString();
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
            if (mesaAyuda.mes_estado == 2)
            {
                areaRespuesta.Visible = false;
                lblPrincipalObservacion.Visible = false;
                txtObservacionCierre.Visible = true;
                txtObservacionCierre.Enabled = false;
                btnCerrarTicket.Visible = false;
                txtObservacionCierre.Text = "Observacion Cierre: " + mesaAyuda.mes_observacion_cierre.ToString();
                lblFechaCierre.InnerText = "Fecha Cierre: " + mesaAyuda.mes_fecha_cierre.ToString("dd/MM/yyyy HH:mm");
                lblUsuarioCierre.InnerText = "Usuario Cierre: " + mesaAyuda.RESPONSABLE_CIERRE.ToString();
                CerrarTicket.Visible = true;

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
            chatHtml.Append("<div class='no-responses'>No hay respuestas registradas</div>");
        }
        else
        {
            int usuarioActual = int.Parse(LeaseCheck.Session.UsuarioId());
            chatHtml.Append("<div class='chat-container' id='chatContainer'>");

            DateTime hoy = DateTime.Today;
            DateTime ayer = hoy.AddDays(-1);
            DateTime? fechaGrupoActual = null;

            // Identificar el último mensaje
            var ultimoMensaje = respuestas.LastOrDefault();

            foreach (var respuesta in respuestas)
            {
                DateTime fecha = respuesta.mer_fecha_creacion.Date;

                // Mostrar encabezado de grupo de fecha
                if (fechaGrupoActual == null || fechaGrupoActual.Value != fecha)
                {
                    fechaGrupoActual = fecha;
                    string encabezadoFecha = fecha == hoy ? "Hoy"
                                                    : fecha == ayer ? "Ayer"
                                                    : fecha.ToString("dd MMMM yyyy");

                    chatHtml.Append("<div class='chat-date-group'>").Append(encabezadoFecha).Append("</div>");
                }

                // Determinar autor
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

                // Avatar
                string fotoHtml = respuesta.FOTO_USUARIO != null && respuesta.FOTO_USUARIO.Length > 0
                    ? "<img class='avatar' src='data:image/jpeg;base64," + Convert.ToBase64String(respuesta.FOTO_USUARIO) + "' alt='avatar' />"
                    : "<div class='avatar avatar-emoji'>&#128100;</div>";

                // Clases según tipo de mensaje
                string cssClass = respuesta.mer_tipo_respuesta == 2 ? "message system" : "message client";
                string alignment = respuesta.mer_tipo_respuesta == 2 ? "left" : "right";

                // Hora y mensaje
                string hora = respuesta.mer_fecha_creacion.ToString("hh:mm tt");
                string mensaje = respuesta.mer_respuesta
                    .Replace("'", "\\'")
                    .Replace("\n", "<br/>")
                    .Replace("\"", "\\\"");

                // Identificador de mensaje nuevo/viejo
                string indicador;
                if (respuesta == ultimoMensaje)
                {
                    if (respuesta.mer_tipo_respuesta == 1) // Cliente
                    {
                        indicador = "<span class='new-message client-new'>🆕 Nuevo mensaje</span>";
                    }
                    else // Sistema
                    {
                        indicador = "<span class='new-message system-new'>🆕 Nuevo mensaje</span>";
                    }
                }
                else
                {
                    indicador = "<span class='old-message'>🕓 Mensaje anterior</span>";
                }

                // Estructura del mensaje
                chatHtml.Append("<div class='").Append(cssClass).Append(" ").Append(alignment).Append("'>");

                chatHtml.Append("<div class='avatar-container'>").Append(fotoHtml).Append("</div>");

                chatHtml.Append("<div class='chat-bubble'>");

                chatHtml.Append("<div class='meta-info'>");
                chatHtml.Append("<span class='author'>").Append(autor).Append("</span>");
                chatHtml.Append("<span class='date'>").Append(hora).Append("</span>");
                chatHtml.Append(indicador);
                chatHtml.Append("</div>");

                chatHtml.Append("<div class='chat-text'>").Append(mensaje).Append("</div>");

                chatHtml.Append("</div>"); // chat-bubble
                chatHtml.Append("</div>"); // message
            }

            chatHtml.Append("</div>"); // chat-container

            // JavaScript para hacer scroll hacia el último mensaje
            chatHtml.Append("<script>");
            chatHtml.Append("Sys.Application.add_load(function() {");
            chatHtml.Append("var chatContainer = document.getElementById('chatContainer');");
            chatHtml.Append("chatContainer.scrollTop = chatContainer.scrollHeight;");
            chatHtml.Append("});");
            chatHtml.Append("</script>");
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
            mesaAyudaRespuesta.mer_tipo_respuesta = 1; // 1 = Sistema, 2 = Cliente

            respuesta = controller.InsertMesaAyudaRespuestas(mesaAyudaRespuesta);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
                udPanel.Update();
                CargarHistorialRespuestas();
                Limpiar();

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

    protected void Limpiar()
    {
        txtRespuesta.Text = "";
    }

    protected void btnCerrarTicket_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            MesaAyuda mesaAyuda = new MesaAyuda();
            mesaAyuda.mes_id = Id;
            mesaAyuda.mes_observacion_cierre = txtObservacionCierre.Text.ToString();


            respuesta = controller.UpdateMesaAyuda(mesaAyuda);

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
         
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