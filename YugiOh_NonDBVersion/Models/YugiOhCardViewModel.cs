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
        smallCardImages = new List<string>();
        sets = new List<string>();
        smallCardImageUrl = new List<string>();
        List<ICardImage> images = card.GetAllImages();

        foreach (ICardImage cardImage in images)
        {
            if (cardImage.GetSmallImages() != null)
            {
                smallCardImages.Add(String.Format("data:image/png;base64,{0}",
                    Convert.ToBase64String(cardImage.GetSmallImages())));
            }
            smallCardImageUrl.Add(cardImage.GetSmallImageUrl());
        }
        List<ICardSet> cardSets = card.GetAllCardSets();
        foreach (ICardSet set in cardSets)
        {
            sets.Add(set.GetSetName());
        }
    }
}