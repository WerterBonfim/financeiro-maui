using AppControleFinanceiro.InputsModel;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.Utils;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;
using Microsoft.Maui.Platform;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private readonly ITransactionRepository _repository;

    public TransactionAdd(ITransactionRepository repository)
    {
        _repository = repository;
        InitializeComponent();
    }

    private void Fechar_Tapped(object? sender, TappedEventArgs e)
    {
        KeyboardHelper.CloseKeyboard();
        Navigation.PopModalAsync();
    }

    private void OnSave(object? sender, EventArgs e)
    {
        var validation = TransactValidation();
        if (validation.IsFailed)
            return;

        _repository.Add(validation.Value);

        App.Current.MainPage.DisplayAlert("Mensagem", "Salvo com sucesso!", "Ok");
        Navigation.PopModalAsync();

        KeyboardHelper.CloseKeyboard();


        WeakReferenceMessenger.Default.Send(string.Empty);
    }

    

    private Result<Transaction> TransactValidation()
    {
        var type = IsIncome.IsChecked
            ? TransactionType.Income
            : TransactionType.Outcome;

        var inputModel = new TransactionInputModel(Name.Text, Value.Text, TransactionDate.Date, type);
        var validation = inputModel.Validate();
        if (validation.IsFailed)
        {
            ErrorSumary.Text = validation.Errors.First().Message;
            ErrorSumary.IsVisible = true;
            return validation;
        }

        ErrorSumary.Text = string.Empty;
        ErrorSumary.IsVisible = false;
        return Result.Ok(validation.Value);
    }


}