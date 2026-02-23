namespace RoleBasedAuth.Models.DTOs
{
    /// <summary>Admin/User szerepkör váltás: userName alapján vált a célfelhasználó.</summary>
    public class ToggleAdminRoleDto
    {
        public string? UserName { get; set; }
    }
}
