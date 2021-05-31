using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IClassroomStudentService
	{
		Task<ClassroomStudentModel> Save(ClassroomStudentModel model);
		Task<IEnumerable<ClassroomStudentModel>> FetchAll();
		Task<ClassroomStudentModel> FetchById(long id);
		Task Delete(long id);
		Task Update(ClassroomStudentModel model);

	}
}
