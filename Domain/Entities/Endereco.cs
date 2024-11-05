using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Endereco")]
    public class Endereco: BaseEntity 
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Rua")]
        public string Rua { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Numero")]
        public string Numero { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Bairro")]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Cidade")]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Uf")]
        public string Uf { get; set; }

        [Required]
        [MaxLength(8)]
        [Column("Cep")]
        public string Cep { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Nome_Endereco")]
        public string Nome_Endereco { get; set; }

        public int Id_Cliente { get; set; }

        [ForeignKey("Id_Cliente")]
        public virtual Usuario Cliente { get; set; }
     

    }
    
}
