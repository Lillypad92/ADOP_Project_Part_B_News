using ADOP_Project_Part_B_News.Models;
using System.Web;

namespace ADOP_Project_Part_B_News.Views
{
    public partial class ArticleView : ContentPage
    {
        
        public ArticleView(NewsItem newsItem)
        {
            InitializeComponent();
            BindingContext = new UrlWebViewSource
            {
                Url = HttpUtility.UrlDecode(newsItem.Url), 
            };
        }

        /*Took the code from this method and moved to the method above.
         By doing so the article showed up when clicked on the specified news article from the list.*/

        //public ArticleView(string Url)
        //{
        //    InitializeComponent();
        //    BindingContext = new UriImageSourceService
        //    {
                
        //    };
        //}

    }
}
