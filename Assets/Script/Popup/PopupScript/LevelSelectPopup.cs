using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ICI
{
    public class LevelSelectPopup : Popup<LevelSelectPopup>
    {

        public void clickLevelButton(int num)
        {
            StageSetting.Instance.stage = num;
            SceneManager.LoadScene("BattleScene");
            ClosePopup();
        }

        static public void OpenPopup()
        {
            PopupManager.OpenPopup<LevelSelectPopup>("LevelSelectPopup");
        }

        public override void Initialize()
        {
        }
    }
}
