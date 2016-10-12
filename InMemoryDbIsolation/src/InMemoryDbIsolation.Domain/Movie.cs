using System;

namespace InMemoryDbIsolation.Domain
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Guid Guid { get; set; }
	}
}