using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async IActionResult Index()
        {
            // Last 7 Days
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransations = await _context.Transactions;

            return View();
        }
    }
}
