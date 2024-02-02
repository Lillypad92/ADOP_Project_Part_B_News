//#define UseNewsApiSample  // Remove or undefine to use your own code to read live data

using System.Net;
using System.Net.Http.Json;
using ADOP_Project_Part_B_News.Models;

namespace ADOP_Project_Part_B_News.Services
{
    public class NewsService
    {
        HttpClient httpClient = new HttpClient();
        //Your API key
        readonly string apiKey = "1c2ed5ab06ce461d91e907f429d953aa";

        public NewsService()
        {
            httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<News> GetNewsAsync(NewsCategory category)
        {
#if UseNewsApiSample
            NewsApiData nd = await NewsApiSampleData.GetNewsApiSampleAsync(category);
            
#else
            try
            {
                //https://newsapi.org/docs/endpoints/top-headlines
                var uri = $"https://newsapi.org/v2/top-headlines?country=se&category={category}";

                // make the http request
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await httpClient.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();

                //Convert Json to Object
                NewsApiData nad = await response.Content.ReadFromJsonAsync<NewsApiData>();
#endif
                var news = new News()
                {
                    Category = category,
                    Articles = nad.Articles.Select(ni => new NewsItem()
                    {
                        DateTime = ni.PublishedAt,
                        Title = ni.Title,
                        Url = ni.Url,
                        UrlToImage = string.IsNullOrEmpty(ni.UrlToImage) ? "news_image.png" : ni.UrlToImage //Incase UrlImage is null, a local image from project map Images is showing instead.
                    }).ToList()
                };
                return news;
            }
            catch (Exception e)
            {
                var errorNews = new News();
                errorNews.HasError = true;
                errorNews.ErrorMessage = e.Message;
                return errorNews;
            }
        }
    }
}
