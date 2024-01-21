#region References
using Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
#endregion

#region Namespaces
namespace Domain.Entities
{
    public class Employees: BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
#endregion
