using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infra.Abstractions.Repositories
{
	public interface IRepository<T> where T : Entity
	{
		IEnumerable<T> All();
		Task<IEnumerable<T>> AllAsync();
		void Delete(long id);
		Task DeleteAsync(long id);
		Task<T> FetchByIDAsync(long id);
	}
}
