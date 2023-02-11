using CardCore;

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
        if (DoesFileExistAlready(card.cardName,location))
        {
            
        }
        else // File does not exist and thus needs to be created. 
        {
            
        }

        return true;
    }

    private static bool DoesFileExistAlready(string cardName, string location)
    {

        if (location[location.Length - 1] == '\\' || location[location.Length - 1] == '/')
        {
            return File.Exists(location + cardName);
        }

        if (location.Contains('\\'))
        {
            return File.Exists(location+"\\"+cardName);    
        }

        return File.Exists(location + "/" + cardName);
    }
}