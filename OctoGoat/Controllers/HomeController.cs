using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OctoGoat.Data;
using OctoGoat.Models;

namespace OctoGoat.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _DbContext = new DatabaseContext();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var tweeters = _DbContext.TweeterModels;
        var checkedTweeters = new List<TweeterModel>();
        foreach (var tweeter in tweeters)
        {
            var checkmark = _DbContext.CheckmarkModels.FirstOrDefault(check => check.Name == tweeter.Name);
            tweeter.Checkmark = checkmark is {Approved: true};
            checkedTweeters.Add(tweeter);
        }
        return View(checkedTweeters);
    }

    public IActionResult ApplyForCheckmark(CheckmarkModel checkmarkModel)
    {
        var f = _DbContext.Database.ExecuteSqlRaw("SELECT * FROM TweeterModels");
        if (checkmarkModel.Name != null)
        {
            var existingCheckmark = _DbContext.CheckmarkModels.FirstOrDefault(check => check.Name == checkmarkModel.Name);
            if (existingCheckmark == null)
            {
                _DbContext.CheckmarkModels.Add(checkmarkModel);
            }
            else
            {
                existingCheckmark.Approved = checkmarkModel.Approved;
                _DbContext.Update(checkmarkModel);
            }
            _DbContext.SaveChanges();
            return View(checkmarkModel);
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}
