
using CardCore;
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

            JArray check = (JArray)cardToken["data"]!;
            foreach (JToken token in check)
            {
                card.CreateCardFromJson(token);
                if (!string.IsNullOrEmpty(card.cardName))
                    SaveCardsToFile.SaveCard(card, settings.linuxFilePathLocation);
            }
        }
        else
        {
            TempData["error"] = "Card was not saved at all";
            return View();
        }

        return RedirectToAction("Index");
    }

    public IActionResult DetailedCard(string? cardName)
    {
        if (string.IsNullOrEmpty(cardName))
        {
            return RedirectToAction("Index");
        }

        YugiOhDetailCardModel detailedView = new YugiOhDetailCardModel(
            (YugiOhCardModel)LoadingCardsFromFile.LoadCard(cardName, settings.linuxFilePathLocation, TradingCardType.YugiOh));
        
        
        return View(detailedView);
    }
    
    public IActionResult Delete(String cardName)
    {
        return View();
    }

    public IActionResult Index()
    {
        List<YugiOhCardViewModel> listOfCards = ConvertToViewModel(LoadingCardsFromFile.LoadAllTheCards(settings.linuxFilePathLocation,TradingCardType.YugiOh));

        return View(listOfCards);
    }

    private List<YugiOhCardViewModel> ConvertToViewModel(List<CardBase> cards)
    {
        List<YugiOhCardViewModel> tempCards = new List<YugiOhCardViewModel>();

        foreach (CardBase cardBase in cards)
        {
            if (cardBase is YugiOhCardModel yugiOhCardModel)
            {
                YugiOhCardViewModel tempCard = new YugiOhCardViewModel(yugiOhCardModel);
                tempCards.Add(tempCard);
            }
        }
        
        return tempCards;
    }
}