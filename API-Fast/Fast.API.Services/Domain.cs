using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fast.API.Models;

namespace Fast.API.Services {
	public class Domain {
		private readonly Repo _Repo;

		public Domain(Repo repo) => _Repo = repo;

		public async Task<int> GetPointsForAmount(decimal amount) {
			int points = 0;

			if(amount > 100) {
				points = ((Convert.ToInt32(Math.Floor(amount)) - 100) * 2) + 50;
			}
			else if(amount > 50) {
				points = Convert.ToInt32(Math.Floor(amount)) - 50;
			}

			return points;
		}

		public async Task<IEnumerable<UserPoints>> GetUserPointsForPastNMonths(int numOfMonths) {
			List<UserPoints> response = new();

			DateTime before = DateTime.UtcNow;
			DateTime after = before.AddMonths(-numOfMonths);

			// Getting all the transactions by every user within the date range.
			IEnumerable<Transaction> transactions = await _Repo.GetTransactionsInDateRange(after.ToString("yyyy-MM-ddTHH:mm:ss.fff"), before.ToString("yyyy-MM-ddTHH:mm:ss.fff"));

			// Creating an ordered set based on the user's id.
			IEnumerable<int> userIds = transactions.Select(transaction => transaction.UserId).Distinct().OrderBy(id => id);

			foreach(var id in userIds) {
				// Grabbing just the current user's transactions.
				IEnumerable<Transaction> userTransactions = transactions.Where(transaction => transaction.UserId == id);

				
				Task<User> user = _Repo.GetUser(id);
				Task<int> points = _GetTotalPoints(userTransactions);
				// Adding 1 back to the numOfMonths because it comes in 1 less than it should.
				Task<IEnumerable<MonthPoints>> monthlyBreakdown = _GetMonthlyBreakdown(userTransactions, after, numOfMonths + 1);

				// Running these in parallel to improve performance.
				await Task.WhenAll(user, points, monthlyBreakdown);
				
				// Building the info for the current user and adding it to the list to be returned.
				response.Add(new UserPoints() {
					Username = user.Result.Username,
					Name = $"{user.Result.FirstName}{((user.Result.MiddleInitial == null) ? "" : $" {user.Result.MiddleInitial}.")}{((user.Result.LastName == null) ? "" : $" {user.Result.LastName}")}",
					Points = points.Result,
					MonthlyBreakdown = monthlyBreakdown.Result
				});
			}

			return response;
		}

		private async Task<IEnumerable<MonthPoints>> _GetMonthlyBreakdown(IEnumerable<Transaction> transactions, DateTime startDate, int numOfMonths) {
			List<MonthPoints> breakdown = new();

			for(int i = 0; i < numOfMonths; i += 1) {
				// Only getting the transactions for the current month.
				IEnumerable<Transaction> monthlyTransactions = transactions.Where(transaction => Convert.ToDateTime(transaction.CreateDate).Month == startDate.Month);
				int monthlyPoints = await _GetTotalPoints(monthlyTransactions);
				breakdown.Add(new MonthPoints() {
					Month = startDate.ToString("MMMM"),
					Points = monthlyPoints
				});
				startDate = startDate.AddMonths(1);
			}

			return breakdown;
		}

		private async Task<int> _GetTotalPoints(IEnumerable<Transaction> transactions) {
			int points = 0;
			foreach(var transaction in transactions) {
				if(transaction.Amount > 100) {
					points += ((Convert.ToInt32(Math.Floor(transaction.Amount)) - 100) * 2) + 50;
				}
				else if(transaction.Amount > 50) {
					points += Convert.ToInt32(Math.Floor(transaction.Amount)) - 50;
				}
			}
			return points;
		}
	}
}
