﻿using System;

namespace ThingFilter
{
	/// <summary>
	/// Evaluates matches on a "not equal to" basis.
	/// </summary>
	public class NotEqualToMatchEvaluator : IMatchEvaluator
	{
		/// <summary>
		/// Gets the operator for this evaluator.  This value will be used between the tag and the query.
		/// </summary>
		public string Operation => "<>";

		/// <summary>
		/// Determines whether a match is found for string values.
		/// </summary>
		/// <param name="query">The query token.</param>
		/// <param name="target">The target value to match.</param>
		/// <param name="caseSensitive"><c>true</c> if the comparison should consider case; <c>false</c> otherwise.</param>
		/// <returns><c>true</c> if a match is found; <c>false</c> otherwise.</returns>
		public bool Match(string query, string target, bool caseSensitive)
		{
			if (string.IsNullOrEmpty(query) && string.IsNullOrEmpty(target)) return false;
			var comparison = caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
			return !string.Equals(query, target, comparison);
		}
		/// <summary>
		/// Determines whether a match is found for numeric values.
		/// </summary>
		/// <param name="query">The query token.</param>
		/// <param name="target">The target value to match.</param>
		/// <returns><c>true</c> if a match is found; <c>false</c> otherwise.</returns>
		public bool Match(double query, double target)
		{
			return query != target;
		}
		/// <summary>
		/// Determines whether a match is found for boolean values.
		/// </summary>
		/// <param name="query">The query token.</param>
		/// <param name="target">The target value to match.</param>
		/// <returns><c>true</c> if a match is found; <c>false</c> otherwise.</returns>
		public bool Match(bool query, bool target)
		{
			return query != target;
		}
	}
}