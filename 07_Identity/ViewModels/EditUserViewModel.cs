using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; } = String.Empty;

        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        public string City { get; set; } = String.Empty;

        public List<string> Roles { get; set; } = new();
    }
}
