using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface ILegalRepresentativeService
	{
		Task<LegalRepresentativeModel> Save(LegalRepresentativeModel model);
		Task<IEnumerable<LegalRepresentativeModel>> FetchAll();
		Task<LegalRepresentativeModel> FetchById(long id);
		Task Delete(long id);
		Task Update(LegalRepresentativeModel model);

	}
}
