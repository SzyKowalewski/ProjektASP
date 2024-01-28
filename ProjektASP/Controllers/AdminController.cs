using System.Web.Mvc;

namespace TwojaAplikacja.Controllers
{
    [Authorize(Roles = "Admin")] // Jeśli wymagane jest uwierzytelnianie
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}