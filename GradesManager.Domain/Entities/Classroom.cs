using Base.Domain;
using GradesManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class Classroom : Entity
	{
		public virtual School School { get; set; }
		public Level Level { get; set; }

	}
}
