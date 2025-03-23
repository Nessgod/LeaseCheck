using AccesoDatosCorreo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Telerik.Charting.Styles;
using Winnovative;

namespace AccesoDatosCorreo.Controller
{
    public class EnvioCorreoController
    {
        public Respuesta EnviarCorreo(EnvioCorreo correo)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                WsCorreo.Mail mail = new WsCorreo.Mail();

                //PREPARO LOS DESTINATARIOS PARA
                List<WsCorreo.Destinatarios> destinatariosPara = new List<WsCorreo.Destinatarios>();
                WsCorreo.Destinatarios destinatarioPara;
                string[] listaPara = correo.para.Split('|');
                for (int i = 0; i < listaPara.Length; i = i + 2)
                {
                    destinatarioPara = new WsCorreo.Destinatarios();
                    destinatarioPara.correo = listaPara[i];
                    destinatarioPara.nombre = listaPara[i + 1];

                    destinatariosPara.Add(destinatarioPara);
                }

                //PREPARO LOS DESTINATARIOS CC
                List<WsCorreo.Destinatarios> destinatariosCC = new List<WsCorreo.Destinatarios>();
                if (!string.IsNullOrEmpty(correo.cc))
                {
                    WsCorreo.Destinatarios destinatarioCC;
                    string[] listaCC = correo.cc.Split('|');
                    for (int i = 0; i < listaCC.Length; i = i + 2)
                    {
                        destinatarioCC = new WsCorreo.Destinatarios();
                        destinatarioCC.correo = listaCC[i];
                        destinatarioCC.nombre = listaCC[i + 1];

                        destinatariosCC.Add(destinatarioCC);
                    }
                    mail.cc = destinatariosCC.ToArray();
                }

                mail.para = destinatariosPara.ToArray();
                mail.asunto = correo.asunto;
                mail.body = correo.body;
                mail.calendario = correo.calendario;
                mail.aplicacion = correo.aplicacion.ToString(); ;
                mail.usuario = correo.usuario;

                WsCorreo.WebServiceCorreo Correo = new WsCorreo.WebServiceCorreo();
                Correo.Url = ConfigurationManager.AppSettings["WsCorreo"];
                WsCorreo.Respuesta respuestaWS = Correo.EnviarCorreo(mail);
                respuesta.error = respuestaWS.error;
                respuesta.detalle = respuestaWS.detalle;

                if (respuestaWS.codigo == -10)
                    respuesta.detalle = "No fue posible procesar la solicitud. Favor intentar mas tarde.";
            }
            catch (Exception ex)
            {
                respuesta.codigo = -1;
                respuesta.detalle = ex.Message;
                respuesta.error = true;
            }

            return respuesta;
        }

        public Respuesta EnviarCorreo(ContactoComercial contacto)
        {
            Respuesta respuesta = new Respuesta();

                try
                {
                    // Genero body correo
                    string Body = "";
                    string ruta = "";

                    ruta = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Comun\PlantillaCorreo\CorreoComercial.html";

                    Body = File.ReadAllText(ruta);
                    Body = Body.Replace("\r", "");
                    Body = Body.Replace("\n", "");
                    Body = Body.Replace("\t", "");
                    Body = Body.Replace("%NOMBRE%", contacto.nombre);
                    Body = Body.Replace("%EMPRESA%", contacto.empresa);
                    Body = Body.Replace("%CARGO%", contacto.cargo);
                    Body = Body.Replace("%TELEFONO%", contacto.telefono);
                    Body = Body.Replace("%CORREO%", contacto.correo);
                    Body = Body.Replace("%MENSAJE%", contacto.mensaje);

                    //Envio Correo
                    EnvioCorreoController envioCorreoController = new EnvioCorreoController();
                    EnvioCorreo envioCorreo = new EnvioCorreo();

                    //envioCorreo.para = "desarrollo.exproges@gmail.com" + "|" + "Soporte LeaseCheck";
                    envioCorreo.para = "comercial@LeaseCheck.cl" + "|" + "Soporte LeaseCheck";
                    envioCorreo.body = Body;
                    envioCorreo.aplicacion = 11; //LeaseCheck
                    envioCorreo.asunto = "Contacto Comercial LeaseCheck | " + contacto.nombre + " - " + contacto.empresa;
                    envioCorreo.usuario = 1;
                    envioCorreoController.EnviarCorreo(envioCorreo);

                    respuesta.detalle = "Registro actualizado exitosamente.";
                }
                catch (Exception ex)
                {
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            

            return respuesta;
        }
    }
}