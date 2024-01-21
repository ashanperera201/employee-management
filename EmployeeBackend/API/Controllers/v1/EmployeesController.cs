#region References
using Application.Core.Models.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

#region Namespace
namespace API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync([FromQuery] string? term, [FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _employeeService.GetEmployeesAsync(term, page, pageSize, cancellationToken);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the employee by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _employeeService.GetEmployeeAsync(email, cancellationToken);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Saves the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveEmployeeAsync([FromBody] EmployeeDto employee, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _employeeService.SaveEmployeeAsync(employee, cancellationToken);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeDto employee, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employee, cancellationToken);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion