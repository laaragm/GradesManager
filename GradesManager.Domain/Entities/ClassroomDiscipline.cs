using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Entities
{
	public class ClassroomDiscipline : Entity
	{
		public virtual Discipline Disciplines { get; set; }
		public virtual Classroom Classroom { get; set; }
		public string Teacher { get; set; }
	}
}
