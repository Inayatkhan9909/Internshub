using Microsoft.AspNetCore.Identity;

namespace Internshub.Models
{
    public class Users 
    {
       
        public string username { get; set; }
        public string email { get; set; }
       
       public  string Phone { get; set; }
        public string password{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
