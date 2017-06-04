using System;

namespace HomeBudget
{
    [ExcludeFromCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ExcludeFromCoverageAttribute : Attribute { }
}