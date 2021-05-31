using Base.Domain;
using GradesManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class ClassroomStudent : Entity
	{
		public virtual Classroom Classroom { get; set; }
		public virtual Student Student { get; set; }

	}
}
