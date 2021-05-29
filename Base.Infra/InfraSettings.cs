using Base.Infra.Abstractions;
using System;

namespace Base.Infra
{
	public class InfraSettings : IInfraSettings
	{
		public string DatabaseConnection { get; set; }
	}
}
