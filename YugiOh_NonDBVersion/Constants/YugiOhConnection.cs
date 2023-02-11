using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using BasicConnection;
using CardSearchApi.Debug;
using static YugiOh_NonDBVersion.Constants.YugiOhEnums;

namespace YugiOh_NonDBVersion.Constants;

public class YugiOhConnection : BaseCardConnection
{
            #region Private Consts Variables
        private const string QuestionMark = "?";
        private const string FuzzySearch = "fname";
        private const string DirectNameSearch = "name";
        private const string Archetype = "archetype";
        private const string Sort = "sort";
        private const string AndSign = "&";
        private const string Attribute = "attribute";
        private const string EqualSign = "=";
        private const string Level = "level";
        private const string Format = "format";
        private const string CardSet = "cardset";
        private const string TypeSearch = "type";

        #endregion

        #region Private Variables
        private string searchName = null;
        private string sortSearchName = null!;
        private bool hasSortTerm = false;
        private SearchTerm SearchTerms = SearchTerm.FuzzySearch;
        private readonly List<string> cardNames = new List<string>();
        #endregion

        #region Public Variables
        public List<string> GetNameOfCardsFound => cardNames;
        public string GetUrlParameters => CreateUrlParameters();
        #endregion
        
        #region Constructors
        public YugiOhConnection(string searchName, SearchTerm term)
        {
            hasSortTerm = false;
            SearchTerms = term;
            this.searchName = searchName;
            httpClient = new HttpClient();
            apiUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";
        }
        
        public YugiOhConnection(string cardNameToSearch, SearchTerm termToSearchCardBy, string sortSearchName,
            bool sort = false)
        {
            searchName = cardNameToSearch;
            SearchTerms = termToSearchCardBy;
            hasSortTerm = sort;
            if (hasSortTerm)
            {
                this.sortSearchName = sortSearchName;
            }
            httpClient = new HttpClient();
            apiUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";
        }
        
        
        #endregion

        #region Public Methods
        public override JToken ConnectToWebsiteWithJson()
        {
            JToken? resultOfConnection = null;
            httpClient.BaseAddress = new Uri(apiUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage responseMessage = httpClient.GetAsync(CreateUrlParameters()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                resultOfConnection = ResultOfConnection(responseMessage);
                httpClient.CancelPendingRequests();
            }
            else
            {
               
                httpClient.CancelPendingRequests();
            }
            
            return resultOfConnection;
        }

        public override byte[] GetImageFromImageUrl(string imageUrl)
        {
            byte[] imageArray = null;

            imageClient = new HttpClient();
            
            try
            {
                imageArray = imageClient.GetByteArrayAsync(imageUrl).Result;
            }
            catch (Exception e)
            {
                DebugLog.WriteDebugLog("There was an issue loading the card: "+ imageUrl);
                DebugLog.WriteDebugLog("Issue was: "+e.Message);
            }
            return imageArray;  
        }

        public override Dictionary<string, byte[]> GetImagesFromListOfUrl(List<string> imageUrls)
        {
            Dictionary<string, byte[]> imageArray = new Dictionary<string, byte[]>();

            foreach (string imageUrl in imageUrls)
            {
                imageArray.Add(imageUrl,GetImageFromImageUrl(imageUrl));
            }
            
            return imageArray;
        }

        #endregion

        #region Private Methods
        
        private  JToken? ResultOfConnection(HttpResponseMessage responseMessage)
        {
           return  JToken.Parse(responseMessage.Content.ReadFromJsonAsync<JsonObject>().Result!.ToJsonString());
        }


        private string CreateUrlParameters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(QuestionMark);

            stringBuilder.Append(ConvertingSearchTermToString(SearchTerms));

            stringBuilder.Append(EqualSign);
            stringBuilder.Append(searchName.Replace(" ","%20"));
            
            if (hasSortTerm)
            {
                stringBuilder.Append(AndSign);
                stringBuilder.Append(EqualSign);
                stringBuilder.Append(sortSearchName);
            }
            return stringBuilder.ToString();
        }

        #endregion
}