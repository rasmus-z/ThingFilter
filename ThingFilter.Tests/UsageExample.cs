﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThingFilter;

namespace FilterTests
{
	[TestClass]
	public class UsageExample
	{
		class Subject
		{
			public string Prop1 { get; set; }
			public int Value { get; set; }
			public string Exclude { get; set; }
			public Subject Deep { get; set; }
		}

		[TestMethod]
		public void DoIt()
		{
			var filter = new ThingFilter<Subject>()
				.MatchOn(s => s.Prop1)
				.MatchOn(s => s.Value)
				.MatchOn(s => s.Deep)
				.MatchOn(s => s.Deep?.Value);
			var collection = new List<Subject>
				{
					new Subject
						{
							Prop1 = "Prop1",
							Value = 5,
							Exclude = "Prop2"
						},
					new Subject
						{
							Prop1 = "Prop1a",
							Value = 6,
							Exclude = "Prop2"
						},
					new Subject
						{
							Prop1 = "Prop3",
							Exclude = "Prop1",
							Deep = new Subject {Value = 6}
						},
					new Subject
						{
							Prop1 = "Prop4",
							Value = 8,
							Exclude = "Prop1"
						}
				};

			var filtered = filter.Apply(collection, "prop1 6");
			Assert.AreEqual(3, filtered.Count());
			collection.Add(new Subject {Prop1 = "Prop5", Value = 6});
			Assert.AreEqual(4, filtered.Count());

			filter.SortByRelevance();
			var sorted = filter.Apply(collection, "prop1 6");

			Assert.AreSame(collection[1], sorted.First());

			var typeFilter = filter.Apply(collection, "ubj");

			Assert.AreEqual(1, typeFilter.Count());
		}

		[TestMethod]
		public void QuotedStringMatch()
		{
			var filter = new ThingFilter<Subject>()
				.MatchOn(s => s.Prop1);
			var collection = new List<Subject>
				{
					new Subject
						{
							Prop1 = "Prop1",
							Value = 5,
							Exclude = "Prop2"
						},
					new Subject
						{
							Prop1 = "Prop1 and Prop2",
							Value = 6,
							Exclude = "Prop2"
						},
					new Subject
						{
							Prop1 = "Prop3",
							Exclude = "Prop1",
							Deep = new Subject {Value = 6}
						},
					new Subject
						{
							Prop1 = "Prop4",
							Value = 8,
							Exclude = "Prop1"
						}
				};

			var results = filter.Apply(collection, "\"prop1 and\" prop4");
			Assert.AreEqual(2, results.Count());
			Assert.AreSame(collection[1], results.First());
		}
	}
}