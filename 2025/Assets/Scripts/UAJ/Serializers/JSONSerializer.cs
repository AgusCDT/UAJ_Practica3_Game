//using Newtonsoft.Json;
using System.Xml;

namespace telemetry
{
    /// <summary>
    /// Serializer in JSON format
    /// </summary>
    public class JsonSerializer : Serializer
    {
        /// <summary>
        /// Serializes the parent attributes first and the child attributes second.
        /// </summary>
        public string Serialize(TelemetryEvent t_event)
        {
            /*JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(t_event, jsonSerializerSettings);*/
            return "holaJsonSerializer";
        }

        public string Extension()
        {
            return ".json";
        }
    }
}