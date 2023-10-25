using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class BuildBattleStage : PersistantSingleton<BuildBattleStage> 
    {
        //맵build할 최상위 하이라키 위치
        public GameObject Root;

        /**
         * 정보 읽어오는 부분이필요하고
         * 읽어온 정보를 기반으로 Root에 오브젝트를 생성하는 함수가 필요함
         * 
         */
        public void BuildUpStage(int stageNum)
        {
        }
    }
}
