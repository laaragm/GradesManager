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

		public ClassroomModel(Classroom classroom)
		{
			ID = classroom.ID;
			Name = classroom.Name;
			School = FetchSchoolModel(classroom);
			Level = classroom.Level;
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
				Level = Level
			};
		}

		private SchoolModel FetchSchoolModel(Classroom classroom)
			=> classroom.School == null ? new SchoolModel() : new SchoolModel(classroom.School);

	}
}
