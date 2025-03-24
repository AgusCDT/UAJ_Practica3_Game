using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEventsManager : MonoBehaviour
{
    [field: Header("Player Footsteps")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Player Breathing")]
    [field: SerializeField] public EventReference playerBreathing { get; private set; }

    [field: Header("Player Damaged")]
    [field: SerializeField] public EventReference playerDamaged { get; private set; }

    [field: Header("Pistol Shot")]
    [field: SerializeField] public EventReference pistolShot { get; private set; }

    [field: Header("Pistol Recharge")]
    [field: SerializeField] public EventReference pistolRecharge { get; private set; }

    [field: Header("Crowbar Swing")]
    [field: SerializeField] public EventReference crowbarSwing { get; private set; }

    [field: Header("Note SFX")]
    [field: SerializeField] public EventReference noteGrabed {  get; private set; }

    [field: Header("Open Door")]
    [field: SerializeField] public EventReference openDoor { get; private set; }

    [field: Header("Picked Item")]
    [field: SerializeField] public EventReference pickedItem { get; private set; }

    [field: Header("Ambience General")]
    [field: SerializeField] public EventReference ambienceGeneral { get; private set; } 

    [field: Header("Zombie Bite")]
    [field: SerializeField] public EventReference zombieBite { get; private set; }

    [field: Header("Zombie Scream")]
    [field: SerializeField] public EventReference zombieScream { get; private set; }

    [field: Header("Zombie Noise")]
    [field: SerializeField] public EventReference zombieNoise { get; private set; }

    [field: Header("Zombie Damaged")]
    [field: SerializeField] public EventReference zombieDamaged { get; private set; }


    static private FMODEventsManager _instance;
    static public FMODEventsManager Instance
    {
        get
        {
            return _instance;
        }
    }

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
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
