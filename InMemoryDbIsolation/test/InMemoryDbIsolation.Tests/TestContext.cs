using System;
using InMemoryDbIsolation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InMemoryDbIsolation.Tests
{
	public class TestContext
	{
		public IServiceProvider ServiceProvider { get; private set; }

		public TestContext()
		{
			var services = new ServiceCollection();

			services
				.AddEntityFrameworkInMemoryDatabase()
				.AddDbContext<MovieContext>(options =>
					options
						.UseInMemoryDatabase(Guid.NewGuid().ToString())
						//.UseInternalServiceProvider(services.BuildServiceProvider())
				);

			ServiceProvider = services.BuildServiceProvider();
		}
	}
}