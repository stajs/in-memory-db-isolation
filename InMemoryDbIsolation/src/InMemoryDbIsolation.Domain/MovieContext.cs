using Microsoft.EntityFrameworkCore;

namespace InMemoryDbIsolation.Domain
{
	public class MovieContext : DbContext
	{
		public DbSet<Movie> Movies { get; set; }

		public MovieContext()
		{
		}
		
		public MovieContext(DbContextOptions<MovieContext> options) : base(options)
		{
		}
	}
}