using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Usuario")] 
    public class User: BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Nome")]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        [Column("Cpf")]
        public string Cpf { get; set; }

        [Required]
        [Column("Data_Nascimento")]
        public DateTime DateBirth { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Telefone")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Email")]
        public string Email { get; set; }


        [Required]
        [MaxLength(100)]
        [Column("Senha")]
        public string Password { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("Ativo")]
        public string Active { get; set; }   

        public virtual ICollection<Address> Addresses { get; set; }

        public int Id_Profile { get; set; }

        [ForeignKey("Id_Perfil")]
        public virtual Profile Profile { get; set; }


    }
}
