using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class loginUser
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

    }
}
