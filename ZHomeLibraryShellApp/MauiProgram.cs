﻿using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace ZHomeLibraryShellApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
            {
                var builder = MauiApp.CreateBuilder();
                builder
                    .UseMauiApp<App>()
                    //.UseLocalNotification()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

#if DEBUG
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
            else
            {
                var builder = MauiApp.CreateBuilder();
                builder
                    .UseMauiApp<App>()
                    .UseLocalNotification()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

#if DEBUG
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
            
        }
    }
}
