using CardCore;
using Microsoft.CodeAnalysis;
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

    private static void SaveTheJsonFile(YugiOhCardModel card, string lcoation)
    {
       
    }


    private static string DoesFileExistAlready(string cardName, string location)
    {

        if (location[location.Length - 1] == '\\' || location[location.Length - 1] == '/')
        {
            if (!File.Exists(location + cardName))
            {
                File.Create(location + cardName);
                
            }
        }

        if (location.Contains('\\'))
        {
            if (!File.Exists(location +"\\"+ cardName))
            {
                File.Create(location +"\\"+ cardName);
            }
            return location + "\\" + cardName+"\\";
        }

        if (location.Contains('/'))
        {
            if (!File.Exists(location +"/"+ cardName))
            {
                File.Create(location +"/"+ cardName);
            }
            return location + "/" + cardName+"/";
        }

        return string.Empty;
    }
}