using System;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Ententies
{
    public class Product : IEntity
    {


        //Se for Id é automaticamente chave primária
        //Outro nome usa-se o key
        //[Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="The field{0} can contain {1} characters max lengh")]
        public string Name { get; set; }    


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }


        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }


        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty ? $"https://supershop20220902144406.azurewebsites.net/images/noimage.png"
            : $"https://supershopngrs.blob.core.windows.net/products/{ImageId}";

        
    }
}
