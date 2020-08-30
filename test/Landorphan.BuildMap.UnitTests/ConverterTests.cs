using System;
using System.IO;
using System.Linq;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization.Formatters;
using Landorphan.BuildMap.Serialization.Formatters.Implementation;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Landorphan.BuildMap.UnitTests
{
    public class ConverterTests
    {
        [SetUp]
        public void Setup()
        {
            // Reserved for future needs.
        }

        [Test]
        public void ICanReadAJsonFile()
        {
            using (var stream = typeof(ConverterTests).Assembly.GetManifestResourceStream(
                "Landorphan.BuildMap.UnitTests.TestAssets.Maps.ExeLibTest.json")) 
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var formatter = new JsonFormatter();
                var map = formatter.Read(json);
            }
        }

        [Test]
        public void ICanReadAMapFile()
        {
            using (var stream = typeof(ConverterTests).Assembly.GetManifestResourceStream(
                "Landorphan.BuildMap.UnitTests.TestAssets.Maps.ExeLibTest.map")) 
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var formatter = new MapFormatter();
                var map = formatter.Read(json);
            }
        }

        [Test]
        public void ICanFormatATable()
        {
            Map map = null;
            using (var stream = typeof(ConverterTests).Assembly.GetManifestResourceStream(
                "Landorphan.BuildMap.UnitTests.TestAssets.Maps.ExeLibTest.map"))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var formatter = new MapFormatter();
                map = formatter.Read(json);
            }

            string[] items = TableFormatter.GetAllItems().ToArray();
            var tableFormatter = new TableFormatter(null);
            string table = tableFormatter.Write(map);
            Console.WriteLine(table);
        }
    }
}