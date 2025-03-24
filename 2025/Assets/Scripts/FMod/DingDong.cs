using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingDong : MonoBehaviour
{
    [field: Header("Elevator")]
    [field: SerializeField] public ELEVATOR state;

    [SerializeField]private Elevator elevator_;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Life_Component player = collision.GetComponent<Player_Life_Component>();

        if (player)
        {
            SoundManager.Instance.SetElevator(state);
            elevator_.OpenElevator();
        }
    }
}
