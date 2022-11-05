using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Ententies
{
    public class User : IdentityUser
    { 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        public int CityId { get; set; }

        //Chave estrangeira não pode ter nulos
        public City City { get; set; }



        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
