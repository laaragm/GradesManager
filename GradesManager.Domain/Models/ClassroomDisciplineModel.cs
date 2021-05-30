using Base.Domain;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class ClassroomDisciplineModel
	{
		public long ID { get; set; }
		public virtual DisciplineModel Discipline { get; set; }
		public virtual ClassroomModel Classroom { get; set; }
		public string Teacher { get; set; }
		public DateTime? Creation { get; }

		public ClassroomDisciplineModel(ClassroomDiscipline classroomDiscipline)
		{
			ID = classroomDiscipline.ID;
			Discipline = GetDisciplineModel(classroomDiscipline);
			Classroom = GetClassroomModel(classroomDiscipline);
			Teacher = classroomDiscipline.Teacher;
			Creation = classroomDiscipline.Creation;
		}

		public ClassroomDisciplineModel()
		{
		}

		public ClassroomDiscipline ToEntity()
		{
			return new ClassroomDiscipline
			{
				ID = ID,
				Discipline = Discipline?.ToEntity(),
				Classroom = Classroom?.ToEntity(),
				Teacher = Teacher,
				Creation = Creation
			};
		}

		private DisciplineModel GetDisciplineModel(ClassroomDiscipline classroomDiscipline)
			=> classroomDiscipline.Discipline == null ? new DisciplineModel() : new DisciplineModel(classroomDiscipline.Discipline);

		private ClassroomModel GetClassroomModel(ClassroomDiscipline classroomDiscipline)
			=> classroomDiscipline.Classroom == null ? new ClassroomModel() : new ClassroomModel(classroomDiscipline.Classroom);

	}
}
