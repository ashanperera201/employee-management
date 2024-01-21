#region References
using Domain.Core.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
#endregion

#region Namespace
namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// The application database context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Gets the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Employees?> GetEmployeeAsync(string employeeId, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Employees.Where(p => p.Email == employeeId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Employees> GetEmployees(string? term, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext.Employees.AsQueryable();

            // Apply any filtering based on the term
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Email.Contains(term) || p.FullName.Contains(term));
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return query;
        }

        /// <summary>
        /// Saves the employee asynchronous.
        /// </summary>
        /// <param name="employees">The employees.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Employees> SaveEmployeeAsync(Employees employees, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = await _applicationDbContext.Employees.AddAsync(employees, cancellationToken);
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
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employees">The employees.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Employees> UpdateEmployeeAsync(Employees employees, CancellationToken cancellationToken = default)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = _applicationDbContext.Employees.Update(employees);
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
