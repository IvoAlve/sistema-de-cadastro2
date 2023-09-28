using SiteMVC.Models;

namespace SiteMVC.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);

        bool Apagar(int id);
    }
}
