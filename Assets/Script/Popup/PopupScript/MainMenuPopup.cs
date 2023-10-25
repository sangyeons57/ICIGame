using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPopup : Popup<MainMenuPopup>
{
    static public void OpenPopup()
    {
        PopupManager.OpenPopup<MainMenuPopup>("MainMenuPopup");
    }

    public void OnClickNewGame()
    {
        ClosePopup();
        SceneManager.LoadScene("SelectLevel");
    }

    public void OnClickContinue()
    {

    }

    public void OnClickSetting()
    {

    }

    public void OnClickQuit()
    {

    }
}

