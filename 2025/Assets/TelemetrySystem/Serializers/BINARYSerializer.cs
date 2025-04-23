using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Telemetry {
    /// <summary>
    /// Serialización en binario
    /// </summary>
    public class BinarySerializer : Serializer {
        public string Serialize(Event t_event) {
            #pragma warning disable SYSLIB0011
            var formatter = new BinaryFormatter();
            #pragma warning restore SYSLIB0011

            // Serializamos en memoria
            using var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, t_event);

            // Convertimos los bytes a string usando base64 para evitar caracteres extraños
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Extension() {
            return ".bin";
        }
    }
}