using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaNotaElevator : MonoBehaviour
{
    #region references
    [SerializeField] private AudioClip _clip;
    #endregion

    #region methods
    public void ToShowNote() // Método que llama al GameManager para mostrar la nota 
    {
        GameManager.Instance.ActivateNoteElevator();
        SoundManager.Instance.PlaySound(_clip);
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.noteGrabed, this.transform.position);
        if (LightManager.Instance._currentFusibles < 1)
        {
            GameManager.Instance.NewMision("Parece que tienes que usar el ascensor... Recoge fusibles 0/3");
        }
    }

    public void ToHideNote() // Método que llama al GameManager para esconder la nota
    {
        GameManager.Instance.DeactivateNoteElevator();
        SoundManager.Instance.PlaySound(_clip);
    }

    #endregion
}
