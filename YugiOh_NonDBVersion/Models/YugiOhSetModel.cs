using CardCore;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhSetModel: ICardSet
{
    private string setName;
    private string setCode;
    private string setRarity;
    private string setRarityCode;
    private string setPrice;
    
    public void SetSetName(string setName)
    {
        this.setName = setName;
    }

    public void SetSetCode(string setCode)
    {
        this.setCode = setCode;
    }

    public void SetSetRarity(string setRarity)
    {
        this.setRarity = setRarity;
    }

    public void SetSetRarityCode(string setRarityCode)
    {
        this.setRarityCode = setRarityCode;
    }

    public void SetSetPrice(string setPrice)
    {
        this.setPrice = setPrice;
    }

    public string GetSetName()
    {
        return setName;
    }

    public string GetSetCode()
    {
        return setCode;
    }

    public string GetSetRarity()
    {
        return setRarity;
    }

    public string GetRarityCode()
    {
        return setRarityCode;
    }

    public string GetStringPrice()
    {
        return setPrice;
    }

    public double GetActualPrice()
    {
        return Convert.ToDouble(setPrice);
    }
}