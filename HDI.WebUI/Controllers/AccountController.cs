using Microsoft.AspNetCore.Mvc;

namespace HDI.WebUI.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Logout()
    {
        return RedirectToAction("Login");
    }
}