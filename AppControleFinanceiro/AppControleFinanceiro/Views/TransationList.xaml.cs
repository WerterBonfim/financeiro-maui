using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.Utils;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransationList : ContentPage
{
    private readonly ITransactionRepository _repository;

    public TransationList(ITransactionRepository transactionRepository)
    {
        _repository = transactionRepository;

        InitializeComponent();
        LoadTransactions();

        WeakReferenceMessenger.Default.Register<string>(this, (_, _) => LoadTransactions());

    }

    private T Get<T>() => Handler!.MauiContext!.Services.GetService<T>()!;

    private void LoadTransactions()
    {
        var itens = _repository.GetAll();


        TransactionsCollectionView.ItemsSource = itens;

        var income = itens
            .Where(x => x.Type == TransactionType.Income).Sum(x => x.Value);

        var outcome = itens
            .Where(x => x.Type == TransactionType.Outcome).Sum(x => x.Value);
        LabelBalance.Text = $"{income - outcome:C}";
        LabelIncome.Text = $"{income:C}";
        LabelOutcome.Text = $"{outcome:C}";

    }

    private void AddTransaction(object? sender, EventArgs e)
        => Navigation.PushModalAsync(Get<TransactionAdd>());

    private void EditTransaction(object? sender, EventArgs e)
        => Navigation.PushModalAsync(Get<TransactionEdit>());

    private void ItemTapped_EditTransaction(object? sender, TappedEventArgs e)
    {
        if (sender is null) return;
        var grid = (Grid)sender;
        var transaction = ((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter as Transaction;


        var transactionEdit = Get<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        KeyboardHelper.CloseKeyboard();
        Navigation.PushModalAsync(transactionEdit);
    }




    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is null) return;

        await AnimationBorder((Border)sender);

        var result = await App.Current.MainPage.DisplayAlert("Excluir!", "Deseja realmente excluir?", "Sim", "Não");

        if (!result)
        {
            await RollbackAnimation((Border)sender);
            return;
        }

        if (e.Parameter == null) return;


        var transaction = (Transaction)e.Parameter;
        _repository.Delete(transaction);
        LoadTransactions();


    }

    private string _oldLabel = string.Empty;
    private Color _borderOriginalBackgroundColor;
    private const int TimeDelay = 200;

    private async Task AnimationBorder(Border border)
    {
        var label = ((Label)border.Content!)!;


        _borderOriginalBackgroundColor = border.BackgroundColor;
        _oldLabel = label.Text;

        await border.RotateYTo(90, TimeDelay);
        border.BackgroundColor = Colors.Red;


        label.Text = "X";
        label.TextColor = Colors.White;

        await border.RotateYTo(180, TimeDelay);

    }


    private async Task RollbackAnimation(Border border)
    {
        await border.RotateYTo(90, TimeDelay);
        border.BackgroundColor = _borderOriginalBackgroundColor;

        var label = ((Label)border.Content!)!;

        label.Text = _oldLabel;
        label.TextColor = Colors.Black;

        await border.RotateYTo(0, TimeDelay);

    }

}