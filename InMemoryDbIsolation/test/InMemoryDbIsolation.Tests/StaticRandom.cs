using System;
using System.Threading;

namespace InMemoryDbIsolation.Tests
{
	/// <summary>
	/// http://stackoverflow.com/a/19271062
	/// </summary>
	public static class StaticRandom
	{
		static int seed = Environment.TickCount;

		static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

		public static int Rand()
		{
			return random.Value.Next(0);
		}
	}
}