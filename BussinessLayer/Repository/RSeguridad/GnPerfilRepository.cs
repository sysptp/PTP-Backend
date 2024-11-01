using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Entities;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RSeguridad
{
    public class GnPerfilRepository : GenericRepository<GnPerfil>, IGnPerfilRepository
    {

        public async Task PatchUpdateAsync(int id, Dictionary<string, object> updatedProperties)
        {
            var perfil = await GetById(id);
            if (perfil == null) throw new KeyNotFoundException("Perfil no encontrado.");

            foreach (var prop in updatedProperties)
            {
                typeof(GnPerfil).GetProperty(prop.Key)?.SetValue(perfil, prop.Value);
            }

            await Update(perfil);
        }
    }
}
