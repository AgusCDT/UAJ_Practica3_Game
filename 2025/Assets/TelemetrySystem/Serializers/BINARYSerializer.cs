using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Telemetry {
    /// <summary>
    /// Serialización en binario
    /// </summary>
    public class BinarySerializer : Serializer {
        public string Serialize(Event t_event) {
            string serialized;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream()) {
                memoryStream.Position = 0;

                #pragma warning disable SYSLIB0011
                binaryFormatter.Serialize(memoryStream, t_event);
                #pragma warning restore SYSLIB0011


                using (StreamReader streamReader = new StreamReader(memoryStream)) {
                    serialized = streamReader.ReadToEnd();
                }
            }

            return serialized;
        }

        public string Extension() {
            return ".bin";
        }
    }
}