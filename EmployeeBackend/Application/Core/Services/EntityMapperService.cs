#region References
using AutoMapper;
using Application.Interfaces;
using Application.Core.Models.DTOs;
using Application.Core.Models.Requests;
using Domain.Entities;
#endregion

#region Namespace
namespace Application.Core.Services
{
    public class EntityMapperService : IEntityMapperService
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The mapper configuration
        /// </summary>
        private MapperConfiguration _mapperConfiguration;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMapperService" /> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public EntityMapperService(ITokenService tokenService)
        {
            _tokenService = tokenService;

            _configureMapper();
            _createMapper();
        }




        /// <summary>
        /// Configures the mapper.
        /// </summary>
        private void _configureMapper()
        {
            _mapperConfiguration = new MapperConfiguration(mapConfig =>
            {

                #region User Login
                mapConfig.CreateMap<UserRequest, Users>()
                       .BeforeMap((s, d) => d.CreatedOn = DateTime.UtcNow)
                       .ForMember(x => x.RoleId, m => m.MapFrom(t => t.RoleId))
                       .ForMember(x => x.FirstName, m => m.MapFrom(t => t.FirstName))
                       .ForMember(x => x.LastName, m => m.MapFrom(t => t.LastName))
                       .ForMember(x => x.EmailId, m => m.MapFrom(t => t.EmailId))
                       .ForMember(x => x.Password, m => m.MapFrom(t => t.Password))
                       .ForMember(x => x.PasswordSalt, m => m.MapFrom(t => t.PasswordSalt))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ReverseMap();

                mapConfig.CreateMap<UserDto, Users>()
                       .ForMember(x => x.FirstName, m => m.MapFrom(t => t.FirstName))
                       .ForMember(x => x.LastName, m => m.MapFrom(t => t.LastName))
                       .ForMember(x => x.EmailId, m => m.MapFrom(t => t.EmailId))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ReverseMap();
                #endregion

                #region Roles
                mapConfig.CreateMap<RoleDto, Roles>()
                       .BeforeMap((s, d) => s.CreatedOn = DateTime.UtcNow)
                       .ForMember(x => x.RoleId, m => m.MapFrom(t => t.Id))
                       .ForMember(x => x.Code, m => m.MapFrom(t => t.RoleCode))
                       .ForMember(x => x.Name, m => m.MapFrom(t => t.RoleName))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ForMember(x => x.IsDeleted, m => m.MapFrom(t => t.IsDeleted))
                       .ReverseMap();
                #endregion

                #region Permissions
                mapConfig.CreateMap<PermissionsDto, Permissions>()
                       .BeforeMap((s,d) => s.CreatedOn = DateTime.UtcNow)
                       .BeforeMap((s,d) => s.CreatedBy = Guid.Parse(_tokenService.DecodeUserToken()?.UserId ?? ""))
                       .ForMember(x => x.PermissionId, m => m.MapFrom(t => t.PermissionId))
                       .ForMember(x => x.Code, m => m.MapFrom(t => t.Code))
                       .ForMember(x => x.Name, m => m.MapFrom(t => t.Name))
                       .ForMember(x => x.IsDeleted, m => m.MapFrom(t => t.IsDeleted))
                       .ReverseMap();
                #endregion

                #region Employees
                mapConfig.CreateMap<EmployeeDto, Employees>()
                      .BeforeMap((s, d) => s.CreatedOn = DateTime.UtcNow)
                      .BeforeMap((s, d) => s.CreatedBy = Guid.Parse(_tokenService.DecodeUserToken()?.UserId ?? ""))
                      .ForMember(x => x.Email, m => m.MapFrom(t => t.Email))
                      .ForMember(x => x.EmployeeId, m => m.MapFrom(t => t.Id))
                      .ForMember(x => x.FullName, m => m.MapFrom(t => t.FullName))
                      .ForMember(x => x.Salary, m => m.MapFrom(t => t.Salary))
                      .ForMember(x => x.JoinedDate, m => m.MapFrom(t => t.JoinedDate))
                      .ForMember(x => x.IsDeleted, m => m.MapFrom(t => t.IsDeleted))
                      .ReverseMap();
                #endregion
            });
        }


        /// <summary>
        /// Creates the mapper.
        /// </summary>
        private void _createMapper()
        {
            _mapper = _mapperConfiguration.CreateMapper();
        }


        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
#endregion