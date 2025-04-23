using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Telemetry;

public class ChangeLvl_2 : MonoBehaviour
{
    #region parameters
    private bool _tracked;
    #endregion
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
            if (_tracked == false)
            {
                Telemetry.Telemetry.Instance.TrackEvent(new LevelEndEvent(Telemetry.Event.ID_Event.LEVEL_END, 2));
                _tracked = true;
            }
           
            _myTransition.FadeOut();            
        }      
    }
    #endregion
}
