using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityActivated : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyObject;
    #region methods
    public void InterruptorActivated()
    {
        emptyObject.SetActive(true);
        SoundManager.Instance.SetEventEmitter();
        GameManager.Instance.UnlockExit();
        LightManager.Instance.LightsGlobalActivated();
    }
    #endregion
}
