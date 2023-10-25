using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace ICI
{
    public class CharacterMove : Singleton<CharacterMove>
    {
        private GameObject directionKeyObject; 
        private Character orderedCharacter;


        public void setPosDirectionKeyObject(Transform characterPosition)
        {
            directionKeyObject.transform.position = characterPosition.position;
        }
        public void setOrderedCharacter(Character orderedCharacter)
        {
            this.orderedCharacter = orderedCharacter;
        }

        public void setActive(bool status) => directionKeyObject.SetActive(status);

        public void inputMove(string dir)
        {

            if(dir == "Front" &&
                GameMap.Instance.isRoad(orderedCharacter.pos + Pos.Front()))
            {
                orderedCharacter.pos.addPos(0,1);
            }
            else if(dir == "Back" &&
                GameMap.Instance.isRoad(orderedCharacter.pos + Pos.Back()))
            {
                orderedCharacter.pos.addPos(0,-1);
            }
            else if(dir == "Right" &&
                GameMap.Instance.isRoad(orderedCharacter.pos + Pos.Right()))
            {
                orderedCharacter.pos.addPos(1,0);
            }
            else if(dir == "Left" &&
                GameMap.Instance.isRoad(orderedCharacter.pos + Pos.Left()))
            {
                orderedCharacter.pos.addPos(-1,0);
            }
            else
            {
                return;
            }
            orderedCharacter.applyInstancePos();
            setActive(false);

            SpeedCounter.Instance.finishAction();
        }

        public override void Initialize()
        {
            this.directionKeyObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + "DirectionKeyObject"));
        }
    }
}
