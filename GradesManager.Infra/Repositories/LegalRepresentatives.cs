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
	public class LegalRepresentatives : DapperRepository<LegalRepresentative>, ILegalRepresentatives
	{
		public LegalRepresentatives(IInfraSettings infraSettings) : base(infraSettings, "LegalRepresentative")
		{
		}

		public async Task<LegalRepresentative> Save(LegalRepresentative legalRepresentative)
		{
			var query = $@"INSERT INTO {Table} (Name, PhoneNumber, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @phoneNumber, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{
					name = legalRepresentative.Name,
					phoneNumber = legalRepresentative.PhoneNumber,
					creation = DateTime.UtcNow
				});
				legalRepresentative.ID = id.FirstOrDefault();

				return legalRepresentative;
			}
		}

		public async Task Update(LegalRepresentative legalRepresentative)
		{
			var query = $@"UPDATE {Table}
							SET
								Name = @name,
								PhoneNumber = @phoneNumber
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<School>(query, new
				{
					name = legalRepresentative.Name,
					phoneNumber = legalRepresentative.PhoneNumber,
					id = legalRepresentative.ID
				});
			}
		}

	}
}
