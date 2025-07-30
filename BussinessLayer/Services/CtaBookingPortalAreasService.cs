using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaBookingPortalAreasService : ICtaBookingPortalAreasService
    {
        private readonly ICtaBookingPortalAreasRepository _repository;
        private readonly IMapper _mapper;

        public CtaBookingPortalAreasService(ICtaBookingPortalAreasRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookingPortalAreaResponse>> GetAreasByPortalIdAsync(int portalId)
        {
            var portalAreas = await _repository.GetByPortalIdAsync(portalId);
            return _mapper.Map<List<BookingPortalAreaResponse>>(portalAreas);
        }

        public async Task<List<BookingPortalAreaResponse>> GetPortalsByAreaIdAsync(int areaId)
        {
            var portalAreas = await _repository.GetByAreaIdAsync(areaId);
            return _mapper.Map<List<BookingPortalAreaResponse>>(portalAreas);
        }

        public async Task<BookingPortalAreaResponse?> GetDefaultAreaByPortalIdAsync(int portalId)
        {
            var defaultArea = await _repository.GetDefaultAreaByPortalIdAsync(portalId);
            return defaultArea != null ? _mapper.Map<BookingPortalAreaResponse>(defaultArea) : null;
        }

        public async Task<BookingPortalAreaResponse> AddAreaToPortalAsync(BookingPortalAreaRequest request)
        {
            var entity = _mapper.Map<CtaBookingPortalAreas>(request);
            var created = await _repository.Add(entity);
            return _mapper.Map<BookingPortalAreaResponse>(created);
        }

        public async Task RemoveAreaFromPortalAsync(int portalId, int areaId)
        {
            await _repository.DeleteByPortalAndAreaAsync(portalId, areaId);
        }

        public async Task<BookingPortalAreaResponse> SetDefaultAreaAsync(int portalId, int areaId)
        {
            // Remover default de todas las áreas del portal
            var existingAreas = await _repository.GetByPortalIdAsync(portalId);
            foreach (var area in existingAreas)
            {
                area.IsDefault = false;
                await _repository.Update(area, area.Id);
            }

            // Establecer la nueva área como default
            var targetArea = existingAreas.FirstOrDefault(a => a.AreaId == areaId);
            if (targetArea != null)
            {
                targetArea.IsDefault = true;
                await _repository.Update(targetArea, targetArea.Id);
                return _mapper.Map<BookingPortalAreaResponse>(targetArea);
            }

            throw new InvalidOperationException("Área no encontrada en el portal especificado");
        }

        public async Task<List<BookingPortalAreaResponse>> UpdatePortalAreasAsync(int portalId, List<BookingPortalAreaRequest> areas)
        {
            // Obtener áreas existentes
            var existingAreas = await _repository.GetByPortalIdAsync(portalId);
            var existingAreaIds = existingAreas.Select(a => a.AreaId).ToList();
            var newAreaIds = areas.Select(a => a.AreaId).ToList();

            // Eliminar áreas que ya no están en la lista
            var areasToRemove = existingAreaIds.Except(newAreaIds);
            foreach (var areaId in areasToRemove)
            {
                await _repository.DeleteByPortalAndAreaAsync(portalId, areaId);
            }

            // Agregar nuevas áreas
            var areasToAdd = newAreaIds.Except(existingAreaIds);
            var addedAreas = new List<CtaBookingPortalAreas>();

            foreach (var areaId in areasToAdd)
            {
                var areaRequest = areas.First(a => a.AreaId == areaId);
                areaRequest.PortalId = portalId;
                var entity = _mapper.Map<CtaBookingPortalAreas>(areaRequest);
                var added = await _repository.Add(entity);
                addedAreas.Add(added);
            }

            // Actualizar áreas existentes
            foreach (var area in areas.Where(a => existingAreaIds.Contains(a.AreaId)))
            {
                var existingArea = existingAreas.First(e => e.AreaId == area.AreaId);
                existingArea.IsDefault = area.IsDefault;
                await _repository.Update(existingArea, existingArea.Id);
            }

            // Retornar todas las áreas actualizadas
            var updatedAreas = await _repository.GetByPortalIdAsync(portalId);
            return _mapper.Map<List<BookingPortalAreaResponse>>(updatedAreas);
        }
    }
}