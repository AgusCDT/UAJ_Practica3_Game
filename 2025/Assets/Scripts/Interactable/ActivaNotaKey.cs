using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaNotaKey : MonoBehaviour
{
    #region references
    [SerializeField] private AudioClip _clip;
    #endregion

    #region methods
    public void ToShowNote() // Método que llama al GameManager para mostrar la nota 
    {
        GameManager.Instance.ActivateNoteKey();
        SoundManager.Instance.PlaySound(_clip);
        GameManager.Instance.NewMision("Busca la llave que te llevará a la salida");
    }

    public void ToHideNote() // Método que llama al GameManager para esconder la nota
    {
        GameManager.Instance.DeactivateNoteKey();
        SoundManager.Instance.PlaySound(_clip);
    }

    #endregion
}
