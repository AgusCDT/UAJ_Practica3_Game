using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaNota : MonoBehaviour
{
    #region references
    [SerializeField] private AudioClip _clip;
    #endregion

    #region methods
    public void ToShowNote() // Método que llama al GameManager para mostrar la nota 
    {
        GameManager.Instance.ActivateNoteRoom();
        SoundManager.Instance.PlaySound(_clip);
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.noteGrabed, this.transform.position);
    }

    public void ToHideNote() // Método que llama al GameManager para esconder la nota
    {         
        GameManager.Instance.DeactivateNoteRoom();
        SoundManager.Instance.PlaySound(_clip);
    }

    #endregion
}
