﻿using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Repositories
{
	public class Disciplines : DapperRepository<Discipline>, IDisciplines
	{
		public Disciplines(IInfraSettings infraSettings) : base(infraSettings, "Discipline")
		{
		}
	}
}
