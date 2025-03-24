using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _changeLevel;
    [SerializeField] private Animator _myAnimator;
    [SerializeField] private AudioClip _clip;
    //[SerializeField] private GameObject _elevator;
    #endregion
    #region methods   
    public void OpenElevator()
    {
        //GameManager.Instance.OpenElevatorDoor();
        AnimatorManager.Instance.OpenElevator(_myAnimator);
        _changeLevel.SetActive(true);
        this.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(_clip);
    }
    #endregion
}
