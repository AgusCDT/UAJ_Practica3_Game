using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando el jugador termina un nivel
    /// </summary>
    [System.Serializable]
    public class LevelEndEvent : Event {
        [JsonProperty(Order = 4)] public int ID_Level { get; private set; }

        public LevelEndEvent(ID_Event type, int currentLevel) : base(type) {
            ID_Level = currentLevel;
        }
    }
}