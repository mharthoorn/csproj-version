using SemVer;
using System.IO;
using System.Xml.Linq;

namespace Bump
{
    public class ProjectFile
    {
        private readonly string filename;
        XDocument document;

        public ProjectFile(string filename)
        {
            this.filename = filename;
            Load();
        }

        public string GetName()
        {
            var name = Path.GetFileNameWithoutExtension(filename);
            return name;
        }

        public void Load()
        {
            document = XDocument.Load(filename, LoadOptions.PreserveWhitespace);
        }

        public void Save()
        {
            // Don't use Save, it adds the xml header.
            //document.Save(csprojFile);
            File.WriteAllText(filename, document.ToString());
        }

        public string GetProperty(string name)
        {
            return document.GetProperty(name);
        }
      
        public string GetVersion()
        {
            return document.GetProperty("Version");
        }

        public void SetVersion(string version)
        {
            var node = document.GetVersionNode();
            node.SetValue(version);
        }

        public void BumpVersion(VersionComponent component)
        {
            var node = document.GetVersionNode();
            var value = node.Value;
            if (value is string)
            {
                Version version = new Version(value, loose: true);
                var bumped = version.Bump(component);
                node.SetValue(bumped.ToString());
            }
            else
            {
                node.SetValue("0.0.1");
            }

        }

        public void ClearPrerelease()
        {
            var node = document.GetVersionNode();
            var value = node.Value;
            if (value is string)
            {
                var v = new Version(value, loose: true);
                var version = v.ClearPrerelease();
                node.SetValue(version.ToString());
            }
            else node.SetValue("0.0.1");
        }

        public void SetPrerelease(string pre)
        {
            var node = document.GetVersionNode();
            var value = node.Value;
            if (value is string)
            {
                var v = new Version(value, loose: true);
                var version = v.SetPrerelease(pre);
                node.SetValue(version.ToString());
            }
            else node.SetValue("0.0.1");
        }
    }
}
