#region References
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Core.Models;
#endregion

#region Namespace

namespace Domain.Entities
{
    public class Permissions : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public UserStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
#endregion