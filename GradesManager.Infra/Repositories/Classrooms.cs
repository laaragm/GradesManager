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
		static IEnumerable<Type> Types = new List<Type> { typeof(School) };

		public Classrooms(IInfraSettings infraSettings) : base(infraSettings, "Classroom")
		{
			//ConfigureAutoMapper();
		}

		//private void ConfigureAutoMapper()
		//{
		//	foreach (var type in Types)
		//		AutoMapper.Configuration.AddIdentifier(type, "ID");
		//}

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
								Level = @level,
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
								Classroom.Creation,
							FROM {Table} 
							WHERE Exclusion IS NULL;";
			using (var connection = GetConnection())
			{
				return await AutoMapper.MapDynamic<Classroom>(connection.QueryAsync<dynamic>(query));
			}
		}

	}
}
