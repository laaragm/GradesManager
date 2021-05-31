using AutoMapper;
using GradesManager.Domain.DTOs;
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
		IMapper Mapper { get; }

		public PerformanceAnalysisService(IClassroomService classroomService, IGradeService gradeService, IMapper mapper)
		{
			ClassroomService = classroomService;
			GradeService = gradeService;
			Mapper = mapper;
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

		public async Task<IEnumerable<StudentGradesModel>> AnalyseStudentsPerformance(StudentsDTO studentsDTO)
		{
			var result = new List<StudentGradesModel>();
			foreach (var student in studentsDTO.Students)
			{
				var gradesAverage = new Dictionary<string, decimal>();
				var grades = await GradeService.ByStudent(student, studentsDTO.School);
				var gradesByLevel = grades
										.GroupBy(o => o.Classroom?.Level.ToString())
										.ToDictionary(g => g.Key, g => g.ToList().Select(Mapper.Map<GradeModel>));
				CalculateGradeAverageByLevel(gradesAverage, gradesByLevel);
				var studentGradesModel = BuildStudentGrades(student, grades, gradesAverage);
				result.Add(studentGradesModel);
			}

			return result;
		}

		private void CalculateGradeAverageByLevel(IDictionary<string, decimal> gradesAverage, IDictionary<string, IEnumerable<GradeModel>> gradesByLevel)
		{
			foreach (var level in gradesByLevel.Keys)
			{
				var gradesInLevel = gradesByLevel.Values.SelectMany(x => x.Where(x => x.Classroom?.Level.ToString() == level));
				var average = gradesInLevel.Select(x => x.ObtainedValue).Average();
				gradesAverage.Add(level, average);
			}
		}

		private StudentGradesModel BuildStudentGrades(long studentID, IEnumerable<GradeModel> grades, IDictionary<string, decimal> gradesAverage)
		{
			return new StudentGradesModel
			{
				StudentID = studentID,
				StudentName = grades.First().Student?.Name,
				ClassroomID = (long)(grades.First().Classroom?.ID),
				ClassroomName = grades.First().Classroom?.Name,
				GradeAverageByLevel = gradesAverage
			};
		}

	}
}
