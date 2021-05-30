using GradesManager.Domain.Enums;
using GradesManager.Domain.Models;
using GradesManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services
{
	public class PerformanceAnalysisService : IPerformanceAnalysisService
	{
		IClassroomService ClassroomService { get; }
		IGradeService GradeService { get; }

		public PerformanceAnalysisService(IClassroomService classroomService, IGradeService gradeService)
		{
			ClassroomService = classroomService;
			GradeService = gradeService;
		}

		public async Task<IDictionary<string, decimal?>> CalculateGradeAverageByLevelsInSchool(long schoolID)
		{
			IDictionary<string, decimal?> gradeAverageByLevel = new Dictionary<string, decimal?>();
			var levelsBySchool = await ClassroomService.DistinctLevelsBySchool(schoolID);
			foreach (var level in levelsBySchool)
			{
				var gradeAverage = await GradeService.CalculateGradeAverageBySchoolLevel(level, schoolID);
				if (Enum.IsDefined(typeof(Level), Convert.ToInt32(level)))
					gradeAverageByLevel.Add(((Level)level).ToString(), gradeAverage);
			}

			return gradeAverageByLevel;
		}

	}
}
