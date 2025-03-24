using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    #region properties
    private bool holdingInteract = false; // Booleano que detecta si se ha presionado la E para interactuar mientras se está en la zona de interacción
    #endregion

    #region references
    private Input_Manager _myInputManager;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision) // Si el player está en la zona de interacción se puede pulsar la E
    {
        InteractDetection hitInteractableObject = collision.GetComponent<InteractDetection>();
        Fusibles hitFusible = collision.GetComponent<Fusibles>();
        Botiquin hitBotiquin = collision.GetComponent<Botiquin>();
        Municion hitMunicion = collision.GetComponent<Municion>();
        Cadaver hitCadaver = collision.GetComponent<Cadaver>();
        Cadaver2 hitCadaver2 = collision.GetComponent<Cadaver2>();
        Cadaver3 hitCadaver3 = collision.GetComponent<Cadaver3>();
        Key hitKey = collision.GetComponent<Key>();
        Elevator hitElevator = collision.GetComponent<Elevator>();
        ParkingDoor hitParkingDoor = collision.GetComponent<ParkingDoor>();
        ElectricityActivated hitElectricityActivated = collision.GetComponent<ElectricityActivated>();
        ActivaNota hitNota = collision.GetComponent<ActivaNota>();
        ActivaNotaElevator hitNotaElevator = collision.GetComponent<ActivaNotaElevator>();
        ActivaNotaKey hitNotaKey = collision.GetComponent<ActivaNotaKey>();
        ActivaNotaShotgun hitNotaShotgun = collision.GetComponent<ActivaNotaShotgun>();

        if (hitInteractableObject)
        {
            _myInputManager.InDetectionZone = true;
        }
        if (holdingInteract) // Interactua con el objeto que está en la zona de interacción
        {
            if (hitMunicion)
            {
                hitInteractableObject.Interact(1);
            }
            else if (hitFusible)
            {
                hitInteractableObject.Interact(2);
            }
            else if (hitBotiquin)
            {
                hitInteractableObject.Interact(3);
            }
            else if (hitNota)
            {
                hitInteractableObject.Interact(4);
            }
            else if (hitCadaver)
            {
                hitInteractableObject.Interact(5);
            }
            else if (hitCadaver2)
            {
                hitInteractableObject.Interact(6);
            }
            else if (hitCadaver3)
            {
                hitInteractableObject.Interact(7);
            }
            else if (hitKey)
            {
                hitInteractableObject.Interact(8);
            }
            else if (hitElevator)
            {
                hitInteractableObject.Interact(9);
            }
            else if (hitParkingDoor)
            {
                hitInteractableObject.Interact(10);
            }
            else if (hitElectricityActivated)
            {
                hitInteractableObject.Interact(11);
            }
            else if (hitNotaElevator)
            {
                hitInteractableObject.Interact(12);
            }
            else if (hitNotaKey)
            {
                hitInteractableObject.Interact(13);
            }
            else if (hitNotaShotgun)
            {
                hitInteractableObject.Interact(14);
            }
            ToCallInteraction();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // Si el player se ha salido de la zona de interacción no se puede pulsar la E
    {
        InteractDetection hitInteractableObject = collision.GetComponent<InteractDetection>();
        if (hitInteractableObject)
        {
            _myInputManager.InDetectionZone = false;
        }
    }
    public void ToCallInteraction() // Revierte el valor del booleano
    {
        holdingInteract = !holdingInteract;
    }
    #endregion

    void Start()
    {
        _myInputManager = GetComponent<Input_Manager>();
    }
}
