using Newtonsoft.Json;

namespace Telemetry{
    /// <summary>
    /// Evento cuando ataca el jugador
    /// </summary>
    [System.Serializable]
    public class HealthUpEvent : Event {
        [JsonProperty(Order = 4)] public int healing_amount { get; private set; }


        public HealthUpEvent(ID_Event type, int healing) : base(type) {
            healing_amount = healing;
        }
    }
}