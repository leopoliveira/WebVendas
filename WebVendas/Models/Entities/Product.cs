using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVendas.Models.Entities
{   
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Um produto deve ter um nome")]
        [Display(Name = "Nome do Produto")]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Um produto deve ter um valor unitário")]
        [Display(Name = "Valor Unitário")]
        public double Value { get; set; }

    }
}
