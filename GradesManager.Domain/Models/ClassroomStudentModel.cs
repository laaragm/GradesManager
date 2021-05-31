using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class ClassroomStudentModel
	{
		public long ID { get; set; }
		public ClassroomModel Classroom { get; set; }
		public StudentModel Student { get; set; }
		public DateTime? Creation { get; }

		public ClassroomStudentModel(ClassroomStudent classroomStudent)
		{
			ID = classroomStudent.ID;
			Classroom = GetClassroomModel(classroomStudent);
			Student = GetStudentModel(classroomStudent);
			Creation = classroomStudent.Creation;
		}

		public ClassroomStudentModel()
		{
		}

		public ClassroomStudent ToEntity()
		{
			return new ClassroomStudent
			{
				ID = ID,
				Classroom = Classroom?.ToEntity(),
				Student = Student?.ToEntity(),
				Creation = Creation
			};
		}

		private StudentModel GetStudentModel(ClassroomStudent classroomStudent)
			=> classroomStudent.Student == null ? new StudentModel() : new StudentModel(classroomStudent.Student);

		private ClassroomModel GetClassroomModel(ClassroomStudent classroomStudent)
			=> classroomStudent.Classroom == null ? new ClassroomModel() : new ClassroomModel(classroomStudent.Classroom);

	}
}
