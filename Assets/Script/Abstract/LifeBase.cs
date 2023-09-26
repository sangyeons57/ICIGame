using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public enum eConditionType
    {
        Status1
    }
    public abstract class LifeBaseObject
    {

        public int hp = 0; 
        public Dictionary<eConditionType, float> HavingCondition;

        public LifeBaseObject attacked(int damage)
        {
            this.hp -= damage;
            if(this.hp <= 0 ) dead();

            return this;
        }
        abstract public void dead();

        public LifeBaseObject addCondition(eConditionType conditionType, float amount, int time)
        {
            if(!HavingCondition.ContainsKey(conditionType))
                HavingCondition.Add(conditionType, amount);
            else return null;
            return this;
        }

        public float getCondition(eConditionType type)
        {
            if(HavingCondition.ContainsKey(type))
                return HavingCondition[type];
            else
                return 0;
        }
    }
}
