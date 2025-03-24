using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadaver3 : MonoBehaviour
{
    private Input_Manager _myInputManager;
    //[SerializeField] private GameObject _player;

    #region methods

    public void EscopetaActivada()
    {
        _myInputManager._escopetaActivada = true;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myInputManager = GameManager.Instance._player.GetComponent<Input_Manager>();
    }
}
