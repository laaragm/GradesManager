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
	public class StudentService : IStudentService
	{
		IStudents Students { get; }
		IClassroomService ClassroomService { get; }
		ILegalRepresentativeService LegalRepresentativeService { get; }
		IMapper Mapper { get; }

		public StudentService(IStudents students, IMapper mapper, IClassroomService classroomService, ILegalRepresentativeService legalRepresentativeService)
		{
			Students = students;
			Mapper = mapper;
			ClassroomService = classroomService;
			LegalRepresentativeService = legalRepresentativeService;
		}

		public async Task<StudentModel> Save(StudentModel model)
		{
			var classroom = model.Classroom?.ID == 0 ? await ClassroomService.Save(model.Classroom) : await ClassroomService.FetchById(model.Classroom.ID);
			var legalRepresentative = model.LegalRepresentative?.ID == 0 
				? await LegalRepresentativeService.Save(model.LegalRepresentative) : await LegalRepresentativeService.FetchById(model.LegalRepresentative.ID);
			model.Classroom = classroom;
			model.LegalRepresentative = legalRepresentative;
			var result = await Students.Save(model.ToEntity());
			return Mapper.Map<StudentModel>(result);
		}

		public async Task<IEnumerable<StudentModel>> FetchAll()
		{
			var result = await Students.AllAsync();
			return result.Select(Mapper.Map<StudentModel>).ToList();
		}

		public async Task<StudentModel> FetchById(long id)
		{
			var result = await Students.FetchByIDAsync(id);
			return Mapper.Map<StudentModel>(result);
		}

		public async Task Delete(long id) => await Students.DeleteAsync(id);

		public async Task Update(StudentModel model) => await Students.Update(model.ToEntity());

	}
}

