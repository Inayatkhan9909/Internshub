using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class EnrollResumeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email field is empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone field is empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address field is empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Skills field is empty")]
        public string Skills { get; set; }
        [Required(ErrorMessage = "Qualification field is empty")]
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Appliedfor field is empty")]
        public string Appliedfor { get; set; }

        [Required(ErrorMessage = "Resume field is empty")]
        public IFormFile Resumefile { get; set; }
    }
}
