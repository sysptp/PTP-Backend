namespace BussinessLayer.DTOs.ModuloGeneral.Empresas
{
    public class GnEmpresaResponse
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Logo { get; set; }
        public string RNC { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PrimaryPhone { get; set; } = null!;
        public string? SecondaryPhone { get; set; }
        public string? PrimaryExtension { get; set; }
        public string? SecondaryExtension { get; set; }
        public int SucursalCount { get; set; }
        public int UserCount { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LanguageCode { get; set; }
        public string? DefaultUrl { get; set; }
    }
}
