using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando el jugador retoma la partida
    /// </summary>
    [System.Serializable]
    public class LevelResumeEvent : Event {
        [JsonProperty(Order = 4)] public int ID_Level { get; private set; }

        public LevelResumeEvent(ID_Event type, int currentLevel) : base(type) {
            ID_Level = currentLevel;
        }
    }
}