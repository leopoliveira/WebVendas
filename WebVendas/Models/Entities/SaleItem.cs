using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVendas.Models.Entities
{
    public class SaleItem
    {
        [Key]
        public int SaleItemId { get; set; }

        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [Required(ErrorMessage = "Um item deve conter um produto")]
        public int ProductId { get; set; } //Propriedade que faz o link com Produto

        [ForeignKey("ProductId")] //Aqui no lado do muitos (1-n) eu indico quem será a chave estrangeira
        public Product Product { get; set; } //Variável de navegação que não será criada no Banco de Dados por causa do atributo ForeignKey acima apontando para outra propriedade

        public double Value { get; set; }

        [Required(ErrorMessage = "Um item deve conter uma quantidade maior que zero")]
        public int Quantity { get; set; }

        [NotMapped] //Não cria a coluna no banco de dados
        public double TotalValue {
            get => (Value * Quantity);
        }
    }

}
