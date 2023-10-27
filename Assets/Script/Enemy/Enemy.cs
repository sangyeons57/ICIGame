using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ICI
{
    public class Enemy : LifeBaseObject, TurnObserver, IApplyPos<Enemy>
    {
        public Pos pos;

        public GameObject instance;


        public int speed { get; set; }
        public float turnPercent { get; set; }

        public Enemy(Pos pos, int hp, int speed)
        {
            this.pos = pos;
            base.hp = hp;

            this.speed = speed;
            this.turnPercent = 0;

            SpeedCounter.Instance.addCounterListener(this);
            EnemysMap.Instance.addEnemy(this);
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



        virtual public void action() { }


        virtual public void startOwnTurn() { }

        virtual public void endOwnTurn() { }

        override public void dead()
        {
            Debug.Log("dead Enemy");
        }
    }
}
