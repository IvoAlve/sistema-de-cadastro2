using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email {get; set;}
        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "Digite um celular válido")]
        public string Celular { get; set;}


    }
}
