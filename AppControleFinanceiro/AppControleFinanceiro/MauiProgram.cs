using AppControleFinanceiro.Configs;
using Microsoft.Extensions.Logging;

namespace AppControleFinanceiro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Add this section anywhere on the builder:
                .UseSentry(options => {
                    // The DSN is the only required setting.
                    options.Dsn = "https://0f631b5698b4af985db22baccf1f94b1@o4507334131777536.ingest.us.sentry.io/4507334133219328";

                    // Use debug mode if you want to see what the SDK is doing.
                    // Debug messages are written to stdout with Console.Writeline,
                    // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                    // This option is not recommended when deploying your application.
                    options.Debug = true;

                    // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                    // We recommend adjusting this value in production.
                    options.TracesSampleRate = 1.0;

                    // Sample rate for profiling, applied on top of othe TracesSampleRate,
                    // e.g. 0.2 means we want to profile 20 % of the captured transactions.
                    // We recommend adjusting this value in production.
                    options.ProfilesSampleRate = 1.0;
                    

                    // Other Sentry options can be set here.
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).RegisterServices();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
