using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    #region references
    static private UI_Manager _instance; 
    static public UI_Manager Instance 
    {
        get
        {
            return _instance; 
        }
    }
    #endregion

    #region parameters
    public Image lifeBar;
    public Text pistolBullets;
    public Text shotgunBullets;
    public Text palancaaviso;
    public Text _mision;
    private float temporizadorpalanca;
    private bool gogo=false;
    public bool _crowbarPicked = false;
    public bool _pistolPicked = false;
    public float maxlife;
    public float currentlife;
    [SerializeField] private GameObject _customizationMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _control1;
    [SerializeField] private GameObject _control2;
    [SerializeField] private GameObject _control3;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private GameObject _pauseMenu;
    #endregion

    #region methods
    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShotgunActivate()
    {
        _control1.SetActive(true);
        palancaaviso.text = "Pulse Click Derecho para disparar con la escopeta";
        gogo = true;
    }
    public void PistolActivate()
    {
        _control2.SetActive(true);
        palancaaviso.text = "Pulse Click Izquierdo para disparar con la pistola";
        gogo = true;
        _pistolPicked = true;
    }
    public void CrowbarActivate()
    {
        palancaaviso.text = "Pulse Espacio para golpear con la palanca";
        gogo = true;
        _control3.SetActive(true);
        _crowbarPicked = true;
    }
    public void balagUI(int bala,int cargador)
    {
        pistolBullets.text = "" + bala + "/" + cargador;
    }
    public void balasgUI(int bala,int cargador)
    {
        shotgunBullets.text= "" + bala + "/" + cargador;
    }
    public void Mision(string write)
    {
        _mision.text = write;
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    public void ControlsMenu(bool enabled)
    {
        _controlsMenu.SetActive(enabled);
    }
    public void PauseMenu(bool enabled)
    {
        _pauseMenu.SetActive(enabled);
        if (!enabled)
        {
            GameManager.Instance.IsGamePaused = false;
        }     
    }
    public void WinMenu()
    {       
        _winMenu.SetActive(true); // Activa el menú de victoria
    }
    public void GameOverMenu()
    {
        _loseMenu.SetActive(true);
    }
    public void MuteMusicButton(bool music)
    {
        SoundManager.Instance.MuteMusic(music);
    }
    public void MuteSFXButton(bool effects)
    {
        SoundManager.Instance.MuteSFX(effects);
    }
    public void ReplayButton()
    {
        GameManager.Instance.Replay();
    }
    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }
    public void PauseReference() // Se usa en el OnLevelWasLoaded del GameManager
    {
        _pauseMenu = GameObject.Find("PauseMenu");
        _pauseMenu.SetActive(false);
    }
    public void ControlsReference() // Se usa en el OnLevelWasLoaded del GameManager
    {
        _controlsMenu = GameObject.Find("ControlsMenu");
        _controlsMenu.SetActive(false);
    }
    public void Control1Reference() // Se usa en el OnLevelWasLoaded del GameManager
    {
        _control1 = GameObject.Find("ControlEscopeta");
        shotgunBullets = _control1.transform.Find("ShotgunBullets").GetComponent<Text>();
        shotgunBullets.text = "";
        _control1.SetActive(false);
    }
    public void Control2Reference() // Se usa en el OnLevelWasLoaded del GameManager
    {
        _control2 = GameObject.Find("ControlPistola");
        pistolBullets = _control2.transform.Find("PistolBullets").GetComponent<Text>();
        pistolBullets.text = "";
        IsPistolPicked();
    }
    public void Control3Reference() // Se usa en el OnLevelWasLoaded del GameManager
    {
        _control3 = GameObject.Find("ControlPalanca");
        palancaaviso = _control3.transform.Find("palancatexto").GetComponent<Text>();
        palancaaviso.text = "";
        IsCrowbarPicked();
    }

    public void MisionReference()
    {
        _mision = GameObject.Find("MisionPanel").transform.Find("Mision").GetComponent<Text>();
        _mision.text = "";

    }

    public void IsCrowbarPicked()
    {
        if (_crowbarPicked == false)
        {
            _control3.SetActive(false);
        }
        else if (_crowbarPicked == true)
        {
            _control3.SetActive(true);
        }

    }
    public void IsPistolPicked()
    {
        if (_pistolPicked == false)
        {
            _control2.SetActive(false);
        }
        else if (_pistolPicked == true) 
        {
            _control2.SetActive(true);
            //pistolBullets.text = "" + bala + "/" + cargador;
        }
            
    }
    public void BackButtonNoteRoomCall()
    {
        GameManager.Instance.DeactivateNoteRoom();
    }
    public void BackButtonNoteElevatorCall()
    {
        GameManager.Instance.DeactivateNoteElevator();
    }
    public void BackButtonNoteKeyCall()
    {
        GameManager.Instance.DeactivateNoteKey();
    }
    public void BackButtonNoteShotgunCall()
    {
        GameManager.Instance.DeactivateNoteShotgun();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);       
    }
    // Update is called once per frame
    void Update()
    {
        if(gogo)
        {
            temporizadorpalanca += Time.deltaTime;
            if (temporizadorpalanca >= 5)
            {
                palancaaviso.text = "";
                temporizadorpalanca = 0;
                gogo = false;
            }
        }
    }
}
