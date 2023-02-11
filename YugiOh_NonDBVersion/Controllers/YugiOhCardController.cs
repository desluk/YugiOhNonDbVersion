
using CardCore;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string? cardName, int cardSearchType)
    {
        if (cardName == null)
            return View();


        YugiOhCardModel card = new YugiOhCardModel();
   //     YuGiOhConnection connection = new YuGiOhConnection(cardName, SearchTerm.NameSearch);
    //    card.CreateCardFromJson(connection.ConnectToWebsiteWithJson());
        if (!string.IsNullOrEmpty(card.cardName))
            SaveCardsToFile.SaveCard(card, settings.linuxFilePathLocation);
        TempData["success"] = "Card " + card.cardName + " has been saved";


        return View();
    }

    public IActionResult Update(string? cardName, int searchType)
    {
        return View("Index");
    }
    
    public IActionResult Delete(String cardName)
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
    
}