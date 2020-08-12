namespace Landorphan.BuildMap.UnitTests
{
    using System;
    using System.IO;
    using FluentAssertions;
    using Landorphan.BuildMap.Construction;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Serialization.Formatters.Implementation;
    using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
    using NUnit.Framework;

    public class CreationTests
    {
        [Test]
        public void ICanCreateAMapFrom()
        {
            var currentLocation = Directory.GetCurrentDirectory();
            while (currentLocation.Length > 0)
            {
                currentLocation = Path.GetDirectoryName(currentLocation);
                if (File.Exists(Path.Combine(currentLocation, "dotnetmap.sln")))
                {
                    break;
                }
            }

//            currentLocation = "/repo/macos/stor/monolith";
            
            currentLocation.Should().NotBeEmpty();
            MapManagement mapManagement = new MapManagement();
            Map map = mapManagement.Create(currentLocation, new [] {
                "**/*.csproj", "**/*.sln", "**/*.vbproj"
            });
            IFormatWriter tableFormatter = new TableFormatter(TableFormatter.GetAllItems());
            var str = tableFormatter.Write(map);
            Console.WriteLine(str);
        }
    }
}
