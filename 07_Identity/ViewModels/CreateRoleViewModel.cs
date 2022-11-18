using Microsoft.Build.Framework;

namespace Identity.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; } = String.Empty;
    }
}
