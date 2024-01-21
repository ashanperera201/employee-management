#region References
using Application.Core.Models.DTOs;
using Application.Models;
#endregion


#region Namespace
namespace Application.Interfaces
{
    public interface IPermissionService
    {
        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permisisons">The permisisons.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<GenericResponse<PermissionsDto>> SavePermissionAsync(PermissionsDto permisisons, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="permisison">The permisison.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<GenericResponse<PermissionsDto>> UpdatePermissionAsync(PermissionsDto permisison, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the permissions asynchronous.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<GenericResponse<List<PermissionsDto>>> GetPermissionsAsync(string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<GenericResponse<PermissionsDto>> GetPermissionAsync(string name, CancellationToken cancellationToken = default);
    }
}
#endregion