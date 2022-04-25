using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVendas.Models.Entities
{
    public class Client
    {
        [Key]
        [Display(Name = "Id")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Um cliente deve ter um nome")]
        [Display(Name = "Nome do Cliente")]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Um cliente deve ter um contato telefónico")]
        [Display(Name = "Contato")]
        [Column(TypeName = "varchar(20)")]
        public string Contact { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        [Column(TypeName = "varchar(500)")]
        public string Address { get; set; }

        [Display(Name = "Cidade")]
        [Column(TypeName = "varchar(20)")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Column(TypeName = "varchar(2)")]
        public string State { get; set; }

        [Display(Name = "CEP")]
        [Column(TypeName = "varchar(9)")]
        public string ZipCode { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
