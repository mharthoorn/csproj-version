using System.IO;
using System.Linq;

namespace Bump
{



    public static class Utils
    {
       

        public static string[] GetCsprojFiles(bool recursive) => Directory
            .EnumerateFileSystemEntries(Directory.GetCurrentDirectory(), "*.csproj",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToArray();

        //public static void SetVersion(string csprojFile, string version)
        //{
        //    var document = XDocument.Load(csprojFile, LoadOptions.PreserveWhitespace);
        //    var versionNode = document.GetVersionNode();
        //    versionNode.SetValue(version);
            
        //    // Don't use Save, it adds the xml header.
        //    //document.Save(csprojFile);
        //    File.WriteAllText(csprojFile, document.ToString());
        //}

        //public static void BumpVersion(string csprojFile, VersionComponent component)
        //{
        //    var document = XDocument.Load(csprojFile);
        //    var node = document.GetVersionNode();
        //    var value = node.Value ?? "0.1";
        //    Version version = new Version(value);
        //    var bumped = version.Bump(component);
        //    node.Value = bumped.ToString();
        //    document.Save(csprojFile);
        //}
    
    }
}
