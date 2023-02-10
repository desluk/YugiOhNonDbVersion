namespace YugiOh_NonDBVersion.Models;

public class YugiOhCardViewModel
{
    public string name { get; set; }
    public string descrtion { get; set; }
    public int[] prices { get; set; }
    public List<string> sets { get; set; }
    public List<byte[]> smallCardImages { get; set; }
    public List<string> searchTypes { get; set; }
}