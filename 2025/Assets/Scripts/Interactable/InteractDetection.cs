using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDetection : MonoBehaviour
{

    private bool _isNotaActivada = false;
    #region references 
    private Municion _myMunicion;
    private Fusibles _myFusibles;
    private Botiquin _myBotiquin;
    private ActivaNota _myActivaNota;
    private ActivaNotaElevator _myActivaNotaElevator;
    private ActivaNotaKey _myActivaNotaKey;
    private ActivaNotaShotgun _myActivaNotaShotgun;
    private Cadaver _myCadaver;
    private Cadaver2 _myCadaver2;
    private Cadaver3 _myCadaver3;
    private Key _myKey;
    private Elevator _myElevator;
    private ParkingDoor _myParkingDoor;
    private ElectricityActivated _myElecricityActivated;
    #endregion

    #region methods
    public void Interact(int indice)
    {
        switch(indice)
        {
            case 1: _myMunicion.DaAmmo(); break; // Municion
            case 2: _myFusibles.SumaFusible(); break; // Fusibles
            case 3: _myBotiquin.AplicaCura(); break; // Botiquin
            case 4: _myActivaNota.ToShowNote(); break; // ActivaNotaRoom
            case 5: _myCadaver.LeverActivated(); UI_Manager.Instance.CrowbarActivate(); break; // Cadaver
            case 6: _myCadaver2.PistolaActivada(); UI_Manager.Instance.PistolActivate(); break; // Cadaver2
            case 7: _myCadaver3.EscopetaActivada(); UI_Manager.Instance.ShotgunActivate(); break; // Cadaver3
            case 8: _myKey.ToActivateParkingDoor(); break; // Llave
            case 9: _myElevator.OpenElevator(); break; // Ascensor
            case 10: _myParkingDoor.ToExitHospital(); break; // Puerta del Parking
            case 11: _myElecricityActivated.InterruptorActivated(); break; // Puerta del Parking
            case 12: _myActivaNotaElevator.ToShowNote(); break; // ActivaNota Elevator
            case 13: _myActivaNotaKey.ToShowNote(); break; // ActivaNota Key
            case 14: _myActivaNotaShotgun.ToShowNote(); break; // ActivaNota Shotgun
        }
        GameManager.Instance.InteractableObjectDone(this); // Llamo al GameManager para eliminar de escena el objeto con el que ya he interactuado
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myBotiquin = GetComponent<Botiquin>();
        _myFusibles = GetComponent<Fusibles>();
        _myMunicion = GetComponent<Municion>();
        _myCadaver = GetComponent<Cadaver>();
        _myCadaver2 = GetComponent<Cadaver2>();
        _myCadaver3 = GetComponent<Cadaver3>();
        _myKey = GetComponent<Key>();
        _myElevator = GetComponent<Elevator>();
        _myParkingDoor = GetComponent<ParkingDoor>();
        _myElecricityActivated = GetComponent<ElectricityActivated>();
        _myActivaNota = GetComponent<ActivaNota>();
        _myActivaNotaElevator = GetComponent<ActivaNotaElevator>();
        _myActivaNotaKey = GetComponent<ActivaNotaKey>();
        _myActivaNotaShotgun = GetComponent<ActivaNotaShotgun>();
        GameManager.Instance._listInteractableObjects.Add(this);
    }
}
