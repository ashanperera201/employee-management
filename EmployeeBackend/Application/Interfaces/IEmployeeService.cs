#region References
using Application.Core.Models.DTOs;
using Application.Models;
#endregion

#region Namespace
namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Saves the employee asynchronous.
        /// </summary>
        /// <param name="employeeDto">The employee dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<GenericResponse<EmployeeDto>> SaveEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeDto">The employee dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<GenericResponse<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<GenericResponse<List<EmployeeDto>>> GetEmployeesAsync(string? term, int page, int pageSize, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the employee asynchronous.
        /// </summary>
        /// <param name="employeeEmail">The employee email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<GenericResponse<EmployeeDto>> GetEmployeeAsync(string employeeEmail, CancellationToken cancellationToken = default);
    }
}
#endregion