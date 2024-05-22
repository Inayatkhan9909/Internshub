using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Internshub.Models
{
    public class UsersModel 
    {
        [Required(ErrorMessage = "Username field is empty")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be 3-30 characters")]
        public string username { get; set; }
        [Required(ErrorMessage = "Email field is empty")]
        public string email { get; set; }
         
        [Required(ErrorMessage = "Password field is empty")]
        //[RegularExpression(@"^(?=.*[0-9!@#&(){}[\\]:;',?/*~$^+=<>])(?=.*[a-zA-Z])[a-zA-Z0-9!@#&(){}[\\]:;',?/*~$^+=<>]{6,15}$\r\n")]
        public string password{ get; set; }
        [Required(ErrorMessage = "First Name field is empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name field is empty")]
        public string LastName { get; set; }
       
        public string Phone { get; set; }

        public string DOB { get; set; }
       
        public string Address { get; set; }
      
        public string City { get; set; }
       
        public string State { get; set; }
   
        public string PostalCode { get; set; }
     
        public string Country { get; set; }
      
        public string Qualification { get; set; }
       
        public string Skills { get; set; }

    }
}
