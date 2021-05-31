using Base.Domain;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class GradeModel
	{
		public long ID { get; set; }
		public virtual StudentModel Student { get; set; }
		public virtual DisciplineModel Discipline { get; set; }
		public virtual ClassroomModel Classroom { get; set; }
		public decimal TotalValue { get; set; }
		public decimal ObtainedValue { get; set; }
		public DateTime? Creation { get; }

		public GradeModel(Grade grade)
		{
			ID = grade.ID;
			Student = GetStudentModel(grade);
			Discipline = GetDisciplineModel(grade);
			Classroom = GetClassroomModel(grade);
			TotalValue = grade.TotalValue;
			ObtainedValue = grade.ObtainedValue;
			Creation = grade.Creation;
		}

		public GradeModel()
		{
		}

		public Grade ToEntity()
		{
			return new Grade
			{
				ID = ID,
				Student = Student?.ToEntity(),
				Discipline = Discipline?.ToEntity(),
				Classroom = Classroom?.ToEntity(),
				TotalValue = TotalValue,
				ObtainedValue = ObtainedValue,
				Creation = Creation
			};
		}

		private StudentModel GetStudentModel(Grade grade)
			=> grade.Student == null ? new StudentModel() : new StudentModel(grade.Student);

		private DisciplineModel GetDisciplineModel(Grade grade)
			=> grade.Discipline == null ? new DisciplineModel() : new DisciplineModel(grade.Discipline);

		private ClassroomModel GetClassroomModel(Grade grade)
			=> grade.Classroom == null ? new ClassroomModel() : new ClassroomModel(grade.Classroom);

	}
}
