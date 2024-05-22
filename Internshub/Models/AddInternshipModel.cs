using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class AddInternshipModel
    {

        public int Id { get; set; }
        public string Internship_Name { get; set; }
        public string Internship_Skills { get; set; }
        public string Internship_details { get; set; }
        public string IntershipPicUrl { get; set; }
        public int Duration { get; set; }
        public string InternshipType { get; set; }
        public string InternshipMode { get; set; }
        public string Responsibilites{ get; set; }
        public string MinQualification { get; set; }
    }
}
