using AutoMapper;
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
	public class Schools : DapperRepository<School>, ISchools
	{
		public Schools(IInfraSettings infraSettings) : base(infraSettings, "School")
		{
		}

		public async Task<School> Save(School school)
		{
			var query = $@"INSERT INTO {Table} (Name, Owner, Principal, Address, PhoneNumber, CNPJ, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @owner, @principal, @address, @phoneNumber, @cnpj, @creation);";
			using (var connection = GetConnection())
			{
				var id = await connection.QueryAsync<long>(query, new
				{ 
					name = school.Name,
					owner = school.Owner,
					principal = school.Principal,
					address = school.Address,
					phoneNumber = school.PhoneNumber,
					cnpj = school.CNPJ,
					creation = DateTime.UtcNow
				});
				school.ID = id.FirstOrDefault();

				return school;
			}
		}

		public async Task Update(School school)
		{
			var query = $@"UPDATE {Table}
							SET
								Name = @name,
								Owner = @owner,
								Principal = @principal,
								Address = @address,
								PhoneNumber = @phoneNumber,
								CNPJ = @cnpj
							WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync<School>(query, new
				{
					name = school.Name,
					owner = school.Owner,
					principal = school.Principal,
					address = school.Address,
					phoneNumber = school.PhoneNumber,
					cnpj = school.CNPJ,
					id = school.ID
				});
			}
		}

	}
}
