namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// JSON builder helper class
    /// </summary>
    public sealed class JsonBuilder
    {

        #region Members

        /// <summary>
        /// JSON object parser based on Newton soft framework
        /// </summary>
        private readonly JObject m_jObject;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public JsonBuilder()
        {
            m_jObject = new JObject();
        }

        /// <summary>
        /// Parse JSON value into JObject
        /// </summary>
        /// <param name="json">String JSON value</param>
        public JsonBuilder(string json)
        {
            m_jObject = JObject.Parse(json);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return final JSON string value
        /// </summary>
        /// <returns>JSON text value</returns>
        public string GetJson()
        {
            return m_jObject.ToString(Formatting.None);
        }

        /// <summary>
        /// Return final JSON converted with custom converter
        /// </summary>
        /// <returns>JSON text value</returns>
        public string GetJsonWithCustomConverter(JsonConverter converter)
        {
            return m_jObject.ToString(Formatting.None, converter);
        }

        /// <summary>
        /// Return final JSON string value with intended formatting
        /// </summary>
        /// <returns>JSON text value</returns>
        public string GetJsonWithIndentedFormatting()
        {
            return m_jObject.ToString(Formatting.Indented);
        }

        /// <summary>
        /// Merge all properties
        /// </summary>
        /// <param name="json">JSON string</param>
        public void MergeFrom(string json)
        {
            JObject sourceJObject = JObject.Parse(json);
            foreach (KeyValuePair<string, JToken> keyValue in sourceJObject)
            {
                m_jObject[keyValue.Key] = keyValue.Value;
            }
        }

        /// <summary>
        /// Function checks property existing in the JSON
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>True - current property provided in JSON</returns>
        public bool Contains(string propertyName)
        {
            return m_jObject[propertyName] != null;
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <typeparam name="TValue">Property value type</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property value</returns>
        public TValue Get<TValue>(string propertyName)
        {
            return m_jObject[propertyName].Value<TValue>();
        }

        /// <summary>
        /// Set JSON property
        /// </summary>
        /// <typeparam name="TValue">Property value type</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyValue">Property value</param>
        public void Set<TValue>(string propertyName, TValue propertyValue)
        {
            m_jObject[propertyName] = new JValue(propertyValue);
        }

        /// <summary>
        /// Set JSON object
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyValue">Object</param>
        public void SetObject(string propertyName, object propertyValue)
        {
            m_jObject[propertyName] = JObject.FromObject(propertyValue);
        }

        /// <summary>
        /// Add property value
        /// </summary>
        /// <typeparam name="TValue">Property value type</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyValue">Property value</param>
        public void Add<TValue>(string propertyName, TValue propertyValue)
        {
            m_jObject.Add(propertyName, new JValue(propertyValue));
        }

        /// <summary>
        /// Add JSON object
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyValue">Object</param>
        public void AddObject(string propertyName, object propertyValue)
        {
            m_jObject.Add(propertyName, JObject.FromObject(propertyValue));
        }

        /// <summary>
        /// Remove property value
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void Remove(string propertyName)
        {
            m_jObject.Remove(propertyName);
        }

        /// <summary>
        /// Remove all properties
        /// </summary>
        public void RemoveAll()
        {
            m_jObject.RemoveAll();
        }

        /// <summary>
        /// True - in current property defined Null value
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>True - null value</returns>
        public bool IsNull(string propertyName)
        {
            return (m_jObject[propertyName].Type == JTokenType.Null);
        }

        #endregion

    }
}
