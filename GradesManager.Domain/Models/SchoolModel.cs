using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
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

		public School ToEntity()
		{
			return new School
			{
				ID = ID,
				Name = Name,
				Owner = Owner,
				Principal = Principal,
				Address = Address,
				PhoneNumber = PhoneNumber,
				CNPJ = CNPJ
			};
		}
	}
}
