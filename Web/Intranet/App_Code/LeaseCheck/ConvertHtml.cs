using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Pechkin;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Winnovative;

namespace LeaseCheck
{
    public class ConvertHtml
    {
        public static byte[] PdfConverterBinario(string html)
        {
            byte[] pdfBytes = null;

            try
            {
                PdfConverter htmlToPdfConverter = new PdfConverter();

                htmlToPdfConverter.LicenseKey = ConfigurationManager.AppSettings.Get("PdfConverterLicenseKey");

                htmlToPdfConverter.PdfDocumentInfo.AuthorName = "LeaseCheck";
                htmlToPdfConverter.PdfDocumentInfo.Title = "LeaseCheck";
                htmlToPdfConverter.PdfDocumentInfo.Subject = "";
                htmlToPdfConverter.PdfDocumentInfo.CreatedDate = DateTime.Now;
                htmlToPdfConverter.PdfDocumentOptions.LeftMargin = 30;
                htmlToPdfConverter.PdfDocumentOptions.RightMargin = 30;
                htmlToPdfConverter.PdfDocumentOptions.TopMargin = 30;
                htmlToPdfConverter.PdfDocumentOptions.BottomMargin = 30;
                htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

                // Convertir HTML a bytes de PDF
                pdfBytes = htmlToPdfConverter.GetPdfBytesFromHtmlString(html);
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o devolver el mensaje de error
                throw new Exception("Error al generar el PDF: " + ex.Message);
            }

            return pdfBytes;
        }


        public static byte[] PdfConverterBinarioRuta(string urlPath)
        {
            byte[] pdfBytes = null;

            string Url = ConfigurationManager.AppSettings.Get("PdfUrlLocal");

            string fullUrl = Url + "/" + urlPath;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream());
            string html = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            
            PdfConverter htmlToPdfConverter = new PdfConverter();

            htmlToPdfConverter.LicenseKey = ConfigurationManager.AppSettings.Get("PdfConverterLicenseKey");
            
            htmlToPdfConverter.PdfDocumentInfo.AuthorName = "LeaseCheck";
            htmlToPdfConverter.PdfDocumentInfo.Title = "LeaseCheck";
            htmlToPdfConverter.PdfDocumentInfo.Subject = "";
            htmlToPdfConverter.PdfDocumentInfo.CreatedDate = DateTime.Now;
            htmlToPdfConverter.PdfDocumentOptions.LeftMargin = 30;
            htmlToPdfConverter.PdfDocumentOptions.RightMargin = 30;
            htmlToPdfConverter.PdfDocumentOptions.TopMargin = 30;
            htmlToPdfConverter.PdfDocumentOptions.BottomMargin = 30;
            htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

            // Set the JPEG Compression Level
            htmlToPdfConverter.PdfDocumentOptions.JpegCompressionLevel = 10;

            // Set images scaling before rendering to PDF
            htmlToPdfConverter.PdfDocumentOptions.ImagesScalingEnabled = true;

            //htmlToPdfConverter.PdfDocumentOptions.SinglePage = true;

            pdfBytes = htmlToPdfConverter.GetPdfBytesFromHtmlString(html);
          
            return pdfBytes;
        }

        public static byte[] MergeDocuments(byte[] docA, byte[] docB)
        {
            try
            {
                Document mergeDocument = new Document();
                mergeDocument.LicenseKey = ConfigurationManager.AppSettings.Get("PdfConverterLicenseKey");
                mergeDocument.AutoCloseAppendedDocs = true;

                Stream stream1 = new MemoryStream(docA);
                Document fisrtDocument = new Document(stream1);
                mergeDocument.AppendDocument(fisrtDocument);

                Stream stream2 = new MemoryStream(docB);
                Document secondDocument = new Document(stream2);
                mergeDocument.AppendDocument(secondDocument);

                byte[] outByte = mergeDocument.Save();

                return outByte;
            }
            catch { return null; }
        }
    }
}