#region References
using Application.Interfaces;
using Application.Core.Models.DTOs;
using Application.Core.Models.Requests;
using Domain.Core.Repositories;
using Domain.Entities;
#endregion

#region Namespace

namespace Application.Core.Services
{
    public class RoleService : IRoleService
    {
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        public RoleService(IRoleRepository roleRepository, IEntityMapperService entityMapperService)
        {
            _roleRepository = roleRepository;
            _entityMapperService = entityMapperService;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var result = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
            if (result != null)
            {
                return _entityMapperService.Map<Roles, RoleDto>(result);
            }
            return null;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> SaveRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            var mappedResult = _entityMapperService.Map<RoleDto, Roles>(role);
            var savedResult = await _roleRepository.SaveRoleAsync(mappedResult, cancellationToken);
            if (savedResult != null)
            {
                return _entityMapperService.Map<Roles, RoleDto>(savedResult);
            }
            return null;
        }

        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            var mappedResult = _entityMapperService.Map<RoleDto, Roles>(role);
            var savedResult = await _roleRepository.UpdateRoleAsync(mappedResult, cancellationToken);
            if (savedResult != null)
            {
                return _entityMapperService.Map<Roles, RoleDto>(savedResult);
            }
            return null; 
        }
    }
}
#endregion