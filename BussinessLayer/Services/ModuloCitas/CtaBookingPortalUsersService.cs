using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaBookingPortalUsersService : ICtaBookingPortalUsersService
    {
        private readonly ICtaBookingPortalUsersRepository _repository;
        private readonly IMapper _mapper;

        public CtaBookingPortalUsersService(ICtaBookingPortalUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookingPortalUserResponse>> GetUsersByPortalIdAsync(int portalId)
        {
            var portalUsers = await _repository.GetByPortalIdAsync(portalId);
            return _mapper.Map<List<BookingPortalUserResponse>>(portalUsers);
        }

        public async Task<List<BookingPortalUserResponse>> GetPortalsByUserIdAsync(int userId)
        {
            var portalUsers = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<List<BookingPortalUserResponse>>(portalUsers);
        }

        public async Task<BookingPortalUserResponse?> GetMainAssigneeByPortalIdAsync(int portalId)
        {
            var mainAssignee = await _repository.GetMainAssigneeByPortalIdAsync(portalId);
            return mainAssignee != null ? _mapper.Map<BookingPortalUserResponse>(mainAssignee) : null;
        }

        public async Task<BookingPortalUserResponse> AddUserToPortalAsync(BookingPortalUserRequest request)
        {
            var entity = _mapper.Map<CtaBookingPortalUsers>(request);
            var created = await _repository.Add(entity);
            return _mapper.Map<BookingPortalUserResponse>(created);
        }

        public async Task RemoveUserFromPortalAsync(int portalId, int userId)
        {
            await _repository.DeleteByPortalAndUserAsync(portalId, userId);
        }

        public async Task<BookingPortalUserResponse> SetMainAssigneeAsync(int portalId, int userId)
        {
            // Remover main assignee de todos los usuarios del portal
            var existingUsers = await _repository.GetByPortalIdAsync(portalId);
            foreach (var user in existingUsers)
            {
                user.IsMainAssignee = false;
                await _repository.Update(user, user.Id);
            }

            // Establecer el nuevo usuario como main assignee
            var targetUser = existingUsers.FirstOrDefault(u => u.UserId == userId);
            if (targetUser != null)
            {
                targetUser.IsMainAssignee = true;
                await _repository.Update(targetUser, targetUser.Id);
                return _mapper.Map<BookingPortalUserResponse>(targetUser);
            }

            throw new InvalidOperationException("Usuario no encontrado en el portal especificado");
        }

        public async Task<List<BookingPortalUserResponse>> UpdatePortalUsersAsync(int portalId, List<BookingPortalUserRequest> users)
        {
            // Obtener usuarios existentes
            var existingUsers = await _repository.GetByPortalIdAsync(portalId);
            var existingUserIds = existingUsers.Select(u => u.UserId).ToList();
            var newUserIds = users.Select(u => u.UserId).ToList();

            // Eliminar usuarios que ya no están en la lista
            var usersToRemove = existingUserIds.Except(newUserIds);
            foreach (var userId in usersToRemove)
            {
                await _repository.DeleteByPortalAndUserAsync(portalId, userId);
            }

            // Agregar nuevos usuarios
            var usersToAdd = newUserIds.Except(existingUserIds);
            var addedUsers = new List<CtaBookingPortalUsers>();

            foreach (var userId in usersToAdd)
            {
                var userRequest = users.First(u => u.UserId == userId);
                userRequest.PortalId = portalId;
                var entity = _mapper.Map<CtaBookingPortalUsers>(userRequest);
                var added = await _repository.Add(entity);
                addedUsers.Add(added);
            }

            // Actualizar usuarios existentes
            foreach (var user in users.Where(u => existingUserIds.Contains(u.UserId)))
            {
                var existingUser = existingUsers.First(e => e.UserId == user.UserId);
                existingUser.IsMainAssignee = user.IsMainAssignee;
                await _repository.Update(existingUser, existingUser.Id);
            }

            // Retornar todos los usuarios actualizados
            var updatedUsers = await _repository.GetByPortalIdAsync(portalId);
            return _mapper.Map<List<BookingPortalUserResponse>>(updatedUsers);
        }
    }
}