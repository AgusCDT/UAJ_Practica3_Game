using Newtonsoft.Json;

namespace Telemetry {
    [System.Serializable]
    public class DeathEvent : Event {
        public DeathEvent(ID_Event type) : base(type){
        
        }
    }
}