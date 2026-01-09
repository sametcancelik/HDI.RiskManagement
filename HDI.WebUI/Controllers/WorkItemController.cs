using Microsoft.AspNetCore.Mvc;

namespace HDI.WebUI.Controllers;

public class WorkItemController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}