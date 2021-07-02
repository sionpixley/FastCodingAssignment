using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Fast.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fast.API.Services {
	public class Repo {
		private readonly string _ConnectionString;

		public Repo(string connectionString) => _ConnectionString = connectionString;

		public async Task<IEnumerable<Transaction>> GetTransactionsInDateRange(string afterDate, string beforeDate) {
			IEnumerable<Transaction> transactions;
			DynamicParameters parameters = new();

			string procedure = "[Fast].[dbo].[sp_Transaction_Get]";
			parameters.Add("@AfterDate", afterDate, DbType.String, ParameterDirection.Input);
			parameters.Add("@BeforeDate", beforeDate, DbType.String, ParameterDirection.Input);

			using(SqlConnection db = new(_ConnectionString)) {
				transactions = await db.QueryAsync<Transaction>(procedure, parameters, commandType: CommandType.StoredProcedure);
			}

			return transactions;
		}

		public async Task<User> GetUser(int id) {
			User user = new();
			DynamicParameters parameters = new();

			string procedure = "[Fast].[dbo].[sp_User_Get]";
			parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

			using(SqlConnection db = new(_ConnectionString)) {
				user = await db.QueryFirstOrDefaultAsync<User>(procedure, parameters, commandType: CommandType.StoredProcedure);
			}

			return user;
		}
	}
}
