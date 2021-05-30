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
	public class Grades : DapperRepository<Grade>, IGrades
	{
		static IEnumerable<Type> Types = new List<Type> { typeof(Classroom), typeof(School), typeof(LegalRepresentative), typeof(Student), typeof(Grade) };

		public Grades(IInfraSettings infraSettings) : base(infraSettings, "Grade")
		{
			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			foreach (var type in Types)
				Slapper.AutoMapper.Configuration.AddIdentifier(type, "ID");
		}

		public async Task<Grade> Save(Grade grade)
		{
			var query = $@"INSERT INTO {Table} (Student, Discipline, TotalValue, ObtainedValue, Creation)
							OUTPUT Inserted.ID
							VALUES(@student, @discipline, @totalValue, @obtainedValue, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					student = grade.Student?.ID,
					discipline = grade.Discipline?.ID,
					totalValue = grade.TotalValue,
					obtainedValue = grade.ObtainedValue,
					creation = DateTime.UtcNow
				});
				grade.ID = id.FirstOrDefault();

				return grade;
			}
		}

		public async Task Update(Grade grade)
		{
			var query = $@"UPDATE {Table}
							SET
								Student = @student, 
								Discipline = @discipline,
								TotalValue = @totalValue,
								ObtainedValue = @obtainedValue
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<Grade>(query, new
				{
					student = grade.Student?.ID,
					discipline = grade.Discipline?.ID,
					totalValue = grade.TotalValue,
					obtainedValue = grade.ObtainedValue,
					ID = grade.ID,
				});
			}
		}

		private string BaseQuery
		{
			get => $@"SELECT
							Grade.ID,
							Student.ID Student_ID,
							Student.Name Student_Name,
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
							LegalRepresentative.ID LegalRepresentative_ID,
							LegalRepresentative.Name LegalRepresentative_Name,
							LegalRepresentative.PhoneNumber LegalRepresentative_PhoneNumber,
							LegalRepresentative.Creation LegalRepresentative_Creation,
							Student.Birthday Student_Birthday,
							Student.Address Student_Address,
							Student.Creation Student_Creation,
							Discipline.ID Discipline_ID,
							Discipline.Name Discipline_Name,
							Discipline.Creation Discipline_Creation,
							Grade.TotalValue,
							Grade.ObtainedValue,
							Grade.Creation
						FROM {Table}
						JOIN Student				ON Student.ID				= Grade.Student
						JOIN Classroom				ON Classroom.ID				= Student.Classroom
						JOIN School					ON School.ID				= Classroom.School
						JOIN LegalRepresentative	ON LegalRepresentative.ID	= Student.LegalRepresentative
						JOIN Discipline				ON Discipline.ID			= Grade.Discipline
						WHERE Grade.Exclusion IS NULL";
		}

		public override async Task<IEnumerable<Grade>> AllAsync()
		{
			var query = BaseQuery;
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Grade>(await connection.QueryAsync<dynamic>(query));
			}
		}

		public override async Task<Grade> FetchByIDAsync(long id)
		{
			var query = $@"{BaseQuery}
							AND Grade.ID = @id;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Grade>(await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { id }));
			}
		}
	}
}
