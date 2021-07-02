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

		public async Task<IEnumerable<UserPoints>> GetPointsPastNMonthsByUser(int numOfMonths) {
			List<UserPoints> response = new();

			DateTime now = DateTime.UtcNow;
			now = now.AddMonths(-numOfMonths);

			IEnumerable<Transaction> transactions = await _Repo.GetTransactionsAfterDate(now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));

			IEnumerable<int> userIds = transactions.Select(transaction => transaction.UserId).Distinct().OrderBy(id => id);
			foreach(var id in userIds) {
				int points = 0;
				User user = await _Repo.GetUser(id);
				IEnumerable<Transaction> userTransactions = transactions.Where(transaction => transaction.UserId == id);
				foreach(var transaction in userTransactions) {
					if(transaction.Amount > 100) {
						points += ((Convert.ToInt32(Math.Floor(transaction.Amount)) - 100) * 2) + 50;
					}
					else if(transaction.Amount > 50) {
						points += Convert.ToInt32(Math.Floor(transaction.Amount)) - 50;
					}
				}
				response.Add(new UserPoints() {
					Username = user.Username,
					Name = $"{user.FirstName}{((user.MiddleInitial == null) ? "" : $" {user.MiddleInitial}.")}{((user.LastName == null) ? "" : $" {user.LastName}")}",
					Points = points
				});
			}

			return response;
		}
	}
}
