using Microsoft.AspNetCore.Mvc;

namespace HDI.WebUI.Controllers;

public class AgreementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}