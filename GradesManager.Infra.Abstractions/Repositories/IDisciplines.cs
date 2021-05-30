using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IDisciplines : IRepository<Discipline>
	{
		Task<Discipline> Save(Discipline discipline);
		Task Update(Discipline discipline);

	}
}
