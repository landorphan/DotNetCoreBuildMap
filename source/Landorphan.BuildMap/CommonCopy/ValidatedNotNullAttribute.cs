
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common
{
    using System;

    /// <summary>
    /// Instructs Code Analysis to treat a method as a validation method for a given parameter and not fire code analysis warning CA1062 when it is used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
