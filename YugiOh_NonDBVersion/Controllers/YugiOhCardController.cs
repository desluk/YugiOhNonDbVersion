using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using YugiOh_NonDBVersion.Constants;
using YugiOh_NonDBVersion.Models;

namespace YugiOh_NonDBVersion.Controllers;

public class YugiOhCardController: Controller
{
    
    private readonly Settings settings;

    public YugiOhCardController(IOptions<Settings> settings)
    {
        this.settings = settings.Value;
    }

    //Get
    public IActionResult Create(string? cardName, int searchType)
    {
        YugiOhCardModel card = new YugiOhCardModel();

        return View();
    }

    public IActionResult Delete(String cardName)
    {
        return View();
    }
    
}