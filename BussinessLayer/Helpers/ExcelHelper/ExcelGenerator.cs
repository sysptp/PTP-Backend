//using System;
//using System.Collections.Generic;
//using System.IO;
//using OfficeOpenXml;

//namespace BussinessLayer.Helpers.ExcelHelper
//{
//    public class ExcelGenerator
//    {
//        public MemoryStream GenerateExcel(List<Dictionary<string, object>> data)
//        {
//            // Configurar el archivo Excel
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

//            var memoryStream = new MemoryStream();

//            using (var package = new ExcelPackage(memoryStream))
//            {
//                // Añadir una nueva hoja al archivo Excel
//                var worksheet = package.Workbook.Worksheets.Add("Report");

//                // Verificar si hay datos
//                if (data == null || data.Count == 0)
//                {
//                    throw new ArgumentException("La lista de datos está vacía.");
//                }

//                // Escribir los encabezados en la primera fila
//                var headers = new List<string>(data[0].Keys);
//                for (int i = 0; i < headers.Count; i++)
//                {
//                    worksheet.Cells[1, i + 1].Value = headers[i]; // Encabezados empiezan en la fila 1
//                }

//                // Escribir los datos en las filas siguientes
//                for (int i = 0; i < data.Count; i++)
//                {
//                    var row = data[i];
//                    for (int j = 0; j < headers.Count; j++)
//                    {
//                        worksheet.Cells[i + 2, j + 1].Value = row[headers[j]]; // Filas empiezan en la fila 2
//                    }
//                }

//                // Ajustar el ancho de las columnas
//                worksheet.Cells.AutoFitColumns();

//                // Guardar el paquete en el flujo
//                package.Save();
//            }

//            // Volver al inicio del flujo
//            memoryStream.Position = 0;
//            return memoryStream;
//        }
//    }
//}
