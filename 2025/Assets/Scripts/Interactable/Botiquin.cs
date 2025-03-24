using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    #region references
    private Player_Life_Component _myPlayerLifeComponent;
    //private GameObject _player;
    #endregion
    #region parameters
    [SerializeField]
    private int _heal = 20;
    #endregion 
    #region methods
    public void AplicaCura() //Elimina el botiquin
    {
        if (_myPlayerLifeComponent._currentLife > 0 && _myPlayerLifeComponent._currentLife < 100)
        {
            _myPlayerLifeComponent.Cura(_heal);
            SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.pickedItem, this.transform.position);
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerLifeComponent = GameManager.Instance._player.GetComponent<Player_Life_Component>();
        //_player = GameObject.Find("Chico");
    }
}
