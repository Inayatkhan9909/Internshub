using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class loginUser
    {
        [Required (ErrorMessage = "Username field is empty")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password field is empty")]
        public string password { get; set; }

    }
}
