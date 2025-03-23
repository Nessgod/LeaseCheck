using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AccesoDatosCorreo.Model
{
    [Serializable]
    public class EnvioCorreo
    {
        public string para { get; set; }
        public string cc { get; set; }
        public string asunto { get; set; }
        public string body { get; set; }
        public string calendario { get; set; }
        public string documento { get; set; }
        public string extension { get; set; }
        public int aplicacion { get; set; }
        public int usuario { get; set; }
        public byte[] adjunto { get; set; }
        public string adjunto_nombre { get; set; }
        public string adjunto_extension { get; set; }

    }
}