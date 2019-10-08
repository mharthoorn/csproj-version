using System.Linq;
using System.Xml.Linq;

namespace Bump
{
    public static class XmlExtensions
    {
        public static XElement GetOrCreateElement(this XContainer container, string name)
        {
            var element = container.Element(name);
            if (element != null) return element;
            element = new XElement(name);
            container.Add(element);
            return element;
        }

        public static string GetProperty(this XDocument document, string property)
        {
            var project = document.Element("Project");
            var node = project
                            .Elements("PropertyGroup")
                            .SelectMany(it => it.Elements(property))
                            .SingleOrDefault();
            
            return node.Value;
        }

        public static XElement GetVersionNode(this XDocument document)
        {
            var projectNode = document.GetOrCreateElement("Project");
            var node = projectNode
                            .Elements("PropertyGroup")
                            .SelectMany(it => it.Elements("Version"))
                            .SingleOrDefault() ??
                            projectNode
                            .GetOrCreateElement("PropertyGroup")
                            .GetOrCreateElement("Version");
            return node;
        }

    }
}
