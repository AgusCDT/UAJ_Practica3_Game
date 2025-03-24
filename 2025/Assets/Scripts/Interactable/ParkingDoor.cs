using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingDoor : MonoBehaviour
{
    public void ToExitHospital()
    {
        GameManager.Instance.OpenParkingDoor();
    }
}
