#region References
using Application.Core.Models.DTOs;
using Application.Interfaces;
using Domain.Core.Repositories;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
#endregion

#region Namespace

namespace Application.Core.Services
{
    public class PermissionService : IPermissionService
    {
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IPermissionRepository _permissionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="entityMapperService">The entity mapper service.</param>
        /// <param name="permissionRepository">The permission repository.</param>
        public PermissionService(IEntityMapperService entityMapperService, IPermissionRepository permissionRepository)
        {
            _entityMapperService = entityMapperService;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<PermissionsDto>> GetPermissionAsync(string name, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionRepository.GetPermissionAsync(name, cancellationToken);

            if (permission != null)
            {
                return new GenericResponse<PermissionsDto>(_entityMapperService.Map<Permissions, PermissionsDto>(permission), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<PermissionsDto>(null, true, StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Gets the permissions asynchronous.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<List<PermissionsDto>>> GetPermissionsAsync(string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var permissionsQuery = _permissionRepository.GetAllPermissionAsync(searchTerm, page, pageSize);
            var permissions = await permissionsQuery.ToListAsync(cancellationToken);

            if (permissions != null)
            {
                return new GenericResponse<List<PermissionsDto>>(_entityMapperService.Map<List<Permissions>, List<PermissionsDto>>(permissions), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<List<PermissionsDto>>(null, true, StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permisisons">The permisisons.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<PermissionsDto>> SavePermissionAsync(PermissionsDto permisisons, CancellationToken cancellationToken = default)
        {
            var mappedPermission = _entityMapperService.Map<PermissionsDto, Permissions>(permisisons);
            var savedPermission = await _permissionRepository.SavePermissionAsync(mappedPermission, cancellationToken);
            
            if(savedPermission != null)
            {
                return new GenericResponse<PermissionsDto>(_entityMapperService.Map<Permissions, PermissionsDto>(savedPermission), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<PermissionsDto>(null, true, StatusCodes.Status204NoContent);
        }

        public async Task<GenericResponse<PermissionsDto>> UpdatePermissionAsync(PermissionsDto permisison, CancellationToken cancellationToken = default)
        {
            var mappedPermission = _entityMapperService.Map<PermissionsDto, Permissions>(permisison);
            var savedPermission = await _permissionRepository.UpdatePermissionAsync(mappedPermission, cancellationToken);

            if (savedPermission != null)
            {
                return new GenericResponse<PermissionsDto>(_entityMapperService.Map<Permissions, PermissionsDto>(savedPermission), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<PermissionsDto>(null, true, StatusCodes.Status204NoContent);
        }
    }
}
#endregion