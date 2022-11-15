using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace _06_Entity.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; } = String.Empty;

        [Required]
        public bool IsAdmin { get; set; }

        public User()
        {

        }

        public User(int id, string email, string password, bool isAdmin)
        {
            Id = id;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
