using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando emepiza la sesión de telemetría
    /// </summary>
    [System.Serializable]
    public class SessionStartEvent : Event {
        public SessionStartEvent(ID_Event type) : base(type) {

        }
    }
}