using CardCore;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Newtonsoft.Json;
using YugiOh_NonDBVersion.Constants;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhCardModel : CardBase
{
private int cardAttack;
    private int cardDefense;
    private int cardLevel;

    private CardType cardType;
    private CardAttributes cardAttribute;
    private CardFrameType cardFrameType;
    private CardRace cardRace;
    


    public int GetAttack() => cardAttack;
    public int GetDefense() => cardDefense;

    public override ICardSet GetACardSet(string setName, string setCode)
    {
        return cardSets.FirstOrDefault(x => String.CompareOrdinal(x.GetSetCode(), setCode) == 0)!;
    }

    public override void CreateCardFromJson(JToken jsonObject)
    {
        
        cardId = (int)(jsonObject["id"] ?? cardId);
        cardName = (string)(jsonObject["name"] ?? cardName)!;
        cardType = YugiOhEnums.ConvertStringToCardTypes((string)jsonObject["type"]!);
        cardFrameType = YugiOhEnums.ConvertStringToCardFrameTypes((string)jsonObject["frameType"]!);
        cardDescription = (string)jsonObject["desc"]!;
        cardAttack = (int)jsonObject["atk"]!;
        cardDefense = (int)jsonObject["def"]!;
        cardLevel = (int)jsonObject["level"]!;
        cardRace = YugiOhEnums.ConvertStringToCardRace((string)jsonObject["race"]!);
        cardAttribute = YugiOhEnums.ConvertStringToCardAttribute((string)jsonObject["attribute"]!);

        if ((JArray)jsonObject["card_sets"] != null)
            SetupCardSets((JArray)jsonObject["card_sets"]);
        if ((JArray)jsonObject["card_images"] != null)
            SetupCardImages((JArray)jsonObject["card_images"]);
        if ((JArray)jsonObject["card_prices"] != null)
            SetupCardPrices((JArray)jsonObject["card_prices"]);
    }

    private void SetupCardPrices(JArray? jArray)
    {
        foreach (JToken token in jArray)
        {
            cardPrice = new YugiOhPriceModel();
            double price = (double)token["amazon_price"]!;
            cardPrice.SetAmazonPrice(price.ToString(CultureInfo.CurrentCulture));
            price = (double)token["ebay_price"]!;
            cardPrice.SetEbayPrice(price.ToString(CultureInfo.CurrentCulture));
            price = (double)token["cardmarket_price"]!;
            cardPrice.SetMarketPrice(price.ToString(CultureInfo.CurrentCulture));
            price = (double)token["coolstuffinc_price"]!;
            cardPrice.SetCoolStuffPrice(price.ToString(CultureInfo.CurrentCulture));
            price = (double)token["tcgplayer_price"]!;
            cardPrice.SetTcgPlayerPrice(price.ToString(CultureInfo.CurrentCulture));
        }
    }

    private void SetupCardImages(JArray? jArray)
    {
        foreach (JToken token in jArray)
        {
            YugiOhImageModel cardImage = new YugiOhImageModel();
            cardImage.SetImageId((int)token["id"]!);
            cardImage.SetLargeImageUrl((string)token["image_url"]!);
            cardImage.SetSmallImageUrl((string)token["image_url_small"]!);
            cardImages.Add(cardImage);
        }
    }

    private void SetupCardSets(JArray jArray)
    {
        foreach (JToken token in jArray)
        {
            YugiOhSetModel cardSet = new YugiOhSetModel();
            cardSet.SetSetCode((string)token["set_code"]!);
            cardSet.SetSetName((string)token["set_name"]!);
            cardSet.SetSetPrice((string)token["set_price"]!);
            cardSet.SetSetRarity((string)token["set_rarity"]!);
            cardSet.SetSetRarityCode((string)token["set_rarity_code"]!);
            cardSets.Add(cardSet);
        }
    }

    public override void CreateCardFromJsonString(string jsonString)
    {
        JToken jsonToken = JToken.Parse(jsonString);
        if (jsonToken != null)
        {
            //TODO
        }
    }

    /// <summary>
    /// This Method will create a General Folder Called YugiOhCards, this will then contain a folder for each card.
    /// The images if chosen to will also be stored within the folder as well.
    ///
    /// You can choose either a Linux or Windows pathing.
    /// </summary>
    /// <param name="locationPathIfWindows">This will be the path that will be stored in (not inlcuding the YugiohCards folder)</param>
    /// <param name="isUsingLinux">If you are using a Linux or Windows based system</param>
    public override void CreateAJsonFileAndImageFolderForCard(bool isUsingLinux = true,
        string locationPathIfWindows = "")
    {
        string folderPath = GetFolderPath(isUsingLinux, locationPathIfWindows);
    }

    private string GetFolderPath(bool isUsingLinux, string locationPath)
    {
        if (isUsingLinux)
        {
            string userName = Environment.UserName;
            return "/home/" + userName + "/Documents/";
        }
        else
        {
            return locationPath;
        }
    }
}