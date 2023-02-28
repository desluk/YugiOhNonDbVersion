using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhCardViewModel
{
    public string name { get; set; }
    public string descrtion { get; }
    public double[] prices { get; }
    public List<string> sets { get; }
    public List<string> smallCardImages { get; }
    public List<string> smallCardImageUrl { get; }
    public double highestPrice { get; set; }

    public YugiOhCardViewModel(YugiOhCardModel card)
    {
        name = card.cardName;
        descrtion = card.GetCardDescription();
        ICardPrice priceModel = card.GetCardPrice();
        if (priceModel != null)
            prices = new[]
            {
                priceModel.GetAmazonPrice(), priceModel.GetMarketPrice(), priceModel.GetCoolStuffPrice(),
                priceModel.GetCoolStuffPrice(), priceModel.GetEBayPrice(), priceModel.GetTcgPlayerPrice()
            };
        else
            prices = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

        foreach (var VARIABLE in prices)
        {
            if (highestPrice < VARIABLE)
                highestPrice = VARIABLE;
        }
        
        smallCardImages = new List<string>();
        sets = new List<string>();
        smallCardImageUrl = new List<string>();
        List<ICardImage> images = card.GetAllImages();
        
            if (card.GetAllImages()[0].GetSmallImages() != null)
            {
                smallCardImages.Add(String.Format("data:image/png;base64,{0}",
                    Convert.ToBase64String(card.GetAllImages()[0].GetSmallImages())));
            }
            smallCardImageUrl.Add(card.GetAllImages()[0].GetSmallImageUrl());
        
        List<ICardSet> cardSets = card.GetAllCardSets();
        if (cardSets.Count <= 0)
        {
            ICardSet temp = new YugiOhSetModel();
            temp.SetSetName("No Set");
            cardSets.Add(temp);
        }
        foreach (ICardSet set in cardSets)
        {
            sets.Add(set.GetSetName());
        }
    }
}