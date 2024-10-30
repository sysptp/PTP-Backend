using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Empresa
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
