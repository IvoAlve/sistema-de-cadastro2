using SiteMVC.Data;
using SiteMVC.Models;

namespace SiteMVC.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {

            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;


        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new Exception("Contato inválido");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;
            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);
            if (contatoDB == null) throw new Exception("Contato inválido");
            _bancoContext.Contatos.Remove(contatoDB);
            _bancoContext.SaveChanges();
            return true;

        }
    }
}
