using Microsoft.AspNetCore.Mvc;

namespace HDI.WebUI.Controllers;

public class AnalysisController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}