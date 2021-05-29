using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface ISchools : IRepository<School>
	{
		Task<School> Save(School school);
		Task Update(School school);

	}
}
