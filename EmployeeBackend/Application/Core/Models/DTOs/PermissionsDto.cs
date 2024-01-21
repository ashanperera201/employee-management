#region Namespace

namespace Application.Core.Models.DTOs
{
    public class PermissionsDto
    {
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
#endregion