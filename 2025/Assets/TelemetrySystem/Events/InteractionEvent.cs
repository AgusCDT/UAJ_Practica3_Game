using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando el jugador inmteractua con objetos
    /// </summary>
    [System.Serializable]
    public class InteractionEvent : Event {
        [JsonProperty(Order = 4)] public string ID_Interactuable { get; private set; }

        public InteractionEvent(ID_Event type, string ID_Interactuable_) : base(type) {
            ID_Interactuable = ID_Interactuable_;
        }
    }
}