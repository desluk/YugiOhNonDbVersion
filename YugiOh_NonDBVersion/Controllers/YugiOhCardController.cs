
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using YugiOh_NonDBVersion.Constants;
using YugiOh_NonDBVersion.Models;


namespace YugiOh_NonDBVersion.Controllers;

public class YugiOhCardController: Controller
{
    
    private readonly Settings settings;

    public YugiOhCardController(IOptions<Settings> settings)
    {
        this.settings = settings.Value;
        string userName = Environment.UserName;
        string linuxName = this.settings.linuxFilePathLocation.Replace("[user]", userName);
        this.settings.linuxFilePathLocation = this.settings.linuxFilePathLocation.Replace("[user]", userName);
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

        if (ModelState.IsValid)
        {
            
            YugiOhCardModel card = new YugiOhCardModel();
            YugiOhConnection connection = new YugiOhConnection(cardName, SearchTerm.NameSearch);

            JToken cardToken = connection.ConnectToWebsiteWithJson();
            if (cardToken == null)
            {
                TempData["error"] = "Card was not saved at all";
                return View();
            }
            
            JArray check = (JArray)cardToken["data"]!;
            foreach (JToken token in check)
            {
                card.CreateCardFromJson(token);
                if (!string.IsNullOrEmpty(card.cardName))
                {
                    SaveCardsToFile.SaveCard(card, settings.linuxFilePathLocation);
                }  
            }
        }
        else
        {
            TempData["error"] = "Card was not saved at all";
            return View();
        }

        return View("Index");
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
        List<YugiOhCardModel> listOfCards = new List<YugiOhCardModel>();
        string[] dictionaries = Directory.GetDirectories(settings.linuxFilePathLocation);
        foreach (string dictionary in dictionaries)
        {
            string[] dicCards = Directory.GetFiles(dictionary);
            foreach (string cardPath in dicCards)
            {
                YugiOhCardModel card = (YugiOhCardModel)LoadingCardsFromFile.LoadCard(cardPath,TradingCardType.YugiOh);
                listOfCards.Add(card);
            }
        }
        return View();
    }
    
}