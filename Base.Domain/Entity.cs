using System;
using System.Text.Json.Serialization;

namespace Base.Domain
{
	public class Entity
	{
		public virtual long ID { get; set; }
		public virtual string Name { get; set; }
		[JsonIgnore]
		public virtual DateTime? Creation { get; set; }
		[JsonIgnore]
		public virtual DateTime? Exclusion { get; set; }
		[JsonIgnore]
		public virtual bool Excluded 
		{ 
			get { return Exclusion.HasValue; }
		}

	}
}
