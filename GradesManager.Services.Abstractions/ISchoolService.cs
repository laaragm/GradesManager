using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface ISchoolService
	{
		Task<IEnumerable<SchoolModel>> FetchAll();
		Task<SchoolModel> Save(SchoolModel model);
		Task Delete(long id);
		Task<SchoolModel> FetchById(long id);
		Task Update(SchoolModel model);

	}
}
