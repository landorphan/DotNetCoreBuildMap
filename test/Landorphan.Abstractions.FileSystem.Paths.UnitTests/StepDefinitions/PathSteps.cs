namespace Landorphan.Abstractions.Tests.StepDefinitions
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using FluentAssertions;
    using Landorphan.Abstractions.FileSystem.Paths;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;
    using Newtonsoft.Json;
    using TechTalk.SpecFlow;
    using YamlDotNet.Serialization;
    using Formatting = System.Xml.Formatting;

    [Binding]
    public sealed class PathSteps
    {
        public IPath normalizedPath;
        public IPath originalForm;
        public IPath parsedPath;
        public IPath pathChangeResult;
        public string preParsedPath;
        public string suppliedPath;
        private static OSPlatform osPlatform;
        private readonly PathParser pathParser = new PathParser();
        private bool areEqual;
        private int compareResult;
        private string nameRequestResult;
        private IPath path1;
        private int path1HashCode;
        private IPath path2;
        private int path2HashCode;
        private string psnForm;
        private ISegment[] segments;
        private string serializedForm;
        private string serializeTo;
        private Exception thrownException;
        private string[] tokens;
        private string toStringReturned;
        private PathType PathType { get; set; }

        [BeforeScenario]
        public void ResetAbstractionManagerForTest()
        {
            PathAbstractionManager.GetRuntimeInformation = () =>
            {
                return new MockRuntimeInformation();
            };
        }

        [Then(@"the two paths should be the same")]
        public void ComparePaths()
        {
            pathChangeResult.PathType.Should().Be(parsedPath.PathType);
            pathChangeResult.Anchor.Should().Be(parsedPath.Anchor);
            pathChangeResult.IsDiscouraged.Should().Be(parsedPath.IsDiscouraged);
            pathChangeResult.IsFullyQualified.Should().Be(parsedPath.IsFullyQualified);
            pathChangeResult.Segments.Count.Should().Be(parsedPath.Segments.Count);
            for (var i = 0; i < pathChangeResult.Segments.Count; i++)
            {
                CompareSegment(pathChangeResult.Segments[i], parsedPath.Segments[i].ToPathSegmentNotation());
            }
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

        [Given(@"I parse the following as path (1|2): (.*)")]
        public void GivenIParseTheFollowingAsPath(int pathNumber, string path)
        {
            var parser = new PathParser();
            PathType = osPlatform == OSPlatform.Windows ? PathType.Windows : PathType.Posix;

            if (pathNumber == 1)
            {
                path1 = path == "(null)" ? null : parser.Parse(path,PathType);
            }
            else
            {
                path2 = path == "(null)" ? null: parser.Parse(path,PathType);
            }
        }

        public string PreparePathForTest(string path)
        {
            for (var i = 0; i <= WindowsRelevantPathCharacters.Space; i++)
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

        [Then(@"an exception of type ""(.*)"" should be thrown")]
        public void ThenAnExceptionOfTypeShouldBeThrown(string exceptionType)
        {
            thrownException.GetType().Name.Should().Be(exceptionType);
        }

        [Then(@"get relative path should return: (.*)")]
        public void ThenGetRelativePathShouldReturn(string relativePath)
        {
            relativePath = PreparePathForTest(relativePath);
            pathChangeResult = parsedPath.ConvertToRelativePath();
            pathChangeResult.ToString().Should().Be(relativePath);
        }

        [Then(@"I should receive a path object")]
        public void ThenIShouldReceiveAPathObject()
        {
            parsedPath.Should().NotBeNull();
        }

        [Then(@"I should receive the following string: (.*)")]
        public void ThenIShouldReceiveTheFollowingString(string expectedString)
        {
            var expectedPath = PreparePathForTest(expectedString);
            toStringReturned.Should().Be(expectedPath);
        }

        [Then(@"no exception should be thrown")]
        public void ThenNoExceptionShouldBeThrown()
        {
            thrownException.Should().BeNull();
        }

        [Then(@"path 1 should be (equal to|less than|greater than) path 2")]
        public void ThenPathShouldBeEqualToPath(string comparison)
        {
            switch (comparison)
            {
                case "less than":
                    compareResult.Should().BeLessThan(0);
                    areEqual.Should().BeFalse();
                    break;
                case "greater than":
                    compareResult.Should().BeGreaterThan(0);
                    areEqual.Should().BeFalse();
                    break;
                default:
                    compareResult.Should().Be(0);
                    areEqual.Should().BeTrue();
                    path1HashCode.Should().Be(path2HashCode);
                    break;
            }
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

        [Then(@"token '(.*)' should be: (.*)")]
        public void ThenSegmentShouldBe(int loc, string expected)
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

        [Then(@"the exception message should contain: (.*)")]
        public void ThenTheExceptionMessageShouldContainMessagePart(string messagePart)
        {
            thrownException.Message.Should().Contain(messagePart);
        }

        [Then(@"The following PSN string should be produced: (.*)")]
        public void ThenTheFollowingPSNStringShouldBeProduced(string expectedPsn)
        {
            psnForm.Should().Be(expectedPsn.Replace('`', '\\'));
        }

        [Then(@"the following should be the serialized form: (.*)")]
        public void ThenTheFollowingShouldBeTheSerializedForm(string serializedForm)
        {
            if (serializeTo == "Json")
            {
                // Json has to escape '\' characters so we make that adjustment here.
                this.serializedForm.Should().Be(serializedForm.Replace("`", @"\\", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                this.serializedForm.Should().Be(serializedForm);
            }
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

        [Then(@"the PathType should be (Windows|Posix)")]
        public void ThenThePathTypeShouldBeWindows(PathType pathType)
        {
            parsedPath.PathType.Should().Be(pathType);
        }

        [Then(@"the psth's anchor property should be (Relative|Absolute)")]
        public void ThenThePsthSIsNormalizedPropertyShouldBeTrue(PathAnchor anchor)
        {
            parsedPath.Anchor.Should().Be(anchor);
        }

        [Then(@"the resulting path should have the following Simplification Level: (NotNormalized|SelfReferenceOnly|LeadingParentsOnly|Fully)")]
        public void ThenTheResultingPathShouldHaveTheFollowingNormalizationLevel(SimplificationLevel simplificationLevel)
        {
            normalizedPath.SimplificationLevel.Should().Be(simplificationLevel);
        }

        [Then(@"the resulting path should read: (.*)")]
        public void ThenTheResultingPathShouldRead(string expected)
        {
            expected = PreparePathForTest(expected);
            preParsedPath.Should().Be(expected);
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

        [Then(@"the result should be: (.*)")]
        public void ThenTheResultShouldBeValue(string value)
        {
            value = PreparePathForTest(value);
            nameRequestResult.Should().Be(value);
        }

        [Then(@"the segment length should be (.*)")]
        public void ThenTheSegmentLengthShouldBe(int expected)
        {
            segments.Length.Should().Be(expected);
        }

        [When(@"I ask for the parent path")]
        public void WhenIAskForTheParentPath()
        {
            preParsedPath = parsedPath.GetParent().ToString();
        }

        [When(@"I ask for the path to be represented as a string")]
        public void WhenIAskForThePathToBeRepresentedAsAString()
        {
            toStringReturned = parsedPath.ToString();
        }

        [When(@"I change the path's extension to: (.*)")]
        public void WhenIChangeThePathSExtensionTo_Json(string newExtension)
        {
            newExtension = PreparePathForTest(newExtension);
            pathChangeResult = parsedPath.ChangeExtension(newExtension);
            nameRequestResult = pathChangeResult.ToString();
        }

        [When(@"I compare the paths using the (Sensitive|Insensitive|Default) comparer")]
        public void WhenICompareThePaths(string Comparer)
        {
            IPathComparerAndEquator comparer = null;
            switch (Comparer)
            {
                case "Sensitive":
                    comparer = PathUtilities.CaseSensitiveComparerAndEquator;
                    break;
                case "Insensitive":
                    comparer = PathUtilities.CaseInsensitiveComparerAndEquator;
                    break;
                default:
                    comparer = PathUtilities.DefaultComparerAndEquator;
                    break;
            }
            compareResult = comparer.Compare(path1, path2);
            path1HashCode = comparer.GetHashCode(path1);
            path2HashCode = comparer.GetHashCode(path2);
            areEqual = comparer.Equals(path1, path2);
        }

        [When(@"I convert the path to path segment notation")]
        public void WhenIConvertThePathToPathSegmentNotation()
        {
            psnForm = parsedPath.ToPathSegmentNotation();
        }

        [When(@"I evaluate the original form")]
        public void WhenIEvaluateTheNonNormalizedForm()
        {
            originalForm = parsedPath.SuppliedPath;
            segments = originalForm.Segments.ToArray();
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

        [When(@"I simplify the path")]
        public void WhenINormalizeThePath()
        {
            normalizedPath = parsedPath.Simplify();
            pathChangeResult = normalizedPath;
            preParsedPath = normalizedPath.ToString();
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
        public void WhenIParseThePathAsA_pathType_Path(PathType pathType)
        {
            PathType = pathType;
            parsedPath = pathParser.Parse(suppliedPath, pathType);
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

        [When(@"I re-parse the path")]
        public void WhenIRe_ParseThePath()
        {
            pathChangeResult = pathParser.Parse(suppliedPath);
        }

        [When(@"the (parsed|resulting) path's (Name|NameWithoutExtension|Extension) should be: (.*)")]
        [Then(@"the (parsed|resulting) path's (Name|NameWithoutExtension|Extension) should be: (.*)")]
        public void WhenIRetrieveTheNameOfTheLastSegment(string whichPath, string nameFunction, string value)
        {
            value = PreparePathForTest(value);
            var path = whichPath == "parsed" ? parsedPath : pathChangeResult;
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
            }

            var segmenter = pathParser.GetSegmenter(PathType);
            segments = segmenter.GetSegments(tokens).ToArray();
        }

        [When(@"I serialize the path to (Json|Xml|Yaml) as (Simple|PathSegmentNotation)")]
        public void WhenISerializeThePathToJsonAsSimple(string serializeTo, SerializationForm serializationForm)
        {
            this.serializeTo = serializeTo;
            parsedPath.SerializationMethod = serializationForm;
            switch (serializeTo)
            {
                case "Json":
                    serializedForm = JsonConvert.SerializeObject(parsedPath);
                    break;
                case "Xml":
                    var XmlSer = new XmlSerializer(typeof(ParsedPath));
                    using (var stream = new MemoryStream())
                    using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSer.Serialize(writer, parsedPath);
                        serializedForm = Encoding.UTF8.GetString(stream.ToArray());
                    }
                    break;
                case "Yaml":
                    var YamlSer = new Serializer();
                    serializedForm = YamlSer.Serialize(parsedPath);
                    break;
            }
        }

        [When(@"I tokenize the path with the '(.*)' tokenizer")]
        public void WhenITokenizeThePathWithTheTokenizer(string pathType)
        {
            if (pathType == "Windows")
            {
                var tokenizer = new WindowsPathTokenizer(suppliedPath);
                tokens = tokenizer.GetTokens();
            }
            else
            {
                var tokenizer = new PosixPathTokenizer(suppliedPath);
                tokens = tokenizer.GetTokens();
            }
        }

        [When(@"the path's has extension property is: (true|false)")]
        [Then(@"the path's has extension property is: (true|false)")]
        public void WhenThePathSHasExtensionPropertyIs(bool hasExtension)
        {
            parsedPath.HasExtension.Should().Be(hasExtension);
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

        internal class MockRuntimeInformation : IRuntimeInformation
        {
            public bool IsOSPlatform(OSPlatform platform)
            {
                return platform == osPlatform;
            }
        }
    }
}
