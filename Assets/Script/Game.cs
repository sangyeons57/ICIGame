using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ICI
{
    public class Game : PersistantSingleton<Game>
    {
        private void Start()
        {
            StageSetting.Instance.buildWall();
            StageSetting.Instance.buildEnemy();

            PlayerData.PlayerCharacters.Add(new Character_test1().applyInstancePos());
            PlayerData.PlayerCharacters.Add(new Character(new Pos(0,0), 2,4,1).setInstance("Cube").applyInstancePos());

            SpeedCounter.Instance.startTurnCounting();
        }

        private void Update()
        {
            if (EnemysMap.Instance.isRemovedAll())
                SceneManager.LoadScene("SelectLevel");
        }
    }
}
