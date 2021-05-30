using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IClassroomDisciplines : IRepository<ClassroomDiscipline>
	{
		Task<ClassroomDiscipline> Save(ClassroomDiscipline classroomDiscipline);
		Task Update(ClassroomDiscipline classroomDiscipline);

	}
}
