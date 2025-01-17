using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpEmail
{
    public class CmpServidoresSmtpUpdateDto
    {
        public int ServidorId { get; set; }
        public string? Nombre { get; set; }
        public string? Host { get; set; }
        public int Puerto { get; set; }
    }
}
