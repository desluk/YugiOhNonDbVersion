
using System.Runtime.InteropServices;
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
    private YugiOhCreateCardModel createCardModel;

    public YugiOhCardController(IOptions<Settings> settings)
    {
        this.settings = settings.Value;
        createCardModel = new YugiOhCreateCardModel();
        string userName = Environment.UserName;
        this.settings.linuxFilePathLocation = this.settings.linuxFilePathLocation.Replace("[user]", userName);
        this.settings.windowsFilePathLocation = this.settings.windowsFilePathLocation.Replace("[user]", userName);
        
    }

    //Get
    public IActionResult Create()
    {
        return View(createCardModel);
    }

    [HttpPost]
    public IActionResult Create(string? cardName, string? cardSearchType)
    {
        if (string.IsNullOrEmpty(cardName) || string.IsNullOrEmpty(cardSearchType))
            return View(createCardModel);

        if (ModelState.IsValid)
        {
            if (DoesCardAlreadyExist(cardName) && cardSearchType.Contains("name"))
            {
                TempData["success"] = "Card already exists please use the Detailed Refresh";
                return RedirectToAction("Index");
            }

            GetAndSaveCards(cardName, cardSearchType);
        }
        else
        {
            TempData["error"] = "Card was not saved";
            return View(createCardModel);
        }
        TempData["success"] = "Card was saved";
        return RedirectToAction("Index");
    }

    public IActionResult RefreshCard(string? cardName)
    {
        if (string.IsNullOrEmpty(cardName))
            return RedirectToAction("Index");

        if (ModelState.IsValid)
        {
            GetAndSaveCards(cardName,"Name Search");
        }

        return RedirectToAction("DetailedCard");
    }
    
    private void GetAndSaveCards(string cardName, string cardSearchType)
    {
        YugiOhConnection connection = new YugiOhConnection(cardName, YugiOhEnums.ConvertStringToSearchTerm(cardSearchType));

        JToken cardToken = connection.ConnectToWebsiteWithJson();

        JArray check = (JArray)cardToken["data"]!;
        foreach (JToken token in check)
        {
            YugiOhCardModel card = new YugiOhCardModel();
            card.CreateCardFromJson(token);
            if (!string.IsNullOrEmpty(card.cardName))
                SaveCardsToFile.SaveCard(card, GetLocationPath());
        }
    }


    public IActionResult DetailedCard(string? cardName)
    {
        if (string.IsNullOrEmpty(cardName))
        {
            return RedirectToAction("Index");
        }

        YugiOhDetailCardModel detailedView = new YugiOhDetailCardModel(
            (YugiOhCardModel)LoadingCardsFromFile.LoadCard(cardName, GetLocationPath(), TradingCardType.YugiOh));
        
        
        return View(detailedView);
    }
    
    public IActionResult Delete(String? cardName)
    {
        if (string.IsNullOrEmpty(cardName))
        {
            return RedirectToAction("Index");
        }

        RemoveCardInFile.DeleteCard(cardName, GetLocationPath());
        
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        List<YugiOhCardViewModel> listOfCards = ConvertToViewModel(LoadingCardsFromFile.LoadAllTheCards(GetLocationPath(),TradingCardType.YugiOh));

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

    private string GetLocationPath()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return settings.linuxFilePathLocation;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return settings.windowsFilePathLocation;
        }

        return settings.linuxFilePathLocation;
    }
    
    private bool DoesCardAlreadyExist(string cardName)
    {
        string finalpath = LoadingCardsFromFile.CheckFileIsThere(cardName, GetLocationPath());
        if (string.IsNullOrEmpty(finalpath))
        {
            return false;
        }

        return System.IO.File.Exists(finalpath);

    }
}