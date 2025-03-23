using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck
{
    public class Paginas
    {
        //Sistema
        #region Zona Geografica
        // Pais
        public enum menu_pais
        {
            Ver = 10,
        }
        // Nacionalidad
        public enum menu_nacionalidad
        {
            Ver = 14,
        }
        // Feriado
        public enum menu_feriado
        {
            Ver = 15,
        }

        #endregion

        #region Accesos
        // Perfil
        public enum menu_perfil
        {
            Ver = 10,
        }
        // Usuario
        public enum menu_usuario
        {
            Ver = 11,
        }
        // Acceso
        public enum menu_accesos
        {
            Ver = 3,
        }
        #endregion

        #region Configuracion
        // Estado Civil
        public enum menu_estado_civi
        {
            Ver = 21,
        }
        // Niveles de Estudio
        public enum menu_nivel_estudio
        {
            Ver = 16,
        }
        // Jornada Laboral
        public enum menu_jornada_laboral
        {
            Ver = 18,
        }

        #endregion

        #region Planes Tarifarios
        // Bolsa
        public enum menu_bolsa
        {
            Ver = 19,
        }
        // Tipo de Plan
        public enum menu_tipo_plan
        {
            Ver = 18,
        }
    
        //Productos
        public enum menu_producto
        {
            Ver = 20,
        }
        //Productos Adicional
        public enum menu_producto_adicional
        {
            Ver = 21,
        }
        #endregion

        //Soporte
        #region Soporte
        //Mesa ayuda
        public enum menu_ayuda
        {
            Ver = 22,
        }
        #endregion

        //Comercial
        #region Comercial-Clientes
        // Comercial-Cliente
        public enum menu_cliente
        {
            Ver = 25,
        }
  
        // Reasignacion Clientes
        public enum menu_reasignacion_clientes
        {
            Ver = 26,
        }
        #endregion
        #region Comercial-Dashboard e Informes
        // Vencimiento de Planes
        public enum menu_vencimiento_planes
        {
            Ver = 27,
        }
        // Planes por Vencer
        public enum menu_planes_por_vencer
        {
            Ver = 28,
        }
        #endregion
   
        //Clientes
        #region Clientes
        // Cliente Identidad
        public enum menu_cliente_identidad
        {
            Ver = 30,
        }
        // Cliente Planes
        public enum menu_cliente_planes
        {
            Ver = 32,
        }
        // Cliente Usuarios
        public enum menu_cliente_usuarios
        {
            Ver = 31,
        }
     
        // Dashboard Plan Vigente
        public enum menu_dashboard_plan_vigente
        {
            Ver = 33,
        }
        #endregion

    }
}