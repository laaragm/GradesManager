using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Models
{
	public class SchoolModel
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public string Owner { get; set; }
		public string Principal { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string CNPJ { get; set; }
	}
}
