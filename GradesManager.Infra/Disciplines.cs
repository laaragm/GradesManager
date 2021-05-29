using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra
{
	public class Disciplines : DapperRepository<Discipline>, IDisciplines
	{
		public Disciplines(IInfraSettings infraSettings) : base(infraSettings, "Discipline")
		{
		}
	}
}
