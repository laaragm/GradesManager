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
	public class Disciplines : DapperRepository<Discipline>, IDisciplines
	{
		public Disciplines(IInfraSettings infraSettings) : base(infraSettings, "Discipline")
		{
		}

		public async Task<Discipline> Save(Discipline discipline)
		{
			var query = $@"INSERT INTO {Table} (Name, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					name = discipline.Name,
					creation = DateTime.UtcNow
				});
				discipline.ID = id.FirstOrDefault();

				return discipline;
			}
		}

		public async Task Update(Discipline discipline)
		{
			var query = $@"UPDATE {Table}
							SET
								Name = @name
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<School>(query, new
				{
					name = discipline.Name,
					id = discipline.ID
				});
			}
		}

	}
}
