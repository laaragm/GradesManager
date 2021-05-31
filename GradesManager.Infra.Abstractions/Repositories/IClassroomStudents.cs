using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IClassroomStudents : IRepository<ClassroomStudent>
	{
		Task<ClassroomStudent> Save(ClassroomStudent classroomStudent);
		Task Update(ClassroomStudent classroomStudent);

	}
}
