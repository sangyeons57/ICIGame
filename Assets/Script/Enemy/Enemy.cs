using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ICI
{
    public class Enemy : TurnObserver, IApplyPos<Enemy>, IBaseLifeInfo
    {
        public Pos pos;

        public GameObject instance;

        public int hp { get; set; }

        public int speed { get; set; }
        public float turnPercent { get; set; }

        public Enemy(Pos pos, int hp, int speed)
        {
            this.pos = pos;
            this.hp = hp;

            this.speed = speed;
            this.turnPercent = 0;

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

        public void attacked(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void dead()
        {
            throw new System.NotImplementedException();
        }
    }
}
