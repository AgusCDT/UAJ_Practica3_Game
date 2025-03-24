using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _muteMusicButton;
    [SerializeField] private GameObject _unmuteMusicButton;
    [SerializeField] private GameObject _muteSFXButton;
    [SerializeField] private GameObject _unmuteSFXButton;
    #endregion

    #region methods
    public void MuteButton(bool music)
    {
        UI_Manager.Instance.MuteMusicButton(music);
        if (music)
        {
            _unmuteMusicButton.SetActive(false);
            _muteMusicButton.SetActive(true);
        }
        else
        {
            _unmuteMusicButton.SetActive(true);
            _muteMusicButton.SetActive(false);
        }

    }
    public void MuteSFXButton(bool effect)
    {
        UI_Manager.Instance.MuteSFXButton(effect);

        if (effect)
        {
            _unmuteSFXButton.SetActive(false);
            _muteSFXButton.SetActive(true);
        }
        else
        {
            _unmuteSFXButton.SetActive(true);
            _muteSFXButton.SetActive(false);
        }
    }
    public void Quit()
    {
        UI_Manager.Instance.Quit();
    }

    public void PauseMenu(bool enabled)
    {
        UI_Manager.Instance.PauseMenu(enabled);
    }

    public void ControlsMenu(bool enabled)
    {
        UI_Manager.Instance.ControlsMenu(enabled);
    }
    public void ReplayButton()
    {
        UI_Manager.Instance.ReplayButton();
    }
    public void StartGame()
    {
        UI_Manager.Instance.StartGame();
    }

    public void BackButtonNoteRoom()
    {
        UI_Manager.Instance.BackButtonNoteRoomCall();
    }
    public void BackButtonNoteElevator()
    {
        UI_Manager.Instance.BackButtonNoteElevatorCall();
    }
    public void BackButtonNoteKey()
    {
        UI_Manager.Instance.BackButtonNoteKeyCall();
    }
    public void BackButtonNoteShotgun()
    {
        UI_Manager.Instance.BackButtonNoteShotgunCall();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _unmuteMusicButton.SetActive(false);
        _unmuteSFXButton.SetActive(false);
    }
}
