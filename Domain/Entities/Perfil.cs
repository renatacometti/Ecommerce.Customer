using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Perfil")]
    public class Perfil : BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Nome")]
        public string Nome { get; set; }

        [MaxLength(255)]
        [Column("Descricao")]
        public string Descrição { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Perfil_Permissao> Perfil_Permissao { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}
