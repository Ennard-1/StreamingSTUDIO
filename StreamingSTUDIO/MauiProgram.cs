﻿using StreamingSTUDIO.Services;

namespace StreamingSTUDIO;

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
            });

       
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddSingleton<ApiService>();

        return builder.Build();
    }
}
