using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.API
{
	public class UserOrder 
	{
		public string tableNumber { get; set; }
		public string partySize { get; set; }
	}
}