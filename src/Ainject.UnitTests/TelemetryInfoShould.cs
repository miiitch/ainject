using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Ainject.Abstractions;
using NFluent;
using Xunit;

namespace Ainject.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class TelemetryInfoShould
    {
        [Fact]
        void Be_Created_With_Empty_Dictionary()
        {
            var info = new TelemetryInfo<int>();

            Check.That(info.GetDictionary()).IsNotNull().And.IsEmpty();
            Check.That(info.IsEmpty).IsTrue();
        }

        [Fact]
        void Copy_Values_From_Constructor()
        {
            var originalValues = new Dictionary<string, int>()
            {
                ["A"] = 1,
                ["B"] = 2,
            };

            var info = new TelemetryInfo<int>(originalValues);


            Check.That(info.GetDictionary()).IsNotNull().And.ContainsExactly(originalValues).And.Not
                .IsSameReferenceAs(originalValues);

            Check.That(info.IsEmpty).IsFalse();
        }


        [Fact]
        void Set_And_Get_Value()
        {
            var originalValues = new Dictionary<string, int>()
            {
                ["A"] = 1,
                ["B"] = 2,
            };

            var info = new TelemetryInfo<int>(originalValues) {["C"] = 3};

            Check.That(info["A"]).IsEqualTo(1);
            Check.That(info["B"]).IsEqualTo(2);
            Check.That(info["C"]).IsEqualTo(3);
        }

        [Fact]
        void Copy_Values_From_Constructor_And_From_Append()
        {
            var originalValues = new Dictionary<string, int>()
            {
                ["A"] = 1,
                ["B"] = 2,
            };

            var appendValues = new Dictionary<string,int>()
            {
                ["A"] = 3,
                ["C"] = 4,
            };

            var expectedValues = new Dictionary<string, int>()
            {
                ["A"] = 1,
                ["B"] = 2,
                ["C"] = 4,
            };


            var info = new TelemetryInfo<int>(originalValues);
            info.Append(appendValues);


            Check.That(info.GetDictionary()).ContainsExactly(expectedValues);
        }

        [Fact]
        void Throw_Exception_When_Copy_To_Null_Dictionary()
        {
            var info = new TelemetryInfo<int>();

            Check.ThatCode(() => info.CopyTo(null)).Throws<ArgumentNullException>();
        }

        [Fact]
        void Throw_Exception_When_Appending_Null_Dictionary()
        {
            var info = new TelemetryInfo<int>();

            Check.ThatCode(() => info.Append(null)).Throws<ArgumentNullException>();
        }
    }
}
