﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        [Range(1, int.MaxValue, ErrorMessage = "You must Select a City.")]
        public int CityId { get; set; }

        [Display(Name = "First Name")]
        [Range(1, int.MaxValue, ErrorMessage = "You must Select a Country.")]
        public int CountryId { get; set; }

        //Chave estrangeira não pode ter nulos
        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

    }
}
