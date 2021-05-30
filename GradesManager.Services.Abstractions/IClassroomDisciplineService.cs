using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IClassroomDisciplineService
	{
		Task<ClassroomDisciplineModel> Save(ClassroomDisciplineModel model);
		Task<IEnumerable<ClassroomDisciplineModel>> FetchAll();
		Task<ClassroomDisciplineModel> FetchById(long id);
		Task Delete(long id);
		Task Update(ClassroomDisciplineModel model);

	}
}
