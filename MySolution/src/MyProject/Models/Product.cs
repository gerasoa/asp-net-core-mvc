using MyProject.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        public string? Image { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        public decimal? Price { get; set; }
    }
}
