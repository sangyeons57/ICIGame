using ICI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Windows.Speech;

namespace ICI
{
    public class Projectile : IApplyPos<Projectile> , TurnObserver
    {
        GameObject instance;

        Pos pos;

        Pos move;

        Type[] targetList;

        int maxDistance;
        int moveCount;

        int damage;

        bool penetrate;

        bool isActivate;

        public float turnPercent { get; set; }
        public int speed { get; set; } = 1;

        private Projectile(Pos pos, Pos move, int damage, int maxDistance, bool penetrate,int speed, Type[] targets)
        {
            this.pos = new Pos(pos); 
            this.move = new Pos(move);

            this.damage = damage;

            this.maxDistance = maxDistance;
            this.moveCount = 0;
            this.penetrate = penetrate;

            this.turnPercent = 0;
            this.speed = speed;

            this.targetList = targets;

            this.isActivate = true;

            SpeedCounter.Instance.addCounterListener(this);
            Debug.Log(speed);
        }
        static public Projectile Attack(Pos pos, Pos move, int damage, int maxDistance, bool penetrate, int speed, params Type[] targets)
        {
            Projectile attackClassInstance = new Projectile(pos, move, damage, maxDistance, penetrate, speed, targets);


            return attackClassInstance;
        }



        private void Move()
        {
            Debug.Log("Move");
            if (!isActivate) return; 
            this.pos += move;
            moveCount++;
        }

        public Projectile setResource(string path)
        {
            GameObject resource = Resources.Load<GameObject>("Prefabs/" + path);
            this.instance = GameObject.Instantiate(resource, Pos.Pos2Vector(this.pos), Quaternion.identity);
            applyInstancePos();

            return this;
        }

        public void setResource(GameObject resource)
        {
            this.instance = GameObject.Instantiate(resource, Pos.Pos2Vector(this.pos), Quaternion.identity);
            applyInstancePos();
        }

        private List<LifeBaseObject> getListOfTargets()
        {
            List<LifeBaseObject> answer = new List<LifeBaseObject>();
            foreach (Type type in targetList)
            {
                if (type == typeof(Character))
                    answer.AddRange(CharactersMap.Instance.getCharactersByPos(this.pos));
                else if (type == typeof(Enemy))
                    answer.AddRange(EnemysMap.Instance.getCharactersByPos(this.pos));
            }
            return answer;
        }

        public Projectile applyInstancePos()
        {
            instance.transform.position = new Vector3(pos.x ,0 ,pos.z);
            return this;
        }

        private void attack(List<LifeBaseObject> attackTargets)
        {
            if(!isActivate) return;
            foreach(LifeBaseObject target in attackTargets)
            {
                Damage.Instance.Attack(target, damage);
            }
            if (penetrate == false) DeleteProcess();
        }
        
        private void DeleteProcess()
        {
            SpeedCounter.Instance.removeCounterListener(this);
            this.isActivate = false;
            GameObject.Destroy(instance);
        }

        public void action()
        {
            Debug.Log("action");
            //현제위치 공격
            List<LifeBaseObject> attackTargets = getListOfTargets();
            if (attackTargets.Count > 0) attack(attackTargets);

            Move();
            applyInstancePos();

            //이동위치 공격
            attackTargets = getListOfTargets();
            if (attackTargets.Count > 0) attack(attackTargets);

            SpeedCounter.Instance.finishAction();

            if(moveCount > maxDistance) DeleteProcess();
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
