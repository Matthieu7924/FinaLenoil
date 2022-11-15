using System.ComponentModel.DataAnnotations;

namespace Validations.Models
{
    public class Employe
    {
        [Required]
        [Display(Name = " User Names : ")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessage ="Mot de passe obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [Required(ErrorMessage = "Date de naissance obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required(ErrorMessage = "Email obligatoire")]
        // [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email invalide")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Evaluation obligatoire")]
        [Range(1,10)] // Doit être compris dans l'intervalle [1, 10]
        public int Evaluation { get; set; }

        [Required(ErrorMessage = "Numéro de téléphone obligatoire")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Numéro invalide")]
        public string NumeroTelephone { get; set; } = String.Empty;

        [Required(ErrorMessage = "Commentaire obligatoire")]
        [DataType(DataType.MultilineText)]
        public string Commentaire { get; set; } = String.Empty;

        [Display(Name = " Uploader avatar : ")]
        public IFormFile? Avatar { get; set; }
    }
}
