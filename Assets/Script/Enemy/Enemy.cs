using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ICI
{
    public class Enemy : TurnObserver, IApplyPos<Enemy>
    {
        public Pos pos;

        public GameObject instance;

        public int hp;

        public int speed { get; set; }
        public float turnPercent { get; set; }

        AITrace aiTrace;

        public Enemy(Pos pos, int hp, int speed)
        {
            this.pos = pos;
            this.hp = hp;

            this.speed = speed;
            this.turnPercent = 0;

            SpeedCounter.Instance.addCounterListener(this);
            aiTrace = new AITrace(this.pos, 7, 1);
        }

        public Enemy setInstance(string name)
        {
            GameObject resource = ResourcesManager.GetResources(eResourcesPath.Prefabs, name);

            instance = GameObject.Instantiate(resource ,Vector2.zero, Quaternion.identity);
            return this;
        }

        public Enemy applyInstancePos()
        {
            instance.transform.position = new Vector3(pos.x ,0 ,pos.z);
            return this;
        }

        private EnemyImmediateAttack_position attack()
        {
            Pos[] pos = new Pos[]{
                this.pos,
                Pos.Front()+this.pos,
                Pos.Right()+this.pos,
                Pos.Left()+this.pos,
                Pos.Back()+this.pos
            };
            return EnemyImmediateAttack_position.Attack(2,pos, 0);
        }


        public void action()
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


        public void startOwnTurn()
        {
        }

        public void endOwnTurn()
        {

        }

    }
}
