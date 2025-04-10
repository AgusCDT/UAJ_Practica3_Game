using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class DamageReceivedEvent : Event {
        [JsonProperty(Order = 4)] public string ID_Entity { get; private set; }
        [JsonProperty(Order = 5)] public int damage { get; private set; }
        [JsonProperty(Order = 6)] public bool death { get; private set; }

        public DamageReceivedEvent(ID_Event type, string ID_Entity_, int damage_, bool death_) : base(type) {
            ID_Entity = ID_Entity_;
            damage = damage_;
            death = death_;
        }
    }
}