#region References
using Application.Core.Models.DTOs;
using Application.Core.Models.Requests;
#endregion

#region Namespace
namespace Application.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<RoleDto?> SaveRoleAsync(RoleDto role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<RoleDto?> UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<RoleDto?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default);
    }
}
#endregion