namespace YugiOh_NonDBVersion.Constants;

public static class RemoveCardInFile
{
    public static bool DeleteCard(string cardName, string location)
    {
        string finalPath = CheckFileIsThere(cardName,location);
        if (string.IsNullOrEmpty(finalPath))
            return false;

        try
        {
            File.Delete(finalPath);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
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
}