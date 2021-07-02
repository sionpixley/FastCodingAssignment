using System.Collections.Generic;

namespace Fast.API.Models {
	public class UserPoints {
		public string Username { get; set; }
		public string Name { get; set; }
		public int Points { get; set; }
		public IEnumerable<MonthPoints> MonthlyBreakdown { get; set; }
	}
}
