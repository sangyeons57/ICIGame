using ICI;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ImmediateAttack_object : DelayAction
{
    public int delay { get; }
    public int fullInDelay { get; set; } = 0;

    private int damage;
    private List<LifeBaseObject> targetList;

    private ImmediateAttack_object(int damage, List<LifeBaseObject> targetList, int beforeDelay) 
    { 
        this.damage = damage;
        this.targetList = targetList;
        this.delay = beforeDelay;
    }

    static public ImmediateAttack_object Attack(int damage ,List<LifeBaseObject>targetList, int beforeDelay = 0)
    {
        ImmediateAttack_object attackClassInstance = 
            new ImmediateAttack_object(damage,targetList, beforeDelay);

        SpeedCounter.Instance.addDelayAction<ImmediateAttack_object>(attackClassInstance);

        return attackClassInstance;
    }

    public void action()
    {
        foreach(LifeBaseObject element in targetList)
        {
            element.attacked(damage);
        }
    }

}
