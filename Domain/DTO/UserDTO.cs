using Domain.Entities;
namespace Domain.DTO
{
    public struct UserDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Telefone { get; set; }
        public string Ativo { get; set; }
        public Profile Perfil { get; set; }
        public ICollection<AddressDTO> Enderecos { get; set; }
        public ICollection<PermissionDTO> Permissoes { get; set; }
    }
}
