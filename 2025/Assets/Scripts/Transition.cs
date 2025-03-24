using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    #region references
    [SerializeField] private Animator _transition;
    [SerializeField] int level;   
    #endregion

    #region methods
    public void Awake()
    {
        _transition = GetComponent<Animator>();
    }
    public void FadeOut()
    {
        GameManager.Instance.IsGamePaused = true;       
        _transition.SetTrigger("Fadeout");       
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        GameManager.Instance.IsGamePaused = false;
    }
    #endregion
}
