﻿using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using System;

namespace GradesManager.Infra
{
	public class Students : DapperRepository<Student>, IStudents
	{
		public Students(IInfraSettings infraSettings) : base(infraSettings, "Student")
		{
		}

	}
}
