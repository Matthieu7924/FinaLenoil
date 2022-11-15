using System.ComponentModel.DataAnnotations;

namespace _06_Entity.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public double  Prix { get; set; }

        // Entity Framwork a besoin d'un constructeur sans paramètre
        public Product()
        {

        }

        public Product(int id, string description, double prix)
        {
            Id = id;
            Description = description;
            Prix = prix;
        }
    }
}
