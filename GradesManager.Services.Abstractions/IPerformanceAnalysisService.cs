using GradesManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IPerformanceAnalysisService
	{
		Task<IDictionary<string, decimal?>> CalculateGradeAverageByLevelsInSchool(long schoolID);

	}
}
