using System.ComponentModel.DataAnnotations;

namespace WebVendas.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(64)]
        public string UserName { get; set; }

        [Required, MaxLength(64)]
        public string Password { get; set; }
    }
}
