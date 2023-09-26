using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damage : Singleton<Damage>
{
    public class Condition : DelayAction
    {
        public int delay { get; } 
        public int fullInDelay { get; set; }

        private eConditionType conditionType;
        private float amount;

        public Condition(eConditionType status, float amount, int time)
        {
            this.conditionType = status;
            this.delay = time;
            this.amount = amount;
        }

        public void applyToTarget(LifeBaseObject target)
        {
            target.HavingCondition.Add(this.conditionType, this.amount);
        }

        public void action()
        {

        }

        private void Status1(LifeBaseObject target)
        {

        }
    }

    public int damage; // µ¥¹ÌÁö


    public void Attack(LifeBaseObject target, int damage, params Condition[] conditions)
    {
        target.attacked(damage);

        foreach(Condition condition in conditions)
        {
            condition.applyToTarget(target);
            SpeedCounter.Instance.addDelayAction(condition);
        }
    }

    public override void Initialize()
    {
    }
}

