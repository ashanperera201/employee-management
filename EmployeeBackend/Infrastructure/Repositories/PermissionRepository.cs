#region References
using Domain.Core.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
#endregion

#region Namespace
namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        /// <summary>
        /// The application database context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public PermissionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Gers the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permissions?> GetPermissionAsync(string permission, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Permissions.Where(p => p.Name == permission).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets all permission asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public IQueryable<Permissions> GetAllPermissionAsync(string term, int page, int pageSize = 10)
        {
            var query = _applicationDbContext.Permissions.AsQueryable();

            // Apply any filtering based on the term
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term) || p.Code.Contains(term));
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return query;
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permissions?> SavePermissionAsync(Permissions permission, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = await _applicationDbContext.Permissions.AddAsync(permission, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permissions?> UpdatePermissionAsync(Permissions permission, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = _applicationDbContext.Permissions.Update(permission);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
#endregion