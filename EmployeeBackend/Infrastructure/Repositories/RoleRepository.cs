#region References
using Microsoft.EntityFrameworkCore;
using Domain.Core.Repositories;
using Domain.Entities;
using Infrastructure.Data;
#endregion

#region Namespace
namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The IAM database context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="tssDbContext">The IAM database context.</param>
        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var role = await _applicationDbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == Guid.Parse(roleId), cancellationToken);
            return role;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> SaveRoleAsync(Roles role, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = await _applicationDbContext.Roles.AddAsync(role, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedRes.Entity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> UpdateRoleAsync(Roles role, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = _applicationDbContext.Roles.Update(role);
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