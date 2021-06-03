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
	public class ClassroomStudentService : IClassroomStudentService
	{
		IMapper Mapper { get; }
		IStudentService StudentService { get; }
		IClassroomService ClassroomService { get; }
		IClassroomStudents ClassroomStudents { get; }

		public ClassroomStudentService(IMapper mapper, IStudentService studentService, IClassroomService classroomService, IClassroomStudents classroomStudents)
		{
			Mapper = mapper;
			StudentService = studentService;
			ClassroomService = classroomService;
			ClassroomStudents = classroomStudents;
		}

		public async Task<ClassroomStudentModel> Save(ClassroomStudentModel model)
		{
			var classroom = model.Classroom?.ID == 0 ? await ClassroomService.Save(model.Classroom) : await ClassroomService.FetchById(model.Classroom.ID);
			var student = model.Student?.ID == 0 ? await StudentService.Save(model.Student) : await StudentService.FetchById(model.Student.ID);
			model.Classroom = classroom;
			model.Student = student;
			var result = await ClassroomStudents.Save(model.ToEntity());
			return Mapper.Map<ClassroomStudentModel>(result);
		}

		public async Task<IEnumerable<ClassroomStudentModel>> FetchAll()
		{
			var result = await ClassroomStudents.AllAsync();
			return result.Select(Mapper.Map<ClassroomStudentModel>).ToList();
		}

		public async Task<ClassroomStudentModel> FetchById(long id)
		{
			var result = await ClassroomStudents.FetchByIDAsync(id);
			return Mapper.Map<ClassroomStudentModel>(result);
		}

		public async Task Delete(long id) => await ClassroomStudents.DeleteAsync(id);

		public async Task Update(ClassroomStudentModel model) => await ClassroomStudents.Update(model.ToEntity());

		public async Task<XyChartModel> StudentCountPerLevel(long schoolID)
		{
			var categories = new List<string>();
			var values = new List<decimal>();
			var studentCountPerLevel = await ClassroomStudents.StudentCountPerLevel(schoolID);
			foreach (var item in studentCountPerLevel)
			{
				categories.Add(item.level.ToString());
				values.Add(item.quantity);
			}

			return new XyChartModel
			{
				Categories = categories,
				Values = values
			};
		}

	}
}

