using ModernWpf.Metro;
using System.Reflection;

[assembly: AssemblyCompany("Yin-Chun Wang")]
[assembly: AssemblyCopyright("Copyright © Yin-Chun Wang 2016")]

[assembly: AssemblyVersion(VersionInfo.MajorVersion)]
[assembly: AssemblyFileVersion(VersionInfo.BuildVersion)]
[assembly: AssemblyInformationalVersion(VersionInfo.BuildVersion)]

namespace ModernWpf.Metro
{
    static class VersionInfo
    {
        // keep this same in major releases
        public const string MajorVersion = "2.0.0.0";
        // change this for each nuget release
        public const string BuildVersion = "2.0.0";
    }
}