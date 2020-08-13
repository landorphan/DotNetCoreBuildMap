using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Landorphan.Abstractions.Tests.StepDefinitions
{
    using System.Runtime.InteropServices;
    using FluentAssertions;
    using Landorphan.Abstractions.FileSystem.Paths;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    [Binding]
    public sealed class PathSteps
    {
        public string suppliedPath;
        PathParser pathParser = new PathParser();
        private string[] tokens;
        private ISegment[] segments;
        public string preParsedPath;
        public IPath parsedPath;
        public IPath originalForm;
        private PathType pathType;
        private static OSPlatform osPlatform;

        internal class MockRuntimeInformation : IRuntimeInformation
        {
            public bool IsOSPlatform(OSPlatform platform)
            {
                return platform == osPlatform;
            }
        }

        [BeforeScenario()]
        public void ResetAbstractionManagerForTest()
        {
            PathAbstractionManager.GetRuntimeInformation = () =>
            {
                return new MockRuntimeInformation();
            };
        }

        [Then(@"the normalization depth should be: (.*)")]
        public void ThenTheNormlizationLevelShouldBe(int normalizationLevel)
        {
            parsedPath.NormalizationDepth.Should().Be(normalizationLevel);
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
            }
        }

        [Then(@"the psth's IsNoramlized property should be (true|false)")]
        public void ThenThePsthSIsNoramlizedPropertyShouldBeTrue(bool isNormalized)
        {
//            parsedPath.NormalizationScope.Should().Be(isNormalized);
        }

        [Given(@"I have the following path: (.*)")]
        public void GivenIHaveTheFollowingPath(string path)
        {
            for (int i = 0; i <= WindowsRelevantPathCharacters.Space; i++)
            {
                path = path.Replace($"%{i:X2}", ((char)i).ToString());
            }
            if (path == "(null)")
            {
                suppliedPath = null;
            }
            else if (path == "(empty)")
            {
                suppliedPath = string.Empty;
            }
            else
            {
                suppliedPath = path.Replace('`', '\\');
            }
        }

        [When(@"I segment the (Windows|Posix) path")]
        public void WhenISegmentThePath(string osPath)
        {
            pathType = PathType.Posix;
            WhenITokenizeThePathWithTheTokenizer(osPath);
            switch (osPath)
            {
                case "Windows":
                    pathType = PathType.Windows;
                    break;
            }

            var segmenter = pathParser.GetSegmenter(pathType);
            segments = segmenter.GetSegments(tokens).ToArray();
        }

        [Then(@"segment '(.*)' should be: (.*)")]
        public void ThenSegmentShouldBeNull(int segment, string value)
        {
            for (int i = 0; i <= WindowsRelevantPathCharacters.Space; i++)
            {
                value = value.Replace($"%{i:X2}", ((char)i).ToString());
            }
//            value = value.Replace("%00", ((char)0x00).ToString());
            ISegment expected = null;
            if (value == "{N} (null)" || segment > segments.Length)
            {
                expected = WindowsSegment.NullSegment;
            }
            else if (value == "{E} (empty)")
            {
                expected = WindowsSegment.EmptySegment;
            }
            else if (value == "{.} .")
            {
                expected = WindowsSegment.SelfSegment;
            }
            else if (value == "{..} ..")
            {
                expected = WindowsSegment.ParentSegment;
            }
            else if (value.StartsWith("{U}"))
            {
                if (pathType == PathType.Posix)
                {
                    expected = new PosixSegment(SegmentType.RemoteSegment, value.Substring(4));
                }
                else
                {
                    expected = new WindowsSegment(SegmentType.RemoteSegment, value.Substring(4));
                }
            }
            else if (value.StartsWith("{R}"))
            {
                expected = new WindowsSegment(SegmentType.RootSegment, value.Substring(4));
            }
            else if (value.StartsWith("{D}"))
            {
                expected = new WindowsSegment(SegmentType.DeviceSegment, value.Substring(4));
            }
            else if (value.StartsWith("{/}"))
            {
                expected = new WindowsSegment(SegmentType.VolumelessRootSegment, value.Substring(4));
            }
            else if (value.StartsWith("{V}"))
            {
                expected = new WindowsSegment(SegmentType.VolumeRelativeSegment, value.Substring(4));
            }
            else if (value.StartsWith("{G}"))
            {
                expected = new WindowsSegment(SegmentType.GenericSegment, value.Substring(4));
            }

            ISegment actual;
            if (segment >= segments.Length)
            {
                actual = WindowsSegment.NullSegment;
            }
            else
            {
                actual = segments[segment];
            }

            if (pathType == PathType.Posix)
            {
                actual.Name.Should().Be(expected.Name);
            }
            else
            {
                actual.Name.Should().BeEquivalentTo(expected.Name);
            }

            actual.SegmentType.Should().Be(expected.SegmentType);
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

        [Then(@"the resulting path should read: (.*)")]
        public void ThenTheResultingPathShouldRead(string expected)
        {
            if (expected == "(null)")
            {
                expected = null;
            }
            else if (expected == "(empty)")
            {
                expected = string.Empty;
            }

            preParsedPath.Should().Be(expected);
        }

        [When(@"I parse the path")]
        public void WhenIParseThePath()
        {
            parsedPath = pathParser.Parse(suppliedPath);
        }

        [When(@"I parse the path as a (Windows|Posix) Path")]
        public void WhenIParseThePathAsA_pathType_Path(PathType pathtype)
        {
            pathType = pathtype;
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
            segments = originalForm.Segments;
        }

        //[Then(@"the segment ""(.*)"" should be (.*)")]
        //public void ThenTheSegmentShouldBeC(string root, string value)
        //{

        //}

        [Then(@"the PathType should be (Windows|Posix)")]
        public void ThenThePathTypeShouldBeWindows(PathType pathType)
        {
            parsedPath.PathType.Should().Be(pathType);
        }


        [Then(@"the path should be anchored to (.*)")]
        public void ThenThePathShouldBeAnchoredToAbsolute(PathAnchor anchor)
        {
            parsedPath.Anchor.Should().Be(anchor);
        }

        [Then(@"the parse status should be (.*)")]
        public void ThenTheParseStatusShouldBeLegal(PathStatus status)
        {
            parsedPath.Status.Should().Be(status);
        }

        [Then(@"the segment length should be (.*)")]
        public void ThenTheSegmentLengthShouldBe(int expected)
        {
            segments.Length.Should().Be(expected);
        }
    }
}
