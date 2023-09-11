using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class Enemy1 : Enemy
    {
        AITrace aiTrace;

        public Enemy1(Pos pos, int hp, int speed)
            : base(pos, hp, speed)
        {
            aiTrace = new AITrace(this.pos, 7, 1);
            SpeedCounter.Instance.addCounterListener(this);
        }

        private ImmediateAttack_position attack()
        {
            Pos[] pos = new Pos[]{
                this.pos,
                Pos.Front()+this.pos,
                Pos.Right()+this.pos,
                Pos.Left()+this.pos,
                Pos.Back()+this.pos
            };
            Func<Pos, IBaseLifeInfo[]> getObjectFunc = (Pos pos) => CharactersMap.Instance.getCharactersByPos(pos).ToArray();
            return ImmediateAttack_position.Attack(2,pos, getObjectFunc,0);
        }

        override public void action()
        {
            Pos pos = aiTrace.setObjectPos(this.pos).Action();

            Debug.Log(aiTrace.isReachTarget);
            if (aiTrace.isReachTarget)
            {
                Debug.Log("attack");
                attack();
            }
            else
            {
                Debug.Log("move");
                if(pos is not null) this.pos += pos;
                applyInstancePos();
            }

            SpeedCounter.Instance.finishAction();
        }


        override public void startOwnTurn()
        {
        }

        override public void endOwnTurn()
        {

        }

    }
}
