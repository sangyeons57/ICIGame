using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;
using System;

namespace ICI
{
    public interface DelayAction
    {
        public int delay { get; }
        public int fullInDelay { get; set; }
    }
    public interface TurnObserver
    {
        public float turnPercent { get; set; }
        public int speed { get; set; }

        public void startOwnTurn();
        public void endOwnTurn();

        public void action();
    }

    public class SpeedCounter : MonoBehaviour
    {
        public static SpeedCounter Instance { get; private set; } = null;

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

        public void addCounterListener(TurnObserver element)
        {
            turnObservers.Add(element);
        }
        public void removeCounterListener(TurnObserver element)
        {
            fullPercentObservers.Remove(element);
            turnObservers.Remove(element);
        }

        private void Awake()
        {
            Instance = this;
        }


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

        private Dictionary<object,(Type,string)> DelayActions;
        private Dictionary<object,(Type, string)> delayActions
        { 
            get
            {
                if (DelayActions == null)
                    DelayActions = new Dictionary<object, (Type, string)>();

                return DelayActions;
            }
        }

        public void addDelayAction<T>(object classInstance, string delayAction = "action") where T : DelayAction
        {
            delayActions.Add(classInstance,(typeof(T),delayAction));
            Debug.Log(fullPercentObservers.Count);
        }

        private void addDelayValueAtDelayActionList()
        {
            int flag = 0;
            List<object> delayActionKeys = delayActions.Keys.ToList();
            while (delayActionKeys.Count > flag)
            {
                ((DelayAction)delayActionKeys[flag]).fullInDelay += 1;

                if (((DelayAction)delayActionKeys[flag]).delay - ((DelayAction)delayActionKeys[flag]).fullInDelay <= 0)
                {
                    delayActions[delayActionKeys[flag]].Item1.GetMethod(DelayActions[delayActionKeys[flag]].Item2)
                        .Invoke(delayActionKeys[flag], null);

                    delayActionKeys.Remove(delayActionKeys[flag]);
                }
                else
                    flag++;
            }
        }
    }
}
