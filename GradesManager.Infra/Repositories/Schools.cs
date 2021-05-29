using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using Dapper;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradesManager.Infra.Repositories
{
	public class Schools : DapperRepository<School>, ISchools
	{
		public Schools(IInfraSettings infraSettings) : base(infraSettings, "School")
		{
		}

		public SchoolModel Create(SchoolModel model)
		{
			var query = $@"INSERT INTO {Table} (Name, Owner, Principal, Address, PhoneNumber, CNPJ, Creation)
							OUTPUT Inserted.ID
							VALUES(@name, @owner, @principal, @address, @phoneNumber, @cnpj, @creation);";
			using (var connection = GetConnection())
			{
				var id = connection.Query<long>(query, new
				{ 
					name = model.Name,
					owner = model.Owner,
					principal = model.Principal,
					address = model.Address,
					phoneNumber = model.PhoneNumber,
					cnpj = model.CNPJ,
					creation = DateTime.UtcNow
				}).Single();
				model.ID = id;

				return model;
			}
		}

		public void Update(SchoolModel model)
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
				connection.Query<SchoolModel>(query, new
				{
					name = model.Name,
					owner = model.Owner,
					principal = model.Principal,
					address = model.Address,
					phoneNumber = model.PhoneNumber,
					cnpj = model.CNPJ
				});
			}
		}

	}
}
