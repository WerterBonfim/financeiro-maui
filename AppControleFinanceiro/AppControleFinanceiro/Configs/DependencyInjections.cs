using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.Views;
using LiteDB;

namespace AppControleFinanceiro.Configs;

public static class DependencyInjections
{
    private const string FinanceiroDatabaseName = "controle-financeiro.db";

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        var connectionString = $"Filename={Utils.LiteDbHelper.GetDatabasePath(FinanceiroDatabaseName)};Connection=shared";    
        builder.Services.AddSingleton<ILiteDatabase>(_ =>
            new LiteDatabase(connectionString)
        );


        builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

        builder.Services.AddTransient<TransactionAdd>();
        builder.Services.AddTransient<TransationList>();
        builder.Services.AddTransient<TransactionEdit>();
        return builder;
    }
}