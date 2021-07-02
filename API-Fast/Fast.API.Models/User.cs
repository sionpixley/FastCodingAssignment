namespace Fast.API.Models {
	public class User {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public char? MiddleInitial { get; set; }
		public string LastName { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Username { get; set; }
		public string Passwd { get; set; }
		public string ModifiedDate { get; set; }
	}
}
