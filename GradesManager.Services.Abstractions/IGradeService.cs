using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IGradeService
	{
		Task<GradeModel> Save(GradeModel model);
		Task<IEnumerable<GradeModel>> FetchAll();
		Task<GradeModel> FetchById(long id);
		Task Delete(long id);
		Task Update(GradeModel model);
		Task<decimal?> CalculateGradeAverageBySchoolLevel(long levelID, long schoolID);

	}
}
