using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhPriceModel: ICardPrice
{
    private string marketPrice;
    private string tcgPlayerPrice;
    private string eBayPrice;
    private string amazonPrice;
    private string coolPrice;
    
    System.Globalization.NumberStyles style = System.Globalization.NumberStyles.AllowDecimalPoint;
    
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
        return double.Parse(marketPrice, style, System.Globalization.CultureInfo.CurrentCulture);
    }

    public double GetTcgPlayerPrice()
    {
        return double.Parse(tcgPlayerPrice, style, System.Globalization.CultureInfo.CurrentCulture);
    }

    public double GetEBayPrice()
    {
        return double.Parse(eBayPrice, style, System.Globalization.CultureInfo.CurrentCulture);
    }

    public double GetAmazonPrice()
    {
        return double.Parse(amazonPrice, style, System.Globalization.CultureInfo.CurrentCulture);
    }

    public double GetCoolStuffPrice()
    {
        return double.Parse(coolPrice, style, System.Globalization.CultureInfo.CurrentCulture);
    }
}