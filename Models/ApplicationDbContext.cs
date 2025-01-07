using Microsoft.EntityFrameworkCore;
namespace Expense_Tracker.Models

{
    public class ApplicationDbContext: DbContext
    {
        // constractor
        public ApplicationDbContext(DbContextOptions options):base(options)
        { }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
