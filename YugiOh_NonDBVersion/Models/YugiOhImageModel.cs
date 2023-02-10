using CardCore;

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
        //TODO need to get this to work
        return false;
    }
}