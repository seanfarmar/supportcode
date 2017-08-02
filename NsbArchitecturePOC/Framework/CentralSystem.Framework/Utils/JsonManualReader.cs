using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using CentralSystem.Framework.Exceptions;

namespace CentralSystem.Framework.Utils
{
    public abstract class JsonManualReader : IDisposable
    {
        protected JsonTextReader JsonTextReader { get; set; }
        protected short PropertiesCounter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void Read(string jsonText)
        {
            if (IsJsonStructureValid(jsonText) == false)
                throw new CentralSystemException("Invalid Json Structure");

            JsonTextReader = new JsonTextReader(new StringReader(jsonText));
            SkipToProperties();
        }

        public bool IsJsonStructureValid(string jsonText)
        {
            if (jsonText.IndexOf("{{", StringComparison.OrdinalIgnoreCase) > -1 || jsonText.IndexOf("}}", StringComparison.OrdinalIgnoreCase) > -1)
                return false;

            return true;
        }

        /// <summary>
        /// Read a value which is placed *after* START_OBJECT tag
        /// </summary>
        /// <returns></returns>
        public List<int> ReadIntArray()
        {
            List<int> result = new List<int>();
            int? intMember;
            while ((intMember = JsonTextReader.ReadAsInt32()) != null)
            {
                result.Add(intMember.Value);
            }
            
            return result;
        }

        /// <summary>
        /// Check more properties exist for reading
        /// </summary>
        /// <returns></returns>
        public bool HasMoreProperties()
        {
            // read property
            while (JsonTextReader.Read())
            {
                if (JsonTextReader.Value != null)
                {
                    return true;
                }
                else
                {
                    if (JsonTextReader.TokenType == JsonToken.EndObject)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Validate the name of the property to ensure its order and its camel case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ValidatePropertyName(string value, string name)
        {
            return string.Equals(value, name);
        }


        private void SkipToProperties()
        {
            JsonTextReader.Read();
        }

        public void Dispose()
        {
            if (JsonTextReader != null)
                JsonTextReader.Close();
        }
    }
}
