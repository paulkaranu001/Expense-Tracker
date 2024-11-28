using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        // categoryId
        public int CategoryId { get; set; }

        public int Amount { get; set; } 

        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
