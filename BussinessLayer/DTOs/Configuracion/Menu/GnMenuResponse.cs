
namespace BussinessLayer.DTOs.Configuracion.Menu
{
    public class GnMenuResponse
    {
        public int MenuID { get; set; }
        public string Name { get; set; } = null!;
        public int Level { get; set; }
        public int Order { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int ModuleID { get; set; }
        public int ParentMenuId { get; set; }
    }

}