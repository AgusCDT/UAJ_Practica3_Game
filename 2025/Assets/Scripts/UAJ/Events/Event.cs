using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class Event {
        public enum ID_Event
        { UNDEFINED, SESSION_START, SESSION_END, LEVEL_START, LEVEL_PAUSE, LEVEL_RESUME, LEVEL_END, 
            ATTACK, DAMAGE_RECIEVED, DEATH, INTERACTION };

        [JsonProperty(Order = 1)] public string ID_Event { get; private set; }
        [JsonProperty(Order = 2)] public long ID_Session { get; private set; }
        [JsonProperty(Order = 3)] public long TimeStamp { get; private set; }

        protected Event(ID_Event type) {
            ID_Event = type.ToString();
            ID_Session = Telemetry.Instance.SessionID;
            TimeStamp = ((System.DateTimeOffset)System.DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}