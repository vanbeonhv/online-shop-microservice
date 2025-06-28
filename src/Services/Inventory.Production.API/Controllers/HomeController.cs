using Microsoft.AspNetCore.Mvc;

namespace Inventory.Production.API.Controllers;

public class HomeController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}