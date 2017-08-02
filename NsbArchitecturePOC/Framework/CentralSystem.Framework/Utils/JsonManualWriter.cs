namespace CentralSystem.Framework.Utils
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for manual json writing
    /// </summary>
    public class JsonManualWriter : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        protected JsonTextWriter JsonTextWriter { get; set; }

        #region Ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textWriter"></param>
        public JsonManualWriter(TextWriter textWriter)
        {
            JsonTextWriter = new JsonTextWriter(textWriter);
        }
        #endregion

        #region public methods
        public void WriteProperty(string propertyName, string value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, int value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, long value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, bool value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, decimal value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, decimal? value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteDecimalFormattedProperty(string propertyName, decimal? value)
        {
            StartProperty(propertyName);

            string valueAsString = String.Format("{0:0.00}", value);
            JsonTextWriter.WriteRawValue(valueAsString);
        }


        public void WriteProperty(string propertyName, char value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, double value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        public void WriteProperty(string propertyName, DateTime value)
        {
            StartProperty(propertyName);
            JsonTextWriter.WriteValue(value);
        }

        /// <summary>
        /// Write value
        /// </summary>
        public void WriteValue(int value)
        {
            JsonTextWriter.WriteValue(value);
        }

        /// <summary>
        /// Create a property that later will be set value of array or object
        /// </summary>
        /// <param name="propertyName"></param>
        public void StartProperty(string propertyName)
        {
            JsonTextWriter.WritePropertyName(propertyName);
        }

        /// <summary>
        /// Start a new property followed by "[{" to mark array and object following
        /// </summary>
        public void StartPropertyWithArrayAndObject(string propertyName)
        {
            StartProperty(propertyName);
            StartArray();
            StartObject();
        }

        /// <summary>
        /// Start Manual array [don't forget to close by EndArray]
        /// </summary>
        public void StartArray()
        {
            JsonTextWriter.WriteStartArray();
        }

        /// <summary>
        /// End manual array 
        /// </summary>
        public void EndArray()
        {
            JsonTextWriter.WriteEndArray();
        }

        /// <summary>
        /// Start manual object
        /// </summary>
        public void StartObject()
        {
            JsonTextWriter.WriteStartObject();
        }

        /// <summary>
        /// End manual object
        /// </summary>
        public void EndObject()
        {
            JsonTextWriter.WriteEndObject();
        }

        /// <summary>
        /// Close a sequence of "[{" markings with the equivalent "}]" to end the array and object that followed it
        /// </summary>
        public void EndArrayAndObject()
        {
            EndObject();
            EndArray();
        }
        #endregion 

        #region IDisposable Implementation
        /// <summary>
        /// Clean resources
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (JsonTextWriter != null)
                    JsonTextWriter.Close();
            }

            disposed = true;
        }
        #endregion
    }
}
