using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ICI
{
    /*
            
        옵저버

        방향값 

        옵저버는 너비우선

        각 옵저버는
        위치값 방향갑 을 가지고있고


        방향값은 이전에 블럭에 위치 값이다

        옵저버는
        지나온 길에 step기록을 남기고
        옵저버가 다른 옵저버의 기록을 발견하면 = >해당 옵저버 제거 (과거 기록인경우 step수가 무조건 많을거기 때문) 
        옵저버가 다른 옵저버를 만나면 => 옵저버 병합 > 방향값을 2개 가지게 됨

        옵저버는 돌아가면서 작동
        큐에 작업을 계속 넣어서 작동

     */
    public class Checker
    {
        private static Dictionary<Pos, Checker> CheckerMap;

        private Pos[] fourDirection = new Pos[] { Pos.Front(), Pos.Right(), Pos.Left(), Pos.Back() };


        public Pos currentPos;
        //nearChecker의 추가는 spread를 통한 생성 absortion에 의한 병합에서만 일어난다
        public List<Checker> nearCheckers;

        public bool head;

        static private int limitStep;

        public int distance;

        //처음 생성
        public Checker(Pos pos, int limitStep)
        {
            Checker.CheckerMap = new Dictionary<Pos, Checker>();

            this.currentPos = pos;
            Checker.limitStep = limitStep;

            this.nearCheckers = new List<Checker>();
            this.nearCheckers.Add(this);

            this.head = true;
            this.distance = 0;
        }
        //이후생성
        Checker(Pos pos, Checker nearChecker)
        {
            this.currentPos = pos;
            this.nearCheckers = new List<Checker>();
            this.nearCheckers.Add(nearChecker);

            this.head = false;
            this.distance = nearChecker.distance + 1;
        }

        public void Action (Queue<Checker> checkers) 
        {
            if(distance >= limitStep) return;

            if (isOverlapCheckerPos())
                absorption();
            else
                spread(checkers);
        }

        public void spread(Queue<Checker> checkers)
        {
            Checker.CheckerMap.Add(currentPos, this);

            foreach (Pos direction in fourDirection)
            {
                Pos newPos = currentPos + direction;
                if (GameMap.Instance.isWall(newPos) || GameMap.Instance.isEmpty(newPos)) continue;

                // 이전체커의 위치와 같지않으면 ㅊ채커 생성
                // 그리고 벽이아니면 채커 생성
                if (!nearCheckers[0].currentPos.Equals(newPos))
                {
                    createNewChecker(checkers, newPos);
                }
            }
        }

        private void createNewChecker(Queue<Checker> checkers, Pos newPos)
        {
            Checker newChecker = new Checker(newPos, this);
            nearCheckers.Add(newChecker);
            checkers.Enqueue(newChecker);
        }

        private void absorption()
        {
            Checker absorptionCecker = Checker.CheckerMap[this.currentPos];
            // 자신의 가까운 체커 값을 원레 존제하던 값에 병합
            absorptionCecker.nearCheckers.AddRange(nearCheckers.ToArray());

            // 자신의 가가운 체커 들의 nearchecker값 변경
            foreach (Checker checker in nearCheckers)
            {
                checker.nearCheckers.Remove(this);
                checker.nearCheckers.Add(absorptionCecker);
            }

            setSizeOfNearCheckerDsitance();
        }

        private void setSizeOfNearCheckerDsitance()
        {
            foreach(Checker checker in nearCheckers)
            {
                if( Mathf.Abs(checker.distance - this.distance) == 0 )
                    continue; //가까운 체커와서로 거리가 같은경우
                else if( Mathf.Abs(checker.distance - this.distance) == 1 )
                    continue; //정상적인 한칸씩 늘어나고 있는경우
                else
                { //거리가 2이상은로 끊겨있는 경우
                    if( checker.distance - this.distance > 0 )
                    { //this 체커의 거리가 더 짧은 경우
                        checker.distance = this.distance + 1;
                    }
                    else
                    { //this 체커의 거리가 더 긴 경우
                        this.distance = checker.distance + 1;
                    }
                    checker.setSizeOfNearCheckerDsitance();
                }
            }
        }

        private bool isOverlapCheckerPos()
        {
            if (Checker.CheckerMap.Keys.Contains(this.currentPos))
                return true;
            else
                return false;
        }

        static public void printCheckerMap(Pos subject)
        {
            Debug.Log(Checker.CheckerMap.Count);
            foreach(KeyValuePair<Pos, Checker> checker in Checker.CheckerMap)
            {
                Debug.Log(subject.x + subject.z + " " + checker.Key + " " + checker.Value);
            }
        }

        static public Checker getCheckerByPos(Pos pos)
        {
            if(Checker.CheckerMap.ContainsKey(pos))
                return Checker.CheckerMap[pos];
            else
                return null;
        }

        static public Pos getFirstStepDirection(Checker purpose, int distanceToPurpose, bool isFirstFind = true)
        {
            if(isFirstFind && purpose.distance <= distanceToPurpose)
            {
                return Pos.Zero();
            } 
            else if(purpose.distance == 1)
            {
                return purpose.currentPos - purpose.nearCheckers[0].currentPos;
            }

            foreach (Checker checker in purpose.nearCheckers)
            {
                if(purpose.distance - checker.distance > 0)
                {
                    return getFirstStepDirection(checker, distanceToPurpose,false);
                }
            }

            return null;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            if (other is Pos)
                return (currentPos.x == ((Pos)other).x && currentPos.z == ((Pos)other).z);
            else if (other is Checker)
                return (currentPos.x == ((Checker)other).currentPos.x && currentPos.z == ((Checker)other).currentPos.z);
            else return false;
        }

        public override int GetHashCode()
        {
            return this.currentPos.x.GetHashCode() ^ this.currentPos.z.GetHashCode();
        }
        
    }

    public class RangeChecker
    {
        private Queue<Checker> Checkers;

        private int limitStep;
        private int distanceToPurpose;

        public RangeChecker(int limitStep, int distanceToPurpose)
        {
            this.limitStep = limitStep;
            this.distanceToPurpose = distanceToPurpose;

            Checkers = new Queue<Checker>();
        }


        public Pos Work(Pos pos)
        {
            makeCheckerMap(pos);
            return findDirectionToMove(0);
        }


        private void makeCheckerMap(Pos pos)
        {
            Checkers.Clear(); 
            Checkers.Enqueue(new Checker(pos.Clone(), limitStep));

            while (Checkers.Count != 0)
                Checkers.Dequeue().Action(Checkers);
        }

        private Pos findDirectionToMove(int order = 0)
        {
            List<Checker> checkers = addAllTargetCheckers();

            List<Checker> sortCheckers = orderCheckers(checkers);
            Pos direction = Checker.getFirstStepDirection(sortCheckers[order], this.distanceToPurpose);

            return direction;
        }

        private List<Checker> addAllTargetCheckers()
        {
            List<Checker> checkers = new List<Checker>();
            foreach (Pos pos in CharactersMap.Instance.getAllCharacterPos())
                if (Checker.getCheckerByPos(pos) != null) 
                    checkers.Add(Checker.getCheckerByPos(pos));

            return checkers;
        }

        private List<Checker> orderCheckers(List<Checker> checkers)
        {
            return (from checker in checkers
                   orderby checker.distance
                   select checker).ToList();
        }
    }
}
