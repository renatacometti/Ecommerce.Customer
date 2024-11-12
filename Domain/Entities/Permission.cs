using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("Permissao")]
    public class Permission : BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        [Required]
        [Column("Nome")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column("Descricao")]
        public string Description { get; set; }

        [Column("Ativo")]
        public bool Active { get; set; }

        public virtual ICollection<PermissionProfile> PermissionProfile { get; set; }
    }
}
