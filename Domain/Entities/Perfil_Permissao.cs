using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Perfil_Permissao")]
    public class Perfil_Permissao: BaseEntity
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }


        public int Id_Perfil { get; set; }

        [ForeignKey("Id_Perfil")]
        public virtual Perfil Perfil { get; set; }


        public int Id_Permissao { get; set; }

        [ForeignKey("Id_Permissao")]
        public virtual Permissao Permissao { get; set; }


    }
}
