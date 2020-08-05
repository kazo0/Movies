using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core.Extensions
{
	public static class StringExtensions
	{
		public static string GetValueOrDefault(this string value, string defaultValue = null)
		{
			return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
		}
	}
}
