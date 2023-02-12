using System.Buffers.Text;
using CardCore;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YugiOh_NonDBVersion.Models;

namespace YugiOh_NonDBVersion.Constants;

public static class SaveCardsToFile
{
    
    /*
     * File setup will be the following "location from appSettings.json" + CardName
     * 
     *  Within the above file location, there will be a .json with the cardName as well as an Image Folder that will contain two folders of 'Large' and 'Small'
     *  the images will be saved as .png just to not worry about compression. 
     */

    public static bool SaveCard(CardBase card,string location)
    {
        
        string cardFolderLocation = DoesFileExistAlready(card.cardName, location);
        if (cardFolderLocation == string.Empty)
            return false;

        try
        {
            if (card is YugiOhCardModel yugiOhCard)
            {
                SaveTheJsonFile(yugiOhCard, cardFolderLocation);
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    private static void SaveTheJsonFile(YugiOhCardModel card, string location)
    {
        MakeSureImagesArePresent(card);
        JObject jCard = new JObject();

        JArray jImages = new JArray();
        foreach (ICardImage cardImage in card.GetAllImages())
        {
            JObject j = new JObject();
            j.Add("imageId", cardImage.GetImageId());
            j.Add("smallImageUrl",cardImage.GetSmallImageUrl());
            j.Add("largeImageUrl",cardImage.GetLargeImageUrl());
            j.Add("smallImage",Convert.ToBase64String(cardImage.GetSmallImages()));
            j.Add("largeImage",Convert.ToBase64String(cardImage.GetLargeImages()));
            jImages.Add(j);
        }
        jCard.Add("CardImages",jImages);

        JArray jCardSets = new JArray();
        foreach (ICardSet cardSet in card.GetAllCardSets())
        {
            JObject j = new JObject();
            j.Add("setName", cardSet.GetSetName());
            j.Add("setCode", cardSet.GetSetCode());
            j.Add("setRarity", cardSet.GetSetRarity());
            j.Add("setRarityCode", cardSet.GetRarityCode());
            j.Add("setPrice", cardSet.GetStringPrice());
            jCardSets.Add(j);

        }

        jCard.Add("CardSets",jCardSets);

        JObject priceModel = new JObject();
        priceModel.Add("tcgPlayerPrice",card.GetCardPrice().GetTcgPlayerPrice());
        priceModel.Add("marketPrice",card.GetCardPrice().GetMarketPrice());
        priceModel.Add("eBayPrice",card.GetCardPrice().GetEBayPrice());
        priceModel.Add("amazonPrice",card.GetCardPrice().GetAmazonPrice());
        priceModel.Add("coolPrice",card.GetCardPrice().GetCoolStuffPrice());
        jCard.Add("CardPrice",priceModel);
        
        jCard.Add("CardName",card.cardName);
        jCard.Add("CardId",card.cardId);
        jCard.Add("CardDescription",card.GetCardDescription());
        jCard.Add("CardDefence",card.GetDefense());
        jCard.Add("CardAttack",card.GetAttack());
        jCard.Add("CardLevel",card.GetLevel());
        jCard.Add("CardType",card.GetCardTypeString());
        jCard.Add("CardRace",card.GetCardRaceString());
        jCard.Add("CardAttribute",card.GetCardAttributeString());
        jCard.Add("CardFrame",card.GetCardFrameTypeString());

        string jsonPath = location + card.cardName + ".json";
        using (StreamWriter file = File.CreateText(jsonPath))
        {
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                jCard.WriteTo(writer);
            }
        }
        
    }

    public static void MakeSureImagesArePresent(YugiOhCardModel card)
    {
        List<ICardImage> cardImages = card.GetAllImages();

        foreach (ICardImage cardImage in cardImages)
        {
            if (cardImage is YugiOhImageModel images)
            {
                if (cardImage.GetLargeImages() != null)
                {
                    if (cardImage.GetLargeImages().Length <= 0)
                    {
                        cardImage.GetImagesFromUrls();
                    }
                }
                else
                {
                    cardImage.GetImagesFromUrls();
                }
            }
        }
    }


    private static string DoesFileExistAlready(string cardName, string location)
    {

        if (location[location.Length - 1] == '\\' || location[location.Length - 1] == '/')
        {
            if (!Directory.Exists(location + cardName))
            {
                Directory.CreateDirectory(location + cardName);
            }
        }

        if (location.Contains('\\'))
        {
            if (!Directory.Exists(location +"\\"+ cardName))
            {
                Directory.CreateDirectory(location +"\\"+ cardName);
            }
            return location + "\\" + cardName+"\\";
        }

        if (location.Contains('/'))
        {
            if (!Directory.Exists(location +"/"+ cardName))
            {
                Directory.CreateDirectory(location +"/"+ cardName);
            }
            return location + "/" + cardName+"/";
        }

        return string.Empty;
    }
}