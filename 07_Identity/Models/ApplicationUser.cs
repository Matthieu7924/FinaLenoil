using Microsoft.AspNetCore.Identity;

namespace _07_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; } = String.Empty;
    }
}
