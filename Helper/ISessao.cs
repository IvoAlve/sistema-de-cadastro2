using SiteMVC.Models;

namespace SiteMVC.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);

        void RemoverSessaoDoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();
    }
}
