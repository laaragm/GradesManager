using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class Student : Entity
	{
		public LegalRepresentative LegalRepresentative { get; set; }
		public virtual IEnumerable<Grade> Grades { get; set; }
		public DateTime? Birthday { get; set; }
		public string Address { get; set; }

	}
}
