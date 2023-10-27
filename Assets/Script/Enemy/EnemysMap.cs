using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class EnemysMap : Singleton<EnemysMap>
    {
        private List<Enemy> Map = new List<Enemy>();

        public void addEnemy(Enemy enemy)
        {
            Map.Add(enemy);
        }

        public void removeCharacter(Enemy enemy) 
        {
            Map.Remove(enemy);
        }

        public Enemy getCharacterByPos(Pos pos)
        {
            foreach (Enemy mapItem in Map)
            {
                if (mapItem.pos.Equals(pos))
                {
                    return mapItem;
                }
            }
            return null;
        }

        public List<Enemy> getCharactersByPos(Pos pos)
        {
            List<Enemy> list = new List<Enemy>();
            foreach (Enemy mapItem in Map)
            {
                if (mapItem.pos.Equals(pos))
                {
                    list.Add(mapItem);
                }
            }
            return list;
        }

        public List<Enemy> getCharactersByPoses(List<Pos> poses)
        {
            List<Enemy> list = new List<Enemy>();
            foreach(Pos pos in poses)
            {
                foreach(Enemy mapItem in Map)
                {
                    if(mapItem.pos.Equals(pos))
                    {
                        list.Add(mapItem);
                    }
                }
            }
            return list;
        }
        public List<T> getCharactersByPoses<T>(List<Pos> poses) where T : class
        {
            List<T> list = new List<T>();
            foreach (Pos pos in poses)
            {
                foreach (Enemy mapItem in Map)
                {
                    if (mapItem.pos.Equals(pos))
                    {
                        list.Add(mapItem as T);
                    }
                }
            }
            return list;
        }

        public bool isCharacterExist(Pos pos)
        {
            foreach (Enemy mapItem in Map)
            {
                if (mapItem.pos.x == pos.x && mapItem.pos.z == pos.z)
                    Debug.Log("same~~~~~~~~~~~~~~~~");
                if (mapItem.Equals(pos))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Enemy> getCharactersInRow(int row)
        {
            List<Enemy> list = new List<Enemy>();
            foreach (Enemy mapItem in Map)
            {
                if(mapItem.pos.x == row)
                {
                    list.Add(mapItem);
                }
            }
            return list;
        }

        public List<Enemy> getCharactersInColumn(int column)
        {
            List<Enemy> list = new List<Enemy>();
            foreach (Enemy mapItem in Map)
            {
                if(mapItem.pos.x == column)
                {
                    list.Add(mapItem);
                }
            }
            return list;
        }

        public List<Pos> getAllCharacterPos()
        {
            List<Pos> characterPosList = new List<Pos>();
            foreach(Enemy mapItem in Map)
            {
                characterPosList.Add(mapItem.pos);

            }
            return characterPosList;
        }

        public bool isRemovedAll()
        {
            return (Map.Count == 0);
        }

        public override string ToString()
        {
            string str = " -CharacterMap- \n";
            foreach (Enemy mapItem in Map)
            {
                str += mapItem + " " + mapItem.pos + "\n";
            }
            return str;
        }

        public override void Initialize()
        {
        }
    }
}
