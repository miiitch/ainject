using System.Diagnostics.CodeAnalysis;
using Ainject.Abstractions;
using NFluent;
using Xunit;

namespace Ainject.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class TelemetryMetricsShould
    {
        [Fact]
        public void Copy_Data_From_Constructor()
        {
            var metrics = new TelemetryMetrics() { ["A"] = 4 };

            var createdMetrics = new TelemetryMetrics(metrics);

            Check.That(createdMetrics.Dictionary).Not.IsSameReferenceAs(metrics.Dictionary).And
                .ContainsExactly(metrics.Dictionary);

        }
    }
}