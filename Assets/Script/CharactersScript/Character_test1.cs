using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class Character_test1 : Character
    {
        public Character_test1() : 
            base( new Pos(0,0), 2, 2, 1)
        {
            setInstance("Cube").applyInstancePos();
        }


        public override void action_Skill1()
        {
            Projectile projectile = new Projectile(this.pos, Pos.Front(), "Sphere");
            projectile.speed = 10;
            projectile.setTarget("Enemy");
            projectile.setAttack(1, new Condition());

            SpeedCounter.Instance.finishAction();

            PopupManager.ClosePopup<SkillPopup>();
        }
    }
}
