using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using FMOD;

public enum AREA
{
    GENERAL = 0,
    CHAPEL = 1
}

public enum ELEVATOR
{
    NOTCOMPLETED = 0,
    COMPLETED = 1
}

public class SoundManager : MonoBehaviour
{
    #region parameters
    public bool zombieMuted = false;
    #endregion

    #region references
    private EventInstance ambienceEventInstance;
    private EventInstance breathingEventInstance;
    [SerializeField]
    private StudioEventEmitter eventEmitter;

    [SerializeField] private AudioSource _effectSource, _musicSource;
    static private SoundManager _instance;
    static public SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region methods
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Nuevo
    public void InitializeAmbience()
    {
        ambienceEventInstance = CreateEventInstance(FMODEventsManager.Instance.ambienceGeneral);
        ambienceEventInstance.start();
    }

    public void InitializePlayerBreathing()
    {
        breathingEventInstance = CreateEventInstance(FMODEventsManager.Instance.playerBreathing);
        breathingEventInstance.start();
    }

    public void UpdateBreathingPosition(Vector3 position)
    {
        FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(position);
        breathingEventInstance.set3DAttributes(attributes);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void SetAmbience(AREA a)
    {
        ambienceEventInstance.setParameterByName("area", (float)a);
    }

    public void SetElevator(ELEVATOR e)
    {

        eventEmitter.EventInstance.setParameterByName("ElevatorParameter", (float)e);
    }

    public void SetPlayerBreathing(float b)
    {
        UnityEngine.Debug.Log("Breathing Vida" + b);
        float v;
        RESULT s = breathingEventInstance.getParameterByName("BreathingSpeed", out v);
        UnityEngine.Debug.Log("Vida anterior" + v);
        breathingEventInstance.setParameterByName("BreathingSpeed", b);
    }

    public void ReleasePlayerBreathing()
    {
        breathingEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        breathingEventInstance.release();
    }


    // A partir de aqui es de lo que ya teniamos
    public void MuteMusic(bool music)
    {
        if (music)
        {
            _musicSource.mute = false;
        }
        else
        {
            _musicSource.mute = true;
        }
    }
    public void MuteSFX(bool effects)
    {
        if (effects)
        {
            _effectSource.mute = false;
            zombieMuted = false;
        }
        else
        {
            zombieMuted = true;
            _effectSource.mute = true;
        }
    }
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetEventEmitter()
    {
        // Busca un GameObject por nombre
        GameObject targetObject = GameObject.Find("GameObjectAscensor");
        if (targetObject != null)
        {
            eventEmitter = targetObject.GetComponent<StudioEventEmitter>();
            if (eventEmitter == null)
            {
                UnityEngine.Debug.LogError("No se encontró un Studio Event Emitter en el GameObject especificado.");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("No se encontró el GameObject especificado.");
        }
    }
}
