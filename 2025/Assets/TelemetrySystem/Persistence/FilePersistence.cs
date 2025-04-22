using System.IO;

namespace Telemetry {
    /// <summary>
    /// Persistencia de los eventos en un archivo
    /// </summary> 
    class FilePersistence : Persistence {        
        protected Serializer serializer;
        string fileName;
        /// <summary>
        /// Guarda una ruta para crear en la carpeta de telemetria (GameName_SessionID.[json/csv/bin])
        /// </summary>
        public FilePersistence(Serializer serializer_) : base(serializer_){
            serializer = serializer_;
            if (!Directory.Exists(Telemetry.Instance.Directory))
                Directory.CreateDirectory(Telemetry.Instance.Directory);
            fileName = Telemetry.Instance.Directory + Telemetry.Instance.GameName + "_" + Telemetry.Instance.SessionID.ToString() + serializer.Extension();
        }
        /// <summary>
        /// Persiste el evento enviado
        /// Escribe el evento en la ruta guardada
        /// </summary>
        public override void Save(Event t_event) {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true)) {
                string serialisedEvent = serializer.Serialize(t_event);
                streamWriter.WriteLine(serialisedEvent);
            }
        }
    }
}