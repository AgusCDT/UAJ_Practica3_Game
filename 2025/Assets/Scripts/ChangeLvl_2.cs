using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLvl_2 : MonoBehaviour
{
    #region references
    private Transition _myTransition;
    #endregion

    #region methods
    void Awake()
    {
        _myTransition = FindObjectOfType<Transition>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player_Life_Component hitPlayer = collision.GetComponent<Player_Life_Component>();

        if (hitPlayer)
        {
            _myTransition.FadeOut();            
        }      
    }
    #endregion
}
