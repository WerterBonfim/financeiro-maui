namespace AppControleFinanceiro.Shared
{
    public class BasePage : ContentPage
    {
        public void Fechar_Tapped(object? sender, TappedEventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
