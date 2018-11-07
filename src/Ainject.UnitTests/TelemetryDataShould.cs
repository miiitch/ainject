using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ainject.Abstract;
using NFluent;
using Xunit;

namespace Ainject.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class TelemetryDataShould
    {
        [Fact]
        public void Copy_Data_From_Constructor()
        {
            var data = new TelemetryData {["A"] = "X"};

            var createdTelemetry = new TelemetryData(data);

            Check.That(createdTelemetry.Dictionary).Not.IsSameReferenceAs(data.Dictionary).And
                .ContainsExactly(data.Dictionary);

        }

        [Fact]
        public void Merge_Of_A_Single_Data_Array_With_No_Always_Clone_Must_Not_Clone_A_Single_Data()
        {
            var data = new TelemetryData {["D"] = "C"};
            var dataArray = new[] { data};
            var mergedData = dataArray.Merge();

            Check.That(mergedData.Dictionary).ContainsExactly(data.Dictionary).And.IsSameReferenceAs(data.Dictionary);
        }

        [Fact]
        public void Merge_Of_A_Single_Data_Array_With_Always_Clone_Must_Clone_A_Single_Data()
        {
            var data = new TelemetryData { ["D"] = "C" };
            var dataArray = new[] { data };
            var mergedData = dataArray.Merge(true);

            Check.That(mergedData.Dictionary).ContainsExactly(data.Dictionary).And.Not.IsSameReferenceAs(data.Dictionary);
        }

        [Fact]
        public void Merge_Data_Into_A_Single_Data()
        {
            var data1 = new TelemetryData
            {
                ["A"] = "X",
                ["B"] = "Y",

            };
            var data2 = new TelemetryData
            {
          
                ["B"] = "Z",
                ["C"] = "Z",
            };

            var data = new TelemetryData [] { data1,data2};
            var result = data.Merge();

            var expected = new Dictionary<string, string>()
            {
                ["A"] = "X",
                ["B"] = "Z",
                ["C"] = "Z",
            };
            Check.That(result.Dictionary).ContainsExactly(expected);
        }


        [Fact]
        public void Append_Data_Into_A_Single_Data()
        {
            var data1 = new TelemetryData
            {
                ["A"] = "X",
                ["B"] = "Y",

            };
            TelemetryData data2 = null;
            var data3 = new TelemetryData
            {

                ["B"] = "Z",
                ["C"] = "Z",
            };

          
            var result = new TelemetryData();

            var dataToAppend = new[] {data1, data2, data3};

            result.AppendAll(dataToAppend);
            var expected = new Dictionary<string, string>()
            {
                ["A"] = "X",
                ["B"] = "Z",
                ["C"] = "Z",
            };
            Check.That(result.Dictionary).ContainsExactly(expected);
        }

        [Fact]
        public void Do_Nothing_If_AppendAll_Content_Is_Null()
        {         
            var result = new TelemetryData();

            Check.ThatCode(() => result.AppendAll(null)).Throws<ArgumentNullException>();
        }


    }
}