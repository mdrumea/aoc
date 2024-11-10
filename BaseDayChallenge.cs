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
			var fullNamespaceName = this.GetType().Namespace ?? string.Empty;
			var lastNamespaceName = fullNamespaceName.Split(".").Last();

			var year = YearRegex().Match(fullNamespaceName).Value;
			var day = DayRegex().Match(lastNamespaceName).Value;

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

			if (sampleData.Length == 0)
			{
				throw new FileLoadException("Input file appears to be empty!");
			}

			return sampleData.Split(Environment.NewLine);
		}
	}
}
