using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class School : Entity
	{
		public string Owner { get; set; }
		public string Principal { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string CNPJ { get; set; }

	}
}
