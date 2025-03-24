using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    #region references
    [SerializeField] private AudioClip _clip;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySound(_clip);
    }
}
