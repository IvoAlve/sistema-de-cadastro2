using SiteMVC.Enums;
using SiteMVC.Helper;
using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]

        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o nome do login")]

        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Digite o senha do usuário")]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ContatoModel> Contatos { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();

        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
