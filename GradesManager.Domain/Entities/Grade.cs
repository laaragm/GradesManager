using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class Grade : Entity
	{
		public virtual Student Student { get; set; }
		public virtual Discipline Discipline { get; set; }
		public decimal TotalValue { get; set; }
		public decimal ObtainedValue { get; set; }
	}
}
