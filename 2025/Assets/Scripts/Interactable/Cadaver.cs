using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadaver : MonoBehaviour
{
    #region references
    private Input_Manager _myInputManager;
    //[SerializeField] private GameObject _player;
    #endregion

    #region methods
    public void LeverActivated() //Palanca activada
    {
        _myInputManager._leverActivated = true;
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.pickedItem, this.transform.position);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myInputManager = GameManager.Instance._player.GetComponent<Input_Manager>();
    }
}
