#region References
using Domain.Entities;
#endregion

#region Namespace
namespace Domain.Core.Repositories
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Permissions?> SavePermissionAsync(Permissions permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all permission asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public IQueryable<Permissions> GetAllPermissionAsync(string term, int page, int pageSize = 10);
        /// <summary>
        /// Gers the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Permissions?> GetPermissionAsync(string permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Permissions?> UpdatePermissionAsync(Permissions permission, CancellationToken cancellationToken = default);
    }
}
#endregion