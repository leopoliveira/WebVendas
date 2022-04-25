using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVendas.Models.Entities
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public int SaleNumber { get; set; }

        public int ClientId { get; set; }

        [Required(ErrorMessage = "Uma venda deve possuir um cliente")]
        [ForeignKey("ClientId")] //Indica que é uma propriedade de navegação e o que fará o link será ClientId
        public Client Client { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; }

        [Display(Name = "Valor Total")]
        public double TotalValue { get; set; }

        public DateTime Date { get; set; }

        public bool Closed { get; set; } = false;
    }
}
