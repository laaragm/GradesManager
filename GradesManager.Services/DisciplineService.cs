using AutoMapper;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradesManager.Services
{
	public class DisciplineService : IDisciplineService
	{
		IDisciplines Disciplines { get; }
		IMapper Mapper { get; }

		public DisciplineService(IDisciplines disciplines, IMapper mapper)
		{
			Disciplines = disciplines;
			Mapper = mapper;
		}

		public async Task<DisciplineModel> Save(DisciplineModel model)
		{
			var result = await Disciplines.Save(model.ToEntity());
			return Mapper.Map<DisciplineModel>(result);
		}

		public async Task<IEnumerable<DisciplineModel>> FetchAll()
		{
			var result = await Disciplines.AllAsync();
			return result.Select(Mapper.Map<DisciplineModel>).ToList();
		}

		public async Task<DisciplineModel> FetchById(long id)
		{
			var result = await Disciplines.FetchByIDAsync(id);
			return Mapper.Map<DisciplineModel>(result);
		}

		public async Task Delete(long id) => await Disciplines.DeleteAsync(id);

		public async Task Update(DisciplineModel model) => await Disciplines.Update(model.ToEntity());

	}
}
