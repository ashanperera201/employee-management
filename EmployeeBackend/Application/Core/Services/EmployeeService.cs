#region References
using Application.Core.Models.DTOs;
using Application.Core.Models.Requests;
using Application.Interfaces;
using Application.Models;
using Domain.Core.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
#endregion

#region Namespace
namespace Application.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService _emailService;
        /// <summary>
        /// The employee repository
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService" /> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="entityMapperService">The entity mapper service.</param>
        /// <param name="emailService">The email service.</param>
        public EmployeeService(IEmployeeRepository employeeRepository, IEntityMapperService entityMapperService, IEmailService emailService, IUserService userService)
        {
            _employeeRepository = employeeRepository;
            _entityMapperService = entityMapperService;
            _emailService = emailService;
            _userService = userService;
        }

        /// <summary>
        /// Gets the employee asynchronous.
        /// </summary>
        /// <param name="employeeEmail">The employee email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<EmployeeDto>> GetEmployeeAsync(string employeeEmail, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeEmail, cancellationToken);
            if (employee != null)
            {
                var mappedEmployee = _entityMapperService.Map<Employees, EmployeeDto>(employee);
                return new GenericResponse<EmployeeDto>(mappedEmployee, true, StatusCodes.Status200OK);
            }
            else
            {
                return new GenericResponse<EmployeeDto>(null, true, StatusCodes.Status204NoContent);
            }
        }

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<List<EmployeeDto>>> GetEmployeesAsync(string? term, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.GetEmployees(term, page, pageSize, cancellationToken).ToListAsync(cancellationToken);
            if (employees != null && employees.Count() > 0)
            {
                var mappedRes = _entityMapperService.Map<List<Employees>, List<EmployeeDto>>(employees);
                return new GenericResponse<List<EmployeeDto>>(mappedRes, true, StatusCodes.Status200OK);
            }
            else
            {
                return new GenericResponse<List<EmployeeDto>>(null, true, StatusCodes.Status204NoContent);
            }
        }

        /// <summary>
        /// Saves the employee asynchronous.
        /// </summary>
        /// <param name="employeeDto">The employee dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<EmployeeDto>> SaveEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _entityMapperService.Map<EmployeeDto, Employees>(employeeDto);
            var savedRes = await _employeeRepository.SaveEmployeeAsync(mappedEntity, cancellationToken);

            if (savedRes != null)
            {
                var user = new UserRequest
                {
                    EmailId = savedRes.Email,
                    FirstName = savedRes.FullName.Split(" ")[0],
                    LastName = savedRes.FullName.Split(" ")[1],
                    Status = 1,
                    Password = GenerateRandomPassword(10),
                };
                await _userService.SaveUserAsync(user, cancellationToken);
                var bodyContent = $@"
                    <html>
                   <body>
                      <h1>Password Informations</h1>
                      <p>User Name : {user.EmailId}.</p>
                      <p>User Name : {user.Password}.</p>
                   </body>
                </html>
                ";
                await _emailService.SendEmailAsync(user.EmailId, bodyContent, "Password Informations");
                return new GenericResponse<EmployeeDto>(_entityMapperService.Map<Employees, EmployeeDto>(savedRes), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<EmployeeDto>(null, true, StatusCodes.Status204NoContent);
        }

        public async Task<GenericResponse<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _entityMapperService.Map<EmployeeDto, Employees>(employeeDto);
            var savedRes = await _employeeRepository.UpdateEmployeeAsync(mappedEntity, cancellationToken);

            if (savedRes != null)
            {
                return new GenericResponse<EmployeeDto>(_entityMapperService.Map<Employees, EmployeeDto>(savedRes), true, StatusCodes.Status200OK);
            }
            return new GenericResponse<EmployeeDto>(null, true, StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Generates the random password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                password.Append(chars[index]);
            }

            return password.ToString();
        }
    }
}
#endregion