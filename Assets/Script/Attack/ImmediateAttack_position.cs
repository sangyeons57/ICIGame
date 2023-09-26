using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace ICI
{
    public class ImmediateAttack_position : DelayAction
    {
        private int damage;
        private Pos[] attackRange;
        private Enemy subject;

        public int delay { get; } = 2;
        public int fullInDelay { get; set; } = 0;

        private List<GameObject> markedPos;

        Func<Pos,LifeBaseObject[]> FgetTargetObjects;

        //cc스킬등 도 추가

        private ImmediateAttack_position
            (int damage, Pos[] attackRange, Func<Pos,LifeBaseObject[]> FgetTargetObjects ,int beforeDelay)
        {
            this.damage = damage;
            this.attackRange = attackRange;

            this.FgetTargetObjects = FgetTargetObjects;

            this.delay = beforeDelay;

            markedPos = new List<GameObject>();
        }
        static public ImmediateAttack_position Attack(int damage, Pos[] attackRange, Func<Pos,LifeBaseObject[]> FgetTargetObjects, int beforeDelay = 0)
        {
            ImmediateAttack_position attackClassInstance =
                new ImmediateAttack_position(damage, attackRange, FgetTargetObjects, beforeDelay);

            attackClassInstance.Start();

            //선딜
            SpeedCounter.Instance.addDelayAction(attackClassInstance);

            return attackClassInstance;
        }

        private void Start()
        {
            markAttackPos(attackRange);
        }

        public void action()
        {
            removeMarkedPosObject();
            //공격
            foreach (Pos pos in attackRange)
            {
                LifeBaseObject[] targets = FgetTargetObjects(pos); 
                if (targets.Length != 0)
                {
                    foreach (LifeBaseObject target in targets)
                    {
                        target.attacked(this.damage);
                    }
                }
            }
        }

        private void markAttackPos(Pos[] attackRange)
        {
            foreach (Pos attackPos in attackRange)
            {
                markedPos.Add(GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + "AttackPos"), Pos.Pos2Vector(attackPos), Quaternion.Euler(90,0,0)));
            }
        }

        private void removeMarkedPosObject()
        {
            foreach (GameObject obj in markedPos)
                GameObject.Destroy(obj);
        }
    }
}
