namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Converter utilities
    /// </summary>
    public static class ConverterUtils
    {

        #region Methods

        /// <summary>
        /// Remove namespaces from xml
        /// </summary>
        /// <param name="xmlNode">XML string</param>
        /// <returns>New xml string</returns>
        public static string RemoveXMLNamespaces(string xmlString)
        {
            XDocument xDocument = XDocument.Parse(xmlString);

            foreach (var elem in xDocument.Descendants())
                elem.Name = elem.Name.LocalName;

            xDocument.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            return xDocument.ToString();
        }

        /// <summary>
        /// Convert XML document to JSON dynamic object
        /// </summary>
        /// <param name="xmlNode">XML string</param>
        /// <returns>Dynamic object</returns>
        public static object ConvertXMLToDynamicObject(string xmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return ConvertXMLToDynamicObject(xmlDocument);
        }

        /// <summary>
        /// Convert XML document to JSON dynamic object
        /// </summary>
        /// <param name="xmlNode">XML node</param>
        /// <returns>Dynamic object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static object ConvertXMLToDynamicObject(XmlNode xmlNode)
        {
            string jsonString = JsonConvert.SerializeXmlNode(xmlNode, Newtonsoft.Json.Formatting.None, true);
            return SerializationUtils.DeserializeFromJsonToDynamicObject(jsonString);
        }

        #endregion

    }
}
