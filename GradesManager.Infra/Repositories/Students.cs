using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using Dapper;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradesManager.Infra.Repositories
{
	public class Students : DapperRepository<Student>, IStudents
	{
		static IEnumerable<Type> Types = new List<Type> { typeof(Classroom), typeof(Student), typeof(LegalRepresentative), typeof(School) };

		public Students(IInfraSettings infraSettings) : base(infraSettings, "Student")
		{
			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			foreach (var type in Types)
				Slapper.AutoMapper.Configuration.AddIdentifier(type, "ID");
		}

		public async Task<Student> Save(Student student)
		{
			var query = $@"INSERT INTO {Table} (Name, Classroom, LegalRepresentative, Birthday, Address, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @classroom, @legalRepresentative, @birthday, @address, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					name = student.Name,
					classroom = student.Classroom?.ID,
					legalRepresentative = student.LegalRepresentative?.ID,
					birthday = student.Birthday,
					address = student.Address,
					creation = DateTime.UtcNow
				});
				student.ID = id.FirstOrDefault();

				return student;
			}
		}

		public async Task Update(Student student)
		{
			var query = $@"UPDATE {Table}
							SET
								Name = @name,
								Classroom = @classroom,
								LegalRepresentative = @legalRepresentative,
								Birthday = @birthday,
								Address = @address
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<Student>(query, new
				{
					name = student.Name,
					classroom = student.Classroom?.ID,
					legalRepresentative = student.LegalRepresentative?.ID,
					birthday = student.Birthday,
					address = student.Address,
					id = student.ID
				});
			}
		}

		public override async Task<IEnumerable<Student>> AllAsync()
		{
			var query = $@"SELECT
								Student.ID,
								Student.Name,
								Classroom.ID Classroom_ID,
								School.ID School_ID,
								School.Name School_Name,
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
								Student.Birthday,
								Student.Address,
								Student.Creation
							FROM {Table}
							JOIN LegalRepresentative	ON LegalRepresentative.ID	= Student.LegalRepresentative
							JOIN Classroom				ON Classroom.ID				= Student.Classroom
							JOIN School					ON School.ID				= Classroom.School
							WHERE Student.Exclusion IS NULL;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Student>(await connection.QueryAsync<dynamic>(query));
			}
		}

		public override async Task<Student> FetchByIDAsync(long id)
		{
			var query = $@"SELECT
								Student.ID,
								Student.Name,
								Classroom.ID Classroom_ID,
								School.ID School_ID,
								School.Name School_Name,
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
								Student.Birthday,
								Student.Address,
								Student.Creation
							FROM {Table}
							JOIN LegalRepresentative	ON LegalRepresentative.ID	= Student.LegalRepresentative
							JOIN Classroom				ON Classroom.ID				= Student.Classroom
							JOIN School					ON School.ID				= Classroom.School
							WHERE Student.Exclusion IS NULL
								AND Student.ID = @id;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Student>(await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { id }));
			}
		}

	}
}
