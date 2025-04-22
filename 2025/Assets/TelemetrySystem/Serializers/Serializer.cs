namespace Telemetry { 
    public interface Serializer {
        /// <summary>
        /// Serializa un evento al formato correspondiente
        /// </summary>
        public string Serialize(Event t_event);
        /// <summary>
        /// Extension del archivo en funcion del tipo de serializacion
        /// </summary>
        public string Extension();
    }
}