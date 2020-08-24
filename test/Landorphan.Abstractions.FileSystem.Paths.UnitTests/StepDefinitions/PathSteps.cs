using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Landorphan.Abstractions.Tests.StepDefinitions
{
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.Serialization;
    using FluentAssertions;
    using Landorphan.Abstractions.FileSystem.Paths;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;
    using Newtonsoft.Json;
    using YamlDotNet.Serialization;
    using Formatting = Newtonsoft.Json.Formatting;

    [Binding]
    public sealed class PathSteps
    {
        public string suppliedPath;
        private readonly PathParser pathParser = new PathParser();
        private string[] tokens;
        private ISegment[] segments;
        public string preParsedPath;
        public IPath parsedPath;
        public IPath originalForm;
        public IPath normalizedPath;
        public IPath pathChangeResult;
        private PathType PathType { get; set; }
        private static OSPlatform osPlatform;
        private string toStringReturned;
        private Exception thrownException;
        private string nameRequestResult;
        private string psnForm;
        private string seralizedForm;

        internal class MockRuntimeInformation : IRuntimeInformation
        {
            public bool IsOSPlatform(OSPlatform platform)
            {
                return platform == osPlatform;
            }
        }

        [BeforeScenario]
        public void ResetAbstractionManagerForTest()
        {
            PathAbstractionManager.GetRuntimeInformation = () =>
            {
                return new MockRuntimeInformation();
            };
        }

        [Given(@"I'm running on the following Operating System: (Windows|Linux|OSX)")]
        public void GivenImRunningOnTheFollowingOperatingSystem(string platform)
        {
            switch (platform)
            {
                case "Linux":
                    osPlatform = OSPlatform.Linux;
                    break;
                case "Windows":
                    osPlatform = OSPlatform.Windows;
                    break;
                case "OSX":
                    osPlatform = OSPlatform.OSX;
                    break;
                default:
                    throw new InvalidOperationException("Unknown OS Type");
            }
        }

        [Then(@"the psth's anchor property should be (Relative|Absolute)")]
        public void ThenThePsthSIsNoramlizedPropertyShouldBeTrue(PathAnchor anchor)
        {
            parsedPath.Anchor.Should().Be(anchor);
        }

        [Given(@"I have the following path: (.*)")]
        public void GivenIHaveTheFollowingPath(string path)
        {
            if (path.Contains("PSN:", StringComparison.Ordinal))
            {
                suppliedPath = path.Replace('`', '\\');
            }
            else
            {
                suppliedPath = PreparePathForTest(path);
            }
        }

        public string PreparePathForTest(string path)
        {
            for (int i = 0; i <= WindowsRelevantPathCharacters.Space; i++)
            {
                path = path.Replace($"%{i:X2}", ((char)i).ToString(), StringComparison.Ordinal);
            }
            if (path == "(null)")
            {
                path = null;
            }
            else if (path == "(empty)")
            {
                path = string.Empty;
            }
            else
            {
                path = path.Replace('`', '\\');
            }

            return path;
        }

        [When(@"I segment the (Windows|Posix) path")]
        public void WhenISegmentThePath(string osPath)
        {
            PathType = PathType.Posix;
            WhenITokenizeThePathWithTheTokenizer(osPath);
            switch (osPath)
            {
                case "Windows":
                    PathType = PathType.Windows;
                    break;
                default:
                    break;
            }

            var segmenter = pathParser.GetSegmenter(PathType);
            segments = segmenter.GetSegments(tokens).ToArray();
        }


        [When(@"I manipulate the path by adding the segment \((.*)\) (insert before|append after) offset (.*)")]
        public void WhenIManipulateThePathByAddingTheSegmentOffset(string segmentString, string location, int offset)
        {
            var segment = ParseSegment(segmentString);
            if (location == "insert before")
            {
                parsedPath = parsedPath.InsertSegmentBefore(offset, segment);
            }
            else
            {
                parsedPath = parsedPath.AppendSegmentAfter(offset, segment);
            }

        }

        [When(@"I re-parse the path")]
        public void WhenIRe_ParseThePath()
        {
            pathChangeResult = pathParser.Parse(suppliedPath);
        }
        
        [Then(@"the two paths should be the same")]
        public void ComparePaths()
        {
            pathChangeResult.PathType.Should().Be(parsedPath.PathType);
            pathChangeResult.Anchor.Should().Be(parsedPath.Anchor);
            pathChangeResult.IsDiscouraged.Should().Be(parsedPath.IsDiscouraged);
            pathChangeResult.IsFullyQualified.Should().Be(parsedPath.IsFullyQualified);
            pathChangeResult.Segments.Count.Should().Be(parsedPath.Segments.Count);
            for (int i = 0; i < pathChangeResult.Segments.Count; i++)
            {
                CompareSegment(pathChangeResult.Segments[i], parsedPath.Segments[i].ToPathSegmentNotation());
            }
        }


        [When(@"I convert the path to path segment notation")]
        public void WhenIConvertThePathToPathSegmentNotation()
        {
            psnForm = parsedPath.ToPathSegmentNotation();
        }

        [Then(@"The following PSN string should be produced: (.*)")]
        public void ThenTheFollowingPSNStringShouldBeProduced(string expectedPsn)
        {
            psnForm.Should().Be(expectedPsn.Replace('`', '\\'));
        }


        public void CompareSegment(ISegment actualSegment, string value)
        {
            //            value = PreparePathForTest(value);
            var expected = ParseSegment(value);
            if (PathType == PathType.Posix)
            {
                actualSegment.ToString().Should().Be(expected.ToString());
            }
            else
            {
                actualSegment.ToString().Should().BeEquivalentTo(expected.ToString());
            }
            actualSegment.SegmentType.Should().Be(expected.SegmentType);
        }

        private ISegment ParseSegment(string value)
        {
            ISegment expected = null;
            if (PathType == PathType.Posix)
            {
                expected = PosixSegment.ParseFromString(value);
            }
            else
            {
                expected = WindowsSegment.ParseFromString(value);
            }

            return expected;
            if (value == "{0} (null)")
            {
                expected = WindowsSegment.NullSegment;
            }
            else if (value == "{E} (empty)")
            {
                expected = WindowsSegment.EmptySegment;
            }
            else if (value == "{S} .")
            {
                expected = WindowsSegment.SelfSegment;
            }
            else if (value == "{P} ..")
            {
                expected = WindowsSegment.ParentSegment;
            }
            else if (value.StartsWith("{X}", StringComparison.Ordinal))
            {
                if (PathType == PathType.Posix)
                {
                    expected = new PosixSegment(SegmentType.RemoteSegment, value.Substring(4));
                }
                else
                {
                    expected = new WindowsSegment(SegmentType.RemoteSegment, value.Substring(4));
                }
            }
            else if (value.StartsWith("{R}", StringComparison.Ordinal))
            {
                if (PathType == PathType.Posix)
                {
                    expected = new PosixSegment(SegmentType.RootSegment, string.Empty);
                }
                else
                {
                    expected = new WindowsSegment(SegmentType.RootSegment, value.Substring(4));
                }
            }
            else if (value.StartsWith("{D}", StringComparison.Ordinal))
            {
                expected = new WindowsSegment(SegmentType.DeviceSegment, value.Substring(4));
            }
            else if (value.StartsWith("{$}", StringComparison.Ordinal))
            {
                expected = new WindowsSegment(SegmentType.VolumelessRootSegment, string.Empty);
            }
            else if (value.StartsWith("{V}", StringComparison.Ordinal))
            {
                expected = new WindowsSegment(SegmentType.VolumeRelativeSegment, value.Substring(4));
            }
            else if (value.StartsWith("{G}", StringComparison.Ordinal))
            {
                expected = new WindowsSegment(SegmentType.GenericSegment, value.Substring(4));
            }

            return expected;
        }

        [Then(@"segment '(.*)' should be: (.*)")]
        public void ThenSegmentShouldBeNull(int segment, string value)
        {

            ISegment actual;
            if (segment >= segments.Length)
            {
                actual = WindowsSegment.NullSegment;
            }
            else
            {
                actual = segments[segment];
            }
            CompareSegment(actual, value);

        }

        [When(@"I preparse the path as a (Posix|Windows) style path")]
        public void WhenIPreparseThePath(PathType style)
        {
            if (style == PathType.Windows)
            {
                preParsedPath = WindowsPathTokenizer.PreParsePath(suppliedPath);
            }
            else
            {
                preParsedPath = PosixPathTokenizer.PreParsePath(suppliedPath);
            }
        }

        [When(@"I ask for the parent path")]
        public void WhenIAskForTheParentPath()
        {
            preParsedPath = parsedPath.GetParent().ToString();
        }


        [Then(@"the resulting path should read: (.*)")]
        public void ThenTheResultingPathShouldRead(string expected)
        {
            expected = PreparePathForTest(expected);
            preParsedPath.Should().Be(expected);
        }

        [When(@"I simplify the path")]
        public void WhenINormalizeThePath()
        {
            normalizedPath = parsedPath.Simplify();
            pathChangeResult = normalizedPath;
            preParsedPath = normalizedPath.ToString();
        }
        
        [Then(@"the resulting path should have the following Simplification Level: (NotNormalized|SelfReferenceOnly|LeadingParentsOnly|Fully)")]
        public void ThenTheResultingPathShouldHaveTheFollowingNormalizationLevel(SimplificationLevel simplificationLevel)
        {
            normalizedPath.SimplificationLevel.Should().Be(simplificationLevel);
        }

        [Then(@"the (parse|resulting) path's IsDiscouraged property should be (true|false)")]
        public void ThenTheResultingPathSIsDiscouragedPropertyShouldBeFalse(string parseOrResulting, bool expected)
        {
            if (parseOrResulting == "parse")
            {
                parsedPath.IsDiscouraged.Should().Be(expected);
            }
            else
            {
                pathChangeResult.IsDiscouraged.Should().Be(expected);
            }
        }

        [Given(@"the (parse|resulting) path's FullyQualified property should be: (true|false)")]
        [Then(@"the (parse|resulting) path's FullyQualified property should be: (true|false)")]
        public void ThenThePathSFullyQualifiedPropertyShouldBeFalse(string parseOrResulting, bool expected)
        {
            if (parseOrResulting == "parse")
            {
                parsedPath.IsFullyQualified.Should().Be(expected);
            }
            else
            {
                pathChangeResult.IsFullyQualified.Should().Be(expected);
            }
        }


        [When(@"I ask for the path to be represented as a string")]
        public void WhenIAskForThePathToBeRepresentedAsAString()
        {
            toStringReturned = parsedPath.ToString();
        }

        [Then(@"an exception of type ""(.*)"" should be thrown")]
        public void ThenAnExceptionOfTypeShouldBeThrown(string exceptionType)
        {
            thrownException.GetType().Name.Should().Be(exceptionType);
        }
        
        [Then(@"the exception message should contain: (.*)")]
        public void ThenTheExceptionMessageShouldContainMessagePart(string messagePart)
        {
            thrownException.Message.Should().Contain(messagePart);
        }


        [Then(@"no exception should be thrown")]
        public void ThenNoExceptionShouldBeThrown()
        {
            thrownException.Should().BeNull();
        }
        
        [Then(@"I should receive the following string: (.*)")]
        public void ThenIShouldReceiveTheFollowingString(string expectedString)
        {
            var expectedPath = PreparePathForTest(expectedString);
            toStringReturned.Should().Be(expectedPath);
        }

        [When(@"the path's has extension property is: (true|false)")]
        [Then(@"the path's has extension property is: (true|false)")]
        public void WhenThePathSHasExtensionPropertyIs(bool hasExtension)
        {
            parsedPath.HasExtension.Should().Be(hasExtension);
        }

        [When(@"I change the path's extension to: (.*)")]
        public void WhenIChangeThePathSExtensionTo_Json(string newExtension)
        {
            newExtension = PreparePathForTest(newExtension);
            pathChangeResult = parsedPath.ChangeExtension(newExtension);
            nameRequestResult = pathChangeResult.ToString();
        }

        [When(@"the (parsed|resulting) path's (Name|NameWithoutExtension|Extension) should be: (.*)")]
        [Then(@"the (parsed|resulting) path's (Name|NameWithoutExtension|Extension) should be: (.*)")]
        public void WhenIRetreiveTheNameOfTheLastSegment(string whichPath, string nameFunction, string value)
        {
            value = PreparePathForTest(value);
            IPath path = whichPath == "parsed" ? parsedPath : pathChangeResult;
            string nameOperation;
            if (nameFunction == "Name")
            {
                nameOperation = path.Name;
            }
            else if (nameFunction == "NameWithoutExtension")
            {
                nameOperation = path.NameWithoutExtension;
            }
            else
            {
                nameOperation = path.Extension;
            }

            nameOperation.Should().Be(value);
        }

        [Then(@"the result should be: (.*)")]
        public void ThenTheResultShouldBeValue(string value)
        {
            value = PreparePathForTest(value);
            nameRequestResult.Should().Be(value);
        }

        private string serializeTo;
        [When(@"I serialize the path to (Json|Xml|Yaml) as (Simple|PathSegmentNotation)")]
        public void WhenISerializeThePathToJsonAsSimple(string serializeTo, SerializationForm serializationForm)
        {
            this.serializeTo = serializeTo;
            parsedPath.SerializationMethod = serializationForm;
            switch (serializeTo)
            {
                case "Json":
                    this.seralizedForm = JsonConvert.SerializeObject(parsedPath);
                    break;
                case "Xml":
                    System.Xml.Serialization.XmlSerializer XmlSer = new System.Xml.Serialization.XmlSerializer(typeof(ParsedPath));
                    using (MemoryStream stream = new MemoryStream())
                    using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        writer.Formatting = System.Xml.Formatting.Indented;
                        XmlSer.Serialize(writer, parsedPath);
                        seralizedForm = Encoding.UTF8.GetString(stream.ToArray());
                    }
                    break;
                case "Yaml":
                    var YamlSer = new Serializer();
                    this.seralizedForm = YamlSer.Serialize(parsedPath);
                    break;
            }
        }

        [Then(@"the following should be the serialized form: (.*)")]
        public void ThenTheFollowingShouldBeTheSerializedForm(string serializedForm)
        {
            if (serializeTo == "Json")
            {
                // Json has to escape '\' characters so we make that adjustment here.
                seralizedForm.Should().Be(serializedForm.Replace("`", @"\\"));
            }
            else
            {
                seralizedForm.Should().Be(serializedForm);
            }
        }


        [Given(@"I parse the path")]
        [When(@"I parse the path")]
        public void WhenIParseThePath()
        {
            parsedPath = pathParser.Parse(suppliedPath);
            PathType = parsedPath.PathType;
            // NOTE: Unless this is overriden by a test step ... the normalized path = the parsedPath on first parsing 
            normalizedPath = parsedPath;
        }

        [Given(@"I parse the path as a (Windows|Posix) Path")]
        [When(@"I parse the path as a (Windows|Posix) Path")]
        public void WhenIParseThePathAsA_pathType_Path(PathType pathtype)
        {
            PathType = pathtype;
            parsedPath = pathParser.Parse(suppliedPath, pathtype);
        }

        [Then(@"I should receive a path object")]
        public void ThenIShouldReceiveAPathObject()
        {
            parsedPath.Should().NotBeNull();
        }

        [When(@"I tokenize the path with the '(.*)' tokenizer")]
        public void WhenITokenizeThePathWithTheTokenizer(string pathType)
        {
            if (pathType == "Windows")
            {
                WindowsPathTokenizer tokenizer = new WindowsPathTokenizer(suppliedPath);
                tokens = tokenizer.GetTokens();
            }
            else
            {
                PosixPathTokenizer tokenizer = new PosixPathTokenizer(suppliedPath);
                tokens = tokenizer.GetTokens();
            }
        }

        [Then(@"token '(.*)' should be: (.*)")]
        public void ThenSetmentShouldBe(int loc, string expected)
        {
            string actual = null;
            if (loc < tokens.Length)
            {
                actual = tokens[loc];
            }

            if (expected == "(null)")
            {
                expected = null;
            }
            else if (expected == "(empty)")
            {
                expected = string.Empty;
            }

            actual.Should().Be(expected);
        }

        [When(@"I evaluate the original form")]
        public void WhenIEvaluteTheNonnormalizedForm()
        {
            this.originalForm = parsedPath.SuppliedPath;
            segments = originalForm.Segments.ToArray();
        }

        [Then(@"the PathType should be (Windows|Posix)")]
        public void ThenThePathTypeShouldBeWindows(PathType pathType)
        {
            parsedPath.PathType.Should().Be(pathType);
        }

        [Given(@"the (resulting|parse) path should be anchored to (.*)")]
        [Then(@"the (resulting|parse) path should be anchored to (.*)")]
        public void ThenThePathShouldBeAnchoredToAbsolute(string parseOrResulting, PathAnchor anchor)
        {
            if (parseOrResulting == "resulting")
            {
                pathChangeResult.Anchor.Should().Be(anchor);
            }
            else
            {
                parsedPath.Anchor.Should().Be(anchor);
            }
        }

        [Given(@"the (parse|resulting) path's root segment should return: (.*)")]
        [Then(@"the (parse|resulting) path's root segment should return: (.*)")]
        public void ThenThePathSHasRootSegmentShouldReturn(string parseOrResulting, string root)
        {
            if (parseOrResulting == "parse")
            {
                CompareSegment(parsedPath.RootSegment, root);
            }
            else
            {
                CompareSegment(pathChangeResult.RootSegment, root);
            }
        }
        
        [Then(@"get relative path should return: (.*)")]
        public void ThenGetRelativePathShouldReturn(string relativePath)
        {
            relativePath = PreparePathForTest(relativePath);
            pathChangeResult = parsedPath.ConvertToRelativePath();
            pathChangeResult.ToString().Should().Be(relativePath);
        }

        [Given(@"the (parse|resulting) status should be (.*)")]
        [Then(@"the (parse|resulting) status should be (.*)")]
        public void ThenTheParseStatusShouldBeLegal(string parseOrResulting, PathStatus status)
        {
            if (parseOrResulting == "parse")
            {
                parsedPath.Status.Should().Be(status);
            }
            else
            {
                pathChangeResult.Status.Should().Be(status);
            }

        }

        [Then(@"the segment length should be (.*)")]
        public void ThenTheSegmentLengthShouldBe(int expected)
        {
            segments.Length.Should().Be(expected);
        }
    }
}
