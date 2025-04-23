using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Telemetry;
public class GameManager : MonoBehaviour
{
    #region parameters
    public bool boy;
    #endregion

    #region properties
    public bool IsGamePaused = false;
    public float _currentLife = 100;
    float _maxLife = 100;
    #endregion

    #region references
    static private GameManager _instance; // Unique GameManager instance (Singleton Pattern).

    
    static public GameManager Instance // Public accesor for GameManager instance.
    {
        get
        {
            return _instance; // Para poder instanciar el GameManager y llamarlo desde cualquier script
        }
    }
    public GameObject _player;
    [SerializeField] private GameObject _notaRoom;
    [SerializeField] private GameObject _notaElevator;
    [SerializeField] private GameObject _notaKey;
    [SerializeField] private GameObject _notaShotgun;
    [SerializeField] private GameObject _roomKey;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _parkingDoor;
    [SerializeField] private GameObject _parkingDoorClosed;
    [SerializeField] private GameObject _exitHospital;
    [SerializeField] private GameObject _spawn;
    [SerializeField] private GameObject _elevatorOn;
    //[SerializeField] private GameObject _elevatorOff;
    [SerializeField] private GameObject _elevatorAnimation;
    [SerializeField] private GameObject _shortcut;
    private Player_Life_Component _myPlayerLifeComponent;

    private GameObject saved;
    public List<InteractDetection> _listInteractableObjects;
    private UI_Manager _UIManager;
    #endregion

