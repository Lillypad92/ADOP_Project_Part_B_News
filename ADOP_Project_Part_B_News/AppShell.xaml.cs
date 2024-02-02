using ADOP_Project_Part_B_News.Models;
using ADOP_Project_Part_B_News.Views;
using MauiApp1.Views;

namespace ADOP_Project_Part_B_News;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        foreach (var category in Enum.GetValues(typeof(NewsCategory)))
        {
            var categoryName = category.ToString();

            var sc = new ShellContent
            {
                Title = categoryName,
                Route = categoryName,
                ContentTemplate = new DataTemplate(() => new NewsPage((NewsCategory)category))
            };

            Items.Add(sc);

            //Routing.RegisterRoute(nameof(ArticleView), typeof(NewsPage));
        }
    }
}
