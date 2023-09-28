using Microsoft.AspNetCore.Mvc;
using SiteMVC.Filters;
using SiteMVC.Helper;
using SiteMVC.Models;
using SiteMVC.Repositorio;

namespace SiteMVC.Controllers
{
    [PaginaParaUsuarioLogado]

    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao )
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
           List<ContatoModel> contatos =  _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }


        public  IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";

                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possivel apagar o contato";

                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possivel apagar, detalhamento: {ex.Message}";
                return RedirectToAction("Index");
            }
            
        }


        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    contato = _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);

            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Contato não cadastrado, detalhamento: {ex.Message}";
                return RedirectToAction("Index");
            }
           

        }
        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    contato =  _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Contato não atualizado, detalhamento: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
