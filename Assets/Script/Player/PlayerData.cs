using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace ICI
{
    public static class PlayerData
    {
        private static List<Character> playerCharacters;

        public static List<Character> PlayerCharacters
        {
            get 
            { 
                if(playerCharacters == null)
                    playerCharacters = new List<Character>();

                return playerCharacters;
            }
        }
    }
}
