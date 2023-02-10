using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhPriceModel: ICardPrice
{
    private string marketPrice;
    private string tcgPlayerPrice;
    private string eBayPrice;
    private string amazonPrice;
    private string coolPrice;
    
    public void SetMarketPrice(string marketPrice)
    {
        this.marketPrice = marketPrice;
    }

    public void SetTcgPlayerPrice(string tcgPlayerPrice)
    {
        this.tcgPlayerPrice = tcgPlayerPrice;
    }

    public void SetEbayPrice(string eBayPrice)
    {
        this.eBayPrice = eBayPrice;
    }

    public void SetAmazonPrice(string amazonPrice)
    {
        this.amazonPrice = amazonPrice;
    }

    public void SetCoolStuffPrice(string coolStuffPrice)
    {
        this.coolPrice = coolStuffPrice;
    }

    public double GetMarketPrice()
    {
        return Convert.ToDouble(marketPrice);
    }

    public double GetTcgPlayerPrice()
    {
        return Convert.ToDouble(tcgPlayerPrice);
    }

    public double GetEBayPrice()
    {
        return Convert.ToDouble(eBayPrice);
    }

    public double GetAmazonPrice()
    {
        return Convert.ToDouble(amazonPrice);
    }

    public double GetCoolStuffPrice()
    {
        return Convert.ToDouble(coolPrice);
    }
}