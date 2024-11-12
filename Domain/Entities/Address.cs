using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Endereco")]
    public class Address: BaseEntity 
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Rua")]
        public string Street { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Numero")]
        public string Number { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Bairro")]
        public string District { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Cidade")]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Uf")]
        public string State { get; set; }

        [Required]
        [MaxLength(8)]
        [Column("Cep")]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Nome_Endereco")]
        public string AddressName { get; set; }

        public int Id_Cliente { get; set; }

        [ForeignKey("Id_Cliente")]
        public virtual User User { get; set; }
     

    }
    
}
