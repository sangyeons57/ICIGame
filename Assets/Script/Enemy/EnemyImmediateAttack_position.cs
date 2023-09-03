using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;
using UnityEngine;

namespace ICI
{
    public class EnemyImmediateAttack_position : DelayAction
    {
        private int damage;
        private Pos[] attackRange;
        private Enemy subject;

        public int delay { get; } = 2;
        public int fullInDelay { get; set; } = 0;

        private List<GameObject> markedPos;

        //cc스킬등 도 추가

        private EnemyImmediateAttack_position
            (int damage, Pos[] attackRange, int beforeDelay)
        {
            this.damage = damage;
            this.attackRange = attackRange;

            this.delay = beforeDelay;

            markedPos = new List<GameObject>();
        }
        static public EnemyImmediateAttack_position Attack(int damage, Pos[] attackRange, int beforeDelay = 0)
        {
            EnemyImmediateAttack_position attackClassInstance =
                new EnemyImmediateAttack_position(damage, attackRange, beforeDelay);

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
                List<Character> characters = CharactersMap.Instance.getCharactersByPos(pos);
                if (characters.Count != 0)
                {
                    foreach (Character character in characters)
                    {
                        character.attacked(this.damage);
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
