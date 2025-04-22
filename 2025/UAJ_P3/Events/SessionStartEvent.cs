using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class SessionStartEvent : Event {
        public SessionStartEvent(ID_Event type) : base(type) {

        }
    }
}