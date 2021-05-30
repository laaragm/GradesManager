using Base.Domain;
using Base.Infra.Abstractions;
using Base.Infra.Abstractions.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infra.Repositories
{
	public abstract class DapperRepository<T> : IDapperRepository<T> where T : Entity
	{
		protected string Table;
		IInfraSettings InfraSettings { get; }

		public DapperRepository(IInfraSettings infraSettings, string table)
		{
			Table = table;
			InfraSettings = infraSettings;
		}

		protected SqlConnection GetConnection()
		{
			var connection = new SqlConnection(InfraSettings.DatabaseConnection);
			connection.Open();
			return connection;
		}

		public virtual IEnumerable<T> All()
		{
			var query = $"SELECT * FROM {Table} WHERE Exclusion IS NULL;";
			using (var connection = GetConnection())
			{
				return connection.Query<T>(query);
			}
		}

		public virtual void Delete(long id)
		{
			var query = $"UPDATE {Table} SET Exclusion = @exclusion WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				connection.Query(query, new { exclusion = DateTime.UtcNow, id });
			}
		}

		public virtual T FetchByID(long id)
		{
			var query = $"SELECT * FROM {Table} WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				return connection.QuerySingle<T>(query, new { id });
			}
		}

		public virtual async Task<IEnumerable<T>> AllAsync()
		{
			var query = $"SELECT * FROM {Table} WHERE Exclusion IS NULL;";
			using (var connection = GetConnection())
			{
				return await connection.QueryAsync<T>(query);
			}
		}

		public virtual async Task DeleteAsync(long id)
		{
			var query = $"UPDATE {Table} SET Exclusion = @exclusion WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				await connection.QueryAsync(query, new { exclusion = DateTime.UtcNow, id });
			}
		}

		public virtual async Task<T> FetchByIDAsync(long id)
		{
			var query = $"SELECT * FROM {Table} WHERE ID = @id;";
			using (var connection = GetConnection())
			{
				return await connection.QuerySingleAsync<T>(query, new { id });
			}
		}
	}
}
