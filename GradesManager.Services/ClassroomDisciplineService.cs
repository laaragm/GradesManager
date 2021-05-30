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
	public class ClassroomDisciplineService : IClassroomDisciplineService
	{
		IClassroomDisciplines ClassroomDisciplines { get; }
		IClassroomService ClassroomService { get; }
		IDisciplineService DisciplineService { get; }
		IMapper Mapper { get; }

		public ClassroomDisciplineService(IMapper mapper, IClassroomDisciplines classroomDisciplines, IClassroomService classroomService,
			IDisciplineService disciplineService)
		{
			Mapper = mapper;
			ClassroomDisciplines = classroomDisciplines;
			ClassroomService = classroomService;
			DisciplineService = disciplineService;
		}

		public async Task<ClassroomDisciplineModel> Save(ClassroomDisciplineModel model)
		{
			var classroom = model.Classroom?.ID == 0 ? await ClassroomService.Save(model.Classroom) : await ClassroomService.FetchById(model.Classroom.ID);
			var discipline = model.Discipline?.ID == 0 ? await DisciplineService.Save(model.Discipline) : await DisciplineService.FetchById(model.Discipline.ID);
			model.Classroom = classroom;
			model.Discipline = discipline;
			var result = await ClassroomDisciplines.Save(model.ToEntity());
			return Mapper.Map<ClassroomDisciplineModel>(result);
		}

		public async Task<IEnumerable<ClassroomDisciplineModel>> FetchAll()
		{
			var result = await ClassroomDisciplines.AllAsync();
			return result.Select(Mapper.Map<ClassroomDisciplineModel>).ToList();
		}

		public async Task<ClassroomDisciplineModel> FetchById(long id)
		{
			var result = await ClassroomDisciplines.FetchByIDAsync(id);
			return Mapper.Map<ClassroomDisciplineModel>(result);
		}

		public async Task Delete(long id) => await ClassroomDisciplines.DeleteAsync(id);

		public async Task Update(ClassroomDisciplineModel model) => await ClassroomDisciplines.Update(model.ToEntity());

	}
}

