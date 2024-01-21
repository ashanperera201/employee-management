#region References
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Core.Models.DTOs;
#endregion

#region Namespace
namespace API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/permissions")]
    [ApiController]
    public class PermissionController : Controller
    {
        /// <summary>
        /// The permission service
        /// </summary>
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="permissionService">The permission service.</param>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] string searchTerm, [FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var permissions = await _permissionService.GetPermissionsAsync(searchTerm, page, pageSize, cancellationToken);
                return StatusCode(permissions.Status, permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionName">Name of the permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{permissionName}")]
        public async Task<IActionResult> GetPermissionAsync(string permissionName, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _permissionService.GetPermissionAsync(permissionName, cancellationToken);
                return StatusCode(permission.Status, permission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SavePermissionAsync([FromBody] PermissionsDto permissions, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _permissionService.SavePermissionAsync(permissions, cancellationToken);
                return StatusCode(permission.Status, permission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }

        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionAsync([FromBody] PermissionsDto permissions, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _permissionService.UpdatePermissionAsync(permissions, cancellationToken);
                return StatusCode(permission.Status, permission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
    }
}
#endregion