using System.ComponentModel.DataAnnotations;

namespace _06_Entity.ViewModel
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Your Email")]
        public string FromEmail { get; set; } = String.Empty;

        //[EmailAddress]
        //public string ToEmail { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Body { get; set; } = String.Empty;

        [DataType(DataType.Upload)]
        public List<IFormFile>? Attachments { get; set; }
    }
}
