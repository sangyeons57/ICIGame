using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

namespace ICI
{
    public interface DelayAction
    {
        public int delay { get; }
        public int fullInDelay { get; set; }
        void action();
    }
    public interface TurnObserver
    {
        public float turnPercent { get; set; }
        public int speed { get; set; }

        public void startOwnTurn();
        public void endOwnTurn();

        public void action();
    }

    class SpeedCounter : PersistantSingleton<SpeedCounter>
    {
        private bool isTurnCounting;

        TurnObserver turnObserver;

        private List<TurnObserver> TurnObservers;
        public List<TurnObserver> turnObservers
        {
            get
            {
                if (TurnObservers == null)
                {
                    TurnObservers = new List<TurnObserver>();
                }
                return TurnObservers;
            }
        }

        private List<TurnObserver> FullPercentObservers;
        public List<TurnObserver> fullPercentObservers
        {
            get
            {
                if (FullPercentObservers == null)
                {
                    FullPercentObservers = new List<TurnObserver>();
                }
                return FullPercentObservers;
            }
        }

        private TurnObserver playingObserver = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void addCounterListener(TurnObserver element)
        {
            turnObservers.Add(element);
        }
        public void removeCounterListener(TurnObserver element) => turnObservers.Remove(element);

        public void startTurnCounting()
        {
            isTurnCounting = true;
            while (isTurnCounting && turnObservers.Count != 0)
            {
                foreach (TurnObserver turnObserver in turnObservers)
                {
                    turnObserver.turnPercent += turnObserver.speed;
                    checkAndAddFullPercentObserver(turnObserver);
                }
                addDelayValueAtDelayActionList();

                executionAction(true);
            }
        }
        public void finishAction()
        {
            playingObserver.endOwnTurn();
            executionAction(false);
        }

        private void executionAction(bool isfirstExecution = true)
        {
            if (fullPercentObservers.Count == 0)
            {
                if (!isfirstExecution) startTurnCounting();
                return;
            }

            if (isfirstExecution)
            {
                isTurnCounting = false;
                sortFullPercentObservers();
            }

            this.turnObserver = fullPercentObservers[0];

            Invoke("actionObserver", 0.5f);

            //actionObserver();

        }

        private bool checkAndAddFullPercentObserver(TurnObserver turnObserver)
        {
            if (turnObserver.turnPercent >= 100)
            {
                fullPercentObservers.Add(turnObserver);
                turnObserver.turnPercent -= 100;
                return true;
            }
            return false;
        }
        private void actionObserver()
        {
            fullPercentObservers.Remove(this.turnObserver);
            playingObserver = this.turnObserver;
            turnObserver.startOwnTurn();
            turnObserver.action();
        }

        IEnumerator waitAction(TurnObserver turnObserver)
        {
            Debug.Log("wait");
            yield return new WaitForSeconds(0.5f);
        }
        
        private void sortFullPercentObservers()
        {
            for (int i = 0; i < fullPercentObservers.Count; i++)
            {
                if (fullPercentObservers.Count >  i+ 1)
                {
                    if (fullPercentObservers[i].turnPercent < fullPercentObservers[i + 1].turnPercent)
                    {
                        TurnObserver turnObserver = fullPercentObservers[i];
                        fullPercentObservers[i] = fullPercentObservers[i + 1];
                        fullPercentObservers[i + 1] = turnObserver;
                    }
                }
            }

        }

        private List<DelayAction> DelayActions;
        private List<DelayAction> delayActions
        { 
            get
            {
                if( DelayActions == null )
                    DelayActions = new List<DelayAction>();

                return DelayActions;
            }
            set
            {
                if (DelayActionPlayList == null)
                    DelayActionPlayList = new List<DelayAction>();

                DelayActionPlayList = value;
            }
        }
        private List<DelayAction> DelayActionPlayList;
        private List<DelayAction> delayActionPlayList
        { 
            get
            {
                if( DelayActionPlayList == null )
                    DelayActionPlayList = new List<DelayAction>();

                return DelayActions;
            }
            set
            {
                if( DelayActionPlayList == null )
                    DelayActionPlayList = new List<DelayAction>();

                DelayActionPlayList = value;
            }
        }

        public void addDelayAction(DelayAction delayAction)
        {
            delayActions.Add(delayAction);
            Debug.Log(fullPercentObservers.Count);
        }

        private List<DelayAction> sortAndClearDelayActionsPlayList()
        {
            var newDelayActionsPlayList =
                from delayAction in delayActionPlayList
                orderby (delayAction.delay - delayAction.fullInDelay)
                select delayAction;

            delayActionPlayList.Clear();

            return newDelayActionsPlayList.ToList();
        }

        private void addDelayValueAtDelayActionList()
        {
            int flag = 0;
            while (delayActions.Count > flag)
            {
                delayActions[flag].fullInDelay += 1;

                if (delayActions[flag].delay - delayActions[flag].fullInDelay <= 0)
                {
                    delayActions[flag].action();
                    delayActions.Remove(delayActions[flag]);
                }
                else
                    flag++;
            }
        }
    }
}
