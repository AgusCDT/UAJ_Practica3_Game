using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoom : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _roomToActivate;
    [SerializeField] private Animator _myAnimator;
    [SerializeField] private float _cont = 1.7f;
    [SerializeField] private AudioClip _clip;
    
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Life_Component hitPlayer = collision.GetComponent<Player_Life_Component>();
        if (hitPlayer)
        {
            NuevaHab();
            //SoundManager
            SoundManager.Instance.PlaySound(_clip);
            SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.openDoor, this.transform.position);

            //Animacion abrir puerta
            AnimatorManager.Instance.OpenDoor(_myAnimator);
           
        }
    }
    private void NuevaHab()
    {
        GameManager.Instance.ActivarHabitacion(_roomToActivate, this);
    }
    #endregion
}
