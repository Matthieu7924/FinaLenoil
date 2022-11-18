using Microsoft.Build.Framework;

namespace Identity.ViewModels
{
    public class EditRoleViewModel
    {
        [Required]
        public string Id { get; set; } = String.Empty;

        [Required]
        public string RoleName { get; set; } = String.Empty;

        public List<string> Users { get; set; } = new();
    }
}
