using ADOP_Project_Part_B_News.Models;
using ADOP_Project_Part_B_News.Services;
using ADOP_Project_Part_B_News.Views;

namespace MauiApp1.Views;

public partial class NewsPage : ContentPage
{
    NewsService service;
    NewsCategory newsCategory;


    public NewsPage(NewsCategory category)
    {
        InitializeComponent();

        service = new NewsService();
        newsCategory = category;

        Title = category.ToString();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var news = await LoadNews();

        //Error handling
        if (news.HasError)
        {
            await DisplayAlert("Error", $"Something went wrong: {news.ErrorMessage}", "OK");
        }
    }

    private async Task<News> LoadNews()
    {
        var news = await service.GetNewsAsync(newsCategory);
        News.ItemsSource = news.Articles;
        return news;
    }

    private async void Refresh_Clicked(object sender, EventArgs e)
    {   
        var news = await LoadNews();

        //Error handling
        if (news.HasError) 
        {
            await DisplayAlert("Error", $"Something went wrong: {news.ErrorMessage}", "OK");
        }
    }

    private void News_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Navigation.PushAsync(new ArticleView((NewsItem)e.SelectedItem));
    }
   

  
}