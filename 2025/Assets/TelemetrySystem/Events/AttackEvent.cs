using Newtonsoft.Json;

namespace Telemetry{
    /// <summary>
    /// Evento cuando ataca el jugador
    /// </summary>
    [System.Serializable]
    public class AttackEvent : Event {
        [JsonProperty(Order = 4)] public string ID_Weapon { get; private set; }
        [JsonProperty(Order = 5)] public int Ammo { get; private set; }


        public AttackEvent(ID_Event type, string ID_Weapon_, int currentAmmo) : base(type) {
            ID_Weapon = ID_Weapon_;
            Ammo = currentAmmo;
        }
    }
}