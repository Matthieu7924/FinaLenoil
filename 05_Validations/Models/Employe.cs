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
        [CustomAvatarValidation]
        public IFormFile? Avatar { get; set; }
    }
    public class CustomAvatarValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int maxContentLength = 1024 * 1024; // 1 MB
            string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };


            if (value is not IFormFile file)
            {
                ErrorMessage = "Please upload a file";
                return false;
            }

            if (file.Length > maxContentLength)
            {
                ErrorMessage = $"Your photo is too large, maximu size is {maxContentLength/1024/1024} MB.";
                return false;
            }


            // c/blabla/uploads/file.name.ext

            int last = file.FileName.LastIndexOf('.'); // Retourne l'index de la dernière occurence du raractère '.'

            string extension = file.FileName.Substring(last); // Retourne l'extension du fichier

            if (!allowedExtensions.Contains(extension))
            {
                ErrorMessage = $"Please upload a photo of type : {String.Join(", ", allowedExtensions)}.";

                return false;
            }
            return true;
        }

    }

}
