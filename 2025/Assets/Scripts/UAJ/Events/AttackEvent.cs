using Newtonsoft.Json;

namespace Telemetry{
    [System.Serializable]
    public class AttackEvent : Event {
        [JsonProperty(Order = 4)] public string ID_Weapon { get; private set; }
        [JsonProperty(Order = 5)] public int Ammo { get; private set; }


        public AttackEvent(ID_Event type, string ID_Weapon, int currentAmmo) : base(type) {
            this->ID_Weapon = ID_Weapon;
            Ammo = currentAmmo;
        }
    }
}