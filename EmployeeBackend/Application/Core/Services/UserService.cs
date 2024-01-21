#region References
using Application.Interfaces;
using Application.Core.Models.DTOs;
using Application.Core.Models.Requests;
using Application.Core.Models.Responses;
using Application.Utils;
using Domain.Core.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Application.Models;
#endregion

namespace Application.Core.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="entityMapperService">The entity mapper service.</param>
        public UserService(
            IUserRepository userRepository,
            IEntityMapperService entityMapperService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _entityMapperService = entityMapperService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticates the user asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<UserAuthResponse>> AuthenticateUserAsync(AuthRequest auth, CancellationToken cancellationToken = default)
        {
            var response = new UserAuthResponse();
            var user = await _userRepository.GetUserByEmailAsync(auth.UserName, cancellationToken);
            if (user != null)
            {
                var mappedUser = _entityMapperService.Map<Users, UserDto>(user);
                var isValidPassword = CryptographyProcessor.AreEqual(auth.Password, user.Password, user?.PasswordSalt ?? "");
                if (isValidPassword)
                {
                    var tokenResult = _tokenService.GenerateTokenAsync(mappedUser);
                    if (tokenResult != null)
                    {
                        response.UserId = user.Id.ToString();
                        response.AccessToken = tokenResult.AccessToken;
                    }
                    return new GenericResponse<UserAuthResponse>(response, true, StatusCodes.Status200OK);
                }
                else
                {
                    return new GenericResponse<UserAuthResponse>(null, false, StatusCodes.Status401Unauthorized, "Password is invalid.");
                }
            }
            else
            {
                return new GenericResponse<UserAuthResponse>(null, false, StatusCodes.Status401Unauthorized, "User not found.");
            }
        }

        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<UserDto>> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            var userResult = await _userRepository.GetUserByEmailAsync(userEmail, cancellationToken);
            if (userResult != null)
            {
                var user = _entityMapperService.Map<Users, UserDto>(userResult);
                return new GenericResponse<UserDto>(user, true, StatusCodes.Status200OK);
            }
            return new GenericResponse<UserDto>(null, true, StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<UserDto>> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            var userResult = await _userRepository.GetUserById(userId, cancellationToken);
            if (userResult != null)
            {
                var user = _entityMapperService.Map<Users, UserDto>(userResult);
                return new GenericResponse<UserDto>(user, true, StatusCodes.Status200OK);
            }
            return new GenericResponse<UserDto>(null, true, StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<UserDto>> SaveUserAsync(UserRequest user, CancellationToken cancellationToken = default)
        {
            var existsUser = await _userRepository.GetUserByEmailAsync(user.EmailId, cancellationToken);

            if (existsUser != null)
            {
                return new GenericResponse<UserDto>(null, false, StatusCodes.Status400BadRequest, "User is already exists.");
            }


            string passwordSalt = CryptographyProcessor.CreateSalt(300);
            user.PasswordSalt = passwordSalt;
            user.Password = CryptographyProcessor.GenerateHash(user.Password, passwordSalt);


            var mappedResult = _entityMapperService.Map<UserRequest, Users>(user);
            var savedUser = await _userRepository.SaveUserAsync(mappedResult, cancellationToken);
            if (savedUser != null)
            {
                var userRes = _entityMapperService.Map<Users, UserDto>(savedUser);
                return new GenericResponse<UserDto>(userRes, true, StatusCodes.Status200OK);
            }
            return new GenericResponse<UserDto>(null, false, StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<UserDto>> UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.EmailId, cancellationToken);

            if (user != null)
            {
                user.Status = (UserStatus)request.Status;
                user.IsDeleted = request.IsDeleted;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.LastModifiedOn = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(request.RoleId))
                {
                    user.RoleId = Guid.Parse(request.RoleId.ToString());
                }

                var updatedUser = await _userRepository.UpdateUserAsync(user, cancellationToken);
                if (updatedUser != null)
                {
                    var userRes = _entityMapperService.Map<Users, UserDto>(updatedUser);
                    return new GenericResponse<UserDto>(userRes, false, StatusCodes.Status400BadRequest);
                }
            }
            return new GenericResponse<UserDto>(null, false, StatusCodes.Status400BadRequest);
        }
    }
}
