using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.SModuloCampaña
{
    public interface ICmpContactoService
    {

        Task<Response<CmpContactoCreateDto>> CreateContactoAsync(CmpContactoCreateDto contacto);
        Task<Response<string>> DeleteContactoAsync(long idContacto, long idEmpresa);
        Task<Response<CmpContactoDto>> GetContactoByIdAsync(long idContacto, long idEmpresa);
        Task<Response<List<CmpContactoDto>>> GetContactosAsync(long idEmpresa);
        Task<Response<string>> UpdateContactoAsync(long idContacto, CmpContactoUpdateDto contacto);

    }
}