using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface ILegalRepresentatives : IRepository<LegalRepresentative>
	{
		Task<LegalRepresentative> Save(LegalRepresentative legalRepresentative);
		Task Update(LegalRepresentative legalRepresentative);

	}
}
