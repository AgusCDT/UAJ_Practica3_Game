using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class SessionEndEvent : Event {
        public SessionEndEvent(ID_Event type) : base(type) {

        }
    }
}