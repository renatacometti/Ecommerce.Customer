using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Usuario")] 
    public class Usuario: BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Nome")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(11)]
        [Column("Cpf")]
        public string Cpf { get; set; }

        [Required]
        [Column("Data_Nascimento")]
        public DateTime Data_Nascimento { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Telefone")]
        public string Telefone { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Email")]
        public string Email { get; set; }


        [Required]
        [MaxLength(100)]
        [Column("Senha")]
        public string Senha { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("Ativo")]
        public string Ativo { get; set; }   

        public virtual ICollection<Endereco> Enderecos { get; set; }

        public int Id_Perfil { get; set; }

        [ForeignKey("Id_Perfil")]
        public virtual Perfil Perfil { get; set; }


    }
}
