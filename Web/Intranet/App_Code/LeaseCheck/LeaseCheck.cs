using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;

namespace LeaseCheck
{
    public class LeaseCheck
    {
        public enum Perfil
        {
            Administrador = 1
        }

        public enum Perfiles
        {
            Root = 1,
            Soporte = 2,
            AdminComercial = 3,
            Comercial = 4,
            AdministradorCorredora = 5,
            ClienteExterno = 6,
            Propietario = 7,
            Ejecutivo = 8
        }

    
        public static string PdfWriteTemp(byte[] binarioArchivo, int idArchivo)
        {
            System.IO.FileStream archivo = null;

            string ruta = "";

            string error = "";

            try
            {

                //1.-Armo las rutas necesarias
                string rutaTemporal = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DirectorioTemporal"]) + "Pdf\\";
                string rutaTemporalArchivo = rutaTemporal + idArchivo.ToString() + ".pdf";

                //Creo el Directorio de pdf si no existe
                if (!Directory.Exists(rutaTemporal))
                {
                    Directory.CreateDirectory(rutaTemporal);
                }

                error += "1.- ok; ";

                //2.-Verifico que el repositorio contenga archivos, si tiene elimino los con 1 día de antiguedad
                if (Directory.GetFiles(rutaTemporal).Length > 0)
                {
                    string[] archivosExistentes = Directory.GetFiles(rutaTemporal);

                    foreach (string archivoExistente in archivosExistentes)
                    {
                        FileInfo fi = new FileInfo(archivoExistente);
                        if (fi.CreationTime < DateTime.Now.AddDays(-1))
                        {
                            fi.Delete();
                        }
                    }
                }

                error += "2.- ok; ";

                //3.-Verifico si existe el mismo archivo, si existe lo elimino
                if (File.Exists(rutaTemporalArchivo))
                {
                    File.Delete(rutaTemporalArchivo);
                }

                error += "3.- ok; ";

                //4.Creo el archivo
                archivo = System.IO.File.Create(rutaTemporalArchivo);

                if (archivo == null) throw new Exception("fallo Filestream");
                if (binarioArchivo == null) throw new Exception("fallo Binario");

                archivo.Write(binarioArchivo, 0, binarioArchivo.Length);
                archivo.Dispose();
                archivo.Close();

                error += "4.- ok; ";

                ruta = "Pdf/" + idArchivo.ToString() + ".pdf";

            }
            catch (Exception ex)
            {
                archivo.Dispose();
                archivo.Close();
                Tools.tools.ClientAlert(ex.Message + "****" + error);
            }

            return ruta;
        }

        //Reduce tamaño imagen
        public static byte[] ReducirImagen(byte[] imageBytes, int pAncho, int pAlto)
        {
            System.Drawing.Image pImagen = null;

            //1.-Convert byte[] to Image
            using (var ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                pImagen = System.Drawing.Image.FromStream(ms, true);
            }
            //FIn  Convierto el base64 a Imagen

            //2.- creamos un bitmap con el nuevo tamaño
            Bitmap vBitmap = new Bitmap(pAncho, pAlto);

            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((System.Drawing.Image)vBitmap))
            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //Se dibuja la nueva imagen

                vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
            }

            //retornamos la nueva imagen
            pImagen = (System.Drawing.Image)vBitmap;

            //3.- Convert to array
            using (var ms = new System.IO.MemoryStream())
            {
                pImagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = ms.ToArray();
            }


            //4.- Retorno el binario
            return imageBytes;

        }
    }
}