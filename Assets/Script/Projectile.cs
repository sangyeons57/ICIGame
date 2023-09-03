using ICI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace ICI
{
    public class Projectile : IApplyPos<Projectile> , TurnObserver
    {
        GameObject instance;

        Pos pos;

        Pos move;

        string target;

        int damage;
        Condition condition;

        public float turnPercent { get; set; }
        public int speed { get; set; } = 1;

        public Projectile(Transform pos, Pos move, string path)
            : this(new Pos((int)pos.position.x, (int)pos.position.z), move, path)
        {

        }

        public Projectile(Pos pos, Pos move, string path)
        {
            this.pos = new Pos(pos); 
            this.move = new Pos(move);

            setResource(path);
            

            SpeedCounter.Instance.addCounterListener(this);
        }



        private void Move()
        {
            this.pos += move;
        }

        public void setTarget(string target)
        {
            this.target = target;
        }

        public void setAttack(int damage, Condition condition)
        {
            this.damage = damage;
            this.condition = condition;

        }


        private void setResource(string path)
        {
            GameObject resource = Resources.Load<GameObject>("Prefabs/" + path);
            this.instance = GameObject.Instantiate(resource,Vector2.zero, Quaternion.identity);

            applyInstancePos();
        }

        public bool isCollision(string collisionType, Pos pos) => (collisionType == target && this.pos.Equals(pos));

        public Projectile applyInstancePos()
        {
            instance.transform.position = new Vector3(pos.x ,0 ,pos.z);
            return this;
        }

        public void attack(ref int hp)
        {
            hp -= damage;
            // condition  ¿€µø
        }

        public void action()
        {
            Move();
            applyInstancePos();

            SpeedCounter.Instance.finishAction();
        }

        public void startOwnTurn()
        {
            instance.transform.localScale = Vector3.one * 1.2f;
        }

        public void endOwnTurn()
        {
            instance.transform.localScale = Vector3.one;
        }
    }
}
