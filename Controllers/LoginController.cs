using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using SiteMVC.Repositorio;
using System;

namespace SiteMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio) {

            _usuarioRepositorio = usuarioRepositorio;


        }
        public IActionResult Index()
        {
            return View();
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
                                         return RedirectToAction("Index", "Home");

                                    }
                                    TempData["MensagemErro"] = $"Senha inválidos, tente novamente.";


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

    }
}
