using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class StageSetting : PersistantSingleton<StageSetting>
    {

        public GameObject Root;

        public GameObject WallBlock;

        public int stage { get; set; } = 0;

        public void buildWall()
        {
            switch (this.stage)
            {
                case 1:
                    GameMap.Instance.addRoadRange(new Pos(0, 0), Pos.Front(), 5);
                    GameMap.Instance.addRoadRange(new Pos(1, 0), Pos.Front(), 5);
                    GameMap.Instance.addRoadRange(new Pos(2, 0), Pos.Front(), 5);
                    GameMap.Instance.addRoadRange(new Pos(-1, 0), Pos.Front(), 5);
                    GameMap.Instance.addRoadRange(new Pos(-2, 0), Pos.Front(), 5);
                    GameMap.Instance.addWallForce(new Pos(0, 2));
                    break;

                default:
                    break;
            }
            GameMap.Instance.printWall();
        }


        //利 积己 何盒
        public void buildEnemy()
        {
            List<Enemy> enemyList = new List<Enemy>();
            switch (this.stage)
            {
                case 1:
                    enemyList.Add(new Enemy1(new Pos(0, 4), 10, 2).setInstance("Cube").applyInstancePos());
                    break;
                default:
                    Debug.LogError("this is not exist stage");
                    break;
            }

        }

    }
}
