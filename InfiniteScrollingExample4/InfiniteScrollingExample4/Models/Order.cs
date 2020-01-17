using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteScrollingExample4.Models
{
	public class Order
	{
		public Order()
		{
		}

		public int Id
		{
			get;
			set;
		}

		public string ProductName
		{
			get;
			set;
		}

		public DateTime Created
		{
			get;
			set;
		}

		public int Quantity
		{
			get;
			set;
		}
	}
}
