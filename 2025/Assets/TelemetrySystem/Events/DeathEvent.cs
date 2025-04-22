using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando el jugador muere
    /// </summary>
    [System.Serializable]
    public class DeathEvent : Event {
        public DeathEvent(ID_Event type) : base(type){
        
        }
    }
}