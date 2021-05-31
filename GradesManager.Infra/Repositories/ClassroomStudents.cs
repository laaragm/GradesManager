using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using Dapper;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Repositories
{
	public class ClassroomStudents : DapperRepository<ClassroomStudent>, IClassroomStudents
	{
		static IEnumerable<Type> Types = new List<Type>
		{ 
			typeof(Classroom), typeof(LegalRepresentative), typeof(Discipline), typeof(ClassroomStudent), typeof(School) 
		};

		public ClassroomStudents(IInfraSettings infraSettings) : base(infraSettings, "ClassroomStudent")
		{
			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			foreach (var type in Types)
				Slapper.AutoMapper.Configuration.AddIdentifier(type, "ID");
		}

		public async Task<ClassroomStudent> Save(ClassroomStudent classroomStudent)
		{
			var query = $@"INSERT INTO {Table} (Classroom, Student, Creation)
							OUTPUT Inserted.ID
							VALUES(@classroom, @student, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					classroom = classroomStudent.Classroom?.ID,
					student = classroomStudent.Student?.ID,
					creation = DateTime.UtcNow
				});
				classroomStudent.ID = id.FirstOrDefault();

				return classroomStudent;
			}
		}

		public async Task Update(ClassroomStudent classroomStudent)
		{
			var query = $@"UPDATE {Table}
							SET
								Classroom = @classroom,
								Student = @student
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<ClassroomDiscipline>(query, new
				{
					classroom = classroomStudent.Classroom?.ID,
					student = classroomStudent.Student?.ID,
					id = classroomStudent.ID
				});
			}
		}

		private string BaseQuery
		{
			get => $@"SELECT
							ClassroomStudent.ID,
							Classroom.ID Classroom_ID,
							School.ID School_ID,
							School.Owner School_Owner,
							School.Principal School_Principal,
							School.Address School_Address,
							School.PhoneNumber School_PhoneNumber,
							School.CNPJ School_CNPJ,
							School.Creation School_Creation,
							Classroom.Level Classroom_Level,
							Classroom.Name Classroom_Name,
							Classroom.Year Classroom_Year,
							Classroom.Creation Classroom_Creation,
							Student.ID Student_ID,
							Student.Name Student_Name,
							LegalRepresentative.ID LegalRepresentative_ID,
							LegalRepresentative.Name LegalRepresentative_Name,
							LegalRepresentative.PhoneNumber LegalRepresentative_PhoneNumber,
							LegalRepresentative.Creation LegalRepresentative_Creation,
							Student.Birthday Student_Birthday,
							Student.Address Student_Address,
							Student.Creation Student_Creation,
							ClassroomStudent.Creation
						FROM {Table}
						JOIN Student				ON Student.ID				= ClassroomStudent.Student
						JOIN LegalRepresentative	ON LegalRepresentative.ID	= Student.LegalRepresentative
						JOIN Classroom				ON Classroom.ID				= ClassroomStudent.Classroom
						JOIN School					ON School.ID				= Classroom.School
						WHERE ClassroomStudent.Exclusion IS NULL";
		}

		public override async Task<IEnumerable<ClassroomStudent>> AllAsync()
		{
			var query = BaseQuery;
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<ClassroomStudent>(await connection.QueryAsync<dynamic>(query));
			}
		}

		public override async Task<ClassroomStudent> FetchByIDAsync(long id)
		{
			var query = $@"{BaseQuery}
							AND ClassroomStudent.ID = @id;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<ClassroomStudent>(await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { id }));
			}
		}

	}
}
