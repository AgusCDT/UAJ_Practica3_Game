using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeAmbiences : MonoBehaviour
{
    [field: Header("Area")]
    [field: SerializeField] public AREA area;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Life_Component player = collision.GetComponent<Player_Life_Component>();

        if (player)
        {
            SoundManager.Instance.SetAmbience(area);          
            Debug.Log("ENtra" + area);
        }
    }
}
