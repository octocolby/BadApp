using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OctoGoat.Data;
using OctoGoat.Models;

namespace OctoGoat.Controllers;

public class TweeterController : Controller
{
    private readonly ILogger<TweeterController> _logger;
    private readonly DatabaseContext _DbContext = new DatabaseContext();

    public TweeterController(ILogger<TweeterController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(TweeterModel tweeterModel)
    {
        if (tweeterModel.Name == null)
        {
            return View();
        }
        _DbContext.TweeterModels.Add(tweeterModel);
        _DbContext.SaveChanges();
        var first = _DbContext.TweeterModels.First();
        return View(first);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}