using System;
using System.Linq;
using System.Threading;
using InMemoryDbIsolation.Domain;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace InMemoryDbIsolation.Tests.SpecFlow
{
	[Binding()]
	public class BaseSteps
	{
		public TestContext TestContext { get; } = new TestContext();

		public BaseSteps()
		{
			var ms = StaticRandom.Rand();
			Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId:00} | BaseSteps | Sleep: {ms} ms");
			//Thread.Sleep(ms);
		}

		public MovieContext GetDb()
		{
			return TestContext.ServiceProvider.GetService<MovieContext>();
		}

		[Given(@"the movies:")]
		public void GivenTheMovies(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				db.Movies.Add(movie);
			}
			db.SaveChanges();
		}

		[When(@"this movie is added")]
		public void WhenThisMovieIsAdded(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				db.Movies.Add(movie);
			}
			db.SaveChanges();
		}

		[Then(@"movies contains")]
		public void ThenMoviesContains(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				var dbMovie = db.Movies.Single(m => m.Guid == movie.Guid);
				Assert.NotNull(dbMovie);
			}
		}
	}
}