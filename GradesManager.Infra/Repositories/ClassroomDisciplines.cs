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
	public class ClassroomDisciplines : DapperRepository<ClassroomDiscipline>, IClassroomDisciplines
	{
		static IEnumerable<Type> Types = new List<Type> { typeof(Classroom), typeof(School), typeof(Discipline), typeof(ClassroomDiscipline) };

		public ClassroomDisciplines(IInfraSettings infraSettings) : base(infraSettings, "ClassroomDiscipline")
		{
			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			foreach (var type in Types)
				Slapper.AutoMapper.Configuration.AddIdentifier(type, "ID");
		}

		public async Task<ClassroomDiscipline> Save(ClassroomDiscipline classroomDiscipline)
		{
			var query = $@"INSERT INTO {Table} (Classroom, Discipline, Teacher, Creation)
							OUTPUT Inserted.ID
							VALUES(@classroom, @discipline, @teacher, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					classroom = classroomDiscipline.Classroom?.ID,
					discipline = classroomDiscipline.Discipline?.ID,
					teacher = classroomDiscipline.Teacher,
					creation = DateTime.UtcNow
				});
				classroomDiscipline.ID = id.FirstOrDefault();

				return classroomDiscipline;
			}
		}

		public async Task Update(ClassroomDiscipline classroomDiscipline)
		{
			var query = $@"UPDATE {Table}
							SET
								Classroom = @classroom,
								Discipline = @discipline,
								Teacher = @teacher
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<ClassroomDiscipline>(query, new
				{
					classroom = classroomDiscipline.Classroom?.ID,
					discipline = classroomDiscipline.Discipline?.ID,
					teacher = classroomDiscipline.Teacher,
					id = classroomDiscipline.ID
				});
			}
		}

		private string BaseQuery
		{
			get => $@"SELECT
							ClassroomDiscipline.ID,
							Discipline.ID Discipline_ID,
							Discipline.Name Discipline_Name,
							Discipline.Creation Discipline_Creation,
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
							Classroom.Creation Classroom_Creation,
							ClassroomDiscipline.Teacher,
							ClassroomDiscipline.Creation
						FROM {Table}
						JOIN Discipline ON Discipline.ID = ClassroomDiscipline.Discipline
						JOIN Classroom	ON Classroom.ID	 = ClassroomDiscipline.Classroom
						JOIN School		ON School.ID	 = Classroom.School
						WHERE ClassroomDiscipline.Exclusion IS NULL";
		}

		public override async Task<IEnumerable<ClassroomDiscipline>> AllAsync()
		{
			var query = BaseQuery;
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<ClassroomDiscipline>(await connection.QueryAsync<dynamic>(query));
			}
		}

		public override async Task<ClassroomDiscipline> FetchByIDAsync(long id)
		{
			var query = $@"{BaseQuery}
							AND ClassroomDiscipline.ID = @id;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<ClassroomDiscipline>(await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { id }));
			}
		}

	}
}
