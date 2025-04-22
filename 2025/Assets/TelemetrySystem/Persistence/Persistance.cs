using Telemetry;

namespace Telemetry
{
    /// <summary>
    /// Clase común para todas las persistencias
    /// </summary>
    public abstract class Persistence
    {
        protected Serializer serializer;

        /// <summary>
        /// Recibe el tipo de serialización
        /// </summary>
        /// <param name="serializer_">Serialization type</param>
        protected Persistence(Serializer serializer_)
        {
            serializer = serializer_;
        }

        /// <summary>
        /// Persiste el evento enviado según la serilaización.
        /// </summary>
        /// <param name="t_event">Event sent</param>
        public abstract void Save(Event t_event);
    }
}
