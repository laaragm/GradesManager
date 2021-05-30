using AutoMapper;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services
{
	public class LegalRepresentativeService : ILegalRepresentativeService
	{
		ILegalRepresentatives LegalRepresentatives { get; }
		IMapper Mapper { get; }

		public LegalRepresentativeService(ILegalRepresentatives legalRepresentatives, IMapper mapper)
		{
			LegalRepresentatives = legalRepresentatives;
			Mapper = mapper;
		}

		public async Task<LegalRepresentativeModel> Save(LegalRepresentativeModel model)
		{
			var result = await LegalRepresentatives.Save(model.ToEntity());
			return Mapper.Map<LegalRepresentativeModel>(result);
		}

		public async Task<IEnumerable<LegalRepresentativeModel>> FetchAll()
		{
			var result = await LegalRepresentatives.AllAsync();
			return result.Select(Mapper.Map<LegalRepresentativeModel>).ToList();
		}

		public async Task<LegalRepresentativeModel> FetchById(long id)
		{
			var result = await LegalRepresentatives.FetchByIDAsync(id);
			return Mapper.Map<LegalRepresentativeModel>(result);
		}

		public async Task Delete(long id) => await LegalRepresentatives.DeleteAsync(id);

		public async Task Update(LegalRepresentativeModel model) => await LegalRepresentatives.Update(model.ToEntity());
	}
}
