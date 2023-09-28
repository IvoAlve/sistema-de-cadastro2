using Microsoft.AspNetCore.Mvc;
using SiteMVC.Filters;

namespace SiteMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
