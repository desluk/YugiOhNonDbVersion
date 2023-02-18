namespace YugiOh_NonDBVersion.Models;

public class YugiOhCreateCardModel
{
    public List<string> searchTermList = new List<string>();
    public string cardName;
    public string cardSearchType;
    
    public YugiOhCreateCardModel()
    {
        searchTermList.Add("Fuzzy Search");
        searchTermList.Add("Name Search");
        searchTermList.Add("Type Search");
        searchTermList.Add("Attribute Search");
        searchTermList.Add("Cardset Search");
        searchTermList.Add("Format Search");
        searchTermList.Add("Stable Search");
        searchTermList.Add("Arhcetype Search");
    }
}