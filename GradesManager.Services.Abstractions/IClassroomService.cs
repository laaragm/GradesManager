using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IClassroomService
	{
		Task<ClassroomModel> Save(ClassroomModel model);
		Task<IEnumerable<ClassroomModel>> FetchAll();
		Task<ClassroomModel> FetchById(long id);
		Task Delete(long id);
		Task Update(ClassroomModel model);
		Task<IEnumerable<ClassroomModel>> BySchool(long schoolID);
		Task<IEnumerable<long>> DistinctLevelsBySchool(long schoolID);
		Task<IEnumerable<ClassroomModel>> FetchBySchoolId(long id);

	}
}
