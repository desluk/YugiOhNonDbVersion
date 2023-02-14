using System.Globalization;
using CardCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YugiOh_NonDBVersion.Models;

namespace YugiOh_NonDBVersion.Constants;

public static class LoadingCardsFromFile
{
    /*
     * File setup will be the following "location from appSettings.json" + CardName
     *
     * All contents is saved as a Json file. The images are saved as a base64 string which will need to be decoded to byte[]
     */

    public static List<CardBase> LoadAllTheCards(string location,TradingCardType tradingCardType)
    {
        List<CardBase> allCardsInDirectory = new List<CardBase>();

        switch (tradingCardType)
        {
            case TradingCardType.YugiOh:
                GetAllYugiOhCardsFromDirectory(location, allCardsInDirectory);
                break;
            case TradingCardType.MagicTheGathering:
            case TradingCardType.Pokemon:
                default:
                allCardsInDirectory.Add(new YugiOhCardModel());
                break;
        }
        
        return allCardsInDirectory;
    }

    private static void GetAllYugiOhCardsFromDirectory(string location, List<CardBase> allCardBases)
    {
        string[] dictionaries = Directory.GetDirectories(location);
        foreach (string dictionary in dictionaries)
        {
            string[] dicCards = Directory.GetFiles(dictionary);
            foreach (string cardPath in dicCards)
            {
                YugiOhCardModel card = (YugiOhCardModel) LoadCard(cardPath,TradingCardType.YugiOh);
                allCardBases.Add(card);
            }
        }
    }

    public static CardBase LoadCard(string location,TradingCardType tradingCardType)
    {
        CardBase cardToLoad = null;

        string finalLocation = CheckFileIsThere(location);
        
        if (string.CompareOrdinal(String.Empty, finalLocation) == 0)
            return cardToLoad;

        switch (tradingCardType)
        {
            case TradingCardType.YugiOh:
                cardToLoad = SetupYugiOhCard(finalLocation);
                break;
            case TradingCardType.MagicTheGathering:
            case TradingCardType.Pokemon:
                default:
                cardToLoad = new YugiOhCardModel();
                return null;
        }

        return cardToLoad;
    }
    
    public static CardBase LoadCard(string cardName, string location,TradingCardType tradingCardType)
    {
        CardBase cardToLoad = null;

        string finalLocation = CheckFileIsThere(cardName, location);

        if (string.CompareOrdinal(String.Empty, finalLocation) == 0)
            return cardToLoad;
        
        switch (tradingCardType)
        {
            case TradingCardType.YugiOh:
                cardToLoad = SetupYugiOhCard(finalLocation);
                break;
            case TradingCardType.MagicTheGathering:
                case TradingCardType.Pokemon:
                return null;
        }

        return cardToLoad;
    }

    private static string CheckFileIsThere(string location)
    {
        if (File.Exists(location))
        {
            return location;
        }

        return string.Empty;
    }
    
    private static string CheckFileIsThere(string cardName, string location)
    {
        if (location[location.Length - 1] == '\\' || location[location.Length - 1] == '/')
        {
            if (File.Exists(location + cardName+"\\"+cardName+".json"))
            {
                return location + cardName + "\\" + cardName + ".json";
            }
            else if(File.Exists(location + cardName+"/"+cardName+".json"))
            {
                return location + cardName + "/" + cardName + ".json";
            }
        }

        if (location.Contains('\\'))
        {
            if (File.Exists(location +"\\"+ cardName))
                return location + "\\" + cardName+"\\"+cardName+".json";
        }

        if (location.Contains('/'))
        {
            if (File.Exists(location +"/"+ cardName))
                return location + "/" + cardName+"/"+".json";
        }

        return string.Empty;
    }

    private static YugiOhCardModel  SetupYugiOhCard(string location)
    {
        YugiOhCardModel card = new YugiOhCardModel();
        JToken cardToken = ReadCard(location);
        card.cardId = (int)(cardToken["CardId"]!);
        card.cardName = (string)cardToken["CardName"]!;
        card.SetCardType(YugiOhEnums.ConvertStringToCardTypes((string)cardToken["CardType"]!));
        card.SetCardFrameType(YugiOhEnums.ConvertStringToCardFrameTypes((string)cardToken["CardFrame"]!));
        card.SetCardDescription((string)cardToken["CardDescription"]!);
        card.SetAttack((int)cardToken["CardAttack"]!);
        card.SetDefense((int)cardToken["CardDefence"]!);
        card.SetLevel((int)cardToken["CardLevel"]!);
        card.SetCardRace(YugiOhEnums.ConvertStringToCardRace((string)cardToken["CardRace"]!));
        card.SetCardAttribute(YugiOhEnums.ConvertStringToCardAttribute((string)cardToken["CardAttribute"]!));

        if ((JArray)cardToken["CardImages"] != null)
        {
            JArray images = (JArray)cardToken["CardImages"];
            foreach (JToken token in images)
            {
                YugiOhImageModel cardImage = new YugiOhImageModel();
                cardImage.SetImageId((int)token["imageId"]!);
                cardImage.SetLargeImageUrl((string)token["smallImageUrl"]!);
                cardImage.SetSmallImageUrl((string)token["largeImageUrl"]!);
                cardImage.SetLargeImage(Convert.FromBase64String((string)token["largeImage"]));
                cardImage.SetSmallImage(Convert.FromBase64String((string)token["smallImage"]));
                card.AddAndUpdateCardImage(cardImage);
            }
        }
        if ((JArray)cardToken["CardSets"] != null)
        {
            JArray cardSets = (JArray)cardToken["CardSets"];
            foreach (JToken token in cardSets)
            {
                YugiOhSetModel cardSet = new YugiOhSetModel();
                cardSet.SetSetCode((string)token["setCode"]!);
                cardSet.SetSetName((string)token["setName"]!);
                cardSet.SetSetPrice((string)token["setPrice"]!);
                cardSet.SetSetRarity((string)token["setRarity"]!);
                cardSet.SetSetRarityCode((string)token["setRarityCode"]!);
                card.AddAndUpdateCardSet(cardSet);
            }
        }
        
        ICardPrice priceModel = new YugiOhPriceModel();
        JToken priceToken = (JToken)cardToken["CardPrice"];
        double price = (double)priceToken["amazonPrice"]!;
        priceModel.SetAmazonPrice(price.ToString(CultureInfo.CurrentCulture));
        price = (double)priceToken["eBayPrice"]!;
        priceModel.SetEbayPrice(price.ToString(CultureInfo.CurrentCulture));
        price = (double)priceToken["marketPrice"]!;
        priceModel.SetMarketPrice(price.ToString(CultureInfo.CurrentCulture));
        price = (double)priceToken["coolPrice"]!;
        priceModel.SetCoolStuffPrice(price.ToString(CultureInfo.CurrentCulture));
        price = (double)priceToken["tcgPlayerPrice"]!;
        priceModel.SetTcgPlayerPrice(price.ToString(CultureInfo.CurrentCulture));
        card.SetCardPrice(priceModel);
        
        return card;
    }

    private static JToken ReadCard(string location)
    {
        using (StreamReader reader = File.OpenText(location))
        {
            using (JsonTextReader fileReader = new JsonTextReader(reader))
            {
                return JToken.ReadFrom(fileReader);
            }
        }
    }
}