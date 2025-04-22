using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando termina la sesión de telemetría
    /// </summary>
    [System.Serializable]
    public class SessionEndEvent : Event {
        public SessionEndEvent(ID_Event type) : base(type) {

        }
    }
}