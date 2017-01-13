namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Serialization utility class
    /// </summary>
    public static class SerializationUtils
    {

        #region Public Methods

        /// <summary>
        /// The function allow to de-serialize JSON formatted string
        /// </summary>
        /// <param name="jsonType">JSON type</param>
        /// <param name="parsedValue">Parsed string value</param>
        /// <returns>Result parsed object</returns>
        public static object DeserializeFromJson(System.Type jsonType, string parsedValue)
        {
            return JsonConvert.DeserializeObject(parsedValue, jsonType);
        }

        /// <summary>
        /// The function allow to de-serialize JSON formatted string with validation of all missing members
        /// </summary>
        /// <param name="jsonType">JSON type</param>
        /// <param name="parsedValue">Parsed value</param>
        /// <returns>Parsed object</returns>
        public static object DeserializeFromJsonCheckMissingMembers(System.Type jsonType, string parsedValue)
        {
            return JsonConvert.DeserializeObject(parsedValue, jsonType, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
        }

        /// <summary>
        /// The function allow to de-serialize JSON formatted string with validation of all missing members
        /// </summary>
        /// <param name="jsonType">JSON type</param>
        /// <param name="parsedValue">Parsed value</param>
        /// <returns>Parsed object</returns>
        public static object DeserializeFromJsonWithoutNullPropertiesAndHandleMissingMembers(System.Type jsonType, string parsedValue)
        {
            return JsonConvert.DeserializeObject(parsedValue, jsonType, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error });
        }

        /// <summary>
        /// The function allow to de-serialize JSON formatted string to defined type with custom converter
        /// </summary>
        /// <param name="jsonType">Target type</param>
        /// <param name="parsedValue">JSON string</param>
        /// <param name="converter">Custom JSON.NET converter</param>
        /// <returns>Type instance</returns>
        public static object DeserializeFromJsonWithCustomConverter(System.Type jsonType, string parsedValue, JsonConverter converter)
        {
            return JsonConvert.DeserializeObject(parsedValue, jsonType, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error, Converters = { converter } });
        }
        
        /// <summary>
        /// Create JSON by Newton soft framework
        /// </summary>
        /// <typeparam name="T">JSono type</typeparam>
        /// <param name="objectToSeriazlize">Object for serialization</param>
        /// <returns>JSON string</returns>
        public static string SerializeToJson<T>(T objectToSeriazlize)
        {
            return JsonConvert.SerializeObject(objectToSeriazlize);
        }

        /// <summary>
        /// Create JSON by Newton soft framework without members defined as NULL values
        /// </summary>
        /// <typeparam name="T">JSono type</typeparam>
        /// <param name="objectToSeriazlize">Object for serialization</param>
        /// <returns>JSON string</returns>
        public static string SerializeToJsonWithoutNullValues<T>(T objectToSeriazlize)
        {
            return JsonConvert.SerializeObject(objectToSeriazlize, 
                Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// Create JSON by Newton soft framework with intended formatting
        /// </summary>
        /// <typeparam name="T">JSono type</typeparam>
        /// <param name="objectToSeriazlize">Object for serialization</param>
        /// <returns>JSON string</returns>
        public static string SerializeToJsonWithIndentedFormatting<T>(T objectToSeriazlize)
        {
            return JsonConvert.SerializeObject(objectToSeriazlize, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// De-serialize JSON string to object
        /// </summary>
        /// <typeparam name="TJSONObject">Result type</typeparam>
        /// <param name="stringToDeSerialize">JSON string</param>
        /// <returns>Result object</returns>
        public static TJSONObject DeserializeFromJson<TJSONObject>(string stringToDeSerialize)
        {
            return JsonConvert.DeserializeObject<TJSONObject>(stringToDeSerialize);
        }
        
        /// <summary>
        /// De-serialize JSON to dynamic object
        /// </summary>
        /// <param name="stringToDeserialize">String for de-serialization</param>
        /// <returns>Dynamic object</returns>
        public static object DeserializeFromJsonToDynamicObject(string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject(stringToDeserialize);
        }

        /// <summary>
        /// Serializes object to the xml string
        /// </summary>
        /// <typeparam name="TXMLObject">type of object to serialize</typeparam>
        /// <param name="objectToSerialize">object to serialize</param>
        /// <returns>serialized string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string SerializeToXML<TXMLObject>(TXMLObject objectToSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TXMLObject));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            string serializedString;

            StringWriter textWriter = null;
            try
            {
                textWriter = new StringWriter();
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, objectToSerialize);
                    serializedString = textWriter.ToString();
                    textWriter = null;
                }
            }
            finally
            {
                if (textWriter != null)
                    textWriter.Dispose();
            }

            return serializedString;
        }

        /// <summary>
        /// Deserializes the object from string 
        /// </summary>
        /// <param name="objectToDeserialize"></param>
        /// <returns></returns>
        public static TObject DeserializeFromXML<TObject>(string objectToDeserialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TObject));

            TObject result;

            using (TextReader reader = new StringReader(objectToDeserialize))
            {
                result = (TObject)serializer.Deserialize(reader);
            }

            return result;
        }

        /// <summary>
        /// Deserializes the object from string 
        /// </summary>
        /// <param name="objectToDeserialize"></param>
        /// <returns></returns>
        public static TObject DeserializeFromXMLFile<TObject>(TextReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TObject));

            TObject result = (TObject)serializer.Deserialize(reader);

            return result;
        }
        #endregion
    }
}
