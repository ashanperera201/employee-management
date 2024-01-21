#region References
using Domain.Entities;
#endregion


#region Namespace
namespace Domain.Core.Repositories
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Saves the employee asynchronous.
        /// </summary>
        /// <param name="employees">The employees.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Employees> SaveEmployeeAsync(Employees employees, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employees">The employees.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Employees> UpdateEmployeeAsync(Employees employees, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        IQueryable<Employees> GetEmployees(string? term, int page, int pageSize, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Employees?> GetEmployeeAsync(string employeeId, CancellationToken cancellationToken = default);
    }
}
#endregion
