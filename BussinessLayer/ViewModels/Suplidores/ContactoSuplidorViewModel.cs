using DataLayer.Models;
using DataLayer.Models.ModuloInventario;
using DataLayer.Models.Suplidor;

namespace BussinessLayer.ViewModels
{
    public class ContactoSuplidorViewModel
    {
        public Suplidores Suplidores { get; set; }

        public ContactosSuplidores ContactosSuplidores { get; set; }
    }
}
