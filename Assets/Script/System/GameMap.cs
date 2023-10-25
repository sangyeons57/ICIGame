using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class GameMap : Singleton<GameMap> 
    {
        private List<Pos> WallList;
        private List<Pos> RoadList;

        public void addWall(Pos pos)
        {
            if (WallList.Contains(pos) || RoadList.Contains(pos)) return;
            WallList.Add(pos);
        }

        public void addWallForce(Pos pos)
        {
            if (RoadList.Contains(pos)) RoadList.Remove(pos);
            if (!WallList.Contains(pos)) WallList.Add(pos);
        }

        public void addWallRange(params Pos[] poses)
        {
            foreach (Pos pos in poses) addWall(pos);
        }

        public void addWallRange(Pos pos, Pos direction, int step)
        {
            for (int i = 0; i < step; i++)
            {
                addWall(pos);
                pos += direction;
            }
        }
        public void addWallRangeForce(params Pos[] poses)
        {
            foreach (Pos pos in poses) addWallForce(pos);
        }

        public void addWallRangeForce(Pos pos, Pos direction, int step)
        {
            for (int i = 0; i < step; i++)
            {
                addWallForce(pos);
                pos += direction;
            }
        }

        public bool removeWall(Pos pos) 
        { 
            if (!WallList.Contains(pos)) return false;
            WallList.Remove(pos);
            return true;
        }


        public void addRoad(Pos pos)
        {
            if(RoadList.Contains(pos)) return;
            if(WallList.Contains(pos)) removeWall(pos);
            RoadList.Add(pos);
            addWall(pos + Pos.Front());
            addWall(pos + Pos.Back());
            addWall(pos + Pos.Right());
            addWall(pos + Pos.Left());
            addWall(pos + Pos.FrontLeft());
            addWall(pos + Pos.FrontRight());
            addWall(pos + Pos.BackLeft());
            addWall(pos + Pos.BackRight());
        }

        public void addRoadRange(params Pos[] poses)
        {
            foreach (Pos pos in poses) addRoad(pos);
        }

        public void addRoadRange(Pos pos, Pos direction, int step)
        {
            for (int i = 0; i < step; i++)
            {
                addRoad(pos);
                pos += direction;
            }
        }

        public void printWall()
        {
            foreach(Pos pos in WallList)
            {
                GameObject instance = GameObject.Instantiate(StageSetting.Instance.WallBlock,Pos.Pos2Vector(pos),Quaternion.identity);
                instance.transform.parent = StageSetting.Instance.Root.transform;
            }
        }

        public bool isWall(Pos pos) => WallList.Contains(pos);
        public bool isRoad(Pos pos) => RoadList.Contains(pos);
        public bool isEmpty(Pos pos) => !isWall(pos) && !isRoad(pos); 


        public override void Initialize()
        {
            RoadList = new List<Pos>();
            WallList = new List<Pos>();
        }
    }
}