    #region methods
    private void Awake()
    {
        Telemetry.Telemetry.Init("./telemetry_data/", "2025", 1);
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void InteractableObjectDone(InteractDetection _myInteractDetection) // Gestiona la interaccion con objetos interactuables
    {
        _listInteractableObjects.Remove(_myInteractDetection); // Elimino al objecto con el que interactuado de la lista
        Destroy(_myInteractDetection.gameObject); // Destruye al objeto interactuado de escena
    }
    public void ActivateNoteRoom() // Activa la nota
    {
        _notaRoom.SetActive(true);
        IsGamePaused = true;
    }
    public void DeactivateNoteRoom() // Desactiva la nota 
    {
        _notaRoom.SetActive(false);
        IsGamePaused = false;
    }
    public void ActivateNoteElevator() // Activa la nota
    {
        _notaElevator.SetActive(true);
        IsGamePaused = true;
    }
    public void DeactivateNoteElevator() // Desactiva la nota 
    {
        _notaElevator.SetActive(false);
        IsGamePaused = false;
    }

    public void ActivateNoteKey() // Activa la nota
    {
        _notaKey.SetActive(true);
        IsGamePaused = true;
    }
    public void DeactivateNoteKey() // Desactiva la nota 
    {
        _notaKey.SetActive(false);
        IsGamePaused = false;
    }
    public void ActivateNoteShotgun() // Activa la nota
    {
        _notaShotgun.SetActive(true);
        IsGamePaused = true;
    }
    public void DeactivateNoteShotgun() // Desactiva la nota 
    {
        _notaShotgun.SetActive(false);
        IsGamePaused = false;
    }
    public void ActivarHabitacion(GameObject room, NewRoom triggerToDestroy)
    {
        room.SetActive(true);
        Destroy(triggerToDestroy);
    }
    public void NewMision(string write)
    {
        UI_Manager.Instance.Mision(write);
    }

    public void UnlockExit()
    {
        _elevatorOn.transform.position = new Vector3(9.42f, -2.99f, 0);
        _elevatorAnimation.transform.position = new Vector3(-59.51f, 71.02f, 0);
        _shortcut.SetActive(false);
        NewMision("Generador activado, parece que el ascensor está listo");
    }
    public void GetKey()
    {       
        _parkingDoor.SetActive(true); // Activa la luz del parking
        _parkingDoorClosed.SetActive(true); // Activa el collider de la puerta cerrada
        _exitHospital.SetActive(true); // Activa la salida
        NewMision("Está llave podría ser la escapatoria de este infierno");
    }

    public void OpenParkingDoor()
    {
        _exitHospital.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void PauseGame()
    {
        Telemetry.Telemetry.Instance.TrackEvent(new LevelPauseEvent(Telemetry.Event.ID_Event.LEVEL_PAUSE, 1));      
        IsGamePaused = true;
        UI_Manager.Instance.PauseMenu(true);   
    }
    public void QuitGame()
    {     
        Application.Quit();
        Telemetry.Telemetry.Release();
    }
   
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Replay() 
    {
        UI_Manager.Instance._crowbarPicked = false;
        UI_Manager.Instance._pistolPicked = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadGameOverMenu()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void OnLevelWasLoaded(int level)
    {
        SceneManager.GetActiveScene(); 
        if (level == 1)
        {
            Telemetry.Telemetry.Instance.TrackEvent(new LevelStartEvent(Telemetry.Event.ID_Event.LEVEL_START, level));

            SoundManager.Instance.InitializeAmbience();
            _notaRoom = GameObject.Find("HojaBlancaRoom_1");
            _notaRoom.SetActive(false);
            _notaElevator = GameObject.Find("HojaBlancaElevator");
            _notaElevator.SetActive(false);           
            UI_Manager.Instance.PauseReference();
            UI_Manager.Instance.ControlsReference();            
            UI_Manager.Instance.Control1Reference();
            UI_Manager.Instance.Control2Reference();
            UI_Manager.Instance.Control3Reference();
            UI_Manager.Instance.MisionReference();
            _player = GameObject.Find("Chico");
            _myPlayerLifeComponent = _player.GetComponent<Player_Life_Component>();
            _elevatorOn = GameObject.Find("Elevator_ClosedReady");
            _elevatorAnimation = GameObject.Find("ElevatorAnimation");
            _shortcut = GameObject.Find("Shortcut");
            _myPlayerLifeComponent.LifeBarReference();
            DontDestroyOnLoad(_player);
        }
        if(level == 2)
        {
            Telemetry.Telemetry.Instance.TrackEvent(new LevelStartEvent(Telemetry.Event.ID_Event.LEVEL_START, level));

            _notaKey = GameObject.Find("HojaBlancaKey");
            _notaKey.SetActive(false);
            _notaShotgun = GameObject.Find("HojaBlancaShotgun");
            _notaShotgun.SetActive(false);
            UI_Manager.Instance.PauseReference();
            UI_Manager.Instance.ControlsReference();
            UI_Manager.Instance.Control1Reference();
            UI_Manager.Instance.Control2Reference();
            UI_Manager.Instance.Control3Reference();
            UI_Manager.Instance.MisionReference();
            _myPlayerLifeComponent.LifeBarReference();
            _spawn = GameObject.Find("Spawn");
            _parkingDoor = GameObject.Find("Parking");
            _parkingDoor.SetActive(false);
            _parkingDoorClosed = GameObject.Find("Parking_Closed");
            _parkingDoorClosed.SetActive(false);
            _exitHospital = GameObject.Find("Parking_Opened");
            _exitHospital.SetActive(false);
            _key = GameObject.Find("Key");
            _roomKey = GameObject.Find("Room_Key");
            _roomKey.SetActive(false);
            _player.transform.position = _spawn.transform.position;
            NewMision("Nueva planta, nueva exploración");
        }
        if(level == 3)
        {
            Telemetry.Telemetry.Instance.TrackEvent(new LevelEndEvent(Telemetry.Event.ID_Event.LEVEL_END, level));
            Destroy(_player);
            UI_Manager.Instance.WinMenu(); // Llamada para que salga el menú de victoria
        }
        if (level == 4)
        {
            Telemetry.Telemetry.Instance.TrackEvent(new LevelEndEvent(Telemetry.Event.ID_Event.LEVEL_END, level));
            Destroy(_player);
            UI_Manager.Instance.GameOverMenu();
        }
    }

    #endregion

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _listInteractableObjects = new List<InteractDetection>();
    }
}
