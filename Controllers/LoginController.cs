using Microsoft.AspNetCore.Mvc;
using SiteMVC.Helper;
using SiteMVC.Models;
using SiteMVC.Repositorio;
using System;

namespace SiteMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                                    ISessao sessao,
                                                    IEmail email) 
        {

            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;


        }
        public IActionResult Index()
        {
            // se o usuario ja estiver logado, redireciona para home
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }
    
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
                try
                {
                    if (ModelState.IsValid)
                    {
                            UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                            


                            
                            if(usuario != null) 
                            {
                                    if (usuario.SenhaValida(loginModel.Senha))
                                    {
                            _sessao.CriarSessaoDoUsuario(usuario);
                                         return RedirectToAction("Index", "Home");

                                    }

                                    TempData["MensagemErro"] = $"Senha inválida, tente novamente.";


                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválidos, tente novamente.";

                }

                return View("Index");

                }
                catch(Exception ex) 
                {
                        TempData["MensagemErro"] = $"Não foi possivel fazer o login, detalhamento: {ex.Message}";
                        return RedirectToAction("Index");
                }
        }
        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);


                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova Senha", mensagem);
                        if(emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Sua nova senha está no email cadastrado!";


                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não foi possivel enviar o e-mail de redefinição";

                        }


                        return RedirectToAction("Index", "Login");

                    }
                    TempData["MensagemErro"] = $"Não foi possivel redefinir sua senha.";

                }

                return View("Index");

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possivel redefinir sua senha, detalhamento: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

    }

}
