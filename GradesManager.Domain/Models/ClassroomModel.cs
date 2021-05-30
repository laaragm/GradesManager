using Base.Domain;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class ClassroomModel
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public virtual SchoolModel School { get; set; }
		public Level Level { get; set; }
		public long Year { get; set; }
		public DateTime? Creation { get; }

		public ClassroomModel(Classroom classroom)
		{
			ID = classroom.ID;
			Name = classroom.Name;
			School = GetSchoolModel(classroom);
			Level = classroom.Level;
			Year = classroom.Year;
			Creation = classroom.Creation;
		}

		public ClassroomModel()
		{
		}

		public Classroom ToEntity()
		{
			return new Classroom
			{
				ID = ID,
				Name = Name,
				School = School?.ToEntity(),
				Level = Level,
				Year = Year,
				Creation = Creation
			};
		}

		private SchoolModel GetSchoolModel(Classroom classroom)
			=> classroom.School == null ? new SchoolModel() : new SchoolModel(classroom.School);

	}
}
