using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
	internal abstract partial class BaseDayChallenge
	{
		private HttpClient httpClient = new HttpClient();

		protected string[] LoadSource(Source source)
		{
			return source switch
			{
				Source.Sample => LoadEmbeddedFile(Source.Sample),
				Source.Input => LoadEmbeddedFile(Source.Input),
				_ => throw new NotImplementedException()
			};
		}

		[GeneratedRegex(@"\d+")]
		private static partial Regex YearRegex();

		[GeneratedRegex(@"\d+")]
		private static partial Regex DayRegex();

		private string[] LoadEmbeddedFile(Source source)
		{
			var namespaceName = this.GetType().Namespace ?? string.Empty;
			var className = this.GetType().Name;

			var year = YearRegex().Match(namespaceName).Value;
			var day = DayRegex().Match(className).Value;

			var samplePath = $"AdventOfCode.Year{year}.Day{day}";

			var resourceName = source switch
			{
				Source.Sample => $"{samplePath}.sample.txt",
				Source.Input => $"{samplePath}.input.txt",
				_ => throw new NotImplementedException()
			};

			var assembly = this.GetType().Assembly;

			using var stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null)
			{
				throw new InvalidOperationException($"Resource '{resourceName}' not found.");
			}
			
			using var reader = new System.IO.StreamReader(stream);
			var sampleData = reader.ReadToEnd();

			return sampleData.Split(Environment.NewLine);
		}
	}
}
