using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhCardViewModel
{
    
    public string name { get; set; }
    public string descrtion { get;  }
    public double[] prices { get;  }
    public List<string> sets { get;  }
    public List<byte[]> smallCardImages { get;  }
    public List<string> smallCardImageUrl { get; } 
    public List<string> searchTypes { get;  }

    public YugiOhCardViewModel(YugiOhCardModel card)
    {
        name = card.cardName;
        descrtion = card.GetCardDescription();
        ICardPrice priceModel = card.GetCardPrice();
        if(priceModel != null)
            prices = new[] { priceModel.GetAmazonPrice(), priceModel.GetMarketPrice(), priceModel.GetCoolStuffPrice(), priceModel.GetCoolStuffPrice(),priceModel.GetEBayPrice(),priceModel.GetTcgPlayerPrice() };
        else
            prices = new[] {0.0,0.0,0.0,0.0,0.0,0.0 };
        smallCardImages = new List<byte[]>();
        smallCardImageUrl = new List<string>();
        List<ICardImage> images = card.GetAllImages();
        if (images != null)
        {
            foreach (ICardImage cardImage in images)
            {
                if(cardImage.GetSmallImages() != null)
                    smallCardImages.Add(cardImage.GetSmallImages());
                smallCardImageUrl.Add(cardImage.GetSmallImageUrl());
            }    
        }
        
        
        
    }
}