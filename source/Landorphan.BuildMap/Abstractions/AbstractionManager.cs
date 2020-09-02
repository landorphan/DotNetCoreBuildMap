namespace Landorphan.BuildMap.Abstractions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Landorphan.BuildMap.Abstractions.FileSystem;
    using Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile;
    using Landorphan.BuildMap.Construction.SolutionModel;
    using Landorphan.Common;
    using Microsoft.Build.Construction;

    [SuppressMessage(
        "CodeSmell",
        "S1104: Make this filed 'private' and encapsulate it in a 'public' property",
        Justification = "This is by design so that this can be overriden by tests.  (tistocks - 2020-08-03)")]
    [SuppressMessage(
        "Unknown",
        "CA2211: Non-constant fields should not be visible",
        Justification = "This is by design so that this can be overriden by tests.  (tistocks - 2020-08-03)")]
    [SuppressMessage(
        "CodeSmell",
        "S2223: Change the visibility of ... or make it 'const' or 'readonly'",
        Justification = "This is by design so that this can be overriden by tests. (tistocks - 2020-08-03")]
    public static class AbstractionManager
    {
        public static Func<IFileSystem> GetFileSystem = InternalGetFileSystem;
        public static Func<SuppliedFile, ISolutionFile> ParseSolutionFile = InternalParseSolutionFile;

        public static IFileSystem InternalGetFileSystem()
        {
            return new FileSystemAbstraction();
        }

        public static ISolutionFile InternalParseSolutionFile(SuppliedFile suppliedFile)
        {
            suppliedFile.ArgumentNotNull(nameof(suppliedFile));
            var slnFile = SolutionFile.Parse(suppliedFile.Paths.Absolute);
            return new SolutionFileAbstraction(slnFile);
        }

        public static void Reset()
        {
            GetFileSystem = InternalGetFileSystem;
            ParseSolutionFile = InternalParseSolutionFile;
        }
    }
}
