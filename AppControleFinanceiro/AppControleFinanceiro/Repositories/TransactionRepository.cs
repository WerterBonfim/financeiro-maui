using AppControleFinanceiro.Models;
using LiteDB;

namespace AppControleFinanceiro.Repositories;
    

public interface ITransactionRepository
{
    public List<Transaction> GetAll();
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
}
    
    
public class TransactionRepository(ILiteDatabase liteDatabase) : ITransactionRepository
{
    private const string CollectionName = "Transactions";
    private ILiteCollection<Transaction> Transactions => liteDatabase.GetCollection<Transaction>(CollectionName);
    
    
    public List<Transaction> GetAll() =>
        Transactions
            .Query()
            .OrderByDescending(x => x.Date)
            .ToList();

    public void Add(Transaction transaction)
    {
        Transactions.Insert(transaction);
        Transactions.EnsureIndex(x => x.Date);
    }

    public void Update(Transaction transaction) => Transactions.Update(transaction);

    public void Delete(Transaction transaction) 
        => Transactions.Delete(transaction.Id);
}