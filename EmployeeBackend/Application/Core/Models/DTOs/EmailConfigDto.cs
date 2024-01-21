#region References
#endregion

#region Namespace
namespace Application.Core.Models.DTOs
{
    public class EmailConfigDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public string Port { get; set; }
    }
}
#endregion