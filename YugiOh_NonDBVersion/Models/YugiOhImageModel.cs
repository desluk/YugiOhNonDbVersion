using CardCore;
using YugiOh_NonDBVersion.Constants;

namespace YugiOh_NonDBVersion.Models;

public class YugiOhImageModel: ICardImage
{
    private string smallImageUrl;
    private string largeImageUrl;
    private int imageId;
    private byte[] smallImage;
    private byte[] largeImage;
    
    public string GetSmallImageUrl()
    {
        return smallImageUrl;
    }

    public string GetLargeImageUrl()
    {
        return largeImageUrl;
    }

    public byte[] GetSmallImages()
    {
        return smallImage;
    }

    public byte[] GetLargeImages()
    {
        return largeImage;
    }

    public int GetImageId()
    {
        return imageId;
    }

    public void SetSmallImageUrl(string smallImageUrl)
    {
        this.smallImageUrl = smallImageUrl;
    }

    public void SetLargeImageUrl(string largeImageUrl)
    {
        this.largeImageUrl = largeImageUrl;
    }

    public void SetImageId(int imageId)
    {
        this.imageId = imageId;
    }

    public bool GetImagesFromUrls()
    {
        YugiOhConnection connection = new YugiOhConnection();
        List<string> listOfImages = new List<string>() { smallImageUrl, largeImageUrl };
        Dictionary<string, byte[]> finalImages = new Dictionary<string, byte[]>();
        
        finalImages = connection.GetImagesFromListOfUrl(listOfImages);
        if (finalImages.Count <= 0)
            return false;

        foreach (KeyValuePair<string,byte[]> imageCollection in finalImages)
        {
            if (string.CompareOrdinal(imageCollection.Key, smallImageUrl) == 0)
            {
                smallImage = imageCollection.Value;
                continue;
            }
            if (string.CompareOrdinal(imageCollection.Key, largeImageUrl) == 0)
            {
                largeImage = imageCollection.Value;
                continue;
            }
        }

        return true;
    }
}