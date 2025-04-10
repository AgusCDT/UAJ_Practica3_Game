using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class LevelEndEvent : Event {
        [JsonProperty(Order = 4)] public int ID_Level { get; private set; }

        public LevelEndEvent(ID_Event type, int currentLevel) : base(type) {
            ID_Level = currentLevel;
        }
    }
}