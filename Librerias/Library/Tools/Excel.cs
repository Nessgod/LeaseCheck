﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Text;
using System.IO;

namespace Tools
{
   public static class Excel
    {
        //Devuelvo un archivo Excel extencion .xls
        public static void exportExcel(DataTable dt, string fileName, bool header = true)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.HeaderEncoding = Encoding.Default;
            HttpContext.Current.Response.ContentEncoding = Encoding.Default;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");

            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";

            StringBuilder output = new StringBuilder();

            if (header == true)
            {
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);
                    output.Append(FIELDSEPARATOR);
                }
                output.Append(ROWSEPARATOR);
            }

            foreach (DataRow item in dt.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ')); //.Replace('.', ','));
                    output.Append(FIELDSEPARATOR);
                }
                 
                output.Append(ROWSEPARATOR);
            }
               
            HttpContext.Current.Response.Write(output.ToString());
            HttpContext.Current.Response.End();
        }

        //Devuelvo un archivo Excel como arreglo de bit 
        public static byte[] exportExcel(DataTable dt, bool header = true)
        {

            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";

            StringBuilder output = new StringBuilder();

            if (header == true)
            {
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);
                    output.Append(FIELDSEPARATOR);
                }
                output.Append(ROWSEPARATOR);
            }

            foreach (DataRow item in dt.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ')); //.Replace('.', ','));
                    output.Append(FIELDSEPARATOR);
                }

                output.Append(ROWSEPARATOR);
            }

            //Convierto el StringBuilder en Arrego de bit
            byte[] buffer = System.Text.Encoding.Default.GetBytes(output.ToString());

            //Creo el Archivo en Memoria
            MemoryStream Archivo = new MemoryStream();
            Archivo.Write(buffer, 0, buffer.Length);

            //Retorno el archivo como arreglo de bit
            return Archivo.ToArray();

        }
       
        public static void exportCsv(DataTable dt, string fileName, bool header = true)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.HeaderEncoding = Encoding.Default;
            HttpContext.Current.Response.ContentEncoding = Encoding.Default;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".csv");

            const string FIELDSEPARATOR = ";";
            const string ROWSEPARATOR = "\n";

            StringBuilder output = new StringBuilder();

            if (header == true)
            {
                int count = 1;
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);

                    if(count < dt.Columns.Count)
                        output.Append(FIELDSEPARATOR);

                    count++;
                }
                output.Append(ROWSEPARATOR);
            }

            foreach (DataRow item in dt.Rows)
            {
                int countRow = 1;

                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ')); //.Replace('.', ','));
                    if(countRow < item.ItemArray.Length)
                        output.Append(FIELDSEPARATOR);

                    countRow++;
                }
                output.Append(ROWSEPARATOR);
            }

            HttpContext.Current.Response.Write(output.ToString());
            HttpContext.Current.Response.End();
        }

        //Lee el archivo y debuelve un DataTable
        public static DataTable importarExcel(FileUpload fileUpload, int encabezado = 1)
        {

            string tmpFullPathName = "";
            string tmpPathName = "";
            StreamReader lector = null;
            DataTable tabla = null;
            try
            {

                if (fileUpload.HasFile)
                {
                    if (Path.GetExtension(fileUpload.FileName).ToLower() == ".csv")
                    {
                        //Creo una Directorio Temp en los Temp del sistema
                        tmpPathName = Path.Combine(Path.GetTempPath(), HttpContext.Current.Session.SessionID);
                        if (!Directory.Exists(tmpPathName))
                        {
                            Directory.CreateDirectory(tmpPathName);
                        }

                        //Convierto el Binario a archivo
                        tmpFullPathName = Path.Combine(tmpPathName, fileUpload.FileName);
                        if (!File.Exists(tmpFullPathName)) File.WriteAllBytes(tmpFullPathName, fileUpload.FileBytes);

                        lector = new StreamReader(tmpFullPathName, Encoding.Default);
                        
                        String fila = String.Empty;
                        Int32 cantidad = 0;
                        String Columna = String.Empty;
                        int count = 0;
                        int encabezado2 = 1;
                        do
                        {
                            fila = lector.ReadLine();
                            if (encabezado2 >= encabezado) {
                                if (fila == null)
                                {
                                    break;
                                }
                                if (0 == cantidad++)
                                {
                                    Columna = fila;
                                    tabla = CrearTabla(fila);
                                }
                                if (count > 0)
                                    AgregarFila(fila, tabla, Columna);

                                count++;
                            }
                            encabezado2++;
                            
                        } while (true);

                        //Libero los recursos
                        if (lector != null) lector.Dispose();

                        //Elimino el archivo Temp y el Directorio Temp
                        if (File.Exists(tmpFullPathName)) File.Delete(tmpFullPathName);
                        if (Directory.Exists(tmpPathName))
                        {
                            if (Directory.GetFiles(tmpPathName).Length == 0)
                                Directory.Delete(tmpPathName);
                        }
                    }
                    else
                    {
                        throw new Exception("El Archivo no es un .CSV");
                    }

                }
                return tabla;
            }
            catch (Exception ex)
            {
                if (lector != null) lector.Dispose();
                if (File.Exists(tmpFullPathName)) File.Delete(tmpFullPathName);
                if (Directory.Exists(tmpPathName))
                {
                    if (Directory.GetFiles(tmpPathName).Length == 0)
                        Directory.Delete(tmpPathName);
                }
                throw new Exception(ex.Message);
            }
            finally
            {
                if (lector != null) lector.Dispose();
                if (File.Exists(tmpFullPathName)) File.Delete(tmpFullPathName);
                if (Directory.Exists(tmpPathName))
                {
                    if (Directory.GetFiles(tmpPathName).Length == 0)
                        Directory.Delete(tmpPathName);
                }
            }

        }

        //Creo la Tabla con las columnas
        private static DataTable CrearTabla(String fila)
        {
            int cantidadColumnas;
            DataTable tabla = new DataTable("Datos");
            String[] valores = fila.Split(';');
            cantidadColumnas = valores.Length;
            //int idx = 0;
            foreach (String val in valores)
            {
                String nombreColumna = String.Format("{0}", val);
                tabla.Columns.Add(nombreColumna, Type.GetType("System.String"));
            }
            return tabla;
        }

        //Creo las Filas de la tabla
        private static DataRow AgregarFila(String fila, DataTable tabla, String Columna)
        {
            int cantidadColumnas = 1000;
            String[] valores = fila.Split(';');
            Int32 numeroTotalValores = valores.Length;
            if (numeroTotalValores > cantidadColumnas)
            {
                Int32 diferencia = numeroTotalValores - cantidadColumnas;
                for (Int32 i = 0; i < diferencia; i++)
                {

                    String nombreColumna = String.Format("{0}", (cantidadColumnas + i));
                    tabla.Columns.Add(nombreColumna, Type.GetType("System.String"));
                }
                cantidadColumnas = numeroTotalValores;
            }
            //int idx = 0;
            int count = 0;
            DataRow dfila = tabla.NewRow();
            String[] colums = Columna.Split(';');
            foreach (String colum in colums)
            {
                int count2 = 0;
                foreach (String val in valores)
                {
                    if (count == count2)
                    {
                        String nombreColumna = String.Format("{0}", colum);
                        dfila[nombreColumna] = val.Trim();
                        break;
                    }
                    count2++;
                }
                count++;
            }

            tabla.Rows.Add(dfila);
            return dfila;
        }


    }

    
}


