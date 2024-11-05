using Domain.Entities;
namespace Domain.DTO
{
    public struct UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Telefone { get; set; }
        public string Ativo { get; set; }
        public Perfil Perfil { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Permissao> Permissoes { get; set; }
    }
}
