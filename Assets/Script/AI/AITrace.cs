using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ICI
{
    public class AITrace 
    {
        private int detectLimitSize;
        private int distanceToPurpose;

        private Pos objectPos;

        private Pos[]  fourDirection = new Pos[] {Pos.Front(), Pos.Right(), Pos.Left(), Pos.Back()};

        private RangeChecker rangeChecker;

        private bool IsReachTarget;
        public bool isReachTarget { get => IsReachTarget; }

        public AITrace(Pos objectPos,int detectLimitSize, int distanceToPurpose) 
        {
            if (detectLimitSize <= 1) Debug.LogError("detectLimitSize must be more than 1");
            this.objectPos = objectPos;
            this.detectLimitSize = detectLimitSize;
            this.distanceToPurpose = distanceToPurpose;

            rangeChecker = new RangeChecker(detectLimitSize, distanceToPurpose);
        }

        public AITrace setObjectPos(Pos pos)
        {
            this.objectPos = pos;
            return this;
        }

        public Pos chooseDirection()
        {
            return rangeChecker.Work(objectPos);
        }

        public Pos Action()
        {
            Pos direction = chooseDirection();
            Debug.Log("EnemyAction");
            if(direction.Equals(Pos.Zero()))
                IsReachTarget = true;
            else
                IsReachTarget = false;

            return direction;
        }


    }
}
