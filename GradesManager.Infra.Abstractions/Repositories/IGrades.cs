using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IGrades : IRepository<Grade>
	{
		Task<Grade> Save(Grade grade);
		Task Update(Grade grade);
		Task<decimal?> GradeAverageBySchoolLevel(long levelID, long schoolID);
		Task<IEnumerable<Grade>> ByStudent(long studentID, long schoolID);
		Task<decimal> GradeAverageByDiscipline(long schoolID, long disciplineID);

	}
}
