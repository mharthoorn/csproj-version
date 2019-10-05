using SemVer;

namespace Bump
{

    public enum VersionComponent
    {
        Major,
        Minor,
        Patch,
    }

    public static class VersionExtensions
    {
        public static Version Bump(this Version version, VersionComponent component)
        {
            if (component == VersionComponent.Major)
                return new Version(version.Major + 1, version.Minor, version.Patch, version.PreRelease, version.Build);

            if (component == VersionComponent.Minor)
                return new Version(version.Major, version.Minor + 1, version.Patch, version.PreRelease, version.Build);

            if (component == VersionComponent.Patch)
                return new Version(version.Major, version.Minor, version.Patch + 1, version.PreRelease, version.Build);

            else return null;
        }
    }
}
