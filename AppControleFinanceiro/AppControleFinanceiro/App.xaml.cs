using AppControleFinanceiro.Views;

namespace AppControleFinanceiro
{
    public partial class App : Application
    {
        public App(TransationList transationList)
        {
            InitializeComponent();

            MainPage = new NavigationPage(transationList);
        }
    }
}
