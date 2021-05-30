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
	public class ClassroomService : IClassroomService
	{
		IClassrooms Classrooms { get; }
		ISchoolService SchoolService { get; }
		IMapper Mapper { get; }

		public ClassroomService(IClassrooms classrooms, IMapper mapper, ISchoolService schoolService)
		{
			Classrooms = classrooms;
			Mapper = mapper;
			SchoolService = schoolService;
		}

		public async Task<ClassroomModel> Save(ClassroomModel model)
		{
			var school = model.School?.ID == 0 ? await SchoolService.Save(model.School) : await SchoolService.FetchById(model.School.ID);
			model.School = school;
			var result = await Classrooms.Save(model.ToEntity());
			return Mapper.Map<Classroom, ClassroomModel>(result);
		}

		public async Task<IEnumerable<ClassroomModel>> FetchAll()
		{
			var result = await Classrooms.AllAsync();
			return result.Select(Mapper.Map<ClassroomModel>).ToList();
		}

		public async Task<ClassroomModel> FetchById(long id)
		{
			var result = await Classrooms.FetchByIDAsync(id);
			return Mapper.Map<Classroom, ClassroomModel>(result);
		}

		public async Task Delete(long id) => await Classrooms.DeleteAsync(id);

		public async Task Update(ClassroomModel model) => await Classrooms.Update(model.ToEntity());

		public async Task<IEnumerable<ClassroomModel>> BySchool(long schoolID)
		{
			var result = await Classrooms.BySchool(schoolID);
			return result.Select(Mapper.Map<ClassroomModel>).ToList();
		}

		public async Task<IEnumerable<long>> DistinctLevelsBySchool(long schoolID)
			=> await Classrooms.DistinctLevelsBySchool(schoolID);

	}
}

