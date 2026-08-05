using System.Reflection;

namespace TestSamples.NugetLib;

public static class NugetVersionInfo
{
    public static string Version =>
        Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "unknown";
}
