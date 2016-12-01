using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AngularController : Controller
    {
        [HttpGet]
        public IActionResult Template(string name)
        {
            return PartialView(name);
        }
    }
}