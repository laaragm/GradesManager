using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IDisciplineService
	{
		Task<DisciplineModel> Save(DisciplineModel model);
		Task<IEnumerable<DisciplineModel>> FetchAll();
		Task<DisciplineModel> FetchById(long id);
		Task Delete(long id);
		Task Update(DisciplineModel model);

	}
}
