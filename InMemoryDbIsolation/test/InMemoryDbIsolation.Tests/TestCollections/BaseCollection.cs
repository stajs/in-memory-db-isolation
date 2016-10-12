using System;
using System.Linq;
using System.Threading;
using InMemoryDbIsolation.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InMemoryDbIsolation.Tests.TestCollections
{
	public class BaseCollection
	{
		public TestContext TestContext { get; } = new TestContext();
		public Guid PulpFictionGuid { get; } = Guid.NewGuid();

		public BaseCollection()
		{
			// Seed.
			var db = TestContext.ServiceProvider.GetService<MovieContext>();
			db.Movies.Add(new Movie { Guid = PulpFictionGuid, Title = "Pulp Fiction" });
			db.Movies.Add(new Movie { Guid = Guid.NewGuid(), Title = "Desperado" });
			db.Movies.Add(new Movie { Guid = Guid.NewGuid(), Title = "Inglourious Basterds" });
			db.Movies.Add(new Movie { Guid = Guid.NewGuid(), Title = "Django Unchained" });
			db.Movies.Add(new Movie { Guid = Guid.NewGuid(), Title = "The Hateful Eight" });
			db.SaveChanges();

			var ms = StaticRandom.Rand();

			Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId:00} | PulpFictionGuid: {PulpFictionGuid} | Sleep: {ms} ms");

			//Thread.Sleep(ms);
		}

		public MovieContext GetDb()
		{
			return TestContext.ServiceProvider.GetService<MovieContext>();
		}

		[Fact]
		public void Test1()
		{
			Assert.Equal(5, GetDb().Movies.Count());
		}

		[Fact]
		public void Test2()
		{
			var db = GetDb();
			db.Movies.Add(new Movie { Guid = Guid.NewGuid(), Title = "Jackie Brown" });
			db.SaveChanges();

			Assert.Equal(6, GetDb().Movies.Count());
		}

		[Fact]
		public void Test3()
		{
			Assert.NotNull(GetDb().Movies.FirstOrDefault(m => m.Title.Contains("Pulp")));
		}

		[Fact]
		public void Test4()
		{
			Assert.Null(GetDb().Movies.FirstOrDefault(m => m.Title.Contains("Jackie")));
		}

		[Fact]
		public void Test5()
		{
			Assert.Equal(PulpFictionGuid, GetDb().Movies.FirstOrDefault(m => m.Title.Contains("Pulp"))?.Guid);
		}
	}
}