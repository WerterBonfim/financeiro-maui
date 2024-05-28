using System.Globalization;
using AppControleFinanceiro.InputsModel;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private readonly ITransactionRepository _repository;
    private int _transactionId;

    public TransactionEdit(ITransactionRepository repository)
    {
        _repository = repository;
        InitializeComponent();
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transactionId = transaction.Id;
        Name.Text = transaction.Name;
        Value.Text = transaction.Value.ToString(CultureInfo.CurrentCulture);
        TransactionDate.Date = transaction.Date.Date;
        
        if (transaction.Type == TransactionType.Income)
            IsIncome.IsChecked = true;
        else
            IsOutcome.IsChecked = true;
    }

    private void Fechar_Tapped(object? sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void Salvar(object? sender, EventArgs e)
    {
        var validation = TransactValidation();
        if (validation.IsFailed)
            return;
        
        _repository.Update(validation.Value);
        Navigation.PopModalAsync();
        
        WeakReferenceMessenger.Default.Send(string.Empty);
    }
    
    private Result<Transaction> TransactValidation()
    {
        var type = IsIncome.IsChecked
            ? TransactionType.Income
            : TransactionType.Outcome;

        var inputModel = new TransactionInputModel(Name.Text, Value.Text, TransactionDate.Date, type, _transactionId);
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