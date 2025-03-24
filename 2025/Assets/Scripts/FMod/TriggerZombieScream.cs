using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZombieScream : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Life_Component player = collision.GetComponent<Player_Life_Component>();

        if (player)
        {
            SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.zombieScream, this.transform.position);   
            Destroy(this);
        }
    }
}
