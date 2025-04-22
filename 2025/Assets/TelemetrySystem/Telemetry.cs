using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
//using UnityEngine;

namespace Telemetry {
    public class Telemetry {
        private static Telemetry? instance;

        /// <summary>
        /// Variables
        /// </summary>
        private const int ThreadDelay = 1500; // in ms
        private Thread telemetryThread;
        private bool runningThread;

        private List<Persistence> persistences;
        private ConcurrentQueue<Event> eventQueue;

        /// <summary>
        /// Session ID utilizada en la telemetría
        /// </summary>
        private long sessionID;
        public long SessionID { get => sessionID; private set { sessionID = value; } }

        /// <summary>
        /// Nombre del juego utilizado en la telemetría
        /// </summary>
        private string gameName;
        public string GameName { get => gameName; private set { gameName = value; } }

        /// <summary>
        /// Directorio utilizado en la telemetría
        /// </summary>
        private string directory;
        public string Directory { get => directory; private set { directory = value; } }

        private Telemetry() { }

        public static Telemetry Instance => instance;

        /// <summary>
        /// Inicializa la instancia de la telemetría
        /// Devuelve null en caso de haberse ya llamado.
        /// </summary>
        /// <param name="directory_">Directory path to store the data files</param>
        /// <param name="gameName_">Game name used to be used across the telemetry</param>
        /// <param name="sessionId_">Session ID to be used across the telemetry</param>
        /// <returns>true if correctly initialized</returns>
        public static bool Init(string directory_, string gameName_, long sessionId_) {
            if (instance != null) {
                System.Console.WriteLine("Ya has inicializado la instancia.");
                return false;
            }

            instance = new Telemetry();
            instance.TelemetrySetup(directory_, gameName_, sessionId_);
            return true;
        }

        /// <summary>
        /// Libera la instancia.
        /// </summary>
        public static void Release() {
            instance.TelemetryStop();
            instance = null;
        }

        /// <summary>
        /// Almacena el evento a trackear.
        /// </summary>
        /// <param name="t_event"></param>
        public void TrackEvent(Event t_event) {
            eventQueue.Enqueue(t_event);
        }

        /// <summary>
        /// Hilo de la telemetría.
        /// </summary>
        private void Run() {
            while (runningThread) {
                Persist();
                Thread.Sleep(ThreadDelay);
            }
            Persist();
        }

        /// <summary>
        /// Persiste todas la persistencias.
        /// </summary>
        private void Persist() {
            Event? t_event;
            while (eventQueue.TryDequeue(out t_event)) {
                foreach (Persistence persistence in persistences)
                    persistence.Save(t_event);
            }
        }

        /// <summary>
        /// Setup de la instancia.
        /// </summary>
        private void TelemetrySetup(string directory_, string gameName_, long sessionId_) {
            Directory = directory_;
            GameName = gameName_;
            SessionID = sessionId_;

            eventQueue = new ConcurrentQueue<Event>();
            eventQueue.Enqueue(new SessionStartEvent(Event.ID_Event.SESSION_START));

            persistences = new List<Persistence>();
            persistences.Add(new FilePersistence(new JsonSerializer()));
           // persistences.Add(new FilePersistence(new CsvSerializer()));
           // persistences.Add(new FilePersistence(new BinarySerializer()));

            runningThread = true;
            telemetryThread = new Thread(Run);
            telemetryThread.Start();
        }

        /// <summary>
        /// Para la instancia
        /// </summary>
        private void TelemetryStop() {
            runningThread = false;
            eventQueue.Enqueue(new SessionEndEvent(Event.ID_Event.SESSION_END));
            telemetryThread.Join();
        }
    }
}