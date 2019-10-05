using SemVer;

namespace Bump
{

    public enum VersionComponent
    {
        Major,
        Minor,
        Patch,
        Pre
    }

    public static class VersionExtensions
    {
        public static Version Bump(this Version version, VersionComponent component)
        {
            if (component == VersionComponent.Major)
                return new Version(version.Major + 1, 0, 0, version.PreRelease, version.Build);

            if (component == VersionComponent.Minor)
                return new Version(version.Major, version.Minor + 1, 0, version.PreRelease, version.Build);

            if (component == VersionComponent.Patch)
                return new Version(version.Major, version.Minor, version.Patch + 1, version.PreRelease, version.Build);

            if (component == VersionComponent.Pre)
            { 
                var pre = version.PreRelease;
                pre = BumpPre(pre);
                return new Version(version.Major, version.Minor, version.Patch, pre, version.Build);
            }
            else return null;
        }

        public static string BumpPre(string pre)
        {
            int j = pre.Length;
            while (j > 0 && char.IsNumber(pre[j-1])) j--;

            var number = pre[j..];
            if (int.TryParse(number, out int value))
            {
                value++;
                pre = pre.Remove(j);
                pre = pre + value.ToString();
            }
            else
            {
                pre = pre + "-1";
            }
            return pre;
        }

        public static Version ClearPrerelease(this Version version)
        {
            var result = new Version(version.Major, version.Minor, version.Patch, null, version.Build);
            return result;
        }

        public static Version SetPrerelease(this Version version, string pre)
        {
            var result = new Version(version.Major, version.Minor, version.Patch, pre, version.Build);
            return result;
        }
    }
}
