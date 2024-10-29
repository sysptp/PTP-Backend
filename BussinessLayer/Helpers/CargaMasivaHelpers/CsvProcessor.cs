using System.Collections.Generic;
using System.IO;
using System.Web;
using System;
using Microsoft.AspNetCore.Http;

namespace BussinessLayer.Helpers.CargaMasivaHelpers
{
    public class CsvProcessor
    {
        public List<string[]> ReadCsv(IFormFile file, string delimitador)
        {
            var dataRows = new List<string[]>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    if (lineNumber == 1) continue;
                    // Ignorar la primera línea(la cabecera)
                    var values = line.Split(new[] { delimitador }, StringSplitOptions.None)
                                        .Select(value => value.Trim())
                                        .ToArray();

                    dataRows.Add(values);
                }
            }

            return dataRows;
        }

    }

}
