using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class Game : PersistantSingleton<Game>
    {
        private void Start()
        {
            Enemy enemy1 = new Enemy1(new Pos(0,5), 10, 2).setInstance("Cube").applyInstancePos();

            PlayerData.PlayerCharacters.Add(new Character_test1().applyInstancePos());
            PlayerData.PlayerCharacters.Add(new Character(new Pos(0,0), 2,4,1).setInstance("Cube").applyInstancePos());

            SpeedCounter.Instance.startTurnCounting();
        }
    }
}
