using System.Text;
using System.Reflection;
using System.Linq;

namespace Telemetry {
    /// <summary>
    /// Serialización en CSV
    /// </summary>
    public class CsvSerializer : Serializer {
        public string Serialize(Event t_event) {
            var type = t_event.GetType();
            var baseType = type.BaseType;

            // Obtenemos todas las propiedades: primero las del padre, luego las propias
            var allProperties = baseType.GetProperties()
                                        .Concat(type.GetProperties()
                                                    .Where(p => p.DeclaringType == type));

            // Serializamos valores 
            var values = allProperties
                         .Select(p => p.GetValue(t_event)?.ToString() ?? string.Empty);

            return string.Join(";", values);
        }

        public string Extension(){
            return ".csv";
        }
    }
}