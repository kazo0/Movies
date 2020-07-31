using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Safe<T>(this IEnumerable<T> list)
		{
			return list ?? Enumerable.Empty<T>();
		}

		public static bool None<T>(this IEnumerable<T> list)
		{
			return !list.Safe().Any();
		}
	}
}
