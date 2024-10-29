using System.Collections.Generic;
using DataLayer.Models;
using DataLayer.Models.MenuApp;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.ViewModels
{
    public class PerfilMenuViewModel
    {
        public Gn_Perfil Gn_Perfil { get;set;}

        public List<GnMenu> Gn_Menu { get; set; }
    }
}
