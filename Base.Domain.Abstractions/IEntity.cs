using System;

namespace Base.Domain.Abstractions
{
	public interface IEntity
	{
		long ID { get; }
		string Name { get; }
		DateTime? Creation { get; }
		DateTime? Exclusion { get; }
		bool Excluded { get; }
	}
}
