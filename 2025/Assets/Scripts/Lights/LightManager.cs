using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _lightDelay = 0.75f;
    public bool _electricidadActiva = false;
    [SerializeField] private int _maxFusibles = 3;
    public int _currentFusibles = 0;
    public float _currentLife = 100;
    [SerializeField] private GameObject _electricityStatic;
    [SerializeField] private GameObject _electricityOn;
    [SerializeField] private GameObject _electricityOff;
    [SerializeField] private GameObject _globalLightOn;
    [SerializeField] private GameObject _globalLightOff;
    [SerializeField] private GameObject _elevatorOff;
    [SerializeField] private AudioClip _clip;  
    #endregion

    #region properties
    private bool state = true;
    #endregion

    #region references
    [SerializeField]
    private List<Light_Behaviour> _lightList;
    static private LightManager _instance; // Unique LightManager instance (Singleton Pattern).
    static public LightManager Instance // Public accesor for LightManager instance.
    {
        get
        {
            return _instance; // Para poder instanciar el LightManager y llamarlo desde cualquier script
        }
    }
    #endregion

    #region methods
    public void LightsActivated(Light_Behaviour lightsToActive)
    {
        _lightList.Add(lightsToActive);
    }
    public void LightsGlobalActivated()
    {
        _electricidadActiva = true;
        Destroy(_electricityOn);
        Destroy(_electricityStatic);
        _electricityOff.SetActive(true);
        _globalLightOff.SetActive(false);
        _globalLightOn.SetActive(true);
        _elevatorOff.SetActive(false);
        SoundManager.Instance.PlaySound(_clip);
    }
    public void CheckFusibles() // Actualiza el numero de fusibles activos y los compara con el numero maximo posible para activar el panel electrico en su momento
    {
        _currentFusibles++;
        GameManager.Instance.NewMision("Parece que tienes que usar el ascensor... Recoge fusibles " + _currentFusibles + "/3");
        if (_currentFusibles >= _maxFusibles)
        {
            GameManager.Instance.NewMision("Todos los fusibles recogidos. Colocalos en el lugar adecuado");
            _electricityOn.SetActive(true);
        }
    }
    private void CambioEstadoLuces()
    {
        state = !state;

        foreach(Light_Behaviour _myLight in _lightList)
        {
            _myLight.gameObject.SetActive(state);
        }

        if (_electricidadActiva && !state)
        {
            CancelInvoke("CambioEstadoLuces");
        }
    }


    private void Awake()
    {
        _instance = this;
        _lightList = new List<Light_Behaviour>();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CambioEstadoLuces", _lightDelay, _lightDelay);
    }
}
