using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Perfil_Permissao")]
    public class PermissionProfile: BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("Id_Perfil")]
        public virtual Profile Profile { get; set; }

        public int PermissionId { get; set; }

        [ForeignKey("Id_Permissao")]
        public virtual Permission Permission { get; set; }


    }
}
