namespace AppControleFinanceiro.Utils;

public static class LiteDbHelper
{
    public static string GetDatabasePath(string fileName)
        => Path.Combine(FileSystem.AppDataDirectory, fileName);
}