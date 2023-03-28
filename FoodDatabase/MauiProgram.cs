using FoodDatabase.Data;
using FoodDatabase.ViewModels;
using FoodDatabase.Mvvm;
using Microsoft.EntityFrameworkCore;

namespace FoodDatabase;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

        builder

            .UseMauiApp<App>()

			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})

			.Services.AddDbContextFactory<FoodContext>(opts =>
			{
				opts.UseSqlite($"Filename=foods.sqlite3");
			})            

			.AddMvvm()

			.AddSingleton<AppShell>()

			.AddTransient<MainPage>()
			.WithViewModel<MainPageViewModel>()

			.AddTransient<SearchPage>()
			.WithViewModel<SearchViewModel>()

			.AddTransient<PowerFoodsPage>()
			.WithViewModel<PowerFoodsViewModel>()

			.AddTransient<FoodPage>()
			.WithViewModel<FoundationFoodViewModel>()

			.AddSingleton<DataLoader>();					

		return builder.Build();
	}
}
