using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class AddInternshipImageModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is empty")]
        public string Internship_Name { get; set; }
        [Required(ErrorMessage = "Skills field is empty")]
        public string Internship_Skills { get; set; }

        [Required(ErrorMessage = "details field is empty")]
        public string Internship_details { get; set; }

        [Required(ErrorMessage = "Duration field is empty")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "Type field is empty")]
        public string InternshipType { get; set; }
        [Required(ErrorMessage = "Mode field is empty")]
        public string InternshipMode { get; set; }
        [Required(ErrorMessage = "Responsibilites field is empty")]
        public string Responsibilites { get; set; }
        [Required(ErrorMessage = "Minimum Qualification field is empty")]
        public string MinQualification { get; set; }

        [Required(ErrorMessage = "image field is empty")]
        public IFormFile IntershipPicFile { get; set; }
    }
}
