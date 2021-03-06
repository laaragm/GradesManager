using AutoMapper;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services
{
	public class GradeService : IGradeService
	{
		IGrades Grades { get; }
		IStudentService StudentService { get; }
		IDisciplineService DisciplineService { get; }
		IMapper Mapper { get; }

		public GradeService(IGrades grades, IMapper mapper, IStudentService studentService, IDisciplineService disciplineService)
		{
			Grades = grades;
			Mapper = mapper;
			StudentService = studentService;
			DisciplineService = disciplineService;
		}

		public async Task<GradeModel> Save(GradeModel model)
		{
			var student = model.Student?.ID == 0 ? await StudentService.Save(model.Student) : await StudentService.FetchById(model.Student.ID);
			var discipline = model.Discipline?.ID == 0 ? await DisciplineService.Save(model.Discipline) : await DisciplineService.FetchById(model.Discipline.ID);
			model.Student = student;
			model.Discipline = discipline;
			var result = await Grades.Save(model.ToEntity());
			return Mapper.Map<GradeModel>(result);
		}

		public async Task<IEnumerable<GradeModel>> FetchAll()
		{
			var result = await Grades.AllAsync();
			return result.Select(Mapper.Map<GradeModel>).ToList();
		}

		public async Task<GradeModel> FetchById(long id)
		{
			var result = await Grades.FetchByIDAsync(id);
			return Mapper.Map<GradeModel>(result);
		}

		public async Task Delete(long id) => await Grades.DeleteAsync(id);

		public async Task Update(GradeModel model) => await Grades.Update(model.ToEntity());

		public async Task<decimal?> CalculateGradeAverageBySchoolLevel(long levelID, long schoolID)
			=> await Grades.GradeAverageBySchoolLevel(levelID, schoolID);

		public async Task<IEnumerable<GradeModel>> ByStudent(long studentID, long schoolID)
		{
			var result = await Grades.ByStudent(studentID, schoolID);
			return result.Select(Mapper.Map<GradeModel>).ToList();
		}

		public async Task<decimal> CalculateGradeAverageByDiscipline(long schoolID, long disciplineID)
			=> await Grades.GradeAverageByDiscipline(schoolID, disciplineID);

		public async Task<XyChartModel> CalculateDisciplinesGradeAverage(long schoolID)
		{
			var disciplines = await DisciplineService.FetchAll();
			var averages = new List<decimal>();
			foreach (var discipline in disciplines)
			{
				var average = await CalculateGradeAverageByDiscipline(schoolID, discipline.ID);
				averages.Add(average);
			}
			var result = new XyChartModel
			{
				Categories = disciplines.Select(x => x.Name),
				Values = averages
			};

			return result;
		}

	}
}

