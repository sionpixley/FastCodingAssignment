using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.API.Models {
	public class Transaction {
		public long Id { get; set; }
		public int UserId { get; set; }
		public decimal Amount { get; set; }
		public string CreateDate { get; set; }
	}
}
