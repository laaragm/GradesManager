﻿using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra
{
	public class Classrooms : DapperRepository<Classroom>, IClassrooms
	{
		public Classrooms(IInfraSettings infraSettings) : base(infraSettings, "Classroom")
		{
		}
	}
}
