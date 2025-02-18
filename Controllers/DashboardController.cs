using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
          
        }
        public async Task<ActionResult> Index()
        {
            // Last 7 Days
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransations = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date < EndDate)
                .ToListAsync();
            //Total Income
            int TotalIncome = SelectedTransations
                .Where(i => i.Category.Type == "Income")
                .Sum(J => J.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            // Total Expense 
            int TotalExpense = SelectedTransations
                .Where(i => i.Category.Type == "Expense ")
                .Sum(J => J.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            // Balance 
            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString("C0");
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            // Doughnut chart- Expense by category 
            ViewBag.DoughnutChartData = SelectedTransations
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon+" "+ k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    FormattedAmount = k.Sum(j => j.Amount).ToString("C0"),
                })
                .ToList();
                

            return View();
        }
    }
}
