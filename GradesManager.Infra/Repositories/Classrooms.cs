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
using Slapper;

namespace GradesManager.Infra.Repositories
{
	public class Classrooms : DapperRepository<Classroom>, IClassrooms
	{
		static IEnumerable<Type> Types = new List<Type> { typeof(Classroom), typeof(School) };

		public Classrooms(IInfraSettings infraSettings) : base(infraSettings, "Classroom")
		{
			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			foreach (var type in Types)
				Slapper.AutoMapper.Configuration.AddIdentifier(type, "ID");
		}

		public async Task<Classroom> Save(Classroom classroom)
		{
			var query = $@"INSERT INTO {Table} (Name, School, Level, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @school, @level, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					name = classroom.Name,
					school = classroom.School.ID,
					level = classroom.Level,
					creation = DateTime.UtcNow
				});
				classroom.ID = id.FirstOrDefault();

				return classroom;
			}
		}

		public async Task Update(Classroom classroom)
		{
			var query = $@"UPDATE {Table}
							SET
								Name = @name,
								School = @school,
								Level = @level
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<School>(query, new
				{
					name = classroom.Name,
					school = classroom.School.ID,
					level = classroom.Level,
					id = classroom.ID
				});
			}
		}

		public override async Task<IEnumerable<Classroom>> AllAsync()
		{
			var query = $@"SELECT
								Classroom.ID,
								School.ID School_ID,
								School.Name School_Name,
								School.Owner School_Owner,
								School.Principal School_Principal,
								School.Address School_Address,
								School.PhoneNumber School_PhoneNumber,
								School.CNPJ School_CNPJ,
								School.Creation School_Creation,
								Classroom.Level,
								Classroom.Name,
								Classroom.Creation
							FROM {Table}
							JOIN School ON School.ID = Classroom.School
							WHERE Classroom.Exclusion IS NULL;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Classroom>(await connection.QueryAsync<dynamic>(query));
			}
		}

		public override async Task<Classroom> FetchByIDAsync(long id)
		{
			var query = $@"SELECT
								Classroom.ID,
								School.ID School_ID,
								School.Name School_Name,
								School.Owner School_Owner,
								School.Principal School_Principal,
								School.Address School_Address,
								School.PhoneNumber School_PhoneNumber,
								School.CNPJ School_CNPJ,
								School.Creation School_Creation,
								Classroom.Level,
								Classroom.Name,
								Classroom.Creation
							FROM {Table}
							JOIN School ON School.ID = Classroom.School
							WHERE Classroom.Exclusion IS NULL
								AND Classroom.ID = @id;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Classroom>(await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { id }));
			}
		}

		public async Task<IEnumerable<Classroom>> BySchool(long schoolID)
		{
			var query = $@"SELECT
								Classroom.ID,
								School.ID School_ID,
								School.Name School_Name,
								School.Owner School_Owner,
								School.Principal School_Principal,
								School.Address School_Address,
								School.PhoneNumber School_PhoneNumber,
								School.CNPJ School_CNPJ,
								School.Creation School_Creation,
								Classroom.Level,
								Classroom.Name,
								Classroom.Creation
							FROM {Table}
							JOIN School ON School.ID = Classroom.School
							WHERE Classroom.Exclusion IS NULL
								AND Classroom.School = @schoolID;";
			using (var connection = GetConnection())
			{
				return Slapper.AutoMapper.MapDynamic<Classroom>(await connection.QueryAsync<dynamic>(query, new { schoolID }));
			}
		}

		public async Task<IEnumerable<long>> DistinctLevelsBySchool(long schoolID)
		{
			var query = $@"SELECT Classroom.[Level]
							FROM Classroom
							JOIN School ON School.ID = Classroom.School
							WHERE Classroom.Exclusion IS NULL
								AND Classroom.School = @schoolID
							GROUP BY Classroom.[Level];";
			using (var connection = GetConnection())
			{
				return await connection.QueryAsync<long>(query, new { schoolID });
			}
		}

	}
}
