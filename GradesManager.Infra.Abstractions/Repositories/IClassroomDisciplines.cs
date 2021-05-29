using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IClassroomDisciplines : IRepository<ClassroomDiscipline>
	{
	}
}
