using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusibles : MonoBehaviour
{
    #region references
    [SerializeField] private AudioClip _clip;
    #endregion 
    #region methods
    public void SumaFusible()
    {
        LightManager.Instance.CheckFusibles();
        SoundManager.Instance.PlaySound(_clip);
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.pickedItem, this.transform.position);
    }
    #endregion
}
