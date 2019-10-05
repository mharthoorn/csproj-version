using Shell.Routing;
using System;
using System.Linq;

namespace Bump
{
    [Module("Commands")]
    public class Commands
    {
        [Command, Default, Hidden]
        public void Default([Alt("?")]Flag help)
        {
            if (help)
            {
                Help();
                return;
            }

            var project = GetSingleProject();
            var version = project.GetVersion();
            Console.WriteLine($"{version}");
        }

        [Command("help", "?"), Help("Shows available commands")]
        public void Help()
        {
            Console.WriteLine("Vsn. Version Bumper App. Copyright (c) 2019 M. Harthoorn");
            Console.WriteLine("This app updates the version in your.csproj project file.");
            RoutingPrinter.PrintRoutes(Routing<Program>.Router.Routes);
            Console.WriteLine("Without parameters -- prints the current version.");
        }

        [Command, Help("Set semver compatible version")]
        public void Set(string version)
        {
            var project = GetSingleProject();
            project.SetVersion(version);
            project.Save();
            var v = project.GetVersion();
            Console.WriteLine($"Version set to: {v}");
        }

        [Command, Help("major, minor, or patch")]
        public void Bump(string part)
        {
            if (!Enum.TryParse<VersionComponent>(part, ignoreCase: true, out var component))
                throw new Exception($"Invalid version component designation: {part}");
            
            var project = GetSingleProject();
            project.BumpVersion(component);
            project.Save();
            var version = project.GetVersion();
            Console.WriteLine($"Version bumped to: {version}");
        }

        [Command]
        public void Release()
        {
            var project = GetSingleProject();
            project.ClearPrerelease();
            project.Save();
            var version = project.GetVersion();
            Console.WriteLine($"Version set to: {version}");
            // wip.
            // removes pre-release details
        }

        [Command]
        public void Pre(string value)
        {
            var project = GetSingleProject();
            project.SetPrerelease(value);
            project.Save();
            var version = project.GetVersion();
            Console.WriteLine($"Version set to: {version}");
            // wip.
            // removes pre-release details
        }

        [Command, Help("Lists all (.csproj file) projects")]
        public void Projects()
        {
            var projects = Utils.GetCsprojFiles(recursive: true);
            foreach(var p in projects)
            {
                Console.WriteLine(p);
            }
        }

        private static ProjectFile GetSingleProject()
        {
            var files = Utils.GetCsprojFiles(recursive: false);

            if (files.Length == 0)
                throw new Exception("No project (.csproj file) was found");

            if (files.Length >= 2)
                throw new Exception("Multiple projects (.csproj files) were found");
            
            return new ProjectFile(files.Single());
        }
    }
}
