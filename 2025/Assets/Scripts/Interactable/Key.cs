using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region methods
    public void ToActivateParkingDoor()
    {
        GameManager.Instance.GetKey();
    }
    #endregion
}
