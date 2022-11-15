using System.ComponentModel;
using System.Security.Cryptography;

namespace TagHelpers.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public double Salary { get; set; }

        [DisplayName("Actif")]
        public bool IsActif { get; set; } = true;
        public string Email { get; set; } = String.Empty;
        public EmployeType Type { get; set; } = EmployeType.STAGIAIRE;

        [DisplayName("Departement Id")]
        public int DepartementId { get; set; }
    }

    public enum EmployeType
    {
        STAGIAIRE = 1,
        JUNIOR = 2,
        SENIOR  = 3
    }
}
