using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Seguridad;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.Schedule
{
    public class GnScheduleUserService : GenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>, IGnScheduleUserService
    {
        private readonly IGnScheduleUserRepository _userScheduleRepository;
        private readonly IMapper _mapper;

        public GnScheduleUserService(IGnScheduleUserRepository userRepository, IMapper mapper) : base(userRepository, mapper) 
        {
            _userScheduleRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GnScheduleUserResponse>> GetAllByFilters(long? companyId, int? userId, int? scheduleId)
        {
            var userSchedules = await _userScheduleRepository.GetAllWithIncludeAsync(new List<string> { "GnEmpresa", "Usuario", "GnSchedule" });

            var userSchedulesResponse = _mapper.Map<List<GnScheduleUserResponse>>(userSchedules);

            if (companyId != null)
            {
                userSchedulesResponse.Where(x => x.CompanyId != companyId).ToList();
            }

            if (userId != null) 
            {
                userSchedulesResponse.Where(x => x.UserId != userId).ToList();
            }

            if (scheduleId != null)
            {
                userSchedulesResponse.Where(x => x.ScheduleId != scheduleId).ToList();
            }

            return userSchedulesResponse;

        }

    }
}
