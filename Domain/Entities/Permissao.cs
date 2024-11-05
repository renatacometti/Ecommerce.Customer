using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("Permissao")]
    public class Permissao : BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [Column("Nome")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(255)]
        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Perfil_Permissao> Perfil_Permissao { get; set; }
    }
}
