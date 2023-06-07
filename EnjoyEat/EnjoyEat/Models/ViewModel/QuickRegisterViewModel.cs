namespace EnjoyEat.Models.ViewModel
{
	public class QuickRegisterViewModel
	{
		public int MemberId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Account { get; set; }
		public string Phone { get; set; }
	}

	public class QuickResult
	{
		public bool IsSucess { get; set; }
		public string ReturnCode { get; set; }
		public string ReturnMessage { get; set; }
	}
}
