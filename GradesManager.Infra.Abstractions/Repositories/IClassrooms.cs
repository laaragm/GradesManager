using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IClassrooms : IRepository<Classroom>
	{
		Task Update(Classroom classroom);
		Task<Classroom> Save(Classroom classroom);

	}
}
