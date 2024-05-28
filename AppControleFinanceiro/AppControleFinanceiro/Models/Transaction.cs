using LiteDB;

namespace AppControleFinanceiro.Models
{
    public class Transaction
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset Date { get; set; }

        public Transaction()
        {
            
        }

        public Transaction(string name, decimal value, DateTimeOffset date, TransactionType type)
        {
            Name = name;
            Value = value;
            Type = type;
            Date = date;
        }
        
    }
    
    public enum TransactionType
    {
        Income,
        Outcome
    }
}
