﻿using Newtonsoft.Json;

namespace Telemetry {
    /// <summary>
    /// Evento cuando el jugador pausa la partida
    /// </summary>
    [System.Serializable]
    public class LevelPauseEvent : Event {
        [JsonProperty(Order = 4)] public int ID_Level { get; private set; }

        public LevelPauseEvent(ID_Event type, int currentLevel) : base(type) {
            ID_Level = currentLevel;
        }
    }
}