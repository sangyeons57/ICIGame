using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class CharactersMap : Singleton<CharactersMap>
    {
        private List<Character> Map = new List<Character>();

        public void addCharacter(Character character)
        {
            Map.Add(character);
        }

        public void removeCharacter(Character character) 
        {
            Map.Remove(character);
        }

        public Character getCharacterByPos(Pos pos)
        {
            foreach (Character mapItem in Map)
            {
                if (mapItem.pos.Equals(pos))
                {
                    return mapItem;
                }
            }
            return null;
        }

        public List<Character> getCharactersByPos(Pos pos)
        {
            List<Character> list = new List<Character>();
            foreach (Character mapItem in Map)
            {
                if (mapItem.pos.Equals(pos))
                {
                    list.Add(mapItem);
                }
            }
            return list;
        }

        public bool isCharacterExist(Pos pos)
        {
            foreach (Character mapItem in Map)
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

        public List<Character> getCharactersInRow(int row)
        {
            List<Character> list = new List<Character>();
            foreach (Character mapItem in Map)
            {
                if(mapItem.pos.x == row)
                {
                    list.Add(mapItem);
                }
            }
            return list;
        }

        public List<Character> getCharactersInColumn(int column)
        {
            List<Character> list = new List<Character>();
            foreach (Character mapItem in Map)
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
            foreach(Character mapItem in Map)
            {
                characterPosList.Add(mapItem.pos);

            }
            return characterPosList;
        }

        public override string ToString()
        {
            string str = " -CharacterMap- \n";
            foreach (Character mapItem in Map)
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
