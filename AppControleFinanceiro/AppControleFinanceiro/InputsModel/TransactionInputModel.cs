using AppControleFinanceiro.Models;
using FluentResults;

namespace AppControleFinanceiro.InputsModel;

public record TransactionInputModel(string Name, string Value, DateTimeOffset Date, TransactionType TransactionType, int? Id = null)
{
    public Result<Transaction> Validate()
    {
        if (string.IsNullOrEmpty(Name))
            return Result.Fail("O campo 'Nome' deve ser preenchido");

        if (string.IsNullOrEmpty(Value))
            return Result.Fail("O campo 'Valor' deve ser obrigatório");

        var priceValid = decimal.TryParse(Value, out decimal value);
        if (!priceValid) return Result.Fail("O campo 'Valor' tem um valor inválido");

        var newTransaction = new Transaction(
            Name,
            Math.Abs(value),
            Date,
            TransactionType
        );


        if (Id != null)
            newTransaction.Id = Id.Value;

        return newTransaction;
    }
}