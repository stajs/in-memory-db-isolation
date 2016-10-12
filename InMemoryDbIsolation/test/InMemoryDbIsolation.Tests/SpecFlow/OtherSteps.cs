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
	[Binding]
	public class OtherSteps
	{
		public TestContext TestContext { get; } = new TestContext();

		public OtherSteps()
		{
			var ms = StaticRandom.Rand();
			Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId:00} | OtherSteps | Sleep: {ms} ms");
			//Thread.Sleep(ms);
		}

		public MovieContext GetDb()
		{
			return TestContext.ServiceProvider.GetService<MovieContext>();
		}

		[Given(@"the other movies:")]
		public void GivenTheOtherMovies(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				movie.Guid = Guid.NewGuid();
				db.Movies.Add(movie);
			}
			db.SaveChanges();
		}

		[When(@"this other movie is added")]
		public void WhenThisOtherMovieIsAdded(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				movie.Guid = Guid.NewGuid();
				db.Movies.Add(movie);
			}
			db.SaveChanges();
		}

		[Then(@"other movies contains")]
		public void ThenOtherMoviesContains(Table table)
		{
			var db = GetDb();
			var movies = table.CreateSet<Movie>();
			foreach (var movie in movies)
			{
				var guid = GetGuidFromTitle(movie.Title);
				var dbMovies = db.Movies.Where(m => m.Guid == guid);
				Assert.Equal(1, dbMovies.Count());
				Assert.Equal(guid, dbMovies.First().Guid);
			}
		}

		private Guid GetGuidFromTitle(string title)
		{
			var movies = GetDb().Movies.ToList();
			return movies.Single(m => m.Title == title).Guid;
		}
	}
}