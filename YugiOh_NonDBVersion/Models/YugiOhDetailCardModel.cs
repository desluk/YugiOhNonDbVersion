using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhDetailCardModel
{
    public string name { get; set; }
    public string desc { get; set; }
    public string attack { get; set; }
    public string defence { get; set; }
    public string type { get; set; }
    public int level { get; set; }
    public List<string> setNames { get; set; }
    public List<string> largeImages { get; set; }
   

    public YugiOhDetailCardModel(YugiOhCardModel card)
    {
        name = card.cardName;
        desc = card.GetCardDescription();
        attack = card.GetAttack().ToString();
        defence = card.GetDefense().ToString();
        type = card.GetCardTypeString();
        level = card.GetLevel();
        setNames = new List<string>();
        largeImages = new List<string>();
      
        foreach (ICardSet set in card.GetAllCardSets())
        {
            setNames.Add(set.GetSetName());
        }

        foreach (ICardImage cardImage in card.GetAllImages())
        {
            largeImages.Add(String.Format("data:image/png;base64,{0}",
                Convert.ToBase64String(cardImage.GetLargeImages())));
        }
    }
}