using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Core.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The t ss database context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="tSSDbContext">The t ss database context.</param>
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Users?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            var user = await _applicationDbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(x => x.EmailId == userEmail, cancellationToken);
            return user;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Users?> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _applicationDbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId), cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Users?> SaveUserAsync(Users user, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRes = await _applicationDbContext.Users.AddAsync(user, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return userRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Users?> UpdateUserAsync(Users user, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRes = _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return userRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
