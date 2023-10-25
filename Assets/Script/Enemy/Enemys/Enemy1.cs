using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void attack()
        {
            /** ImmediateAttack_position
            Pos[] pos = new Pos[]{
                this.pos,
                Pos.Front()+this.pos,
                Pos.Right()+this.pos,
                Pos.Left()+this.pos,
                Pos.Back()+this.pos
            };
            Func<Pos, IBaseLifeInfo[]> getObjectFunc = (Pos pos) => CharactersMap.Instance.getCharactersByPos(pos).ToArray();
            return ImmediateAttack_position.Attack(2,pos, getObjectFunc,0);

            ImmediateAttack_object
            return ImmediateAttack_object.Attack(10, 
            CharactersMap.Instance.getCharactersByPoses<IBaseLifeInfo>(pos.ToList<Pos>()), 0);
            */
            Pos[] directionList = new Pos[]
            {
                Pos.Front(),
                Pos.Back(),
                Pos.Right(),
                Pos.Left(),
            };
            Projectile projectile = null;
            foreach (Pos direction in directionList)
            {
                if (CharactersMap.Instance.isCharacterExist(Pos.Range(this.pos, direction, 7)))
                {
                    projectile = Projectile.Attack(this.pos, direction, 10, 100, false, 10, typeof(Character));
                    projectile.setResource("Sphere");
                }
            }

        }

        override public void action()
        {
            Pos pos = aiTrace.setObjectPos(this.pos).Action();

            if (aiTrace.isReachTarget)
            {
                attack();
            }
            else
            {
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
